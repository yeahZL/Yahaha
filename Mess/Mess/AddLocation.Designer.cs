namespace Mess
{
    partial class AddLocation
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
            this.shpPathText = new System.Windows.Forms.TextBox();
            this.shpPath = new System.Windows.Forms.Button();
            this.confirm = new System.Windows.Forms.Button();
            this.proWork = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // shpPathText
            // 
            this.shpPathText.Location = new System.Drawing.Point(198, 62);
            this.shpPathText.Name = "shpPathText";
            this.shpPathText.Size = new System.Drawing.Size(273, 21);
            this.shpPathText.TabIndex = 8;
            // 
            // shpPath
            // 
            this.shpPath.Location = new System.Drawing.Point(59, 62);
            this.shpPath.Name = "shpPath";
            this.shpPath.Size = new System.Drawing.Size(124, 21);
            this.shpPath.TabIndex = 7;
            this.shpPath.Text = "打开shp文件";
            this.shpPath.UseVisualStyleBackColor = true;
            this.shpPath.Click += new System.EventHandler(this.shpPath_Click);
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(472, 225);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(115, 20);
            this.confirm.TabIndex = 9;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.button1_Click);
            // 
            // proWork
            // 
            this.proWork.Location = new System.Drawing.Point(59, 133);
            this.proWork.Name = "proWork";
            this.proWork.Size = new System.Drawing.Size(412, 47);
            this.proWork.TabIndex = 10;
            // 
            // AddLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 289);
            this.Controls.Add(this.proWork);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.shpPathText);
            this.Controls.Add(this.shpPath);
            this.Name = "AddLocation";
            this.Text = "添加位置信息";
            this.Load += new System.EventHandler(this.AddLocation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox shpPathText;
        private System.Windows.Forms.Button shpPath;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.ProgressBar proWork;
    }
}