using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using k8s;
using k8s.Models;

namespace KubernetsClient
{
    public partial class PodsForm : Form
    {
        MainForm formAux;
        public PodsForm(MainForm main)
        {
            InitializeComponent();
            formAux = main;
        }

        private void PodsForm_Load(object sender, EventArgs e)
        {
            listPods();

        }

        private void listPods()
        {
            listBoxListPods.Items.Clear();
            var pods = formAux.client.ListNamespacedPod(formAux.namespaceSelected);
            foreach (var pod in pods.Items)
            {
                listBoxListPods.Items.Add(pod.Metadata.Name);
            }
        }

        private void btnCreatePod_Click(object sender, EventArgs e)
        {

            string namePod = null;
            formAux.InputBox("Pod - Name", "Insert name:", ref namePod);

            var pod = new V1Pod { Metadata = new V1ObjectMeta { Labels = new Dictionary<string, string>() { { "app", namePod } }, Name = namePod }, Spec = new V1PodSpec { Containers = new List<V1Container>() { new V1Container { Name = namePod, Image = namePod } }, NodeName = formAux.nodeSelected, HostNetwork = true } };
            formAux.client.CreateNamespacedPod(pod, formAux.namespaceSelected);
            listPods();
        }

        private async void btnDeletePod_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var status = formAux.client.DeleteNamespacedPod(listBoxListPods.SelectedItem.ToString(), formAux.namespaceSelected, new V1DeleteOptions());
                await Task.Delay(8000);
                listPods();
                MessageBox.Show("Deleted successfuly !");
            }
            else if (result == DialogResult.No)
            {
                //...
            }

        }

        private void btnCreateService_Click(object sender, EventArgs e)
        {

            string nameService = null;
            string protocol = null;
            //formAux.InputBox("Service - Name", "Insert name:", ref nameService);
            ServiceInputBox("Service - Create", "Insert name:", ref nameService, ref protocol);
            //MessageBox.Show(protocol);
            var createService = new V1Service { Metadata = new V1ObjectMeta { Name = nameService }, Spec = new V1ServiceSpec { Selector = new Dictionary<string, string>() { { "app", listBoxListPods.SelectedItem.ToString() } }, Ports = new List<V1ServicePort>() { new V1ServicePort { Protocol = protocol, Port = 80 } } } };
            formAux.client.CreateNamespacedService(createService, formAux.namespaceSelected);
        }

        public DialogResult ServiceInputBox(string title, string promptText, ref string value, ref string protocol)
        {
            Form form = new Form();
            Label label = new Label();
            Label labelProtocol = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            ComboBox comboBoxProtocols = new ComboBox();
            string[] protocols = { "TCP", "UDP", "SCTP" };
            comboBoxProtocols.Items.AddRange(protocols);
            labelProtocol.Text = "Protocol:";
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
            labelProtocol.SetBounds(9, 75, 372, 13);
            comboBoxProtocols.SetBounds(159, 75, 75, 23);
            buttonOk.SetBounds(228, 172, 75, 23);
            buttonCancel.SetBounds(309, 172, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            labelProtocol.AutoSize = true;
            comboBoxProtocols.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 207);
            form.Controls.AddRange(new Control[] { label, textBox, labelProtocol, comboBoxProtocols, buttonOk, buttonCancel });
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
    }
}
