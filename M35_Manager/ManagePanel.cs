using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M35_Manager
{
    public partial class ManagePanel : UserControl
    {
        public SimSlot SimSlot { get; set; }
        public ManagePanel(SimSlot slot)
        {
            InitializeComponent();
            SimSlot = slot;
        }

        private void ManagePanel_Load(object sender, EventArgs e)
        {

        }
        protected void OnMouseWheel(object sender, MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            // Двигаем панель по вертикали в зависимости от направления прокрутки
            int scrollValue = e.Delta > 0 ? -20 : 20;
            recievePanel.AutoScrollPosition = new Point(0, -recievePanel.AutoScrollPosition.Y + scrollValue);
        }
        private void ConsoleCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (ConsoleCommand.Text == "restart")
                    {
                        SimSlot.RestartScan();
                    }
                    if(ConsoleCommand.Text == "getimei")
                    {
                        ConsoleWindow.AppendText(AtCommander.GetImei(SimSlot.serialPort)+"\r\n");
                    }
                }
                catch(Exception ex) 
                {
                    ConsoleWindow.AppendText($"{ex.Message} {ex.StackTrace}\r\n");
                }
                e.Handled = true;
            }
        }
        public void CreateMessage(string mess, bool send)
        {
            var panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.MouseWheel += OnMouseWheel;
            var textbox = new TextBox();
            textbox.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            if(send)
            {
                textbox.Location = new Point(-24, 0);
                textbox.Anchor = AnchorStyles.Right;
                textbox.BackColor = Color.MistyRose;
            }
            else
            {
                textbox.Location = new Point(14, 0);
                textbox.Anchor = AnchorStyles.Left;
                textbox.BackColor = SystemColors.GradientActiveCaption;
            }
            textbox.MaximumSize = new Size(MessagePage.Width - 38, MessagePage.Height);
            textbox.BorderStyle = BorderStyle.FixedSingle;
            textbox.Multiline = true;
            textbox.ReadOnly = true;
            textbox.Text = mess;
            textbox.MouseWheel += OnMouseWheel;
            label1.Text = mess + "w";
            label1.Font = textbox.Font;
            label1.MaximumSize = textbox.MaximumSize;
            textbox.Size = new Size(label1.Size.Width, label1.Size.Height + 6);
            panel.Size = new Size(textbox.Width, textbox.Height + 14);
            panel.MinimumSize = panel.Size;
            panel.Controls.Add(textbox);
            recievePanel.Controls.Add(panel);
            recievePanel.Controls.SetChildIndex(panel, 0);
            recievePanel.ScrollControlIntoView(panel);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (messageBox.Text == "" || numberBox.Text == "+7" || SimSlot.serialPort == null || string.IsNullOrEmpty(SimSlot.SlotData.CurrentNumber))
                return;
            var res = AtCommander.SendSMS(SimSlot.serialPort, numberBox.Text, messageBox.Text);
            var sms = new SmsData
            {
                DateTime = DateTime.Now,
                SourceNumber = SimSlot.SlotData.CurrentNumber,
                DestNumber = numberBox.Text,
                SlotId = SimSlot.SlotData.Id,
                Name = SimSlot.SlotData.Port,
                Message = messageBox.Text,
                Status = $"SEND: {res}"
            };
            DB.Insert(sms);
            var mess = $"{sms.DateTime} \r\n" +
                $"to: {numberBox.Text} \r\n" +
                $"{messageBox.Text} \r\n" +
                $"Status: {res}";
            CreateMessage(mess, true);
            numberBox.Text = "+7";
            messageBox.Text = "";
        }

        private void messageBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                sendButton_Click(sender, new EventArgs());
            }
        }
    }
}
