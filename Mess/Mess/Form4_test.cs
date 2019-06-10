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
    public partial class Form4_test : Form
    {
        public Form4_test()
        {
            InitializeComponent();
        }

        private void Form4_test_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Thread.CurrentThread.GetHashCode().ToString() + "AAA");
            Thread invokeThread = new Thread(new ThreadStart(StartMethod));
            invokeThread.Start();
            string a = string.Empty;
            for (int i = 0; i < 3; i++)      //调整循环次数，看的会更清楚
            {
                Thread.Sleep(1000);
                a = a + "B";
            }
            MessageBox.Show(Thread.CurrentThread.GetHashCode().ToString() + a);
        }

        private void StartMethod()
        {
            MessageBox.Show(Thread.CurrentThread.GetHashCode().ToString() + "CCC");
            button1.BeginInvoke(new MethodInvoker(invokeMethod));
            MessageBox.Show(Thread.CurrentThread.GetHashCode().ToString() + "DDD");
        }

        private void invokeMethod()
        {
            //Thread.Sleep(3000);
            MessageBox.Show(Thread.CurrentThread.GetHashCode().ToString() + "EEE");
        } 
    }
}
