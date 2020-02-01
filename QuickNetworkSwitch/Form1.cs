using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickNetworkSwitch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();

            if (Properties.Settings.Default.FirstLaunch)
            {
                using (FirstLaunch fl = new FirstLaunch())
                {
                    fl.ShowDialog();
                    Properties.Settings.Default.FirstLaunch = !fl.NeverShowAgain;
                    Properties.Settings.Default.Save();
                }
            }
            
            this.timer1.Enabled = true;
        }

        /// <summary>
        /// ログのテキストボックス一行のログを追加します。
        /// </summary>
        /// <param name="LogText">追加するログの文字列</param>
        public void AppendLog(string LogText)
        {
            this.LogWindow.AppendText("[" + DateTime.Now.ToString() + "]" + LogText.Replace("\r\n", "\r").Replace('\r', '\n').Replace("\n", "") + "\r\n");
        }
        public void AppendLog(Exception ErrorObj)
        {
            //簡易的な実装
            this.AppendLog(ErrorObj.Message);
        }
        public void AppendLog(object LogObj)
        {
            this.AppendLog(LogObj.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CurrentNetworkState.Text = NetworkInterface.GetIsNetworkAvailable() ? "有効" : "無効";
            this.AppendLog("ネットワークの状態を更新しました");

            this.DisableNetwork.Enabled = NetworkInterface.GetIsNetworkAvailable();
            this.EnableNetwork.Enabled = !NetworkInterface.GetIsNetworkAvailable();
        }

        private void StateRefrashButton_Click(object sender, EventArgs e)
        {
            this.Form1_Load(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            using(AboutBox1 aboutBox = new AboutBox1())
            {
                aboutBox.ShowDialog();
            }
            this.TopMost = true;
        }

        private void EnableNetwork_Click(object sender, EventArgs e)
        {
            this.Work(Working.NetworkState.Enable);
        }

        private void DisableNetwork_Click(object sender, EventArgs e)
        {
            this.Work(Working.NetworkState.Disable);
        }

        private void Work(Working.NetworkState state)
        {
            this.TopMost = this.timer1.Enabled = false;
            using(Working workingWin = new Working(this, state))
            {
                this.AppendLog("操作を実行します。");
                workingWin.ShowDialog();
                this.AppendLog("操作を実行しました。");
                if (state == Working.NetworkState.Enable)
                {
                    MessageBox.Show("有効にする操作後はWiFiが正常に接続されるまで多少時間がかかるため、\r\nネットワーク機能が使用可能な状態になるまで少々お待ちください。");
                }
            }
            this.TopMost = this.timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Form1_Load(null, null);
        }
    }
}
