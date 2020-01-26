using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickNetworkSwitch
{
    public partial class Working : Form
    {
        private bool CanCloseForm = false;
        private Form1 ParentWin = null;
        private NetworkState state = NetworkState.None;

        public enum NetworkState
        {
            None = 0,
            Enable,
            Disable
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
            
        }
    }
}
