using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickNetworkSwitch
{
    public partial class Working : Form
    {
        private const string DEVCONEXE_STR = "DevCon.exe";
        private const string TARGETFILE_STR = "index.target";
        private bool CanCloseForm = false;
        private Form1 ParentWin = null;
        private NetworkState state = NetworkState.None;
        private bool? _hasFinished = null;

        public enum NetworkState
        {
            None = 0,
            Enable,
            Disable
        }
        
        public bool? HasFinished 
        {
            get{
                return this._hasFinished;
            }
            set {
                this._hasFinished = value;
            }
        }
        
        public Working(Form1 p, NetworkState state)
        {
            this.InitializeComponent();

            this.ParentWin = p;
            this.state = state == NetworkState.None ? state : throw new ArgumentException();
        }

        private void Working_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.CanCloseForm)
            {
                e.Cancel = true;
                return;
            }
        }

        private void Working_Shown(object sender, EventArgs e)
        {
            //Check existing the file "index.target"
            if (!File.Exists(Helper.GetFullPath(TARGETFILE_STR))){
                MessageBox.Show("Coundn't find index.target file. Please contact to the software developer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.CanCloseForm = true;
                this.Close();
                return;
            }
            //Check existing the file "DevCon.exe"
            if (!File.Exists(Helper.GetFullPath(DEVCONEXE_STR)))
            {
                MessageBox.Show("Couldn't find executable file of Device Controller (DevCon.exe). Please contact to the software developer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.CanCloseForm = true;
                this.Close();
                return;
            }
            //User Check Dialog
            if(MessageBox.Show("デバイスの設定を適用します。よろしいですか？","確認",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                this.CanCloseForm = true;
                this.Close();
            }
            //Main processing logic
            try
            {
                LinkedList<string> list = new LinkedList<string>();
                using (StreamReader reader = new StreamReader(Helper.GetFullPath(TARGETFILE_STR)))
                {
                    while (!reader.EndOfStream)
                    {
                        list.AddLast(reader.ReadLine());
                    }
                }
                string[] TargetDeviceList = list.ToArray();
                foreach(string v in TargetDeviceList)
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = Helper.GetFullPath(DEVCONEXE_STR);
                    info.Arguments = $"{(this.state == NetworkState.Enable ? "enable" : "disable")} \"{}\"}";
                }
            }
            catch
            {

            }
        }
    }
}
