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

                string[] row = { node.Metadata.Name, node.Spec.PodCIDR, node.Status.Capacity["cpu"].ToString(), memoryNode(node) };
                var listItem = new ListViewItem(row);
                this.listViewNodes.Items.Add(listItem);
                this.listViewNodes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);



            }
            showNamespaces();


        }

        private string memoryNode(V1Node node)
        {

            string memory = node.Status.Capacity["memory"].ToString();
            int lenght = memory.Length;
            string renew = memory.Substring(0, lenght - 2);
            long memoryInBytes = long.Parse(renew);
            string result = BytesToString(memoryInBytes * 1024);
            return result;
        }

        private void showNamespaces()
        {
            listBox1.Items.Clear();
            var namespaces = client.ListNamespace();
            foreach (var ns in namespaces.Items)
            {
                listBox1.Items.Add(ns.Metadata.Name);
            }
        }

        public Kubernetes login()
        {
            try
            {
                using (var sftp = new SftpClient(textBoxIPServer.Text, 22, textBoxUsername.Text, textBoxPassword.Text))
                {
                    sftp.Connect();


                    try
                    {

                        //using (Stream fileStream = File.Create(@"C:\Users\Goncalo\.kube\config"))
                        using (Stream fileStream = File.Create($@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\.kube\config"))
                        {

                            sftp.DownloadFile("/home/" + textBoxUsername.Text.ToLower() + "/.kube/config", fileStream);
                            sftp.Disconnect();
                            sftp.Dispose();

                        }
                    }
                    catch
                    {
                        Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\.kube\");
                        using (Stream fileStream = File.Create($@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\.kube\config"))
                        {

                            sftp.DownloadFile("/home/" + textBoxUsername.Text.ToLower() + "/.kube/config", fileStream);
                            sftp.Disconnect();
                            sftp.Dispose();

                        }
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
                    string itemSelected = listBox1.SelectedItem.ToString();
                    var status = client.DeleteNamespace(listBox1.SelectedItem.ToString(), new V1DeleteOptions());
                    listBox1.Items[listBox1.SelectedIndex] = itemSelected + "  ( deleting .... )";
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
            if (listBox1.SelectedIndex != -1)
            {
                namespaceSelected = listBox1.SelectedItem.ToString();
                ServiceForm serviceForm = new ServiceForm(this);
                serviceForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select namespace!");
            }

        }

        public void createService(string nameService, string protocol, string serviceType)
        {
            var createService = new V1Service { Metadata = new V1ObjectMeta { Name = nameService }, Spec = new V1ServiceSpec { Type = serviceType, Selector = new Dictionary<string, string>() { { "app", nameService } }, Ports = new List<V1ServicePort>() { new V1ServicePort { Protocol = protocol, Port = 80 } } } };
            try
            {
                client.CreateNamespacedService(createService, namespaceSelected);
            }
            catch { MessageBox.Show("Impossible create!"); }
        }


        private void btnDeployment_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                namespaceSelected = listBox1.SelectedItem.ToString();
                DeploymentForm deploymentForm = new DeploymentForm(this);
                deploymentForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select namespace!");
            }
        }


        public async void createDeployment(string name, int replicas, string containerName, string image, int port)
        {
            var createDeployment = new V1Deployment
            {
                Metadata = new V1ObjectMeta { Name = name },
                Spec = new V1DeploymentSpec
                {
                    Selector = new V1LabelSelector { MatchLabels = new Dictionary<string, string>() { { "app", image } } },
                    Replicas = replicas,
                    Template = new V1PodTemplateSpec
                    {
                        Metadata = new V1ObjectMeta { Labels = new Dictionary<string, string>() { { "app", image } } },
                        Spec = new V1PodSpec
                        {
                            Containers = new List<V1Container>() { new V1Container { Name = containerName, Image = image,
                Ports = new List<V1ContainerPort>() { new V1ContainerPort { ContainerPort = port } } } }
                        }
                    }
                }
            };
            try { await client.CreateNamespacedDeploymentAsync(createDeployment, namespaceSelected); } catch { MessageBox.Show("Impossible create deployment!"); }
        }

        private String BytesToString(long byteCount)
        {
            string[] suf = { " B", " KB", " MB", " GB", " TB", " PB", " EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
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