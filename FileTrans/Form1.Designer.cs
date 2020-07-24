namespace FileTrans
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tips = new System.Windows.Forms.ToolStripStatusLabel();
            this.userList = new System.Windows.Forms.ListBox();
            this.sendFile = new System.Windows.Forms.Button();
            this.sendFileAll = new System.Windows.Forms.Button();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.sendText = new System.Windows.Forms.TextBox();
            this.sendMessage = new System.Windows.Forms.Button();
            this.sendMessageAll = new System.Windows.Forms.Button();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tips});
            this.statusBar.Location = new System.Drawing.Point(0, 428);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(800, 22);
            this.statusBar.TabIndex = 0;
            this.statusBar.Text = "statusStrip1";
            // 
            // tips
            // 
            this.tips.ForeColor = System.Drawing.Color.Blue;
            this.tips.Name = "tips";
            this.tips.Size = new System.Drawing.Size(0, 17);
            // 
            // userList
            // 
            this.userList.Dock = System.Windows.Forms.DockStyle.Left;
            this.userList.FormattingEnabled = true;
            this.userList.ItemHeight = 12;
            this.userList.Location = new System.Drawing.Point(0, 0);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(157, 428);
            this.userList.TabIndex = 1;
            // 
            // sendFile
            // 
            this.sendFile.Location = new System.Drawing.Point(551, 402);
            this.sendFile.Name = "sendFile";
            this.sendFile.Size = new System.Drawing.Size(101, 23);
            this.sendFile.TabIndex = 4;
            this.sendFile.Text = "发送文件";
            this.sendFile.UseVisualStyleBackColor = true;
            this.sendFile.Click += new System.EventHandler(this.sendFile_Click);
            // 
            // sendFileAll
            // 
            this.sendFileAll.Location = new System.Drawing.Point(683, 402);
            this.sendFileAll.Name = "sendFileAll";
            this.sendFileAll.Size = new System.Drawing.Size(105, 23);
            this.sendFileAll.TabIndex = 5;
            this.sendFileAll.Text = "发送群文件";
            this.sendFileAll.UseVisualStyleBackColor = true;
            this.sendFileAll.Click += new System.EventHandler(this.sendFileAll_Click);
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(163, 0);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(637, 375);
            this.chatBox.TabIndex = 6;
            this.chatBox.Text = "";
            // 
            // sendText
            // 
            this.sendText.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendText.Location = new System.Drawing.Point(180, 388);
            this.sendText.Name = "sendText";
            this.sendText.Size = new System.Drawing.Size(348, 29);
            this.sendText.TabIndex = 7;
            // 
            // sendMessage
            // 
            this.sendMessage.Location = new System.Drawing.Point(551, 377);
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.Size = new System.Drawing.Size(101, 23);
            this.sendMessage.TabIndex = 8;
            this.sendMessage.Text = "发送消息";
            this.sendMessage.UseVisualStyleBackColor = true;
            this.sendMessage.Click += new System.EventHandler(this.sendMessage_Click);
            // 
            // sendMessageAll
            // 
            this.sendMessageAll.Location = new System.Drawing.Point(683, 377);
            this.sendMessageAll.Name = "sendMessageAll";
            this.sendMessageAll.Size = new System.Drawing.Size(105, 23);
            this.sendMessageAll.TabIndex = 9;
            this.sendMessageAll.Text = "发送群消息";
            this.sendMessageAll.UseVisualStyleBackColor = true;
            this.sendMessageAll.Click += new System.EventHandler(this.sendMessageAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sendMessageAll);
            this.Controls.Add(this.sendMessage);
            this.Controls.Add(this.sendText);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.sendFileAll);
            this.Controls.Add(this.sendFile);
            this.Controls.Add(this.userList);
            this.Controls.Add(this.statusBar);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tips;
        private System.Windows.Forms.ListBox userList;
        private System.Windows.Forms.Button sendFile;
        private System.Windows.Forms.Button sendFileAll;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.TextBox sendText;
        private System.Windows.Forms.Button sendMessage;
        private System.Windows.Forms.Button sendMessageAll;
    }
}

