using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace M35_Manager
{
    public partial class AddHub : Form
    {
        public MainForm Mainform;
        public AddHub(MainForm form)
        {
            InitializeComponent();
            Mainform = form;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Mainform.Enabled = true;
            Mainform.Show();
            var hubs = DB.Select<HubData>(s => s.ServerId == MainForm.config.serverId);
            var hub = DB.Insert(new HubData() { SlotCount = (int)Math.Pow(2, trackBar.Value), ServerId = MainForm.config.serverId, LocalId = hubs.Count + 1 });
            Mainform.AddHub(hub);
            this.Close();
        }

        private void AddHub_Load(object sender, EventArgs e)
        {

        }
    }
}
