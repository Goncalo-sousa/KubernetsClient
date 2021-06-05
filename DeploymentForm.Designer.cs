
namespace KubernetsClient
{
    partial class DeploymentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxImages = new System.Windows.Forms.ComboBox();
            this.btnCreateDeployment = new System.Windows.Forms.Button();
            this.comboBoxReplicas = new System.Windows.Forms.ComboBox();
            this.textBoxDeploymentName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listViewDeployments = new System.Windows.Forms.ListView();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnDeleteDeployment = new System.Windows.Forms.Button();
            this.textBoxImage = new System.Windows.Forms.TextBox();
            this.checkBoxOther = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBoxImages
            // 
            this.comboBoxImages.FormattingEnabled = true;
            this.comboBoxImages.Items.AddRange(new object[] {
            "Wordpress",
            "Ubuntu",
            "MySQL",
            "MongoDB",
            "Mariadb",
            "Arangodb",
            "Ruby on Rails",
            "PostgreSQL",
            "Django",
            "Redis",
            "Memcached",
            "Centos",
            "Fedora",
            "Nginx",
            "RethinkDB",
            "SonarQube",
            "Sentry",
            "Jenkins",
            "Owncloud",
            "Tomcat",
            "Joomla",
            "Apache Server",
            "Neo4J",
            "Elasticsearch",
            "Apache Maven"});
            this.comboBoxImages.Location = new System.Drawing.Point(61, 84);
            this.comboBoxImages.Name = "comboBoxImages";
            this.comboBoxImages.Size = new System.Drawing.Size(113, 23);
            this.comboBoxImages.TabIndex = 1;
            // 
            // btnCreateDeployment
            // 
            this.btnCreateDeployment.Location = new System.Drawing.Point(60, 187);
            this.btnCreateDeployment.Name = "btnCreateDeployment";
            this.btnCreateDeployment.Size = new System.Drawing.Size(75, 23);
            this.btnCreateDeployment.TabIndex = 2;
            this.btnCreateDeployment.Text = "Create";
            this.btnCreateDeployment.UseVisualStyleBackColor = true;
            this.btnCreateDeployment.Click += new System.EventHandler(this.btnCreateDeployment_Click);
            // 
            // comboBoxReplicas
            // 
            this.comboBoxReplicas.FormattingEnabled = true;
            this.comboBoxReplicas.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxReplicas.Location = new System.Drawing.Point(61, 142);
            this.comboBoxReplicas.Name = "comboBoxReplicas";
            this.comboBoxReplicas.Size = new System.Drawing.Size(69, 23);
            this.comboBoxReplicas.TabIndex = 3;
            // 
            // textBoxDeploymentName
            // 
            this.textBoxDeploymentName.Location = new System.Drawing.Point(60, 55);
            this.textBoxDeploymentName.Name = "textBoxDeploymentName";
            this.textBoxDeploymentName.Size = new System.Drawing.Size(176, 23);
            this.textBoxDeploymentName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Replicas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Images:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(60, 113);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(84, 23);
            this.textBoxPort.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Port:";
            // 
            // listViewDeployments
            // 
            this.listViewDeployments.HideSelection = false;
            this.listViewDeployments.Location = new System.Drawing.Point(265, 55);
            this.listViewDeployments.Name = "listViewDeployments";
            this.listViewDeployments.Size = new System.Drawing.Size(523, 250);
            this.listViewDeployments.TabIndex = 10;
            this.listViewDeployments.UseCompatibleStateImageBehavior = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 415);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 11;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnDeleteDeployment
            // 
            this.btnDeleteDeployment.Location = new System.Drawing.Point(713, 311);
            this.btnDeleteDeployment.Name = "btnDeleteDeployment";
            this.btnDeleteDeployment.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteDeployment.TabIndex = 12;
            this.btnDeleteDeployment.Text = "Delete";
            this.btnDeleteDeployment.UseVisualStyleBackColor = true;
            this.btnDeleteDeployment.Click += new System.EventHandler(this.btnDeleteDeployment_Click);
            // 
            // textBoxImage
            // 
            this.textBoxImage.Location = new System.Drawing.Point(61, 84);
            this.textBoxImage.Name = "textBoxImage";
            this.textBoxImage.Size = new System.Drawing.Size(113, 23);
            this.textBoxImage.TabIndex = 13;
            // 
            // checkBoxOther
            // 
            this.checkBoxOther.AutoSize = true;
            this.checkBoxOther.Location = new System.Drawing.Point(180, 86);
            this.checkBoxOther.Name = "checkBoxOther";
            this.checkBoxOther.Size = new System.Drawing.Size(56, 19);
            this.checkBoxOther.TabIndex = 14;
            this.checkBoxOther.Text = "Other";
            this.checkBoxOther.UseVisualStyleBackColor = true;
            this.checkBoxOther.CheckedChanged += new System.EventHandler(this.checkBoxOther_CheckedChanged);
            // 
            // DeploymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBoxOther);
            this.Controls.Add(this.textBoxImage);
            this.Controls.Add(this.btnDeleteDeployment);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.listViewDeployments);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDeploymentName);
            this.Controls.Add(this.comboBoxReplicas);
            this.Controls.Add(this.btnCreateDeployment);
            this.Controls.Add(this.comboBoxImages);
            this.Name = "DeploymentForm";
            this.Text = "DeploymentForm";
            this.Load += new System.EventHandler(this.DeploymentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxImages;
        private System.Windows.Forms.Button btnCreateDeployment;
        private System.Windows.Forms.ComboBox comboBoxReplicas;
        private System.Windows.Forms.TextBox textBoxDeploymentName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listViewDeployments;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnDeleteDeployment;
        private System.Windows.Forms.TextBox textBoxImage;
        private System.Windows.Forms.CheckBox checkBoxOther;
    }
}