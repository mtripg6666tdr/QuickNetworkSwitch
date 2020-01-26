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
        }

        /// <summary>
        /// ログのテキストボックス一行のログを追加します。
        /// </summary>
        /// <param name="LogText">追加するログの文字列</param>
        public void AppendLog(string LogText)
        {
            this.LogWindow.AppendText((DateTime.Now).ToString("hh:mm:ss") + ":" + LogText.Replace("\r\n", "\r").Replace('\r', '\n').Replace("\n", "") + "\r\n");
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
            using(AboutBox1 aboutBox = new AboutBox1())
            {
                aboutBox.ShowDialog();
            }
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
            using(Working workingWin = new Working(this, state))
            {
                workingWin.ShowDialog();
            }
        }
    }
}
