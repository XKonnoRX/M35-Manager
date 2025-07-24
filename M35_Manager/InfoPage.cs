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
    public partial class InfoPage : UserControl
    {
        public SimSlot SimSlot { get; set; }
        public InfoPage(SimSlot slot)
        {
            InitializeComponent();
            SimSlot = slot;
            NameLabel.Text += $"{SimSlot.SlotData.HubId}-{SimSlot.SlotData.LocalId}";
            SetDefault();
        }
        public void SetDefault()
        {
            if(SimSlot.SlotData.Id!=0)
                SimSlot.SlotData = DB.Find<SlotData>(s => s.Id == SimSlot.SlotData.Id);
            if(SimSlot.SlotData!=null)
            {
                numberBox.Text = SimSlot.SlotData.CurrentNumber;
                ImeiBox.Text = SimSlot.SlotData.IMEI;
                operatorBox.Text = SimSlot.SlotData.Operator;
            }
            else
            {
                numberBox.Text = "";
                ImeiBox.Text = "";
                operatorBox.Text = "";
            }
        }

        private void InfoPage_Load(object sender, EventArgs e)
        {

        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            SimSlot.SlotData.CurrentNumber = numberBox.Text;
            SimSlot.SlotData.IMEI = ImeiBox.Text;
            SimSlot.SlotData.Operator = operatorBox.Text;
            DB.Update<SlotData>(s => s.Id == SimSlot.SlotData.Id, s => { s.IMEI = ImeiBox.Text; s.CurrentNumber = numberBox.Text; s.Operator = operatorBox.Text; });
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            
            SetDefault();
        }
    }
}
