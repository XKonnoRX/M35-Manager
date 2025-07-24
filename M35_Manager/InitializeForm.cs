using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace M35_Manager
{
    public partial class InitializeForm : Form
    {
        private MainForm mainForm;
        private HubData[] hubs;
        private List<SerialPort> portList = [];
        private bool taskKey = true;
        public InitializeForm(MainForm form, HubData[] hubs)
        {
            InitializeComponent();
            this.mainForm = form;
            this.hubs = hubs;
            pictureBox1.Visible = true;
            textBox1.Visible = false;
        }

        private void InitializeForm_Load(object sender, EventArgs e)
        {
            List<string> ports = [.. SerialPort.GetPortNames()];
            var slots = DB.Select<SlotData>(s => s.ServerId == MainForm.config.serverId && hubs.FirstOrDefault(h => h.Id == s.HubId) == null);
            foreach (var slot in slots)
                ports.Remove(slot.SerialPort);
            foreach (string portName in ports)
                portList.Add(AtCommander.OpenPort(portName));
            IProgress<(int hub, int slot, string log)> progress = new Progress<(int hub, int slot, string log)>(report =>
            {
                if (report.hub == -1)
                {
                    button1_Click(this, new EventArgs());
                }
                slotLabel.Text = $"Слот: {report.hub}-{report.slot}";
                textBox1.Text += report.log + "\r\n";
            });
            Task.Run(() => WaitPort(progress));
                
        }
        private async Task WaitPort(IProgress<(int hub, int slot, string log)> progress, int hubIndex = 0, int slotId = 1)
        {
            try
            {

                if (hubIndex >= hubs.Length)
                {
                    progress.Report((-1, 0, "закончил, выключаюсь"));
                    return;
                }
                var hub = hubs[hubIndex];
                progress.Report((hub.LocalId, slotId, "начал круг"));
                SerialPort findingPort = null;
                taskKey = true;
                while (taskKey)
                {
                    List<Task> tasks = [];
                    foreach (SerialPort port in portList)
                    {
                        var res = AtCommander.GetPortStatus(port);
                        if (res.Contains("QINISTAT"))
                        {
                            findingPort = port;
                            taskKey = false;
                        }
                    }
                }
                if (findingPort == null)
                {
                    progress.Report((hub.LocalId, slotId, "стопнули"));
                    return;
                }
                progress.Report((hub.LocalId, slotId, "нашел порт"));
                portList.Remove(findingPort);
                var imei = AtCommander.GetImei(findingPort);
                var slot = MainForm.Slots.First(s => s.SlotData.LocalId == slotId && s.SlotData.HubId == hub.LocalId);
                slot.serialPort = findingPort;
                slot.SlotData.SerialPort = findingPort.PortName;
                slot.SlotData.IMEI = imei;
                slot.SlotData = DB.Insert(slot.SlotData);
                slot.StartScan();
                progress.Report((hub.LocalId, slotId, "залил в бд"));
                slotId++;
                if (slotId > hub.SlotCount)
                {
                    slotId = 1;
                    hubIndex++;
                }
                progress.Report((hub.LocalId, slotId, "стартую следующий"));
                Task.Run(() => WaitPort(progress, hubIndex, slotId));
            }
            catch (Exception ex)
            {
                progress.Report((-2, 0, $"{ex.Message} {ex.StackTrace}"));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (var port in portList)
                port.Close();
            taskKey = false;
            mainForm.Enabled = true;
            mainForm.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            textBox1.Visible = true;
        }
    }
}
