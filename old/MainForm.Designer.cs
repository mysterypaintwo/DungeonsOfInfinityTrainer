namespace Dungeons_Of_Infinity_Trainer
{
    partial class MainForm
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
            this.Activate_Button = new System.Windows.Forms.Button();
            this.NotNeeded1 = new System.Windows.Forms.Label();
            this.processLabel = new System.Windows.Forms.Label();
            this.BGWorker = new System.ComponentModel.BackgroundWorker();
            this.Export = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Close_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.InfiniteHealthCheck = new System.Windows.Forms.CheckBox();
            this.MaxHealthDisplay = new System.Windows.Forms.TextBox();
            this.CurrentHealthDisplay = new System.Windows.Forms.TextBox();
            this.Magic = new System.Windows.Forms.Label();
            this.MagicDisplay = new System.Windows.Forms.TextBox();
            this.RupeesDisplay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.XPosDisplay = new System.Windows.Forms.Label();
            this.YPosDisplay = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Activate_Button
            // 
            this.Activate_Button.Location = new System.Drawing.Point(613, 51);
            this.Activate_Button.Name = "Activate_Button";
            this.Activate_Button.Size = new System.Drawing.Size(75, 28);
            this.Activate_Button.TabIndex = 0;
            this.Activate_Button.Text = "Activate";
            this.Activate_Button.UseVisualStyleBackColor = true;
            this.Activate_Button.Click += new System.EventHandler(this.Activate_Button_Click);
            // 
            // NotNeeded1
            // 
            this.NotNeeded1.AutoSize = true;
            this.NotNeeded1.Location = new System.Drawing.Point(12, 9);
            this.NotNeeded1.Name = "NotNeeded1";
            this.NotNeeded1.Size = new System.Drawing.Size(44, 16);
            this.NotNeeded1.TabIndex = 1;
            this.NotNeeded1.Text = "Status:";
            // 
            // processLabel
            // 
            this.processLabel.AutoSize = true;
            this.processLabel.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite;
            this.processLabel.Location = new System.Drawing.Point(62, 9);
            this.processLabel.Name = "processLabel";
            this.processLabel.Size = new System.Drawing.Size(26, 16);
            this.processLabel.TabIndex = 2;
            this.processLabel.Text = "N/A";
            // 
            // BGWorker
            // 
            this.BGWorker.WorkerReportsProgress = true;
            this.BGWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGWorker_DoWork);
            this.BGWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BGWorker_ProgressChanged);
            this.BGWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGWorker_RunWorkerCompleted);
            // 
            // Export
            // 
            this.Export.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Export.Location = new System.Drawing.Point(547, 412);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(153, 31);
            this.Export.TabIndex = 3;
            this.Export.Text = "Export Cheat Engine.CT";
            this.Export.UseVisualStyleBackColor = false;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(196, 430);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.NotNeeded1);
            this.panel2.Controls.Add(this.processLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(194, 37);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 467);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(704, 127);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.Close_Button);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(704, 37);
            this.panel4.TabIndex = 6;
            this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDown);
            this.panel4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove);
            // 
            // Close_Button
            // 
            this.Close_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(70)))));
            this.Close_Button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(70)))));
            this.Close_Button.FlatAppearance.BorderSize = 0;
            this.Close_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Close_Button.ForeColor = System.Drawing.Color.Gray;
            this.Close_Button.Location = new System.Drawing.Point(669, 4);
            this.Close_Button.Name = "Close_Button";
            this.Close_Button.Size = new System.Drawing.Size(29, 29);
            this.Close_Button.TabIndex = 7;
            this.Close_Button.Text = "X";
            this.Close_Button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Close_Button.UseVisualStyleBackColor = false;
            this.Close_Button.Click += new System.EventHandler(this.Close_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(268, 7);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.label1.Size = new System.Drawing.Size(159, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dungeons of Infinity Trainer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(208, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Player Information";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(208, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "MaxHealth:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "CurrentHealth:";
            // 
            // InfiniteHealthCheck
            // 
            this.InfiniteHealthCheck.AutoSize = true;
            this.InfiniteHealthCheck.Location = new System.Drawing.Point(211, 141);
            this.InfiniteHealthCheck.Name = "InfiniteHealthCheck";
            this.InfiniteHealthCheck.Size = new System.Drawing.Size(131, 20);
            this.InfiniteHealthCheck.TabIndex = 10;
            this.InfiniteHealthCheck.Text = "Infinite Player Stats";
            this.InfiniteHealthCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.InfiniteHealthCheck.UseVisualStyleBackColor = true;
            this.InfiniteHealthCheck.CheckedChanged += new System.EventHandler(this.InfiniteHealthCheck_CheckedChanged);
            // 
            // MaxHealthDisplay
            // 
            this.MaxHealthDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.MaxHealthDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MaxHealthDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.MaxHealthDisplay.Location = new System.Drawing.Point(294, 71);
            this.MaxHealthDisplay.Name = "MaxHealthDisplay";
            this.MaxHealthDisplay.Size = new System.Drawing.Size(29, 16);
            this.MaxHealthDisplay.TabIndex = 13;
            this.MaxHealthDisplay.Text = "N/A";
            this.MaxHealthDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaxHealthDisplay.Enter += new System.EventHandler(this.Editing);
            this.MaxHealthDisplay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaxHealthConfirm);
            this.MaxHealthDisplay.Validated += new System.EventHandler(this.DoneEditing);
            // 
            // CurrentHealthDisplay
            // 
            this.CurrentHealthDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.CurrentHealthDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CurrentHealthDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.CurrentHealthDisplay.Location = new System.Drawing.Point(294, 87);
            this.CurrentHealthDisplay.Name = "CurrentHealthDisplay";
            this.CurrentHealthDisplay.Size = new System.Drawing.Size(29, 16);
            this.CurrentHealthDisplay.TabIndex = 14;
            this.CurrentHealthDisplay.Text = "N/A";
            this.CurrentHealthDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CurrentHealthDisplay.Enter += new System.EventHandler(this.Editing);
            this.CurrentHealthDisplay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CurrentHealthConfirm);
            this.CurrentHealthDisplay.Validated += new System.EventHandler(this.DoneEditing);
            // 
            // Magic
            // 
            this.Magic.AutoSize = true;
            this.Magic.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Magic.Location = new System.Drawing.Point(208, 103);
            this.Magic.Name = "Magic";
            this.Magic.Size = new System.Drawing.Size(44, 16);
            this.Magic.TabIndex = 15;
            this.Magic.Text = "Magic:";
            // 
            // MagicDisplay
            // 
            this.MagicDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.MagicDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MagicDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.MagicDisplay.Location = new System.Drawing.Point(294, 103);
            this.MagicDisplay.Name = "MagicDisplay";
            this.MagicDisplay.Size = new System.Drawing.Size(29, 16);
            this.MagicDisplay.TabIndex = 16;
            this.MagicDisplay.Text = "N/A";
            this.MagicDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MagicDisplay.Enter += new System.EventHandler(this.Editing);
            this.MagicDisplay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MagicConfirm);
            this.MagicDisplay.Validated += new System.EventHandler(this.DoneEditing);
            // 
            // RupeesDisplay
            // 
            this.RupeesDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.RupeesDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RupeesDisplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.RupeesDisplay.Location = new System.Drawing.Point(294, 119);
            this.RupeesDisplay.Name = "RupeesDisplay";
            this.RupeesDisplay.Size = new System.Drawing.Size(29, 16);
            this.RupeesDisplay.TabIndex = 18;
            this.RupeesDisplay.Text = "N/A";
            this.RupeesDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RupeesDisplay.Enter += new System.EventHandler(this.Editing);
            this.RupeesDisplay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RupeesConfirm);
            this.RupeesDisplay.Validated += new System.EventHandler(this.DoneEditing);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(208, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Rupees:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(374, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Player Coords";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(345, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "X_Pos:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(433, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Y_Pos:";
            // 
            // XPosDisplay
            // 
            this.XPosDisplay.AutoSize = true;
            this.XPosDisplay.Location = new System.Drawing.Point(388, 74);
            this.XPosDisplay.Name = "XPosDisplay";
            this.XPosDisplay.Size = new System.Drawing.Size(35, 16);
            this.XPosDisplay.TabIndex = 22;
            this.XPosDisplay.Text = "0000";
            // 
            // YPosDisplay
            // 
            this.YPosDisplay.AutoSize = true;
            this.YPosDisplay.Location = new System.Drawing.Point(474, 74);
            this.YPosDisplay.Name = "YPosDisplay";
            this.YPosDisplay.Size = new System.Drawing.Size(35, 16);
            this.YPosDisplay.TabIndex = 23;
            this.YPosDisplay.Text = "0000";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(704, 594);
            this.Controls.Add(this.YPosDisplay);
            this.Controls.Add(this.XPosDisplay);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RupeesDisplay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MagicDisplay);
            this.Controls.Add(this.Magic);
            this.Controls.Add(this.CurrentHealthDisplay);
            this.Controls.Add(this.MaxHealthDisplay);
            this.Controls.Add(this.InfiniteHealthCheck);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Export);
            this.Controls.Add(this.Activate_Button);
            this.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(120)))), ((int)(((byte)(138)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Dungeons of Infinity Trainer v0.0.1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Activate_Button;
        private System.Windows.Forms.Label NotNeeded1;
        private System.Windows.Forms.Label processLabel;
        private System.ComponentModel.BackgroundWorker BGWorker;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Close_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox InfiniteHealthCheck;
        private System.Windows.Forms.TextBox MaxHealthDisplay;
        private System.Windows.Forms.TextBox CurrentHealthDisplay;
        private System.Windows.Forms.Label Magic;
        private System.Windows.Forms.TextBox MagicDisplay;
        private System.Windows.Forms.TextBox RupeesDisplay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label XPosDisplay;
        private System.Windows.Forms.Label YPosDisplay;
    }
}

