using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KubernetsClient
{
    public partial class DeploymentForm : Form
    {
        public MainForm formAux;
        public DeploymentForm(MainForm main)
        {
            InitializeComponent();
            formAux = main;
            comboBoxReplicas.SelectedIndex = 0;
            comboBoxImages.SelectedIndex = 0;
            textBoxImage.Hide();
            showDeployments();
        }

        private void showDeployments()
        {
            this.listViewDeployments.View = View.Details;
            this.listViewDeployments.Columns.Clear();
            this.listViewDeployments.Columns.Add("Name", -2, HorizontalAlignment.Left);
            this.listViewDeployments.Columns.Add("Ready", -2, HorizontalAlignment.Left);
            this.listViewDeployments.Columns.Add("Pods Available", -2, HorizontalAlignment.Left);
            this.listViewDeployments.Columns.Add("Date created", -2, HorizontalAlignment.Left);
            this.listViewDeployments.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void DeploymentForm_Load(object sender, EventArgs e)
        {
            listDeployments();
        }

        private async void listDeployments()
        {
            listViewDeployments.Items.Clear();
            var deployments = await formAux.client.ListNamespacedDeploymentWithHttpMessagesAsync(formAux.namespaceSelected);
            foreach (var deployment in deployments.Body.Items)
            {
                try
                {
                    createListView(deployment.Metadata.Name, deployment.Status.Replicas.Value, deployment.Status.AvailableReplicas.Value, deployment.Status.AvailableReplicas.Value.ToString(), deployment.Metadata.CreationTimestamp.Value);

                }
                catch { MessageBox.Show("Impossible connect with worker !"); this.Close(); }



            }
        }

        private static string deploymentType(k8s.Models.V1Deployment deployment)
        {
            string typeAux = "";
            foreach (var deploymentCondition in deployment.Status.Conditions)
            {
                typeAux = deploymentCondition.Type;
            }
            return typeAux;
        }

        private void createListView(string name, int ready, int readyAvailable, string status, DateTime age)
        {
            string ageString = age.ToString();
            string readyString = readyAvailable.ToString() + "/" + ready.ToString();
            string[] row = { name, readyString, status, ageString };
            var listItem = new ListViewItem(row);
            this.listViewDeployments.Items.Add(listItem);
            this.listViewDeployments.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void btnCreateDeployment_Click(object sender, EventArgs e)
        {
            if (checkBoxOther.Checked)
            {
                if (Int64.Parse(textBoxPort.Text) >= 1 && Int64.Parse(textBoxPort.Text) <= 65534)
                {
                    formAux.createDeployment(textBoxDeploymentName.Text, Int32.Parse(comboBoxReplicas.SelectedItem.ToString()), comboBoxImages.SelectedItem.ToString().ToLower(), textBoxImage.Text.ToLower(), Int32.Parse(comboBoxReplicas.SelectedItem.ToString()));
                }
            }
            else
            {
                if (Int64.Parse(textBoxPort.Text) >= 1 && Int64.Parse(textBoxPort.Text) <= 65534)
                {
                    formAux.createDeployment(textBoxDeploymentName.Text, Int32.Parse(comboBoxReplicas.SelectedItem.ToString()), comboBoxImages.SelectedItem.ToString().ToLower(), comboBoxImages.SelectedItem.ToString().ToLower(), Int32.Parse(comboBoxReplicas.SelectedItem.ToString()));
                }
            }

            comboBoxImages.Show();
            textBoxImage.Hide();
            listDeployments();
        }

        private void btnDeleteDeployment_Click(object sender, EventArgs e)
        {
            formAux.client.DeleteNamespacedDeploymentWithHttpMessagesAsync(listViewDeployments.FocusedItem.Text, formAux.namespaceSelected);
            listDeployments();
        }

        private void checkBoxOther_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOther.Checked)
            {
                comboBoxImages.Hide();
                textBoxImage.Show();
            }
            else
            {
                comboBoxImages.Show();
                textBoxImage.Hide();
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
