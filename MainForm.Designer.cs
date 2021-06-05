
namespace KubernetsClient
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCreateNamespace = new System.Windows.Forms.Button();
            this.btnDeleteNamespace = new System.Windows.Forms.Button();
            this.btnPods = new System.Windows.Forms.Button();
            this.btnDeployment = new System.Windows.Forms.Button();
            this.btnServices = new System.Windows.Forms.Button();
            this.listViewNodes = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(38, 89);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(38, 148);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 23);
            this.textBox2.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(38, 241);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(38, 203);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 23);
            this.textBox3.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(266, 130);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(504, 229);
            this.listBox1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(422, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nodes:";
            // 
            // btnCreateNamespace
            // 
            this.btnCreateNamespace.Location = new System.Drawing.Point(266, 381);
            this.btnCreateNamespace.Name = "btnCreateNamespace";
            this.btnCreateNamespace.Size = new System.Drawing.Size(115, 23);
            this.btnCreateNamespace.TabIndex = 9;
            this.btnCreateNamespace.Text = "Create Namespace";
            this.btnCreateNamespace.UseVisualStyleBackColor = true;
            this.btnCreateNamespace.Click += new System.EventHandler(this.btnCreateNamespace_Click);
            // 
            // btnDeleteNamespace
            // 
            this.btnDeleteNamespace.Location = new System.Drawing.Point(387, 381);
            this.btnDeleteNamespace.Name = "btnDeleteNamespace";
            this.btnDeleteNamespace.Size = new System.Drawing.Size(115, 23);
            this.btnDeleteNamespace.TabIndex = 10;
            this.btnDeleteNamespace.Text = "Delete Namespace";
            this.btnDeleteNamespace.UseVisualStyleBackColor = true;
            this.btnDeleteNamespace.Click += new System.EventHandler(this.btnDeleteNamespace_Click);
            // 
            // btnPods
            // 
            this.btnPods.Location = new System.Drawing.Point(680, 381);
            this.btnPods.Name = "btnPods";
            this.btnPods.Size = new System.Drawing.Size(90, 23);
            this.btnPods.TabIndex = 11;
            this.btnPods.Text = "Pods";
            this.btnPods.UseVisualStyleBackColor = true;
            this.btnPods.Click += new System.EventHandler(this.btnPods_Click);
            // 
            // btnDeployment
            // 
            this.btnDeployment.Location = new System.Drawing.Point(589, 381);
            this.btnDeployment.Name = "btnDeployment";
            this.btnDeployment.Size = new System.Drawing.Size(85, 23);
            this.btnDeployment.TabIndex = 12;
            this.btnDeployment.Text = "Deployment";
            this.btnDeployment.UseVisualStyleBackColor = true;
            this.btnDeployment.Click += new System.EventHandler(this.btnDeployment_Click);
            // 
            // btnServices
            // 
            this.btnServices.Location = new System.Drawing.Point(508, 381);
            this.btnServices.Name = "btnServices";
            this.btnServices.Size = new System.Drawing.Size(75, 23);
            this.btnServices.TabIndex = 13;
            this.btnServices.Text = "Services";
            this.btnServices.UseVisualStyleBackColor = true;
            this.btnServices.Click += new System.EventHandler(this.btnServices_Click);
            // 
            // listViewNodes
            // 
            this.listViewNodes.HideSelection = false;
            this.listViewNodes.Location = new System.Drawing.Point(472, 15);
            this.listViewNodes.Name = "listViewNodes";
            this.listViewNodes.Size = new System.Drawing.Size(298, 97);
            this.listViewNodes.TabIndex = 14;
            this.listViewNodes.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 450);
            this.Controls.Add(this.listViewNodes);
            this.Controls.Add(this.btnServices);
            this.Controls.Add(this.btnDeployment);
            this.Controls.Add(this.btnPods);
            this.Controls.Add(this.btnDeleteNamespace);
            this.Controls.Add(this.btnCreateNamespace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "MainForm";
            this.Text = "Create Namespace";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCreateNamespace;
        private System.Windows.Forms.Button btnDeleteNamespace;
        private System.Windows.Forms.Button btnPods;
        private System.Windows.Forms.Button btnDeployment;
        private System.Windows.Forms.Button btnServices;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listViewNodes;
    }
}

