namespace Mess
{
    partial class Other
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Other));
            this.shpPathText = new System.Windows.Forms.TextBox();
            this.shpPath = new System.Windows.Forms.Button();
            this.confirm = new System.Windows.Forms.Button();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // shpPathText
            // 
            this.shpPathText.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shpPathText.Location = new System.Drawing.Point(174, 33);
            this.shpPathText.Name = "shpPathText";
            this.shpPathText.Size = new System.Drawing.Size(337, 23);
            this.shpPathText.TabIndex = 12;
            // 
            // shpPath
            // 
            this.shpPath.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shpPath.Location = new System.Drawing.Point(9, 35);
            this.shpPath.Name = "shpPath";
            this.shpPath.Size = new System.Drawing.Size(144, 21);
            this.shpPath.TabIndex = 11;
            this.shpPath.Text = "打开shp文件";
            this.shpPath.UseVisualStyleBackColor = true;
            this.shpPath.Click += new System.EventHandler(this.shpPath_Click_1);
            // 
            // confirm
            // 
            this.confirm.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm.Location = new System.Drawing.Point(405, 173);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(125, 41);
            this.confirm.TabIndex = 13;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(42, 86);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(72, 36);
            this.axMapControl1.TabIndex = 14;
            this.axMapControl1.Visible = false;
            // 
            // Other
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 226);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.shpPathText);
            this.Controls.Add(this.shpPath);
            this.Name = "Other";
            this.Text = "Other";
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox shpPathText;
        private System.Windows.Forms.Button shpPath;
        private System.Windows.Forms.Button confirm;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
    }
}