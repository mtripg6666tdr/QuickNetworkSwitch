using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

        /// <summary>
        /// Try to connect a host in internet and return the result of judging whether
        /// Machine is connected to internet or not.
        /// </summary>
        /// <returns>Whether connected to internet.</returns>
        public static async Task<bool> GetIsConnectedToInternet()
        {
            //Method 'GetResponse' seems to need a few seconds, so running async.
            //Having thought code's readability, substitute result into variable once.
            bool result = await Task.Run(() =>
            {
                //Host used in test has changed from Yahoo to Google
                //Because Google returns a response more immediate than Yahoo
                const string host = "ht" + "tp://www.google.co.jp";
                HttpWebRequest request;
                HttpWebResponse response = null;
                try
                {
                    request = (HttpWebRequest)WebRequest.Create(host);
                    request.Method = "HEAD";
                    response = (HttpWebResponse)request.GetResponse();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    response?.Dispose();
                }
            });
            return result;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //Stops timer so that checking process never run multiple while running checking process already.
            this.timer1.Enabled = false;
            this.CurrentNetworkState.Text = await GetIsConnectedToInternet() ? "有効" : "無効";
            this.AppendLog("ネットワークの状態を更新しました");

            this.DisableNetwork.Enabled = await GetIsConnectedToInternet();
            this.EnableNetwork.Enabled = !await GetIsConnectedToInternet();
            this.timer1.Enabled = true;
            this.Enabled = true;
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
            this.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Form1_Load(null, null);
        }
    }
}
