using System.Text.Json;
using Telegram.Bot;

namespace M35_Manager
{
    public partial class MainForm : Form
    {
        public static Config config;
        public static List<SimSlot> Slots = [];
        public static List<Hub> Hubs = [];
        public static TelegramBotClient botClient;
        public MainForm()
        {
            InitializeComponent();
            config = JsonSerializer.Deserialize<Config>(File.ReadAllText("config.json"));
            DB.connectionString = config.mysql;
            botClient = new TelegramBotClient(config.telegramToken);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var hubs = DB.Select<HubData>(s => s.ServerId == config.serverId);
            foreach (var hub in hubs)
                AddHub(hub);
            if (Slots.Count != 0)
                SlotButton_Click(Slots[0].button, new EventArgs());
            foreach (var slot in Slots)
                slot.StartScan();
        }
        public void AddHub(HubData hub)
        {
            
            SlotPanel.VerticalScroll.Value = 0;
            var newHub = new Hub();
            newHub.HubData = hub;
            var hubBtn = new Button();
            hubBtn.BackColor = SystemColors.InactiveCaption;
            hubBtn.Location = new Point(0, 43 * (hub.LocalId - 1));
            hubBtn.Size = new Size(57, 43);
            hubBtn.Text = $"Hub {hub.LocalId}";
            hubBtn.Tag = $"{hub.LocalId}";
            hubBtn.Enabled = false;
            hubBtn.FlatStyle = FlatStyle.Flat;
            hubBtn.Click += HubButton_Click;
            SlotPanel.Controls.Add(hubBtn);
            for (int i = 1; i <= hub.SlotCount; i++)
            {
                var btn = new Button();
                btn.Location = new Point(57 * i, 43 * (hub.LocalId - 1));
                btn.Size = new Size(57, 43);
                btn.TabIndex = 0;
                btn.Text = $"Port {i}";
                btn.Tag = $"{hub.LocalId}";
                btn.FlatStyle = FlatStyle.Flat;
                btn.UseVisualStyleBackColor = true;
                btn.Click += SlotButton_Click;
                SlotPanel.Controls.Add(btn);

                var slot = DB.Find<SlotData>(s => s.HubId == hub.LocalId && s.Port == btn.Text);
                SimSlot simslot;
                if (slot == null)
                {
                    hubBtn.Enabled = true;
                    simslot = new SimSlot(btn, new SlotData { HubId = hub.LocalId, LocalId = i, Port = btn.Text, ServerId = config.serverId });
                }
                else
                {
                    simslot = new SimSlot(btn, slot);
                    try
                    {
                        simslot.serialPort = AtCommander.OpenPort(simslot.SlotData.SerialPort);
                    }
                    catch
                    {

                    }
                }
                var managePanel = new ManagePanel(simslot);
                managePanel.Dock = DockStyle.Fill;
                managePanel.Location = new Point(294, 0);
                managePanel.TabIndex = 0;
                var infoPage = new InfoPage(simslot);
                infoPage.Dock = DockStyle.Left;
                infoPage.TabIndex = 0;
                simslot.managePanel = managePanel;
                simslot.infoPage = infoPage;
                newHub.SimSlots.Add(simslot);
                Slots.Add(simslot);
            }
            Hubs.Add(newHub);
        }
        public void InitializeHub(HubData[] hubs)
        {
            var init = new InitializeForm(this, hubs);
            init.ShowDialog();

        }
        private void LoadSlot()
        {

        }
        private void HubButton_Click(object sender, EventArgs e)
        {
            var hub = DB.Find<HubData>(s => s.LocalId.ToString() == ((Button)sender).Tag.ToString() && s.ServerId == config.serverId);
            if (MessageBox.Show(text: $"Проинициализировать хаб {hub.LocalId}?", caption: "", buttons: MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ((Button)sender).Enabled = false;
                var slots = Hubs.Find(s => s.HubData.Id == hub.Id).SimSlots;
                foreach (var slot in slots)
                    slot.scanKey = false;
                DB.DeleteAll<SlotData>(s => s.ServerId == config.serverId && s.HubId == hub.Id);
                InitializeHub([hub]);
            }
        }
        private void SlotButton_Click(object sender, EventArgs e)
        {
            foreach (var slot in Slots)
                slot.button.BackColor = slot.StateColor;
            var btn = (Button)sender;
            btn.BackColor = SystemColors.ActiveCaption;
            btn.Select();
            var simslot = Slots.FirstOrDefault(s => s.SlotData.Port == btn.Text && s.SlotData.HubId.ToString() == btn.Tag.ToString());
            simslot.StateColor = Color.Transparent;
            LoadSlotPage(simslot);
        }
        private void LoadSlotPage(SimSlot slot)
        {
            MainPanel.Controls.Clear();
            if (slot.managePanel == null)
            {
                var managePanel = new ManagePanel(slot);
                managePanel.Dock = DockStyle.Fill;
                managePanel.Location = new Point(294, 0);
                managePanel.TabIndex = 0;
                MainPanel.Controls.Add(managePanel);
                var infoPage = new InfoPage(slot);
                infoPage.Dock = DockStyle.Left;
                infoPage.TabIndex = 0;
                infoPage.SetDefault();
                MainPanel.Controls.Add(infoPage);
                slot.managePanel = managePanel;
                slot.infoPage = infoPage;
            }
            else
            {
                MainPanel.Controls.Add(slot.managePanel);
                MainPanel.Controls.Add(slot.infoPage);
            }

        }

        private void ReinitializeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(text: "Вы уверены? Все порты будут удалены!", caption: "", buttons: MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var slot in Slots)
                {
                    try
                    {
                        if (slot.serialPort != null)
                            slot.serialPort.Close();
                        slot.SlotData.SerialPort = null;
                    }
                    catch { }
                }
                DB.DeleteAll<SlotData>(s => s.ServerId == config.serverId);
                var hubs = DB.Select<HubData>(s => s.ServerId == config.serverId);
                InitializeHub([.. hubs]);
            }
        }

        private void AddHubButton_Click(object sender, EventArgs e)
        {
            var form = new AddHub(this);
            form.ShowDialog();
            form.Enabled = false;
        }

        private void nothingButton_Click(object sender, EventArgs e)
        {

        }
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    var text = textBox1.Text;
        //    string res = "";
        //    for (int i = 0; i < text.Length / 4; i++)
        //    {
        //        string buf = "";
        //        for (int j = 0; j < 4; j++)
        //            buf += text[4 * i + j];
        //        res += (char)Convert.ToInt32(buf, 16);
        //    }
        //    textBox2.Text = res;
        //}
    }
}
