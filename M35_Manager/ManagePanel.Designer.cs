namespace M35_Manager
{
    partial class ManagePanel
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePanel));
            Tab = new TabControl();
            MessagePage = new TabPage();
            recievePanel = new Panel();
            label1 = new Label();
            senderPanel = new Panel();
            messageBox = new TextBox();
            numberBox = new TextBox();
            sendButton = new Button();
            CallPage = new TabPage();
            ConsolePage = new TabPage();
            ConsoleWindow = new RichTextBox();
            ConsoleCommand = new TextBox();
            Tab.SuspendLayout();
            MessagePage.SuspendLayout();
            recievePanel.SuspendLayout();
            senderPanel.SuspendLayout();
            ConsolePage.SuspendLayout();
            SuspendLayout();
            // 
            // Tab
            // 
            Tab.Controls.Add(MessagePage);
            Tab.Controls.Add(CallPage);
            Tab.Controls.Add(ConsolePage);
            Tab.Dock = DockStyle.Fill;
            Tab.Location = new Point(0, 0);
            Tab.Name = "Tab";
            Tab.SelectedIndex = 0;
            Tab.Size = new Size(1042, 496);
            Tab.TabIndex = 0;
            // 
            // MessagePage
            // 
            MessagePage.AutoScroll = true;
            MessagePage.Controls.Add(recievePanel);
            MessagePage.Controls.Add(senderPanel);
            MessagePage.Location = new Point(4, 24);
            MessagePage.Name = "MessagePage";
            MessagePage.Size = new Size(1034, 468);
            MessagePage.TabIndex = 2;
            MessagePage.Text = "Message";
            MessagePage.UseVisualStyleBackColor = true;
            // 
            // recievePanel
            // 
            recievePanel.AutoScroll = true;
            recievePanel.Controls.Add(label1);
            recievePanel.Dock = DockStyle.Fill;
            recievePanel.Location = new Point(0, 0);
            recievePanel.Name = "recievePanel";
            recievePanel.Size = new Size(1034, 439);
            recievePanel.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 421);
            label1.MaximumSize = new Size(1000, 1000);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            label1.Visible = false;
            // 
            // senderPanel
            // 
            senderPanel.Controls.Add(messageBox);
            senderPanel.Controls.Add(numberBox);
            senderPanel.Controls.Add(sendButton);
            senderPanel.Dock = DockStyle.Bottom;
            senderPanel.Location = new Point(0, 439);
            senderPanel.Name = "senderPanel";
            senderPanel.Size = new Size(1034, 29);
            senderPanel.TabIndex = 5;
            // 
            // messageBox
            // 
            messageBox.Dock = DockStyle.Fill;
            messageBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            messageBox.Location = new Point(148, 0);
            messageBox.Name = "messageBox";
            messageBox.Size = new Size(858, 29);
            messageBox.TabIndex = 5;
            messageBox.KeyUp += messageBox_KeyUp;
            // 
            // numberBox
            // 
            numberBox.Dock = DockStyle.Left;
            numberBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numberBox.Location = new Point(0, 0);
            numberBox.Name = "numberBox";
            numberBox.Size = new Size(148, 29);
            numberBox.TabIndex = 3;
            numberBox.Text = "+7";
            // 
            // sendButton
            // 
            sendButton.BackgroundImage = (Image)resources.GetObject("sendButton.BackgroundImage");
            sendButton.BackgroundImageLayout = ImageLayout.Stretch;
            sendButton.Dock = DockStyle.Right;
            sendButton.FlatAppearance.BorderSize = 0;
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.Location = new Point(1006, 0);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(28, 29);
            sendButton.TabIndex = 4;
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // CallPage
            // 
            CallPage.Location = new Point(4, 24);
            CallPage.Name = "CallPage";
            CallPage.Padding = new Padding(3);
            CallPage.Size = new Size(1034, 468);
            CallPage.TabIndex = 1;
            CallPage.Text = "Calls";
            CallPage.UseVisualStyleBackColor = true;
            // 
            // ConsolePage
            // 
            ConsolePage.Controls.Add(ConsoleWindow);
            ConsolePage.Controls.Add(ConsoleCommand);
            ConsolePage.Location = new Point(4, 24);
            ConsolePage.Name = "ConsolePage";
            ConsolePage.Padding = new Padding(3);
            ConsolePage.Size = new Size(1034, 468);
            ConsolePage.TabIndex = 0;
            ConsolePage.Text = "Console";
            ConsolePage.UseVisualStyleBackColor = true;
            // 
            // ConsoleWindow
            // 
            ConsoleWindow.BackColor = SystemColors.InfoText;
            ConsoleWindow.Dock = DockStyle.Fill;
            ConsoleWindow.ForeColor = SystemColors.Window;
            ConsoleWindow.Location = new Point(3, 3);
            ConsoleWindow.Name = "ConsoleWindow";
            ConsoleWindow.ReadOnly = true;
            ConsoleWindow.ScrollBars = RichTextBoxScrollBars.Vertical;
            ConsoleWindow.Size = new Size(1028, 439);
            ConsoleWindow.TabIndex = 2;
            ConsoleWindow.Text = "";
            // 
            // ConsoleCommand
            // 
            ConsoleCommand.Dock = DockStyle.Bottom;
            ConsoleCommand.Location = new Point(3, 442);
            ConsoleCommand.Name = "ConsoleCommand";
            ConsoleCommand.Size = new Size(1028, 23);
            ConsoleCommand.TabIndex = 0;
            ConsoleCommand.KeyUp += ConsoleCommand_KeyUp;
            // 
            // ManagePanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Tab);
            Name = "ManagePanel";
            Size = new Size(1042, 496);
            Load += ManagePanel_Load;
            Tab.ResumeLayout(false);
            MessagePage.ResumeLayout(false);
            recievePanel.ResumeLayout(false);
            recievePanel.PerformLayout();
            senderPanel.ResumeLayout(false);
            senderPanel.PerformLayout();
            ConsolePage.ResumeLayout(false);
            ConsolePage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl Tab;
        private TabPage ConsolePage;
        private TabPage CallPage;
        private TextBox ConsoleCommand;
        public Label label1;
        public TabPage MessagePage;
        public RichTextBox ConsoleWindow;
        private Panel senderPanel;
        private TextBox numberBox;
        private Button sendButton;
        public Panel recievePanel;
        private TextBox messageBox;
    }
}
