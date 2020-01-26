using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickNetworkSwitch
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string MutexName = "QuickNetworkSwitchApplication";
            using (Mutex mutex = new Mutex(false, MutexName))
            {
                bool hasHandle = false;
                try
                {
                    try
                    {
                        hasHandle = mutex.WaitOne(20, false);
                    }
                    catch (AbandonedMutexException)
                    {
                        hasHandle = true;
                    }
                    if (!hasHandle)
                    {
                        MessageBox.Show("このアプリケーションは、複数起動することはできません。タスクバーを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //Main
                    if (MessageBox.Show("このアプリケーションを使用すると、ご使用のシステムに多大な損害が発生する可能性があります。\r\n発生した損害については使用者が責任を負うものとします。\r\n\r\n続行しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
                finally
                {
                    if (hasHandle)
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
        }
    }
}
