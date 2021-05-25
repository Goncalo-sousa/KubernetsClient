using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            string nameNS = null;
            formAux.InputBox("Pod - Name", "Insert name:", ref nameNS);
            var pod = new V1Pod { Metadata = new V1ObjectMeta { Name = nameNS } };
            //var result = formAux.client.CreateNamespacedPod(pod, formAux.namespaceSelected);
        }
    }
}
