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

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            //Environment.SetEnvironmentVariable("KUBECONFIG", "ubuntu@192.168.56.105:/home/ubuntu/.kube/config");
            client = login();


            var nodes = client.ListCSINode();

            foreach (var node in nodes.Items)
            {

                comboBoxNodes.Items.Add(node.Metadata.Name);
            }
            comboBoxNodes.SelectedIndex = 0;
            listNamespaces();

        }

        private void listNamespaces()
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
            using (var sftp = new SftpClient("192.168.56.105", 22, "ubuntu", "ubuntu"))
            {
                sftp.Connect();

                using (Stream fileStream = File.Create(@"C:\Users\Goncalo\.kube\config"))
                {
                    sftp.DownloadFile("/home/ubuntu/.kube/config", fileStream);
                    sftp.Disconnect();
                    sftp.Dispose();
                }


            }
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(Environment.GetEnvironmentVariable("KUBECONFIG"));

            // Use the config object to create a client.
            var client = new Kubernetes(config);

            return client;
        }

        private void btnCreateNamespace_Click(object sender, EventArgs e)
        {
            string nameNS = null;
            InputBox("Namespace - Name", "Insert name:", ref nameNS);

            var ns = new V1Namespace { Metadata = new V1ObjectMeta { Name = nameNS } };
            var result = client.CreateNamespace(ns);
            MessageBox.Show(result.Name());
            listNamespaces();


        }

        private async void btnDeleteNamespace_Click(object sender, EventArgs e)
        {
            /*if (!string.Equals("default", listBox1.SelectedItem.ToString())) { MessageBox.Show("Can't delete namespaces from kubernete system !"); }
            else if (!string.Equals("kube-node-lease",listBox1.SelectedItem.ToString())) { MessageBox.Show("Can't delete namespaces from kubernete system !"); }
            else if (!string.Equals("kube-public", listBox1.SelectedItem.ToString())) { MessageBox.Show("Can't delete namespaces from kubernete system !"); }
            else */
            if (string.Equals("kube-system", listBox1.SelectedItem.ToString())) { MessageBox.Show("Can't delete namespaces from kubernete system !"); }
            else
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    var status = client.DeleteNamespace(listBox1.SelectedItem.ToString(), new V1DeleteOptions());
                    await Task.Delay(8000);
                    listNamespaces();
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