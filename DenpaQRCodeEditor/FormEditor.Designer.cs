namespace PushmoLevelEditor
{
    partial class FormEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditor));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newDenpaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.openQRCodeToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveQRCodeToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedInterfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep0 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.label13 = new System.Windows.Forms.Label();
            this.cboGlasses = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboNose = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboEyes = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboHairColor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tbEditor = new System.Windows.Forms.ToolStrip();
            this.cboColor = new System.Windows.Forms.ComboBox();
            this.cboHeadShape = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hexBox1 = new Be.Windows.Forms.HexBox();
            this.picBox2 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnRandomDenpa = new System.Windows.Forms.Button();
            this.cboRegion = new System.Windows.Forms.ComboBox();
            this.nudStats = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cboCheeks = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboMouth = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboFaceColor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnChangeID = new System.Windows.Forms.Button();
            this.cboEyeBrows = new System.Windows.Forms.ComboBox();
            this.btnSwitchPicBox = new System.Windows.Forms.Button();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboFaceShapeHairStyle = new System.Windows.Forms.ComboBox();
            this.cboAntennaPower = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbEditorSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stripColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.stripPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwCheckForUpdates = new System.ComponentModel.BackgroundWorker();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.newDenpaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cboStats = new System.Windows.Forms.ComboBox();
            this.StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(786, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDenpaToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.openQRCodeToolStripMenuItem2,
            this.saveQRCodeToolStripMenuItem2,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // newDenpaToolStripMenuItem1
            // 
            this.newDenpaToolStripMenuItem1.Image = global::PushmoLevelEditor.Properties.Resources.page_white;
            this.newDenpaToolStripMenuItem1.Name = "newDenpaToolStripMenuItem1";
            this.newDenpaToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newDenpaToolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
            this.newDenpaToolStripMenuItem1.Text = "&New Denpa";
            this.newDenpaToolStripMenuItem1.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(193, 6);
            // 
            // openQRCodeToolStripMenuItem2
            // 
            this.openQRCodeToolStripMenuItem2.Image = global::PushmoLevelEditor.Properties.Resources.folder_picture;
            this.openQRCodeToolStripMenuItem2.Name = "openQRCodeToolStripMenuItem2";
            this.openQRCodeToolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.openQRCodeToolStripMenuItem2.Size = new System.Drawing.Size(196, 22);
            this.openQRCodeToolStripMenuItem2.Text = "Open &QR Code";
            this.openQRCodeToolStripMenuItem2.Click += new System.EventHandler(this.openQRCodeToolStripMenuItem_Click);
            // 
            // saveQRCodeToolStripMenuItem2
            // 
            this.saveQRCodeToolStripMenuItem2.Image = global::PushmoLevelEditor.Properties.Resources.barcode2d;
            this.saveQRCodeToolStripMenuItem2.Name = "saveQRCodeToolStripMenuItem2";
            this.saveQRCodeToolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveQRCodeToolStripMenuItem2.Size = new System.Drawing.Size(196, 22);
            this.saveQRCodeToolStripMenuItem2.Text = "&Save QR Code";
            this.saveQRCodeToolStripMenuItem2.Click += new System.EventHandler(this.saveQRCodeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(193, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::PushmoLevelEditor.Properties.Resources.door_in;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advancedInterfaceToolStripMenuItem,
            this.toolStripMenuItem4,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // advancedInterfaceToolStripMenuItem
            // 
            this.advancedInterfaceToolStripMenuItem.Name = "advancedInterfaceToolStripMenuItem";
            this.advancedInterfaceToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.advancedInterfaceToolStripMenuItem.Text = "Advanced Interface";
            this.advancedInterfaceToolStripMenuItem.Click += new System.EventHandler(this.advancedInterfaceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(173, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuFile
            // 
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            // 
            // menuFileSep0
            // 
            this.menuFileSep0.Name = "menuFileSep0";
            this.menuFileSep0.Size = new System.Drawing.Size(150, 6);
            // 
            // menuFileSep1
            // 
            this.menuFileSep1.Name = "menuFileSep1";
            this.menuFileSep1.Size = new System.Drawing.Size(150, 6);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer.Panel1.Controls.Add(this.label13);
            this.splitContainer.Panel1.Controls.Add(this.cboGlasses);
            this.splitContainer.Panel1.Controls.Add(this.label9);
            this.splitContainer.Panel1.Controls.Add(this.cboNose);
            this.splitContainer.Panel1.Controls.Add(this.label7);
            this.splitContainer.Panel1.Controls.Add(this.cboEyes);
            this.splitContainer.Panel1.Controls.Add(this.label6);
            this.splitContainer.Panel1.Controls.Add(this.cboHairColor);
            this.splitContainer.Panel1.Controls.Add(this.label1);
            this.splitContainer.Panel1.Controls.Add(this.label4);
            this.splitContainer.Panel1.Controls.Add(this.txtName);
            this.splitContainer.Panel1.Controls.Add(this.tbEditor);
            this.splitContainer.Panel1.Controls.Add(this.cboColor);
            this.splitContainer.Panel1.Controls.Add(this.cboHeadShape);
            this.splitContainer.Panel1.Controls.Add(this.label2);
            this.splitContainer.Panel1.Controls.Add(this.hexBox1);
            this.splitContainer.Panel1.Controls.Add(this.picBox2);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.label15);
            this.splitContainer.Panel2.Controls.Add(this.btnRandomDenpa);
            this.splitContainer.Panel2.Controls.Add(this.cboRegion);
            this.splitContainer.Panel2.Controls.Add(this.label14);
            this.splitContainer.Panel2.Controls.Add(this.label12);
            this.splitContainer.Panel2.Controls.Add(this.cboCheeks);
            this.splitContainer.Panel2.Controls.Add(this.label11);
            this.splitContainer.Panel2.Controls.Add(this.cboMouth);
            this.splitContainer.Panel2.Controls.Add(this.label10);
            this.splitContainer.Panel2.Controls.Add(this.cboFaceColor);
            this.splitContainer.Panel2.Controls.Add(this.label8);
            this.splitContainer.Panel2.Controls.Add(this.btnChangeID);
            this.splitContainer.Panel2.Controls.Add(this.cboEyeBrows);
            this.splitContainer.Panel2.Controls.Add(this.btnSwitchPicBox);
            this.splitContainer.Panel2.Controls.Add(this.picBox);
            this.splitContainer.Panel2.Controls.Add(this.label5);
            this.splitContainer.Panel2.Controls.Add(this.cboFaceShapeHairStyle);
            this.splitContainer.Panel2.Controls.Add(this.cboAntennaPower);
            this.splitContainer.Panel2.Controls.Add(this.label3);
            this.splitContainer.Panel2.Controls.Add(this.nudStats);
            this.splitContainer.Panel2.Controls.Add(this.cboStats);
            this.splitContainer.Size = new System.Drawing.Size(786, 454);
            this.splitContainer.SplitterDistance = 432;
            this.splitContainer.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(59, 369);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Glasses:";
            // 
            // cboGlasses
            // 
            this.cboGlasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGlasses.FormattingEnabled = true;
            this.cboGlasses.Items.AddRange(new object[] {
            "No Glasses",
            "Green Glasses",
            "Pink Glasses",
            "Sun Glasses",
            "Yellow Glasses",
            "Orange Glasses",
            "Hero Mask 1",
            "Brown Glasses",
            "Blue Glasses",
            "Hero Mask 2",
            "Normal Glasses",
            "Hero Mask 3",
            "Goggles",
            "Bug Eyes",
            "Alien Eyes",
            "Heart Glasses"});
            this.cboGlasses.Location = new System.Drawing.Point(112, 366);
            this.cboGlasses.Name = "cboGlasses";
            this.cboGlasses.Size = new System.Drawing.Size(311, 21);
            this.cboGlasses.TabIndex = 27;
            this.cboGlasses.SelectedIndexChanged += new System.EventHandler(this.cboGlasses_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(71, 342);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Nose:";
            // 
            // cboNose
            // 
            this.cboNose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNose.FormattingEnabled = true;
            this.cboNose.Items.AddRange(new object[] {
            "Nose_1",
            "Nose_2",
            "Nose_3",
            "Nose_4",
            "Nose_5",
            "Nose_6",
            "Nose_7",
            "Nose_8",
            "Nose_9",
            "Nose_10",
            "Nose_11",
            "Nose_12",
            "Nose_13",
            "Nose_14",
            "Nose_15",
            "Nose_16"});
            this.cboNose.Location = new System.Drawing.Point(112, 339);
            this.cboNose.Name = "cboNose";
            this.cboNose.Size = new System.Drawing.Size(311, 21);
            this.cboNose.TabIndex = 25;
            this.cboNose.SelectedIndexChanged += new System.EventHandler(this.cboNose_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(73, 315);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Eyes:";
            // 
            // cboEyes
            // 
            this.cboEyes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEyes.FormattingEnabled = true;
            this.cboEyes.Items.AddRange(new object[] {
            "Eyes_1",
            "Eyes_2",
            "Eyes_3",
            "Eyes_4",
            "Eyes_5",
            "Eyes_6",
            "Eyes_7",
            "Eyes_8",
            "Eyes_9",
            "Eyes_10",
            "Eyes_11",
            "Eyes_12",
            "Eyes_13",
            "Eyes_14",
            "Eyes_15",
            "Eyes_16",
            "Eyes_17",
            "Eyes_18",
            "Eyes_19",
            "Eyes_20",
            "Eyes_21",
            "Eyes_22",
            "Eyes_23",
            "Eyes_24",
            "Eyes_25",
            "Eyes_26",
            "Eyes_27",
            "Eyes_28",
            "Eyes_29",
            "Eyes_30",
            "Eyes_31",
            "Eyes_32"});
            this.cboEyes.Location = new System.Drawing.Point(112, 312);
            this.cboEyes.Name = "cboEyes";
            this.cboEyes.Size = new System.Drawing.Size(311, 21);
            this.cboEyes.TabIndex = 21;
            this.cboEyes.SelectedIndexChanged += new System.EventHandler(this.cboEyes_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Hair Color:";
            // 
            // cboHairColor
            // 
            this.cboHairColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHairColor.FormattingEnabled = true;
            this.cboHairColor.Items.AddRange(new object[] {
            "Grey",
            "White",
            "Brown",
            "Tan",
            "Maroon",
            "Yellow",
            "Blue",
            "Green",
            "Purple",
            "Pink",
            "Orange",
            "Red"});
            this.cboHairColor.Location = new System.Drawing.Point(112, 285);
            this.cboHairColor.Name = "cboHairColor";
            this.cboHairColor.Size = new System.Drawing.Size(311, 21);
            this.cboHairColor.TabIndex = 19;
            this.cboHairColor.SelectedIndexChanged += new System.EventHandler(this.cboHairColor_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 396);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Head Shape:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(112, 393);
            this.txtName.MaxLength = 12;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(311, 20);
            this.txtName.TabIndex = 7;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // tbEditor
            // 
            this.tbEditor.AutoSize = false;
            this.tbEditor.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbEditor.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tbEditor.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tbEditor.Location = new System.Drawing.Point(0, 228);
            this.tbEditor.Name = "tbEditor";
            this.tbEditor.Size = new System.Drawing.Size(32, 226);
            this.tbEditor.TabIndex = 4;
            // 
            // cboColor
            // 
            this.cboColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColor.FormattingEnabled = true;
            this.cboColor.Items.AddRange(new object[] {
            "Black",
            "White ",
            "Red ",
            "Blue",
            "Cyan ",
            "Orange",
            "Green",
            "Red/Blue",
            "Red/Cyan",
            "Red/Orange",
            "Red/Green",
            "Red/White",
            "Blue/Cyan",
            "Blue/Orange",
            "Blue/Green",
            "Blue/White",
            "Cyan/Orange",
            "Cyan/Green",
            "Cyan/White",
            "Orange/Green",
            "Orange/White",
            "Green/White"});
            this.cboColor.Location = new System.Drawing.Point(112, 231);
            this.cboColor.Name = "cboColor";
            this.cboColor.Size = new System.Drawing.Size(311, 21);
            this.cboColor.TabIndex = 5;
            this.cboColor.SelectedIndexChanged += new System.EventHandler(this.cboColor_SelectedIndexChanged);
            // 
            // cboHeadShape
            // 
            this.cboHeadShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHeadShape.FormattingEnabled = true;
            this.cboHeadShape.Items.AddRange(new object[] {
            "Head_0_0",
            "Head_0_1",
            "Head_0_2",
            "Head_0_3",
            "Head_0_4",
            "Head_0_5",
            "Head_0_6",
            "Head_0_7",
            "Head_0_8",
            "Head_0_9",
            "Head_0_A",
            "Head_1_0",
            "Head_1_1",
            "Head_1_2",
            "Head_1_3",
            "Head_1_4",
            "Head_1_5",
            "Head_1_6",
            "Head_2_0",
            "Head_2_1",
            "Head_2_2",
            "Head_2_3",
            "Head_2_4",
            "Head_2_5"});
            this.cboHeadShape.Location = new System.Drawing.Point(112, 258);
            this.cboHeadShape.Name = "cboHeadShape";
            this.cboHeadShape.Size = new System.Drawing.Size(311, 21);
            this.cboHeadShape.TabIndex = 11;
            this.cboHeadShape.SelectedIndexChanged += new System.EventHandler(this.cboHeadShape_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Color:";
            // 
            // hexBox1
            // 
            this.hexBox1.BoldFont = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox1.BytesPerLine = 8;
            this.hexBox1.ColumnInfoVisible = true;
            this.hexBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hexBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox1.GroupSeparatorVisible = true;
            this.hexBox1.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox1.LineInfoVisible = true;
            this.hexBox1.Location = new System.Drawing.Point(0, 0);
            this.hexBox1.Name = "hexBox1";
            this.hexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox1.Size = new System.Drawing.Size(432, 228);
            this.hexBox1.StringViewVisible = true;
            this.hexBox1.TabIndex = 5;
            this.hexBox1.UseFixedBytesPerLine = true;
            this.hexBox1.Visible = false;
            this.hexBox1.VScrollBarVisible = true;
            this.hexBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hexBox1_KeyPress);
            this.hexBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.hexBox1_KeyUp);
            // 
            // picBox2
            // 
            this.picBox2.Location = new System.Drawing.Point(223, 0);
            this.picBox2.Name = "picBox2";
            this.picBox2.Size = new System.Drawing.Size(200, 200);
            this.picBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBox2.TabIndex = 37;
            this.picBox2.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 207);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "Denpa Men Region:";
            // 
            // btnRandomDenpa
            // 
            this.btnRandomDenpa.Location = new System.Drawing.Point(207, 32);
            this.btnRandomDenpa.Name = "btnRandomDenpa";
            this.btnRandomDenpa.Size = new System.Drawing.Size(81, 23);
            this.btnRandomDenpa.TabIndex = 36;
            this.btnRandomDenpa.Text = "Random";
            this.btnRandomDenpa.UseVisualStyleBackColor = true;
            this.btnRandomDenpa.Click += new System.EventHandler(this.btnRandomDenpa_Click);
            // 
            // cboRegion
            // 
            this.cboRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRegion.FormattingEnabled = true;
            this.cboRegion.Items.AddRange(new object[] {
            "North America",
            "Japan"});
            this.cboRegion.Location = new System.Drawing.Point(127, 204);
            this.cboRegion.Name = "cboRegion";
            this.cboRegion.Size = new System.Drawing.Size(223, 21);
            this.cboRegion.TabIndex = 37;
            this.cboRegion.SelectedIndexChanged += new System.EventHandler(this.cboRegion_SelectedIndexChanged);
            // 
            // nudStats
            // 
            this.nudStats.Location = new System.Drawing.Point(40, 394);
            this.nudStats.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.nudStats.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudStats.Name = "nudStats";
            this.nudStats.Size = new System.Drawing.Size(42, 20);
            this.nudStats.TabIndex = 35;
            this.nudStats.Value = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.nudStats.ValueChanged += new System.EventHandler(this.nudStats_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 396);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "Stats";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(75, 369);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 32;
            this.label12.Text = "Cheeks:";
            // 
            // cboCheeks
            // 
            this.cboCheeks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCheeks.FormattingEnabled = true;
            this.cboCheeks.Items.AddRange(new object[] {
            "No Cheeks",
            "Small Cheeks",
            "Spiral Cheeks",
            "Freckles",
            "Large Cheeks",
            "Round Cheeks",
            "Whiskers 1",
            "Whiskers 2"});
            this.cboCheeks.Location = new System.Drawing.Point(127, 366);
            this.cboCheeks.Name = "cboCheeks";
            this.cboCheeks.Size = new System.Drawing.Size(223, 21);
            this.cboCheeks.TabIndex = 31;
            this.cboCheeks.SelectedIndexChanged += new System.EventHandler(this.cboCheeks_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(81, 342);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Mouth:";
            // 
            // cboMouth
            // 
            this.cboMouth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMouth.FormattingEnabled = true;
            this.cboMouth.Items.AddRange(new object[] {
            "Mouth_1",
            "Mouth_2",
            "Mouth_3",
            "Mouth_4",
            "Mouth_5",
            "Mouth_6",
            "Mouth_7",
            "Mouth_8",
            "Mouth_9",
            "Mouth_10",
            "Mouth_11",
            "Mouth_12",
            "Mouth_13",
            "Mouth_14",
            "Mouth_15",
            "Mouth_16",
            "Mouth_17",
            "Mouth_18",
            "Mouth_19",
            "Mouth_20",
            "Mouth_21",
            "Mouth_22",
            "Mouth_23",
            "Mouth_24",
            "Mouth_25",
            "Mouth_26",
            "Mouth_27",
            "Mouth_28",
            "Mouth_29",
            "Mouth_30",
            "Mouth_31",
            "Mouth_32"});
            this.cboMouth.Location = new System.Drawing.Point(127, 339);
            this.cboMouth.Name = "cboMouth";
            this.cboMouth.Size = new System.Drawing.Size(223, 21);
            this.cboMouth.TabIndex = 29;
            this.cboMouth.SelectedIndexChanged += new System.EventHandler(this.cboMouth_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(60, 288);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Face Color:";
            // 
            // cboFaceColor
            // 
            this.cboFaceColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFaceColor.FormattingEnabled = true;
            this.cboFaceColor.Items.AddRange(new object[] {
            "****** Lightest",
            "*****",
            "****",
            "***",
            "**",
            "* Darkest"});
            this.cboFaceColor.Location = new System.Drawing.Point(127, 285);
            this.cboFaceColor.Name = "cboFaceColor";
            this.cboFaceColor.Size = new System.Drawing.Size(223, 21);
            this.cboFaceColor.TabIndex = 27;
            this.cboFaceColor.SelectedIndexChanged += new System.EventHandler(this.cboFaceColor_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 315);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Eye Brows:";
            // 
            // btnChangeID
            // 
            this.btnChangeID.Location = new System.Drawing.Point(207, 61);
            this.btnChangeID.Name = "btnChangeID";
            this.btnChangeID.Size = new System.Drawing.Size(81, 23);
            this.btnChangeID.TabIndex = 17;
            this.btnChangeID.Text = "Change ID";
            this.btnChangeID.UseVisualStyleBackColor = true;
            this.btnChangeID.Click += new System.EventHandler(this.btnChangeID_Click);
            // 
            // cboEyeBrows
            // 
            this.cboEyeBrows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEyeBrows.FormattingEnabled = true;
            this.cboEyeBrows.Items.AddRange(new object[] {
            "Eye_Brows_1",
            "Eye_Brows_2",
            "Eye_Brows_3",
            "Eye_Brows_4",
            "Eye_Brows_5",
            "Eye_Brows_6",
            "Eye_Brows_7",
            "Eye_Brows_8"});
            this.cboEyeBrows.Location = new System.Drawing.Point(127, 312);
            this.cboEyeBrows.Name = "cboEyeBrows";
            this.cboEyeBrows.Size = new System.Drawing.Size(223, 21);
            this.cboEyeBrows.TabIndex = 23;
            this.cboEyeBrows.SelectedIndexChanged += new System.EventHandler(this.cboEyeBrows_SelectedIndexChanged);
            // 
            // btnSwitchPicBox
            // 
            this.btnSwitchPicBox.Location = new System.Drawing.Point(207, 3);
            this.btnSwitchPicBox.Name = "btnSwitchPicBox";
            this.btnSwitchPicBox.Size = new System.Drawing.Size(81, 23);
            this.btnSwitchPicBox.TabIndex = 8;
            this.btnSwitchPicBox.Text = "Feature";
            this.btnSwitchPicBox.UseVisualStyleBackColor = true;
            this.btnSwitchPicBox.Visible = false;
            this.btnSwitchPicBox.Click += new System.EventHandler(this.btnSwitchPicBox_Click);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(1, 0);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(200, 200);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox.TabIndex = 4;
            this.picBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Face Shape/Hair Style:";
            // 
            // cboFaceShapeHairStyle
            // 
            this.cboFaceShapeHairStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFaceShapeHairStyle.FormattingEnabled = true;
            this.cboFaceShapeHairStyle.Items.AddRange(new object[] {
            "face_shape_0",
            "face_shape_1",
            "face_shape_2",
            "face_shape_3",
            "face_shape_4",
            "face_shape_5",
            "face_shape_6",
            "face_shape_7",
            "face_shape_8",
            "hair_style_0",
            "hair_style_1",
            "hair_style_2",
            "hair_style_3",
            "hair_style_4",
            "hair_style_5",
            "hair_style_6",
            "hair_style_7",
            "hair_style_8",
            "hair_style_9",
            "hair_style_10",
            "hair_style_11",
            "hair_style_12",
            "hair_style_13",
            "hair_style_14",
            "hair_style_15",
            "hair_style_16",
            "hair_style_17",
            "hair_style_18",
            "hair_style_19",
            "hair_style_20",
            "hair_style_21",
            "hair_style_22"});
            this.cboFaceShapeHairStyle.Location = new System.Drawing.Point(127, 258);
            this.cboFaceShapeHairStyle.Name = "cboFaceShapeHairStyle";
            this.cboFaceShapeHairStyle.Size = new System.Drawing.Size(223, 21);
            this.cboFaceShapeHairStyle.TabIndex = 18;
            this.cboFaceShapeHairStyle.SelectedIndexChanged += new System.EventHandler(this.cboFaceShapeHairStyle_SelectedIndexChanged);
            // 
            // cboAntennaPower
            // 
            this.cboAntennaPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAntennaPower.FormattingEnabled = true;
            this.cboAntennaPower.Items.AddRange(new object[] {
            "No Antenna",
            "Recovery (single)",
            "Revival (Single)",
            "Cure: Poison",
            "Cure: Burn",
            "Cure: Paralysis",
            "Cure: Frozen",
            "Attack: Fire",
            "Attack: Water",
            "Attack: Ice",
            "Attack: Earth",
            "Attack: Wind",
            "Attack: Light",
            "Recovery (All)",
            "Revival (All)",
            "Bonus: Always Treasure",
            "Bonus: Double Gold",
            "Buff: Invincible",
            "Debuff: Fatal",
            "Attack: Fire (All)",
            "Attack: Water (All)",
            "Attack: Ice (All)",
            "Attack: Earth (All)",
            "Attack: Wind (All)",
            "Attack: Light (All)",
            "Cure: Sleep",
            "Cure: Blind",
            "Buff: Excitement",
            "Buff: Attack (All)",
            "Buff: Defense (All)",
            "Buff: Speed (All)",
            "Debuff: Attack (All)",
            "Buff: Evasion (All)",
            "Debuff: Defense (All)",
            "Debuff: Speed (All)",
            "Buff: Attack",
            "Buff: Defense",
            "Buff: Speed",
            "Buff: Evasion",
            "Debuff: Poison",
            "Debuff: Sleep",
            "Debuff: Paralysis",
            "Debuff: Blind",
            "Debuff: Attack",
            "Debuff: Defense",
            "Debuff: Speed"});
            this.cboAntennaPower.Location = new System.Drawing.Point(127, 231);
            this.cboAntennaPower.Name = "cboAntennaPower";
            this.cboAntennaPower.Size = new System.Drawing.Size(223, 21);
            this.cboAntennaPower.TabIndex = 6;
            this.cboAntennaPower.SelectedIndexChanged += new System.EventHandler(this.cboAntennaPower_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Antenna Power:";
            // 
            // tbEditorSep1
            // 
            this.tbEditorSep1.Name = "tbEditorSep1";
            this.tbEditorSep1.Size = new System.Drawing.Size(30, 6);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 456);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(786, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 3;
            // 
            // stripColor
            // 
            this.stripColor.Name = "stripColor";
            this.stripColor.Size = new System.Drawing.Size(0, 17);
            // 
            // stripPosition
            // 
            this.stripPosition.Name = "stripPosition";
            this.stripPosition.Size = new System.Drawing.Size(581, 17);
            this.stripPosition.Spring = true;
            this.stripPosition.Text = "Position: (0,0)";
            this.stripPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bwCheckForUpdates
            // 
            this.bwCheckForUpdates.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCheckForUpdates_DoWork);
            this.bwCheckForUpdates.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCheckForUpdates_RunWorkerCompleted);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // newDenpaToolStripMenuItem
            // 
            this.newDenpaToolStripMenuItem.Name = "newDenpaToolStripMenuItem";
            this.newDenpaToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.newDenpaToolStripMenuItem.Text = "&New Denpa";
            this.newDenpaToolStripMenuItem.Click += new System.EventHandler(this.newDenpaToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // cboStats
            // 
            this.cboStats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStats.FormattingEnabled = true;
            this.cboStats.Location = new System.Drawing.Point(88, 393);
            this.cboStats.Name = "cboStats";
            this.cboStats.Size = new System.Drawing.Size(262, 21);
            this.cboStats.TabIndex = 39;
            this.cboStats.SelectedIndexChanged += new System.EventHandler(this.cboStats_SelectedIndexChanged);
            // 
            // StatusStripLabel
            // 
            this.StatusStripLabel.Name = "StatusStripLabel";
            this.StatusStripLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 478);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(796, 510);
            this.MinimumSize = new System.Drawing.Size(796, 510);
            this.Name = "FormEditor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Denpa QR Editor";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripSeparator menuFileSep0;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel stripPosition;
        private System.Windows.Forms.ToolStripStatusLabel stripColor;
        private System.Windows.Forms.ToolStrip tbEditor;
        private System.Windows.Forms.ToolStripSeparator tbEditorSep1;
        private System.Windows.Forms.ToolStripSeparator menuFileSep1;
        private System.ComponentModel.BackgroundWorker bwCheckForUpdates;
        private Be.Windows.Forms.HexBox hexBox1;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.ComboBox cboColor;
        private System.Windows.Forms.ComboBox cboAntennaPower;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSwitchPicBox;
        private System.Windows.Forms.ComboBox cboHeadShape;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem newDenpaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newDenpaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem openQRCodeToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveQRCodeToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btnChangeID;
        private System.Windows.Forms.ComboBox cboFaceShapeHairStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboHairColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboEyes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboEyeBrows;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboNose;
        private System.Windows.Forms.ComboBox cboFaceColor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboMouth;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboCheeks;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboGlasses;
        private System.Windows.Forms.NumericUpDown nudStats;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnRandomDenpa;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedInterfaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.PictureBox picBox2;
        private System.Windows.Forms.ComboBox cboRegion;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboStats;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripLabel;
    }
}

