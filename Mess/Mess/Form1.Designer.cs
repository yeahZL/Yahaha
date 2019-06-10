namespace Mess
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ScreenShot = new System.Windows.Forms.Button();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.Report = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.before = new System.Windows.Forms.Button();
            this.after = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // ScreenShot
            // 
            this.ScreenShot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScreenShot.BackColor = System.Drawing.Color.Transparent;
            this.ScreenShot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ScreenShot.ForeColor = System.Drawing.Color.Yellow;
            this.ScreenShot.Location = new System.Drawing.Point(12, 162);
            this.ScreenShot.Name = "ScreenShot";
            this.ScreenShot.Size = new System.Drawing.Size(215, 78);
            this.ScreenShot.TabIndex = 0;
            this.ScreenShot.Text = "截图";
            this.ScreenShot.UseVisualStyleBackColor = false;
            this.ScreenShot.Click += new System.EventHandler(this.button1_Click);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(12, 12);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // Report
            // 
            this.Report.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Report.BackColor = System.Drawing.Color.Transparent;
            this.Report.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Report.ForeColor = System.Drawing.Color.Yellow;
            this.Report.Location = new System.Drawing.Point(12, 277);
            this.Report.Name = "Report";
            this.Report.Size = new System.Drawing.Size(215, 80);
            this.Report.TabIndex = 3;
            this.Report.Text = "报告";
            this.Report.UseVisualStyleBackColor = false;
            this.Report.Click += new System.EventHandler(this.Report_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Yellow;
            this.button1.Location = new System.Drawing.Point(12, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 78);
            this.button1.TabIndex = 4;
            this.button1.Text = "添加位置信息";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // before
            // 
            this.before.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.before.BackColor = System.Drawing.Color.Transparent;
            this.before.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.before.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.before.ForeColor = System.Drawing.Color.Yellow;
            this.before.Location = new System.Drawing.Point(341, 50);
            this.before.Name = "before";
            this.before.Size = new System.Drawing.Size(215, 78);
            this.before.TabIndex = 5;
            this.before.Text = "前期";
            this.before.UseVisualStyleBackColor = false;
            this.before.Click += new System.EventHandler(this.before_Click);
            // 
            // after
            // 
            this.after.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.after.BackColor = System.Drawing.Color.Transparent;
            this.after.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.after.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.after.ForeColor = System.Drawing.Color.Yellow;
            this.after.Location = new System.Drawing.Point(341, 162);
            this.after.Name = "after";
            this.after.Size = new System.Drawing.Size(215, 78);
            this.after.TabIndex = 6;
            this.after.Text = "后期";
            this.after.UseVisualStyleBackColor = false;
            this.after.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Yellow;
            this.button2.Location = new System.Drawing.Point(341, 279);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(215, 78);
            this.button2.TabIndex = 7;
            this.button2.Text = "其他区";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(587, 433);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.after);
            this.Controls.Add(this.before);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Report);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.ScreenShot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "青浦区垃圾巡查报告生成软件";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ScreenShot;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.Button Report;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button before;
        private System.Windows.Forms.Button after;
        private System.Windows.Forms.Button button2;
    }
}

