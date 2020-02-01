namespace QuickNetworkSwitch
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CurrentNetworkState = new System.Windows.Forms.TextBox();
            this.EnableNetwork = new System.Windows.Forms.Button();
            this.DisableNetwork = new System.Windows.Forms.Button();
            this.LogWindow = new System.Windows.Forms.TextBox();
            this.StateRefrashButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ネットワークの有効/無効を変更できます";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "現在のネットワークの状態：";
            // 
            // CurrentNetworkState
            // 
            this.CurrentNetworkState.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CurrentNetworkState.Location = new System.Drawing.Point(185, 40);
            this.CurrentNetworkState.Name = "CurrentNetworkState";
            this.CurrentNetworkState.ReadOnly = true;
            this.CurrentNetworkState.Size = new System.Drawing.Size(152, 23);
            this.CurrentNetworkState.TabIndex = 2;
            // 
            // EnableNetwork
            // 
            this.EnableNetwork.Location = new System.Drawing.Point(16, 76);
            this.EnableNetwork.Name = "EnableNetwork";
            this.EnableNetwork.Size = new System.Drawing.Size(121, 23);
            this.EnableNetwork.TabIndex = 3;
            this.EnableNetwork.Text = "有効にする";
            this.EnableNetwork.UseVisualStyleBackColor = true;
            this.EnableNetwork.Click += new System.EventHandler(this.EnableNetwork_Click);
            // 
            // DisableNetwork
            // 
            this.DisableNetwork.Location = new System.Drawing.Point(143, 76);
            this.DisableNetwork.Name = "DisableNetwork";
            this.DisableNetwork.Size = new System.Drawing.Size(127, 23);
            this.DisableNetwork.TabIndex = 4;
            this.DisableNetwork.Text = "無効にする";
            this.DisableNetwork.UseVisualStyleBackColor = true;
            this.DisableNetwork.Click += new System.EventHandler(this.DisableNetwork_Click);
            // 
            // LogWindow
            // 
            this.LogWindow.Location = new System.Drawing.Point(16, 105);
            this.LogWindow.Multiline = true;
            this.LogWindow.Name = "LogWindow";
            this.LogWindow.ReadOnly = true;
            this.LogWindow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogWindow.Size = new System.Drawing.Size(321, 109);
            this.LogWindow.TabIndex = 5;
            // 
            // StateRefrashButton
            // 
            this.StateRefrashButton.Location = new System.Drawing.Point(277, 76);
            this.StateRefrashButton.Name = "StateRefrashButton";
            this.StateRefrashButton.Size = new System.Drawing.Size(60, 23);
            this.StateRefrashButton.TabIndex = 6;
            this.StateRefrashButton.Text = "更新";
            this.StateRefrashButton.UseVisualStyleBackColor = true;
            this.StateRefrashButton.Click += new System.EventHandler(this.StateRefrashButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(277, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "閉じる";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(143, 221);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "本アプリについて";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 265);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.StateRefrashButton);
            this.Controls.Add(this.LogWindow);
            this.Controls.Add(this.DisableNetwork);
            this.Controls.Add(this.EnableNetwork);
            this.Controls.Add(this.CurrentNetworkState);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "設定ウインドウ";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CurrentNetworkState;
        private System.Windows.Forms.Button EnableNetwork;
        private System.Windows.Forms.Button DisableNetwork;
        private System.Windows.Forms.TextBox LogWindow;
        private System.Windows.Forms.Button StateRefrashButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
    }
}

