namespace Mess
{
    partial class opFolder
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
            this.label1 = new System.Windows.Forms.Label();
            this.shpPath = new System.Windows.Forms.Button();
            this.shpPathText = new System.Windows.Forms.TextBox();
            this.template = new System.Windows.Forms.Button();
            this.templateText = new System.Windows.Forms.TextBox();
            this.ScreenShot = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.sShot = new System.Windows.Forms.TextBox();
            this.insitu = new System.Windows.Forms.TextBox();
            this.QPeach = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.outputPath = new System.Windows.Forms.TextBox();
            this.progWorkQP = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.phase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "青浦区";
            // 
            // shpPath
            // 
            this.shpPath.Location = new System.Drawing.Point(96, 47);
            this.shpPath.Name = "shpPath";
            this.shpPath.Size = new System.Drawing.Size(124, 21);
            this.shpPath.TabIndex = 5;
            this.shpPath.Text = "打开shp文件";
            this.shpPath.UseVisualStyleBackColor = true;
            this.shpPath.Click += new System.EventHandler(this.shpPath_Click);
            // 
            // shpPathText
            // 
            this.shpPathText.Location = new System.Drawing.Point(235, 47);
            this.shpPathText.Name = "shpPathText";
            this.shpPathText.Size = new System.Drawing.Size(273, 21);
            this.shpPathText.TabIndex = 6;
            this.shpPathText.TextChanged += new System.EventHandler(this.shpPathText_TextChanged);
            // 
            // template
            // 
            this.template.Location = new System.Drawing.Point(96, 89);
            this.template.Name = "template";
            this.template.Size = new System.Drawing.Size(124, 21);
            this.template.TabIndex = 7;
            this.template.Text = "模板文件";
            this.template.UseVisualStyleBackColor = true;
            this.template.Click += new System.EventHandler(this.template_Click);
            // 
            // templateText
            // 
            this.templateText.Location = new System.Drawing.Point(235, 89);
            this.templateText.Name = "templateText";
            this.templateText.ReadOnly = true;
            this.templateText.Size = new System.Drawing.Size(273, 21);
            this.templateText.TabIndex = 8;
            this.templateText.Text = "D:\\青浦区垃圾巡查\\0301.docx";
            // 
            // ScreenShot
            // 
            this.ScreenShot.Location = new System.Drawing.Point(96, 131);
            this.ScreenShot.Name = "ScreenShot";
            this.ScreenShot.Size = new System.Drawing.Size(124, 21);
            this.ScreenShot.TabIndex = 9;
            this.ScreenShot.Text = "遥感截图文件夹";
            this.ScreenShot.UseVisualStyleBackColor = true;
            this.ScreenShot.Click += new System.EventHandler(this.ScreenShot_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(96, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 21);
            this.button1.TabIndex = 10;
            this.button1.Text = "现场拍照文件夹";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // sShot
            // 
            this.sShot.Location = new System.Drawing.Point(235, 132);
            this.sShot.Name = "sShot";
            this.sShot.Size = new System.Drawing.Size(273, 21);
            this.sShot.TabIndex = 11;
            this.sShot.Text = "D:\\青浦区垃圾巡查\\2019年02期\\截图";
            // 
            // insitu
            // 
            this.insitu.Location = new System.Drawing.Point(235, 171);
            this.insitu.Name = "insitu";
            this.insitu.Size = new System.Drawing.Size(273, 21);
            this.insitu.TabIndex = 12;
            this.insitu.Text = "D:\\青浦区垃圾巡查\\2019年02期\\现场照片1904-02";
            // 
            // QPeach
            // 
            this.QPeach.Location = new System.Drawing.Point(526, 158);
            this.QPeach.Name = "QPeach";
            this.QPeach.Size = new System.Drawing.Size(133, 33);
            this.QPeach.TabIndex = 13;
            this.QPeach.Text = "生成各镇报告";
            this.QPeach.UseVisualStyleBackColor = true;
            this.QPeach.Click += new System.EventHandler(this.QPeach_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(96, 209);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 21);
            this.button2.TabIndex = 14;
            this.button2.Text = "选择输出文件夹";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // outputPath
            // 
            this.outputPath.Location = new System.Drawing.Point(235, 209);
            this.outputPath.Name = "outputPath";
            this.outputPath.Size = new System.Drawing.Size(273, 21);
            this.outputPath.TabIndex = 15;
            this.outputPath.Text = "D:\\青浦区垃圾巡查\\2019年02期\\报告";
            // 
            // progWorkQP
            // 
            this.progWorkQP.Location = new System.Drawing.Point(514, 89);
            this.progWorkQP.Name = "progWorkQP";
            this.progWorkQP.Size = new System.Drawing.Size(162, 34);
            this.progWorkQP.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(514, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "进度";
            // 
            // phase
            // 
            this.phase.Location = new System.Drawing.Point(349, 250);
            this.phase.Name = "phase";
            this.phase.Size = new System.Drawing.Size(159, 21);
            this.phase.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "第几期（格式20190X-0X，比如201904-02）";
            // 
            // opFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 460);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.phase);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progWorkQP);
            this.Controls.Add(this.outputPath);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.QPeach);
            this.Controls.Add(this.insitu);
            this.Controls.Add(this.sShot);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ScreenShot);
            this.Controls.Add(this.templateText);
            this.Controls.Add(this.template);
            this.Controls.Add(this.shpPathText);
            this.Controls.Add(this.shpPath);
            this.Controls.Add(this.label1);
            this.Name = "opFolder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button shpPath;
        private System.Windows.Forms.TextBox shpPathText;
        private System.Windows.Forms.Button template;
        private System.Windows.Forms.TextBox templateText;
        private System.Windows.Forms.Button ScreenShot;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox sShot;
        private System.Windows.Forms.TextBox insitu;
        private System.Windows.Forms.Button QPeach;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox outputPath;
        private System.Windows.Forms.ProgressBar progWorkQP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox phase;
        private System.Windows.Forms.Label label2;
    }
}