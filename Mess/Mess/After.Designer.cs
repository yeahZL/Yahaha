namespace Mess
{
    partial class After
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(After));
            this.label2 = new System.Windows.Forms.Label();
            this.phase = new System.Windows.Forms.TextBox();
            this.excelText = new System.Windows.Forms.TextBox();
            this.excelPath = new System.Windows.Forms.Button();
            this.shpPathText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.resultShow = new System.Windows.Forms.RichTextBox();
            this.insituPath = new System.Windows.Forms.Button();
            this.insituText = new System.Windows.Forms.TextBox();
            this.proWork = new System.Windows.Forms.ProgressBar();
            this.confirm = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(29, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(273, 14);
            this.label2.TabIndex = 23;
            this.label2.Text = "第几期（格式20190X-0X，比如201904-02）";
            // 
            // phase
            // 
            this.phase.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.phase.Location = new System.Drawing.Point(323, 24);
            this.phase.Name = "phase";
            this.phase.Size = new System.Drawing.Size(208, 23);
            this.phase.TabIndex = 22;
            // 
            // excelText
            // 
            this.excelText.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.excelText.Location = new System.Drawing.Point(194, 110);
            this.excelText.Name = "excelText";
            this.excelText.Size = new System.Drawing.Size(337, 23);
            this.excelText.TabIndex = 25;
            // 
            // excelPath
            // 
            this.excelPath.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.excelPath.Location = new System.Drawing.Point(32, 110);
            this.excelPath.Name = "excelPath";
            this.excelPath.Size = new System.Drawing.Size(144, 23);
            this.excelPath.TabIndex = 26;
            this.excelPath.Text = "打开汇总Excel";
            this.excelPath.UseVisualStyleBackColor = true;
            this.excelPath.Click += new System.EventHandler(this.excelPath_Click);
            // 
            // shpPathText
            // 
            this.shpPathText.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shpPathText.Location = new System.Drawing.Point(194, 62);
            this.shpPathText.Name = "shpPathText";
            this.shpPathText.Size = new System.Drawing.Size(337, 23);
            this.shpPathText.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(32, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 24);
            this.button1.TabIndex = 27;
            this.button1.Text = "打开shapefile";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // resultShow
            // 
            this.resultShow.Location = new System.Drawing.Point(32, 219);
            this.resultShow.Name = "resultShow";
            this.resultShow.Size = new System.Drawing.Size(175, 168);
            this.resultShow.TabIndex = 30;
            this.resultShow.Text = "";
            this.resultShow.TextChanged += new System.EventHandler(this.resultShow_TextChanged);
            // 
            // insituPath
            // 
            this.insituPath.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.insituPath.Location = new System.Drawing.Point(32, 160);
            this.insituPath.Name = "insituPath";
            this.insituPath.Size = new System.Drawing.Size(144, 23);
            this.insituPath.TabIndex = 32;
            this.insituPath.Text = "打开现场照片文件夹";
            this.insituPath.UseVisualStyleBackColor = true;
            this.insituPath.Click += new System.EventHandler(this.insituPath_Click);
            // 
            // insituText
            // 
            this.insituText.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.insituText.Location = new System.Drawing.Point(194, 160);
            this.insituText.Name = "insituText";
            this.insituText.Size = new System.Drawing.Size(337, 23);
            this.insituText.TabIndex = 31;
            // 
            // proWork
            // 
            this.proWork.Location = new System.Drawing.Point(230, 247);
            this.proWork.Name = "proWork";
            this.proWork.Size = new System.Drawing.Size(301, 31);
            this.proWork.TabIndex = 34;
            this.proWork.Visible = false;
            // 
            // confirm
            // 
            this.confirm.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm.Location = new System.Drawing.Point(406, 369);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(125, 41);
            this.confirm.TabIndex = 33;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(323, 327);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 25);
            this.button2.TabIndex = 36;
            this.button2.Text = "测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(230, 339);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(53, 48);
            this.axMapControl1.TabIndex = 35;
            this.axMapControl1.Visible = false;
            // 
            // After
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(553, 455);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.axMapControl1);
            this.Controls.Add(this.proWork);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.insituPath);
            this.Controls.Add(this.insituText);
            this.Controls.Add(this.resultShow);
            this.Controls.Add(this.shpPathText);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.excelPath);
            this.Controls.Add(this.excelText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.phase);
            this.Name = "After";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox phase;
        private System.Windows.Forms.TextBox excelText;
        private System.Windows.Forms.Button excelPath;
        private System.Windows.Forms.TextBox shpPathText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox resultShow;
        private System.Windows.Forms.Button insituPath;
        private System.Windows.Forms.TextBox insituText;
        private System.Windows.Forms.ProgressBar proWork;
        private System.Windows.Forms.Button confirm;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.Button button2;
    }
}