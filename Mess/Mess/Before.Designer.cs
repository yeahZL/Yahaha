namespace Mess
{
    partial class Before
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
            this.proWork = new System.Windows.Forms.ProgressBar();
            this.confirm = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.phase = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // shpPathText
            // 
            this.shpPathText.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shpPathText.Location = new System.Drawing.Point(193, 40);
            this.shpPathText.Name = "shpPathText";
            this.shpPathText.Size = new System.Drawing.Size(337, 23);
            this.shpPathText.TabIndex = 10;
            // 
            // shpPath
            // 
            this.shpPath.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shpPath.Location = new System.Drawing.Point(28, 42);
            this.shpPath.Name = "shpPath";
            this.shpPath.Size = new System.Drawing.Size(144, 21);
            this.shpPath.TabIndex = 9;
            this.shpPath.Text = "打开shp文件";
            this.shpPath.UseVisualStyleBackColor = true;
            this.shpPath.Click += new System.EventHandler(this.shpPath_Click);
            // 
            // proWork
            // 
            this.proWork.Location = new System.Drawing.Point(28, 143);
            this.proWork.Name = "proWork";
            this.proWork.Size = new System.Drawing.Size(502, 47);
            this.proWork.TabIndex = 12;
            // 
            // confirm
            // 
            this.confirm.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm.Location = new System.Drawing.Point(387, 225);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(125, 41);
            this.confirm.TabIndex = 11;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(26, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(273, 14);
            this.label2.TabIndex = 21;
            this.label2.Text = "第几期（格式20190X-0X，比如201904-02）";
            // 
            // phase
            // 
            this.phase.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.phase.Location = new System.Drawing.Point(322, 88);
            this.phase.Name = "phase";
            this.phase.Size = new System.Drawing.Size(208, 23);
            this.phase.TabIndex = 20;
            // 
            // Before
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 295);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.phase);
            this.Controls.Add(this.proWork);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.shpPathText);
            this.Controls.Add(this.shpPath);
            this.Name = "Before";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Before";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox shpPathText;
        private System.Windows.Forms.Button shpPath;
        private System.Windows.Forms.ProgressBar proWork;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox phase;
    }
}