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
    public partial class ScreenShot : Form
    {
        private List<string> labelItems = new List<string>();  //记录图片标注信息

        private string mxdPath = null;  //记录mxd文件路径
        private string[] shpPath = null; //记录要截图shpfile文件路径
        private string outputPath = null; //记录输出文件夹路径
        private object locker = new object();

        private MayGod mg = new MayGod();

        public ScreenShot()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
            for(int i=0; i<checkedListBox1.Items.Count; ++i)
            {
                if (checkedListBox1.GetItemChecked(i))
                    labelItems.Add(checkedListBox1.Items[i].ToString());
            }
            //Thread fThread = new Thread(new ThreadStart(SleepT));
            //fThread.Start();

            //Thread workThread = new Thread(new ParameterizedThreadStart(WorkProc));
            //workThread.Start(mg);

            
             //Thread pThread = new Thread(new ParameterizedThreadStart(progressTest));
             //pThread.Start(mg);
            /*
             * 2019年3月8日11时，多线程显示进度条功能，残念。。。            
             * 因为ArcObjects都被标记为STA，即单线程套间，每个STAs受限于一个线程，但是COM对于每个进程内的STA没有限制.
             * 但一个方法调用进入一个STA时，该方法被迁移至STA中唯一的线程中，因此，一个STA中的对象一次将只能接受和处理
             * 一个方法调用，它接受的每个方法调用都将会到达同一个线程中。（摘自Arcgis官网）
             * 
             * 因此，尽管我新建了子线程，但仍然需要等待父线程结束执行截图方法后，才会执行进度条任务。且委托方法不能进行引用。
             * 
             * 学习到了委托、匿名方法、lambda方法、泛型委托Fun<>，子线程中创建控件时，使用beginInvoke方法将该控件归到父线程中.
             * CheckForIllegalCrossThreadCalls=false 方法可以修改子线程中的空间。
             * 限于自己目前知识有限，对于多线程编程还是不够了解，就留在这里吧，以后有机会再处理。不过应该是没机会了。。。
            */

            mg.setFields(textBox2.Text, textBox3.Text, textBox9.Text, labelItems, Convert.ToInt16(textBox8.Text),
                ref axMapControl1, Convert.ToInt16(textBox5.Text), Convert.ToInt16(textBox6.Text));
            mg.outPut();
            MessageBox.Show("完成截图！");
        }

        //private void WorkProc(object param) {
        //    MayGod my = param as MayGod;
        //    int taskCount = 123;
        //    int i = 0;
        //    Invoke(new MethodInvoker(() => {
        //        progWork.Maximum = taskCount;
        //        progWork.Value = 0;
        //    }));
        //    while (i<taskCount)
        //    {
        //        i++;
        //        Thread.Sleep(500);
        //        Invoke(new MethodInvoker(() => {
        //            progWork.Value = i;
        //        }));
        //    }
        //    Invoke(new MethodInvoker(() =>
        //    {
        //        progWork.Value = taskCount;
        //        Thread.Sleep(1000);
        //    }));
        //}

        //private void UpdateProgress(int v) {
        //    progWork.Value = v;
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        public void setTip()
        {
            //设置鼠标放置上显示的tips
            ToolTip tip_button_tip = new ToolTip();
            tip_button_tip.IsBalloon = true;
            tip_button_tip.AutomaticDelay = 0;
            //tip_button_tip.SetToolTip(this.button3, "xixixi");
            tip_button_tip.SetToolTip(this.textBox4, "缩略图为400，放大图为100");
            tip_button_tip.SetToolTip(this.textBox7, "缩略图为15，放大图为5");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mxdPath = openMxdDialog();
            textBox2.Text = mxdPath;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            shpPath = openShpDialog();
            string result = mg.checkValidShp(shpPath);
            if (result != null && result != "") MessageBox.Show(result, "错误");
            textBox3.Text = shpPath[0] + @"\" + shpPath[1];
        }

        private string openMxdDialog()
        {
            string strFileName = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "mxd文件|*.mxd|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strFileName = ofd.FileName;
            }
            else
            {
                return "请打开MXD文件";
            }
            return strFileName;
        }

        private string[] openShpDialog()
        {
            string[] str = new string[2];
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开需要截图的矢量文件";
            ofd.Filter = "shapefile文件|*.shp|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string strFileName = ofd.FileName;
                str[0] = System.IO.Path.GetDirectoryName(strFileName);
                str[1] = System.IO.Path.GetFileName(strFileName);
            }
            return str;
        }

        private string openFolderdialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件保存路径";
            string file = null;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.SelectedPath;
            }
            return file;            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            outputPath = openFolderdialog();
            textBox9.Text = outputPath;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Thread pThread = new Thread(new ParameterizedThreadStart(progressTest));
            pThread.Start(mg);
        }

        //private delegate void SetPos(int ipos, string vinfo, int all);

        //private void update(int ipos, string vinfo, int all)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        SetPos setpos = new SetPos(update);
        //        this.Invoke(setpos, new object[] { ipos, vinfo, all });
        //    }
        //    else
        //    {
        //        this.label1.Text = ipos.ToString() + "/" + all.ToString();
        //        this.progressBar1.Maximum = Convert.ToInt32(all);
        //        this.progressBar1.Value = Convert.ToInt32(ipos);
        //        this.textBox10.AppendText(vinfo);
        //    }
        //}

        public void progressTest(object mg)
        {
            MayGod mgd = mg as MayGod;
            Form3 frm3 = new Form3();
            this.BeginInvoke((MethodInvoker)(() => frm3.Show())); //使用BeginInvoke将控件赋值给主线程调用
            frm3.showProgress(mgd);
        }                

       



       
    }
}
