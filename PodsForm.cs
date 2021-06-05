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
            showPods();
        }

        private void showPods()
        {
            this.listViewPods.View = View.Details;
            this.listViewPods.Columns.Clear();
            this.listViewPods.Columns.Add("Name", -2, HorizontalAlignment.Left);
            this.listViewPods.Columns.Add("Pod IP", -2, HorizontalAlignment.Left);
            this.listViewPods.Columns.Add("Status", -2, HorizontalAlignment.Left);
            this.listViewPods.Columns.Add("Date created", -2, HorizontalAlignment.Left);
        }

        private void PodsForm_Load(object sender, EventArgs e)
        {
            listPods();

        }

        private void listPods()
        {
            listViewPods.Items.Clear();
            var pods = formAux.client.ListNamespacedPod(formAux.namespaceSelected);
            foreach (var pod in pods.Items)
            {
                string[] row = { pod.Metadata.Name,pod.Status.PodIP ,podStatus(pod), pod.Metadata.CreationTimestamp.ToString() };
                var listItem = new ListViewItem(row);
                this.listViewPods.Items.Add(listItem);
                this.listViewPods.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
               
            }
        }
        private static string podStatus(V1Pod pod)
        {
            string typeAux = "";
            foreach (var podCondition in pod.Status.Conditions)
            {
                typeAux = podCondition.Type;
            }
            return typeAux;
        }

        private void btnCreatePod_Click(object sender, EventArgs e)
        {

            string namePod = null;
            formAux.InputBox("Pod - Name", "Insert name:", ref namePod);

            var pod = new V1Pod { Metadata = new V1ObjectMeta { Labels = new Dictionary<string, string>() { { "app", namePod } }, Name = namePod }, Spec = new V1PodSpec { Containers = new List<V1Container>() { new V1Container { Name = namePod, Image = namePod, ImagePullPolicy = "Always" } }, NodeName = formAux.nodeSelected, HostNetwork = true } };
            try { formAux.client.CreateNamespacedPod(pod, formAux.namespaceSelected); } catch { }
            listPods();
        }

        private async void btnDeletePod_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var status = formAux.client.DeleteNamespacedPod(listViewPods.FocusedItem.Text, formAux.namespaceSelected, new V1DeleteOptions());
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

            /* string nameService = null;
             string protocol = null;
             //formAux.InputBox("Service - Name", "Insert name:", ref nameService);
             ServiceInputBox("Service - Create", "Insert name:", ref nameService, ref protocol);
             //MessageBox.Show(protocol);
             var createService = new V1Service { Metadata = new V1ObjectMeta { Name = nameService }, Spec = new V1ServiceSpec { Selector = new Dictionary<string, string>() { { "app", listBoxListPods.SelectedItem.ToString() } }, Ports = new List<V1ServicePort>() { new V1ServicePort { Protocol = protocol, Port = 80 } } } };
             formAux.client.CreateNamespacedService(createService, formAux.namespaceSelected);*/
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
