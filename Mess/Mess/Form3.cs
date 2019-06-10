using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Mess
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;           
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        public void showProgress(MayGod mg)
        {
            progressBar1.Value = mg.number;
        }

        private delegate void SetPos(int ipos, string vinfo);

        private void SetTextMesssage(int ipos, string vinfo)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMesssage);
                this.Invoke(setpos, new object[] { ipos, vinfo });
            }
            else
            {
                this.label1.Text = ipos.ToString() + "/1000";
                this.progressBar1.Value = Convert.ToInt32(ipos);
                this.textBox1.AppendText(vinfo); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread fThread = new Thread(new ThreadStart(SleepT));
            fThread.Start();
           
        }

        public void SleepT()
        {
            for (int i = 0; i < 500; i++)
            {
                System.Threading.Thread.Sleep(1000);
                SetTextMesssage(100 * i / 500, i.ToString() + "\r\n");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void StartBackGroundWork()
        {
            if (Application.RenderWithVisualStyles)
                progressBar1.Style = ProgressBarStyle.Marquee;
            else
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Maximum = 100;
                progressBar1.Value = 0;
                timer1.Enabled = true;
            }
            backgroundWorker1.RunWorkerAsync();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
                progressBar1.Increment(5);
            else
                progressBar1.Value = progressBar1.Minimum;
        }
        
        
    }
}
