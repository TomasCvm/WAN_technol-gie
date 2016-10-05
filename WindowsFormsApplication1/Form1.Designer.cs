using System.ComponentModel;
using System.Windows.Forms;

namespace xcicman_zadanie__WAN
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.start = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ResetStat = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staticRoutingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rIPDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetMAC = new System.Windows.Forms.Button();
            this.IP1_1 = new System.Windows.Forms.TextBox();
            this.IP1_2 = new System.Windows.Forms.TextBox();
            this.IP1_4 = new System.Windows.Forms.TextBox();
            this.IP1_3 = new System.Windows.Forms.TextBox();
            this.IP2_4 = new System.Windows.Forms.TextBox();
            this.IP2_3 = new System.Windows.Forms.TextBox();
            this.IP2_2 = new System.Windows.Forms.TextBox();
            this.IP2_1 = new System.Windows.Forms.TextBox();
            this.MASK2_4 = new System.Windows.Forms.TextBox();
            this.MASK2_3 = new System.Windows.Forms.TextBox();
            this.MASK2_2 = new System.Windows.Forms.TextBox();
            this.MASK2_1 = new System.Windows.Forms.TextBox();
            this.MASK1_4 = new System.Windows.Forms.TextBox();
            this.MASK1_3 = new System.Windows.Forms.TextBox();
            this.MASK1_2 = new System.Windows.Forms.TextBox();
            this.MASK1_1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.pingIP_4 = new System.Windows.Forms.TextBox();
            this.pingIP_3 = new System.Windows.Forms.TextBox();
            this.pingIP_2 = new System.Windows.Forms.TextBox();
            this.pingIP_1 = new System.Windows.Forms.TextBox();
            this.pingGroup = new System.Windows.Forms.GroupBox();
            this.pingBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pingGroup.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Window;
            this.label10.Location = new System.Drawing.Point(9, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 17);
            this.label10.TabIndex = 50;
            this.label10.Text = "Adapter 2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Window;
            this.label9.Location = new System.Drawing.Point(9, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 17);
            this.label9.TabIndex = 49;
            this.label9.Text = "Adapter 1";
            // 
            // comboBox2
            // 
            this.comboBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(87, 94);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(357, 24);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBox1.Location = new System.Drawing.Point(87, 64);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(357, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.ForestGreen;
            this.start.Location = new System.Drawing.Point(1234, 69);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(97, 46);
            this.start.TabIndex = 4;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = false;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(9, 290);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(554, 248);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(9, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(554, 248);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(461, 214);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(11, 21);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(513, 301);
            this.dataGridView2.TabIndex = 1;
            // 
            // ResetStat
            // 
            this.ResetStat.Location = new System.Drawing.Point(427, 328);
            this.ResetStat.Name = "ResetStat";
            this.ResetStat.Size = new System.Drawing.Size(97, 28);
            this.ResetStat.TabIndex = 0;
            this.ResetStat.Text = "Reset";
            this.ResetStat.UseVisualStyleBackColor = true;
            this.ResetStat.Click += new System.EventHandler(this.ResetStat_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1782, 28);
            this.menuStrip1.TabIndex = 76;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filtersToolStripMenuItem,
            this.staticRoutingToolStripMenuItem,
            this.rIPDatabaseToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.BackColor = System.Drawing.SystemColors.HotTrack;
            this.filtersToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // staticRoutingToolStripMenuItem
            // 
            this.staticRoutingToolStripMenuItem.BackColor = System.Drawing.SystemColors.HotTrack;
            this.staticRoutingToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.staticRoutingToolStripMenuItem.Name = "staticRoutingToolStripMenuItem";
            this.staticRoutingToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
            this.staticRoutingToolStripMenuItem.Text = "Routing Table";
            this.staticRoutingToolStripMenuItem.Click += new System.EventHandler(this.staticRoutingToolStripMenuItem_Click);
            // 
            // rIPDatabaseToolStripMenuItem
            // 
            this.rIPDatabaseToolStripMenuItem.BackColor = System.Drawing.SystemColors.HotTrack;
            this.rIPDatabaseToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.rIPDatabaseToolStripMenuItem.Name = "rIPDatabaseToolStripMenuItem";
            this.rIPDatabaseToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
            this.rIPDatabaseToolStripMenuItem.Text = "RIP Database";
            this.rIPDatabaseToolStripMenuItem.Click += new System.EventHandler(this.rIPDatabaseToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.HotTrack;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(175, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // ResetMAC
            // 
            this.ResetMAC.Location = new System.Drawing.Point(370, 244);
            this.ResetMAC.Name = "ResetMAC";
            this.ResetMAC.Size = new System.Drawing.Size(97, 28);
            this.ResetMAC.TabIndex = 0;
            this.ResetMAC.Text = "Reset";
            this.ResetMAC.UseVisualStyleBackColor = true;
            this.ResetMAC.Click += new System.EventHandler(this.ResetARP_Click);
            // 
            // IP1_1
            // 
            this.IP1_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP1_1.Location = new System.Drawing.Point(11, 20);
            this.IP1_1.MaxLength = 3;
            this.IP1_1.Name = "IP1_1";
            this.IP1_1.Size = new System.Drawing.Size(43, 24);
            this.IP1_1.TabIndex = 0;
            this.IP1_1.Text = "10";
            this.IP1_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP1_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // IP1_2
            // 
            this.IP1_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP1_2.Location = new System.Drawing.Point(56, 20);
            this.IP1_2.MaxLength = 3;
            this.IP1_2.Name = "IP1_2";
            this.IP1_2.Size = new System.Drawing.Size(43, 24);
            this.IP1_2.TabIndex = 1;
            this.IP1_2.Text = "10";
            this.IP1_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP1_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // IP1_4
            // 
            this.IP1_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP1_4.Location = new System.Drawing.Point(146, 20);
            this.IP1_4.MaxLength = 3;
            this.IP1_4.Name = "IP1_4";
            this.IP1_4.Size = new System.Drawing.Size(43, 24);
            this.IP1_4.TabIndex = 3;
            this.IP1_4.Text = "6";
            this.IP1_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP1_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // IP1_3
            // 
            this.IP1_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP1_3.Location = new System.Drawing.Point(101, 20);
            this.IP1_3.MaxLength = 3;
            this.IP1_3.Name = "IP1_3";
            this.IP1_3.Size = new System.Drawing.Size(43, 24);
            this.IP1_3.TabIndex = 2;
            this.IP1_3.Text = "10";
            this.IP1_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP1_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // IP2_4
            // 
            this.IP2_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP2_4.Location = new System.Drawing.Point(146, 47);
            this.IP2_4.MaxLength = 3;
            this.IP2_4.Name = "IP2_4";
            this.IP2_4.Size = new System.Drawing.Size(43, 24);
            this.IP2_4.TabIndex = 8;
            this.IP2_4.Text = "7";
            this.IP2_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP2_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // IP2_3
            // 
            this.IP2_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP2_3.Location = new System.Drawing.Point(101, 47);
            this.IP2_3.MaxLength = 3;
            this.IP2_3.Name = "IP2_3";
            this.IP2_3.Size = new System.Drawing.Size(43, 24);
            this.IP2_3.TabIndex = 7;
            this.IP2_3.Text = "20";
            this.IP2_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP2_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // IP2_2
            // 
            this.IP2_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP2_2.Location = new System.Drawing.Point(56, 47);
            this.IP2_2.MaxLength = 3;
            this.IP2_2.Name = "IP2_2";
            this.IP2_2.Size = new System.Drawing.Size(43, 24);
            this.IP2_2.TabIndex = 6;
            this.IP2_2.Text = "20";
            this.IP2_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP2_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // IP2_1
            // 
            this.IP2_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP2_1.Location = new System.Drawing.Point(11, 47);
            this.IP2_1.MaxLength = 3;
            this.IP2_1.Name = "IP2_1";
            this.IP2_1.Size = new System.Drawing.Size(43, 24);
            this.IP2_1.TabIndex = 5;
            this.IP2_1.Text = "20";
            this.IP2_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP2_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // MASK2_4
            // 
            this.MASK2_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MASK2_4.Location = new System.Drawing.Point(145, 48);
            this.MASK2_4.MaxLength = 3;
            this.MASK2_4.Name = "MASK2_4";
            this.MASK2_4.Size = new System.Drawing.Size(43, 24);
            this.MASK2_4.TabIndex = 8;
            this.MASK2_4.Text = "0";
            this.MASK2_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MASK2_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // MASK2_3
            // 
            this.MASK2_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MASK2_3.Location = new System.Drawing.Point(100, 48);
            this.MASK2_3.MaxLength = 3;
            this.MASK2_3.Name = "MASK2_3";
            this.MASK2_3.Size = new System.Drawing.Size(43, 24);
            this.MASK2_3.TabIndex = 7;
            this.MASK2_3.Text = "255";
            this.MASK2_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MASK2_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // MASK2_2
            // 
            this.MASK2_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MASK2_2.Location = new System.Drawing.Point(55, 48);
            this.MASK2_2.MaxLength = 3;
            this.MASK2_2.Name = "MASK2_2";
            this.MASK2_2.Size = new System.Drawing.Size(43, 24);
            this.MASK2_2.TabIndex = 6;
            this.MASK2_2.Text = "255";
            this.MASK2_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MASK2_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // MASK2_1
            // 
            this.MASK2_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MASK2_1.Location = new System.Drawing.Point(10, 48);
            this.MASK2_1.MaxLength = 3;
            this.MASK2_1.Name = "MASK2_1";
            this.MASK2_1.Size = new System.Drawing.Size(43, 24);
            this.MASK2_1.TabIndex = 5;
            this.MASK2_1.Text = "255";
            this.MASK2_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MASK2_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // MASK1_4
            // 
            this.MASK1_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MASK1_4.Location = new System.Drawing.Point(145, 21);
            this.MASK1_4.MaxLength = 3;
            this.MASK1_4.Name = "MASK1_4";
            this.MASK1_4.Size = new System.Drawing.Size(43, 24);
            this.MASK1_4.TabIndex = 3;
            this.MASK1_4.Text = "0";
            this.MASK1_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MASK1_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // MASK1_3
            // 
            this.MASK1_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MASK1_3.Location = new System.Drawing.Point(100, 21);
            this.MASK1_3.MaxLength = 3;
            this.MASK1_3.Name = "MASK1_3";
            this.MASK1_3.Size = new System.Drawing.Size(43, 24);
            this.MASK1_3.TabIndex = 2;
            this.MASK1_3.Text = "255";
            this.MASK1_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MASK1_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // MASK1_2
            // 
            this.MASK1_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MASK1_2.Location = new System.Drawing.Point(55, 21);
            this.MASK1_2.MaxLength = 3;
            this.MASK1_2.Name = "MASK1_2";
            this.MASK1_2.Size = new System.Drawing.Size(43, 24);
            this.MASK1_2.TabIndex = 1;
            this.MASK1_2.Text = "255";
            this.MASK1_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MASK1_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // MASK1_1
            // 
            this.MASK1_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MASK1_1.Location = new System.Drawing.Point(10, 21);
            this.MASK1_1.MaxLength = 3;
            this.MASK1_1.Name = "MASK1_1";
            this.MASK1_1.Size = new System.Drawing.Size(43, 24);
            this.MASK1_1.TabIndex = 0;
            this.MASK1_1.Text = "255";
            this.MASK1_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MASK1_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.IP1_1);
            this.groupBox1.Controls.Add(this.IP1_2);
            this.groupBox1.Controls.Add(this.IP1_3);
            this.groupBox1.Controls.Add(this.IP1_4);
            this.groupBox1.Controls.Add(this.IP2_1);
            this.groupBox1.Controls.Add(this.IP2_2);
            this.groupBox1.Controls.Add(this.IP2_3);
            this.groupBox1.Controls.Add(this.IP2_4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(525, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 83);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(195, 49);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(71, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "SET";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.MASK1_2);
            this.groupBox2.Controls.Add(this.MASK1_1);
            this.groupBox2.Controls.Add(this.MASK1_3);
            this.groupBox2.Controls.Add(this.MASK1_4);
            this.groupBox2.Controls.Add(this.MASK2_4);
            this.groupBox2.Controls.Add(this.MASK2_1);
            this.groupBox2.Controls.Add(this.MASK2_3);
            this.groupBox2.Controls.Add(this.MASK2_2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(803, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 83);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MASK";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(194, 47);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(71, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "SET";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(194, 20);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(71, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "SET";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // pingIP_4
            // 
            this.pingIP_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pingIP_4.Location = new System.Drawing.Point(155, 20);
            this.pingIP_4.MaxLength = 3;
            this.pingIP_4.Name = "pingIP_4";
            this.pingIP_4.Size = new System.Drawing.Size(43, 24);
            this.pingIP_4.TabIndex = 3;
            this.pingIP_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pingIP_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // pingIP_3
            // 
            this.pingIP_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pingIP_3.Location = new System.Drawing.Point(110, 20);
            this.pingIP_3.MaxLength = 3;
            this.pingIP_3.Name = "pingIP_3";
            this.pingIP_3.Size = new System.Drawing.Size(43, 24);
            this.pingIP_3.TabIndex = 2;
            this.pingIP_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pingIP_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // pingIP_2
            // 
            this.pingIP_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pingIP_2.Location = new System.Drawing.Point(65, 20);
            this.pingIP_2.MaxLength = 3;
            this.pingIP_2.Name = "pingIP_2";
            this.pingIP_2.Size = new System.Drawing.Size(43, 24);
            this.pingIP_2.TabIndex = 1;
            this.pingIP_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pingIP_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // pingIP_1
            // 
            this.pingIP_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pingIP_1.Location = new System.Drawing.Point(20, 20);
            this.pingIP_1.MaxLength = 3;
            this.pingIP_1.Name = "pingIP_1";
            this.pingIP_1.Size = new System.Drawing.Size(43, 24);
            this.pingIP_1.TabIndex = 0;
            this.pingIP_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pingIP_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // pingGroup
            // 
            this.pingGroup.Controls.Add(this.pingIP_4);
            this.pingGroup.Controls.Add(this.pingIP_3);
            this.pingGroup.Controls.Add(this.pingBtn);
            this.pingGroup.Controls.Add(this.pingIP_2);
            this.pingGroup.Controls.Add(this.pingIP_1);
            this.pingGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pingGroup.Location = new System.Drawing.Point(1322, 529);
            this.pingGroup.Name = "pingGroup";
            this.pingGroup.Size = new System.Drawing.Size(296, 59);
            this.pingGroup.TabIndex = 5;
            this.pingGroup.TabStop = false;
            this.pingGroup.Text = "Ping";
            // 
            // pingBtn
            // 
            this.pingBtn.Location = new System.Drawing.Point(212, 21);
            this.pingBtn.Name = "pingBtn";
            this.pingBtn.Size = new System.Drawing.Size(75, 26);
            this.pingBtn.TabIndex = 4;
            this.pingBtn.Text = "Ping";
            this.pingBtn.UseVisualStyleBackColor = true;
            this.pingBtn.Click += new System.EventHandler(this.pingBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Controls.Add(this.ResetStat);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(1088, 138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(530, 366);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Statistics";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.richTextBox2);
            this.groupBox4.Controls.Add(this.richTextBox1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(499, 138);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(576, 551);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Trafic";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.ResetMAC);
            this.groupBox5.Controls.Add(this.dataGridView1);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(9, 138);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(476, 281);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ARP table";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "SET";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Time:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(60, 247);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 2;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(1099, 64);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(112, 21);
            this.radioButton1.TabIndex = 77;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Static routing";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(1099, 91);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(131, 21);
            this.radioButton2.TabIndex = 78;
            this.radioButton2.Text = "Dynamic routing";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(444, 65);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 79;
            this.button6.Text = "Passive";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(444, 95);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 80;
            this.button7.Text = "Passive";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(195, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "SET";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1782, 753);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pingGroup);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.start);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Little router 1.0";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pingGroup.ResumeLayout(false);
            this.pingGroup.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label10;
        private Label label9;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Button start;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox1;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Button ResetStat;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem filtersToolStripMenuItem;
        private ToolStripMenuItem staticRoutingToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private Button ResetMAC;
        private TextBox IP1_1;
        private TextBox IP1_2;
        private TextBox IP1_4;
        private TextBox IP1_3;
        private TextBox IP2_4;
        private TextBox IP2_3;
        private TextBox IP2_2;
        private TextBox IP2_1;
        private TextBox MASK2_4;
        private TextBox MASK2_3;
        private TextBox MASK2_2;
        private TextBox MASK2_1;
        private TextBox MASK1_4;
        private TextBox MASK1_3;
        private TextBox MASK1_2;
        private TextBox MASK1_1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox pingIP_4;
        private TextBox pingIP_3;
        private TextBox pingIP_2;
        private TextBox pingIP_1;
        private GroupBox pingGroup;
        private Button pingBtn;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private Button button3;
        private Button button4;
        private Button button5;
        private ToolStripMenuItem exitToolStripMenuItem;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Button button1;
        private Label label1;
        private TextBox textBox1;
        private ToolStripMenuItem rIPDatabaseToolStripMenuItem;
        private Button button6;
        private Button button7;
        private Button button2;
    }
}

