
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
            this.btnCreatePod = new System.Windows.Forms.Button();
            this.btnDeletePod = new System.Windows.Forms.Button();
            this.listViewPods = new System.Windows.Forms.ListView();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreatePod
            // 
            this.btnCreatePod.Location = new System.Drawing.Point(88, 279);
            this.btnCreatePod.Name = "btnCreatePod";
            this.btnCreatePod.Size = new System.Drawing.Size(75, 23);
            this.btnCreatePod.TabIndex = 1;
            this.btnCreatePod.Text = "Create";
            this.btnCreatePod.UseVisualStyleBackColor = true;
            this.btnCreatePod.Click += new System.EventHandler(this.btnCreatePod_Click);
            // 
            // btnDeletePod
            // 
            this.btnDeletePod.Location = new System.Drawing.Point(530, 279);
            this.btnDeletePod.Name = "btnDeletePod";
            this.btnDeletePod.Size = new System.Drawing.Size(75, 23);
            this.btnDeletePod.TabIndex = 2;
            this.btnDeletePod.Text = "Delete";
            this.btnDeletePod.UseVisualStyleBackColor = true;
            this.btnDeletePod.Click += new System.EventHandler(this.btnDeletePod_Click);
            // 
            // listViewPods
            // 
            this.listViewPods.HideSelection = false;
            this.listViewPods.Location = new System.Drawing.Point(88, 27);
            this.listViewPods.Name = "listViewPods";
            this.listViewPods.Size = new System.Drawing.Size(517, 246);
            this.listViewPods.TabIndex = 4;
            this.listViewPods.UseCompatibleStateImageBehavior = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 376);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // PodsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 411);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.listViewPods);
            this.Controls.Add(this.btnDeletePod);
            this.Controls.Add(this.btnCreatePod);
            this.Name = "PodsForm";
            this.Text = "PodsForm";
            this.Load += new System.EventHandler(this.PodsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCreatePod;
        private System.Windows.Forms.Button btnDeletePod;
        private System.Windows.Forms.ListView listViewPods;
        private System.Windows.Forms.Button btnBack;
    }
}