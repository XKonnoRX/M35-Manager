namespace M35_Manager
{
    partial class CallForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CallForm));
            NumberLabel = new Label();
            AcceptButton = new Button();
            RejectButton = new Button();
            CallLabel = new Label();
            label1 = new Label();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // NumberLabel
            // 
            NumberLabel.AutoSize = true;
            NumberLabel.Dock = DockStyle.Fill;
            NumberLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            NumberLabel.ForeColor = SystemColors.ControlText;
            NumberLabel.Location = new Point(0, 0);
            NumberLabel.Name = "NumberLabel";
            NumberLabel.Size = new Size(160, 30);
            NumberLabel.TabIndex = 0;
            NumberLabel.Text = "+78005553535";
            NumberLabel.Click += NumberLabel_Click;
            NumberLabel.MouseDown += CallForm_MouseDown;
            // 
            // AcceptButton
            // 
            AcceptButton.AutoEllipsis = true;
            AcceptButton.BackgroundImage = (Image)resources.GetObject("AcceptButton.BackgroundImage");
            AcceptButton.BackgroundImageLayout = ImageLayout.Stretch;
            AcceptButton.FlatAppearance.BorderSize = 0;
            AcceptButton.FlatAppearance.MouseDownBackColor = SystemColors.AppWorkspace;
            AcceptButton.FlatAppearance.MouseOverBackColor = SystemColors.AppWorkspace;
            AcceptButton.FlatStyle = FlatStyle.Flat;
            AcceptButton.Location = new Point(12, 181);
            AcceptButton.Name = "AcceptButton";
            AcceptButton.Size = new Size(67, 67);
            AcceptButton.TabIndex = 1;
            AcceptButton.UseVisualStyleBackColor = true;
            // 
            // RejectButton
            // 
            RejectButton.AutoEllipsis = true;
            RejectButton.BackgroundImage = (Image)resources.GetObject("RejectButton.BackgroundImage");
            RejectButton.BackgroundImageLayout = ImageLayout.Stretch;
            RejectButton.FlatAppearance.BorderSize = 0;
            RejectButton.FlatAppearance.MouseDownBackColor = SystemColors.AppWorkspace;
            RejectButton.FlatAppearance.MouseOverBackColor = SystemColors.AppWorkspace;
            RejectButton.FlatStyle = FlatStyle.Flat;
            RejectButton.Location = new Point(149, 181);
            RejectButton.Name = "RejectButton";
            RejectButton.Size = new Size(67, 67);
            RejectButton.TabIndex = 2;
            RejectButton.UseVisualStyleBackColor = true;
            RejectButton.Click += RejectButton_Click;
            // 
            // CallLabel
            // 
            CallLabel.AutoSize = true;
            CallLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            CallLabel.Location = new Point(27, 9);
            CallLabel.Name = "CallLabel";
            CallLabel.Size = new Size(173, 21);
            CallLabel.TabIndex = 3;
            CallLabel.Text = "ВХОДЯЩИЙ ЗВОНОК";
            CallLabel.TextAlign = ContentAlignment.TopCenter;
            CallLabel.MouseDown += CallForm_MouseDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(56, 68);
            label1.Name = "label1";
            label1.Size = new Size(122, 32);
            label1.TabIndex = 4;
            label1.Text = "Port #1-1";
            label1.TextAlign = ContentAlignment.TopCenter;
            label1.MouseDown += CallForm_MouseDown;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(NumberLabel);
            panel1.Location = new Point(34, 35);
            panel1.Name = "panel1";
            panel1.Size = new Size(160, 30);
            panel1.TabIndex = 5;
            panel1.Click += NumberLabel_Click;
            // 
            // CallForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(228, 252);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(CallLabel);
            Controls.Add(RejectButton);
            Controls.Add(AcceptButton);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CallForm";
            Text = "CallForm";
            MouseDown += CallForm_MouseDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label NumberLabel;
        private Button AcceptButton;
        private Button RejectButton;
        private Label CallLabel;
        private Label label1;
        private Panel panel1;
    }
}