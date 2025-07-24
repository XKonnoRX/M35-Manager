namespace M35_Manager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            SlotPanel = new Panel();
            nothingButton = new Button();
            MainPanel = new Panel();
            panel1 = new Panel();
            toolStrip1 = new ToolStrip();
            ReinitializeButton = new ToolStripButton();
            AddHubButton = new ToolStripButton();
            SlotPanel.SuspendLayout();
            panel1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // SlotPanel
            // 
            SlotPanel.AutoScroll = true;
            SlotPanel.BorderStyle = BorderStyle.FixedSingle;
            SlotPanel.Controls.Add(nothingButton);
            SlotPanel.Dock = DockStyle.Top;
            SlotPanel.Location = new Point(0, 27);
            SlotPanel.Name = "SlotPanel";
            SlotPanel.Size = new Size(1218, 172);
            SlotPanel.TabIndex = 0;
            // 
            // nothingButton
            // 
            nothingButton.Location = new Point(1130, 3);
            nothingButton.Name = "nothingButton";
            nothingButton.Size = new Size(75, 23);
            nothingButton.TabIndex = 0;
            nothingButton.Text = "nothing";
            nothingButton.UseVisualStyleBackColor = true;
            nothingButton.Visible = false;
            nothingButton.Click += nothingButton_Click;
            // 
            // MainPanel
            // 
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(0, 199);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(1218, 524);
            MainPanel.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(toolStrip1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1218, 27);
            panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { ReinitializeButton, AddHubButton });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1218, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // ReinitializeButton
            // 
            ReinitializeButton.AccessibleName = "";
            ReinitializeButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ReinitializeButton.Image = (Image)resources.GetObject("ReinitializeButton.Image");
            ReinitializeButton.ImageTransparentColor = Color.Magenta;
            ReinitializeButton.Name = "ReinitializeButton";
            ReinitializeButton.Size = new Size(23, 22);
            ReinitializeButton.Text = "Initialization";
            ReinitializeButton.Click += ReinitializeButton_Click;
            // 
            // AddHubButton
            // 
            AddHubButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AddHubButton.Image = (Image)resources.GetObject("AddHubButton.Image");
            AddHubButton.ImageTransparentColor = Color.Magenta;
            AddHubButton.Name = "AddHubButton";
            AddHubButton.Size = new Size(23, 22);
            AddHubButton.Text = "Add Hub";
            AddHubButton.Click += AddHubButton_Click;
            // 
            // MainForm
            // 
            AcceptButton = nothingButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1218, 723);
            Controls.Add(MainPanel);
            Controls.Add(SlotPanel);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1234, 500);
            Name = "MainForm";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            SlotPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel SlotPanel;
        private Panel MainPanel;
        private ManagePanel managePanel1;
        private InfoPage infoPage1;
        private Panel panel1;
        private ToolStrip toolStrip1;
        private ToolStripButton ReinitializeButton;
        private ToolStripButton AddHubButton;
        private Button nothingButton;
    }
}
