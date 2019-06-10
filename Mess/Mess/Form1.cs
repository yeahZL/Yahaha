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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScreenShot frm = new ScreenShot();
            frm.setTip();
            frm.Show();
            //Thread pThread = new Thread(new ThreadStart(Screen));
            //pThread.SetApartmentState(ApartmentState.STA);
            //pThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //private void Screen()
        //{
        //    Form2 frm2 = new Form2();
        //    this.Invoke((MethodInvoker)(() => frm2.Show()));   
        //}

        void openNewForm()
        {
            Form4_test newForm = new Form4_test();
            newForm.Show();
        }

        void _threadProc()
        {
            MethodInvoker mi = new MethodInvoker(openNewForm);
            BeginInvoke(mi);
            Console.WriteLine("新打开的窗口没有阻塞之后的执行");
            Console.ReadLine();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread newThread = new Thread(new ThreadStart(_threadProc));
            newThread.Start();
        }

        private void Report_Click(object sender, EventArgs e)
        {
            opFolder frm = new opFolder();
            frm.setTips();
            frm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AddLocation frm = new AddLocation();
            frm.setTips();
            frm.Show();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.button1.BackColor = Color.Yellow;            
        }

        private void oneButton_Click(object sender, EventArgs e)
        {

        }

        private void before_Click(object sender, EventArgs e)
        {
            Before frm = new Before();           
            frm.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            After frm = new After();
            frm.setTip();
            frm.Show();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Other frm = new Other();
            frm.Show();
        }

        

    }
}
