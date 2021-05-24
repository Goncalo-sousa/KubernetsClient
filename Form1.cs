using k8s;
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
    
    public partial class Form1 : Form
    {
        Kubernetes client;
        
        public Form1()
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

            //var namespaces = client.ListNamespace();
            var namespaces = client.ListCSINode();
            MessageBox.Show("aqui!!");
            foreach (var ns in namespaces.Items)
            {
                MessageBox.Show("aqui 1");
                comboBoxNodes.Items.Add(ns.Metadata.Name);
                listBox1.Items.Add(ns.Metadata.Name);
                Console.WriteLine(ns.Metadata.Name);
                var list = client.ListNamespacedPod(ns.Metadata.Name);
                foreach (var item in list.Items)
                {
                    MessageBox.Show("aqui 2");
                    
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
                }


            }
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(Environment.GetEnvironmentVariable("KUBECONFIG"));

            // Use the config object to create a client.
            var client = new Kubernetes(config);

            return client;
        }




    }
}
