using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
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
        private string LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\quicknetworkswitch\\";

        public enum NetworkState
        {
            None = 0,
            Enable,
            Disable
        }

        public bool? HasFinished
        {
            get {
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
            this.state = state != NetworkState.None ? state : throw new ArgumentException();

            if (!Directory.Exists(this.LogFilePath))
            {
                Directory.CreateDirectory(this.LogFilePath);
            }
        }

        private void Working_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.CanCloseForm)
            {
                e.Cancel = true;
                return;
            }
        }

        private async void Working_Shown(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(this.LogFilePath + "ManageLog.log", true, Encoding.UTF8))
            {
                writer.WriteLine("\r\n");
                //Check existing the file "index.target"
                if (!File.Exists(Helper.GetFullPath(TARGETFILE_STR))) {
                    MessageBox.Show("Coundn't find index.target file. Please contact to the software developer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    writer.WriteLine("[" + DateTime.Now.ToString() + "]Error!Couldn't find TARGETFILE");
                    this.CanCloseForm = true;
                    this.Close();
                    return;
                }
                //Check existing the file "DevCon.exe"
                if (!File.Exists(Helper.GetFullPath(DEVCONEXE_STR)))
                {
                    MessageBox.Show("Couldn't find executable file of Device Controller (DevCon.exe). Please contact to the software developer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    writer.WriteLine("[" + DateTime.Now.ToString() + "]Error!Couldn't find DEVCONEXE");
                    this.CanCloseForm = true;
                    this.Close();
                    return;
                }
                //User Check Dialog
                if (MessageBox.Show("デバイスの設定を適用します。よろしいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    writer.WriteLine("[" + DateTime.Now.ToString() + "] User canceled.");
                    this.CanCloseForm = true;
                    this.Close();
                    return;
                }
                //Main processing logic
                try
                {
                    writer.WriteLine("[" + DateTime.Now.ToString() + "]操作を開始します。");
                    LinkedList<string> list = new LinkedList<string>();
                    using (StreamReader reader = new StreamReader(Helper.GetFullPath(TARGETFILE_STR)))
                    {
                        string line = null;
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            if (!line.StartsWith("/!!!/") && !string.IsNullOrEmpty(line))
                            {
                                list.AddLast(line);
                            }
                        }
                    }
                    string[] TargetDeviceList = list.ToArray();
                    foreach (string v in TargetDeviceList)
                    {
                        ProcessStartInfo info = new ProcessStartInfo();
#if EDEBUG
                        info.FileName = "cmd.exe";
                        info.Arguments = $"echo {(this.state == NetworkState.Enable ? "enable" : "disable")} \"{v}\"";

#else
                        info.FileName = Helper.GetFullPath(DEVCONEXE_STR);
                        info.Arguments = $"{(this.state == NetworkState.Enable ? "enable" : "disable")} \"{v}\"";
#endif
                        writer.WriteLine("[" + DateTime.Now.ToString() + $"]デバイス:{v}の有効無効の設定を変更します。");
                        writer.WriteLine($"デバイスID:{v}を{(this.state == NetworkState.Enable ? "有効" : "無効")}にする操作を実行します");
                        Task t = new Task(() =>
                        {
                            Process p = Process.Start(info);
                            p.WaitForExit(10000);
                        });
                        t.Start();
                        await t.ConfigureAwait(true);
                        writer.WriteLine($"デバイスID:{v}を{(this.state == NetworkState.Enable ? "有効" : "無効")}にする操作:プロセスを実行しました");
                        if (t.IsFaulted)
                        {
                            writer.WriteLine($"デバイスID:{v}を{(this.state == NetworkState.Enable ? "有効" : "無効")}にする操作を実行中に例外が発生しました:" + t.Exception.InnerException.Message);
                        }
                    }
                    writer.WriteLine("[" + DateTime.Now.ToString() + "]操作を完了しました。");
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("ファイルのパスが正しくありません。開発者にお問い合わせください。", "例外", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CanCloseForm = true;
                    writer.WriteLine(ex.Message);
                    writer.WriteLine(ex.StackTrace);
                    this.Close();
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("依存ファイルの一部またはすべてが見つかりませんでした。開発者にお問い合わせください。", "例外", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CanCloseForm = true;
                    writer.WriteLine(ex.Message);
                    writer.WriteLine(ex.StackTrace);
                    this.Close();
                }
                catch (DirectoryNotFoundException ex)
                {
                    MessageBox.Show("ディレクトリ構成が正しくありません。開発者にお問い合わせください。", "例外", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CanCloseForm = true;
                    writer.WriteLine(ex.Message);
                    writer.WriteLine(ex.StackTrace);
                    this.Close();
                }
                catch (ObjectDisposedException ex)
                {
                    MessageBox.Show($"実行中に例外が発生しました。{ex.Message}", "例外", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CanCloseForm = true;
                    writer.WriteLine(ex.Message);
                    writer.WriteLine(ex.StackTrace);
                    this.Close();
                }
                catch (OutOfMemoryException ex)
                {
                    MessageBox.Show("メモリが足りません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CanCloseForm = true;
                    writer.WriteLine(ex.Message);
                    writer.WriteLine(ex.StackTrace);
                    this.Close();
                }
                catch (PathTooLongException ex)
                {
                    MessageBox.Show("パスが長すぎます。開発者にお問い合わせください。", "例外", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CanCloseForm = true;
                    writer.WriteLine(ex.Message);
                    writer.WriteLine(ex.StackTrace);
                    this.Close();
                }
                catch (IOException ex)
                {
                    MessageBox.Show("I/Oエラーが発生しました。開発者にお問い合わせください。", "例外", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CanCloseForm = true;
                    writer.WriteLine(ex.Message);
                    writer.WriteLine(ex.StackTrace);
                    this.Close();
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show("ファイルにアクセスする権限がありません。開発者にお問い合わせください。", "例外", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CanCloseForm = true;
                    writer.WriteLine(ex.Message);
                    writer.WriteLine(ex.StackTrace);
                    this.Close();
                }
            }

            this.CanCloseForm = true;
            this.Close();

        }
    }
}
