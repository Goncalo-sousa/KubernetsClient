using k8s.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KubernetsClient
{
    public partial class ServiceForm : Form
    {
        MainForm formAux;
        public ServiceForm(MainForm main)
        {
            InitializeComponent();
            formAux = main;
            showServices();
        }

        private void showServices()
        {
            this.listViewServices.View = View.Details;
            this.listViewServices.Columns.Clear();
            this.listViewServices.Columns.Add("Name", -2, HorizontalAlignment.Left);
            this.listViewServices.Columns.Add("Type", -2, HorizontalAlignment.Left);
            this.listViewServices.Columns.Add("Cluster IP", -2, HorizontalAlignment.Left);
            this.listViewServices.Columns.Add("Port(s)", -2, HorizontalAlignment.Left);
            this.listViewServices.Columns.Add("Date created", -2, HorizontalAlignment.Left);
        }

        private void btnCreateService_Click(object sender, EventArgs e)
        {
            formAux.createService(textBoxNameService.Text, comboBoxProtocol.SelectedItem.ToString(), comboBoxServiceType.SelectedItem.ToString());
            listServices();
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            listServices();

        }

        private void listServices()
        {
            listViewServices.Items.Clear();
            var services = formAux.client.ListNamespacedServiceWithHttpMessagesAsync(formAux.namespaceSelected);
            foreach (var service in services.Result.Body.Items)
            {
                string[] row = { service.Metadata.Name, service.Spec.Type, service.Spec.ClusterIP, portRequest(service), service.Metadata.CreationTimestamp.ToString() };
                var listItem = new ListViewItem(row);
                this.listViewServices.Items.Add(listItem);
                this.listViewServices.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            };
        }

        private static string portRequest(V1Service service)
        {
            string portAux = "";
            string nodeportAux = "";
            string protocolAux = "";
            foreach (var port in service.Spec.Ports)
            {
                portAux = port.Port.ToString();
                protocolAux = port.Protocol.ToString();
                nodeportAux = port.NodePort.ToString();
            }
            string result = portAux + ":" + nodeportAux + "/" + protocolAux;
            return result;
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            formAux.client.DeleteNamespacedServiceWithHttpMessagesAsync(listViewServices.FocusedItem.Text, formAux.namespaceSelected);
            listServices();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

