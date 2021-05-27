
namespace KubernetsClient
{
    partial class PodsForm
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
            this.listBoxListPods = new System.Windows.Forms.ListBox();
            this.btnCreatePod = new System.Windows.Forms.Button();
            this.btnDeletePod = new System.Windows.Forms.Button();
            this.btnCreateService = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxListPods
            // 
            this.listBoxListPods.FormattingEnabled = true;
            this.listBoxListPods.ItemHeight = 15;
            this.listBoxListPods.Location = new System.Drawing.Point(224, 71);
            this.listBoxListPods.Name = "listBoxListPods";
            this.listBoxListPods.Size = new System.Drawing.Size(435, 214);
            this.listBoxListPods.TabIndex = 0;
            // 
            // btnCreatePod
            // 
            this.btnCreatePod.Location = new System.Drawing.Point(224, 291);
            this.btnCreatePod.Name = "btnCreatePod";
            this.btnCreatePod.Size = new System.Drawing.Size(75, 23);
            this.btnCreatePod.TabIndex = 1;
            this.btnCreatePod.Text = "Create";
            this.btnCreatePod.UseVisualStyleBackColor = true;
            this.btnCreatePod.Click += new System.EventHandler(this.btnCreatePod_Click);
            // 
            // btnDeletePod
            // 
            this.btnDeletePod.Location = new System.Drawing.Point(305, 291);
            this.btnDeletePod.Name = "btnDeletePod";
            this.btnDeletePod.Size = new System.Drawing.Size(75, 23);
            this.btnDeletePod.TabIndex = 2;
            this.btnDeletePod.Text = "Delete";
            this.btnDeletePod.UseVisualStyleBackColor = true;
            this.btnDeletePod.Click += new System.EventHandler(this.btnDeletePod_Click);
            // 
            // btnCreateService
            // 
            this.btnCreateService.Location = new System.Drawing.Point(584, 291);
            this.btnCreateService.Name = "btnCreateService";
            this.btnCreateService.Size = new System.Drawing.Size(75, 23);
            this.btnCreateService.TabIndex = 3;
            this.btnCreateService.Text = "Service";
            this.btnCreateService.UseVisualStyleBackColor = true;
            this.btnCreateService.Click += new System.EventHandler(this.btnCreateService_Click);
            // 
            // PodsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCreateService);
            this.Controls.Add(this.btnDeletePod);
            this.Controls.Add(this.btnCreatePod);
            this.Controls.Add(this.listBoxListPods);
            this.Name = "PodsForm";
            this.Text = "PodsForm";
            this.Load += new System.EventHandler(this.PodsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxListPods;
        private System.Windows.Forms.Button btnCreatePod;
        private System.Windows.Forms.Button btnDeletePod;
        private System.Windows.Forms.Button btnCreateService;
    }
}