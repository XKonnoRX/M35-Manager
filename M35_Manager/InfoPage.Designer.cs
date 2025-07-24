namespace M35_Manager
{
    partial class InfoPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoPage));
            NameLabel = new Label();
            label1 = new Label();
            numberBox = new TextBox();
            operatorBox = new TextBox();
            label2 = new Label();
            ImeiBox = new TextBox();
            label3 = new Label();
            acceptButton = new Button();
            updateButton = new Button();
            SuspendLayout();
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            NameLabel.Location = new Point(15, 15);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(48, 21);
            NameLabel.TabIndex = 0;
            NameLabel.Text = "Port: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 42);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 1;
            label1.Text = "Number:";
            // 
            // numberBox
            // 
            numberBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numberBox.Location = new Point(84, 42);
            numberBox.Name = "numberBox";
            numberBox.Size = new Size(195, 23);
            numberBox.TabIndex = 2;
            // 
            // operatorBox
            // 
            operatorBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            operatorBox.Location = new Point(84, 71);
            operatorBox.Name = "operatorBox";
            operatorBox.Size = new Size(195, 23);
            operatorBox.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 71);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 3;
            label2.Text = "Operator:";
            // 
            // ImeiBox
            // 
            ImeiBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ImeiBox.Location = new Point(84, 100);
            ImeiBox.Name = "ImeiBox";
            ImeiBox.Size = new Size(195, 23);
            ImeiBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 100);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 5;
            label3.Text = "IMEI:";
            // 
            // acceptButton
            // 
            acceptButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            acceptButton.Location = new Point(204, 360);
            acceptButton.Name = "acceptButton";
            acceptButton.Size = new Size(75, 23);
            acceptButton.TabIndex = 21;
            acceptButton.Text = "Accept";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += acceptButton_Click;
            // 
            // updateButton
            // 
            updateButton.BackgroundImage = (Image)resources.GetObject("updateButton.BackgroundImage");
            updateButton.BackgroundImageLayout = ImageLayout.Stretch;
            updateButton.FlatAppearance.BorderSize = 0;
            updateButton.FlatStyle = FlatStyle.Flat;
            updateButton.Location = new Point(257, 15);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(22, 21);
            updateButton.TabIndex = 22;
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += updateButton_Click;
            // 
            // InfoPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(updateButton);
            Controls.Add(acceptButton);
            Controls.Add(ImeiBox);
            Controls.Add(label3);
            Controls.Add(operatorBox);
            Controls.Add(label2);
            Controls.Add(numberBox);
            Controls.Add(label1);
            Controls.Add(NameLabel);
            Name = "InfoPage";
            Size = new Size(293, 396);
            Load += InfoPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label NameLabel;
        private Label label1;
        private TextBox numberBox;
        private TextBox operatorBox;
        private Label label2;
        private TextBox ImeiBox;
        private Label label3;
        private Button acceptButton;
        private Button updateButton;
    }
}
