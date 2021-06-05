using k8s;
using k8s.Models;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KubernetsClient
{

    public partial class MainForm : Form
    {
        public Kubernetes client;
        public string namespaceSelected;
        public string nodeSelected;

        public MainForm()
        {
            InitializeComponent();
            showNodes();
        }

        private void showNodes()
        {
            this.listViewNodes.View = View.Details;
            this.listViewNodes.Columns.Clear();
            this.listViewNodes.Columns.Add("Name", -2, HorizontalAlignment.Left);
            this.listViewNodes.Columns.Add("podCIDR", -2, HorizontalAlignment.Left);
            this.listViewNodes.Columns.Add("CPU", -2, HorizontalAlignment.Left);
            this.listViewNodes.Columns.Add("Memory", -2, HorizontalAlignment.Left);
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            //Environment.SetEnvironmentVariable("KUBECONFIG", "ubuntu@192.168.56.105:/home/ubuntu/.kube/config");
            client = login();


            var nodes = client.ListNode();

            foreach (var node in nodes.Items)
            {

                string[] row = { node.Metadata.Name, node.Spec.PodCIDR, node.Status.Capacity["cpu"].ToString(), node.Status.Capacity["memory"].ToString() };
                // keyis = capacity.Key;
                var listItem = new ListViewItem(row);
                this.listViewNodes.Items.Add(listItem);
                this.listViewNodes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                

            }
            showNamespaces();


        }

        private void showNamespaces()
        {
            listBox1.Items.Clear();
            var namespaces = client.ListNamespace();
            foreach (var ns in namespaces.Items)
            {
                listBox1.Items.Add(ns.Metadata.Name);
                var list = client.ListNamespacedPod(ns.Metadata.Name);
                foreach (var item in list.Items)
                {

                    Console.WriteLine(item.Metadata.Name);
                }
            }
        }

        public Kubernetes login()
        {
            try
            {
                using (var sftp = new SftpClient("192.168.124.50", 22, "luisb", "1234"))
                {
                   sftp.Connect();

                    using (Stream fileStream = File.Create(@"C:\Users\Goncalo\.kube\config"))
                    {

                        sftp.DownloadFile("/home/luisb/.kube/config", fileStream);
                        sftp.Disconnect();
                        sftp.Dispose();

                    }


                }
                var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(Environment.GetEnvironmentVariable("KUBECONFIG"));

                // Use the config object to create a client.
                var client = new Kubernetes(config);

                return client;
            }
            catch { MessageBox.Show("Impossible connected with server!"); return null; }
        }

        private void btnCreateNamespace_Click(object sender, EventArgs e)
        {
            string nameNS = null;
            InputBox("Namespace - Name", "Insert name:", ref nameNS);

            var ns = new V1Namespace { Metadata = new V1ObjectMeta { Name = nameNS } };
            var result = client.CreateNamespace(ns);
            MessageBox.Show(result.Name());
            showNamespaces();


        }

        private async void btnDeleteNamespace_Click(object sender, EventArgs e)
        {

            if (string.Equals("kube-system", listBox1.SelectedItem.ToString())) { MessageBox.Show("Can't delete namespaces from kubernete system !"); }
            else
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    var status = client.DeleteNamespace(listBox1.SelectedItem.ToString(), new V1DeleteOptions());
                    await Task.Delay(8000);
                    showNamespaces();
                    MessageBox.Show("Deleted successfuly !");
                }
                else if (result == DialogResult.No)
                {
                    //...
                }

            }

        }
        public DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }


        private void btnPods_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                namespaceSelected = this.listBox1.SelectedItem.ToString();
                PodsForm formAux = new PodsForm(this);
                formAux.ShowDialog();
            }
            else { MessageBox.Show("Select a namespace !"); }

        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            /*string nameService = null;
            string protocol = null;
            //formAux.InputBox("Service - Name", "Insert name:", ref nameService);
            namespaceSelected = listBox1.SelectedItem.ToString();
            ServiceInputBox("Service - Create", "Insert name:", ref nameService, ref protocol);*/
            namespaceSelected = listBox1.SelectedItem.ToString();
            ServiceForm serviceForm = new ServiceForm(this);
            serviceForm.ShowDialog();
            //MessageBox.Show(port);

        }

        public void createService(string nameService, string protocol, string serviceType)
        {
            var createService = new V1Service { Metadata = new V1ObjectMeta { Name = nameService }, Spec = new V1ServiceSpec {Type = serviceType ,Selector = new Dictionary<string, string>() { { "app", nameService } }, Ports = new List<V1ServicePort>() { new V1ServicePort { Protocol = protocol, Port = 80 } } } };
            try
            {
                client.CreateNamespacedService(createService, namespaceSelected);
            }
            catch { MessageBox.Show("Impossible create!"); }
        }

        public DialogResult DeploymentInputBox(string title, string promptText, ref string value, ref string port, ref int replicas)
        {
            Form form = new Form();
            Label label = new Label();
            Label labelPort = new Label();
            Label labelReplicas = new Label();

            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            TextBox textBoxPort = new TextBox();
            ComboBox comboBoxReplicas = new ComboBox();

            labelPort.Text = "Port:";
            labelReplicas.Text = "Replicas:";
            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            textBoxPort.Text = port;
            string[] replics = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            comboBoxReplicas.Items.AddRange(replics);
            comboBoxReplicas.SelectedIndex = 0;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            labelPort.SetBounds(9, 75, 372, 13);
            textBoxPort.SetBounds(140, 75, 65, 23);
            labelReplicas.SetBounds(240, 75, 65, 23);
            comboBoxReplicas.SetBounds(309, 75, 65, 23);
            buttonOk.SetBounds(228, 172, 75, 23);
            buttonCancel.SetBounds(309, 172, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            labelPort.AutoSize = true;
            labelReplicas.AutoSize = true;
            textBoxPort.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            comboBoxReplicas.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 207);
            form.Controls.AddRange(new Control[] { label, textBox, labelPort, textBoxPort, labelReplicas, comboBoxReplicas, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            port = textBoxPort.Text;
            replicas = int.Parse(comboBoxReplicas.SelectedItem.ToString());
            return dialogResult;
        }

        private void btnDeployment_Click(object sender, EventArgs e)
        {
            /*   if (listBox1.SelectedIndex != -1)
               {
                   namespaceSelected = this.listBox1.SelectedItem.ToString();
                   DeploymentForm deploymentForm = new DeploymentForm(this);
                   deploymentForm.ShowDialog();
               }
               else { MessageBox.Show("Select a namespace !"); }//var createDeployment = new V1Deployment { Metadata = new V1ObjectMeta { Name=""}, Spec = new V1DeploymentSpec { Replicas = 1, Template = new V1PodTemplateSpec { Spec = new V1PodSpec { Containers = new List<V1Container>() { new V1Container { Name = "", Image = "", Ports = new List<V1ContainerPort>() { new V1ContainerPort { ContainerPort = 80 } } } } } } } };
             */
            string val = null;
            string val1 = null;
            int val2 = 0;
            namespaceSelected = listBox1.SelectedItem.ToString();
            DeploymentForm deploymentForm = new DeploymentForm(this);
            deploymentForm.ShowDialog();
            //DeploymentInputBox("Deployment - Create", "Insert name:", ref val, ref val1, ref val2);
        }


        public DialogResult ServiceInputBox(string title, string promptText, ref string value, ref string protocol)
        {
            Form form = new Form();
            Label label = new Label();
            Label labelPort = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            ComboBox comboBoxProtocols = new ComboBox();
            string[] protocols = { "UDP", "TCP", "SCTP" };
            comboBoxProtocols.Items.AddRange(protocols);

            labelPort.Text = "Protocol:";
            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            comboBoxProtocols.SelectedIndex = 0;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            labelPort.SetBounds(9, 75, 372, 13);
            comboBoxProtocols.SetBounds(164, 75, 75, 23);
            buttonOk.SetBounds(228, 172, 75, 23);
            buttonCancel.SetBounds(309, 172, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            labelPort.AutoSize = true;
            comboBoxProtocols.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 207);
            form.Controls.AddRange(new Control[] { label, textBox, labelPort, comboBoxProtocols, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            protocol = comboBoxProtocols.SelectedItem.ToString();
            return dialogResult;
        }

        public async void createDeployment(string name, int replicas, string containerName, string image, int port)
        {
            var createDeployment = new V1Deployment { Metadata = new V1ObjectMeta { Name = name }, Spec = new V1DeploymentSpec { Selector = new V1LabelSelector { MatchLabels = new Dictionary<string, string>() { { "app", image } } }, Replicas = replicas, Template = new V1PodTemplateSpec { Metadata = new V1ObjectMeta { Labels = new Dictionary<string, string>() { { "app", image } } }, Spec = new V1PodSpec { Containers = new List<V1Container>() { new V1Container { Name = containerName, Image = image,
                Ports = new List<V1ContainerPort>() { new V1ContainerPort { ContainerPort = port } } } } } } } };
            try { await client.CreateNamespacedDeploymentAsync(createDeployment, namespaceSelected); } catch { MessageBox.Show("Impossible create deployment!"); }
        }

    }
}




/*   DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNoCancel);
if(result == DialogResult.Yes)
{ 
    //...
}
else if (result == DialogResult.No)
{ 
    //...
}
else
{
    //...
}*/