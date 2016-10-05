using System.ComponentModel;
using System.Windows.Forms;

namespace xcicman_zadanie__WAN
{
    partial class StaticRoutingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaticRoutingForm));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.IP1_1 = new System.Windows.Forms.TextBox();
            this.IP1_2 = new System.Windows.Forms.TextBox();
            this.IP1_3 = new System.Windows.Forms.TextBox();
            this.IP1_4 = new System.Windows.Forms.TextBox();
            this.Mask1 = new System.Windows.Forms.TextBox();
            this.Mask2 = new System.Windows.Forms.TextBox();
            this.Mask3 = new System.Windows.Forms.TextBox();
            this.Mask4 = new System.Windows.Forms.TextBox();
            this.NextHop1 = new System.Windows.Forms.TextBox();
            this.NextHop2 = new System.Windows.Forms.TextBox();
            this.NextHop3 = new System.Windows.Forms.TextBox();
            this.NextHop4 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(287, 37);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 21);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Next hop";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(287, 69);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(94, 21);
            this.checkBox2.TabIndex = 13;
            this.checkBox2.Text = "Interface";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(668, 51);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 15;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeButton.Location = new System.Drawing.Point(766, 51);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(90, 23);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.removeButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.IP1_1);
            this.groupBox1.Controls.Add(this.IP1_2);
            this.groupBox1.Controls.Add(this.IP1_3);
            this.groupBox1.Controls.Add(this.IP1_4);
            this.groupBox1.Controls.Add(this.Mask1);
            this.groupBox1.Controls.Add(this.Mask2);
            this.groupBox1.Controls.Add(this.Mask3);
            this.groupBox1.Controls.Add(this.Mask4);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.NextHop1);
            this.groupBox1.Controls.Add(this.NextHop2);
            this.groupBox1.Controls.Add(this.NextHop3);
            this.groupBox1.Controls.Add(this.NextHop4);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1023, 113);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add static route";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(875, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Adapter 1",
            "Adapter 2"});
            this.comboBox1.Location = new System.Drawing.Point(393, 69);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 16;
            this.comboBox1.SelectedIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Mask:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Network:";
            // 
            // IP1_1
            // 
            this.IP1_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP1_1.Location = new System.Drawing.Point(84, 35);
            this.IP1_1.MaxLength = 3;
            this.IP1_1.Name = "IP1_1";
            this.IP1_1.Size = new System.Drawing.Size(43, 24);
            this.IP1_1.TabIndex = 1;
            this.IP1_1.Text = "10";
            this.IP1_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP1_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // IP1_2
            // 
            this.IP1_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP1_2.Location = new System.Drawing.Point(129, 35);
            this.IP1_2.MaxLength = 3;
            this.IP1_2.Name = "IP1_2";
            this.IP1_2.Size = new System.Drawing.Size(43, 24);
            this.IP1_2.TabIndex = 1;
            this.IP1_2.Text = "10";
            this.IP1_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP1_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // IP1_3
            // 
            this.IP1_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP1_3.Location = new System.Drawing.Point(174, 35);
            this.IP1_3.MaxLength = 3;
            this.IP1_3.Name = "IP1_3";
            this.IP1_3.Size = new System.Drawing.Size(43, 24);
            this.IP1_3.TabIndex = 2;
            this.IP1_3.Text = "10";
            this.IP1_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP1_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // IP1_4
            // 
            this.IP1_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IP1_4.Location = new System.Drawing.Point(219, 35);
            this.IP1_4.MaxLength = 3;
            this.IP1_4.Name = "IP1_4";
            this.IP1_4.Size = new System.Drawing.Size(43, 24);
            this.IP1_4.TabIndex = 3;
            this.IP1_4.Text = "6";
            this.IP1_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP1_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // Mask1
            // 
            this.Mask1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mask1.Location = new System.Drawing.Point(84, 65);
            this.Mask1.MaxLength = 3;
            this.Mask1.Name = "Mask1";
            this.Mask1.Size = new System.Drawing.Size(43, 24);
            this.Mask1.TabIndex = 3;
            this.Mask1.Text = "255";
            this.Mask1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Mask1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // Mask2
            // 
            this.Mask2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mask2.Location = new System.Drawing.Point(129, 65);
            this.Mask2.MaxLength = 3;
            this.Mask2.Name = "Mask2";
            this.Mask2.Size = new System.Drawing.Size(43, 24);
            this.Mask2.TabIndex = 5;
            this.Mask2.Text = "255";
            this.Mask2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Mask2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // Mask3
            // 
            this.Mask3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mask3.Location = new System.Drawing.Point(174, 65);
            this.Mask3.MaxLength = 3;
            this.Mask3.Name = "Mask3";
            this.Mask3.Size = new System.Drawing.Size(43, 24);
            this.Mask3.TabIndex = 6;
            this.Mask3.Text = "255";
            this.Mask3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Mask3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // Mask4
            // 
            this.Mask4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mask4.Location = new System.Drawing.Point(219, 65);
            this.Mask4.MaxLength = 3;
            this.Mask4.Name = "Mask4";
            this.Mask4.Size = new System.Drawing.Size(43, 24);
            this.Mask4.TabIndex = 7;
            this.Mask4.Text = "0";
            this.Mask4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Mask4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // NextHop1
            // 
            this.NextHop1.Enabled = false;
            this.NextHop1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextHop1.Location = new System.Drawing.Point(393, 35);
            this.NextHop1.MaxLength = 3;
            this.NextHop1.Name = "NextHop1";
            this.NextHop1.Size = new System.Drawing.Size(43, 24);
            this.NextHop1.TabIndex = 9;
            this.NextHop1.Text = "10";
            this.NextHop1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NextHop1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // NextHop2
            // 
            this.NextHop2.Enabled = false;
            this.NextHop2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextHop2.Location = new System.Drawing.Point(438, 35);
            this.NextHop2.MaxLength = 3;
            this.NextHop2.Name = "NextHop2";
            this.NextHop2.Size = new System.Drawing.Size(43, 24);
            this.NextHop2.TabIndex = 10;
            this.NextHop2.Text = "10";
            this.NextHop2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NextHop2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // NextHop3
            // 
            this.NextHop3.Enabled = false;
            this.NextHop3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextHop3.Location = new System.Drawing.Point(483, 35);
            this.NextHop3.MaxLength = 3;
            this.NextHop3.Name = "NextHop3";
            this.NextHop3.Size = new System.Drawing.Size(43, 24);
            this.NextHop3.TabIndex = 11;
            this.NextHop3.Text = "10";
            this.NextHop3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NextHop3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
            // 
            // NextHop4
            // 
            this.NextHop4.Enabled = false;
            this.NextHop4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextHop4.Location = new System.Drawing.Point(528, 35);
            this.NextHop4.MaxLength = 3;
            this.NextHop4.Name = "NextHop4";
            this.NextHop4.Size = new System.Drawing.Size(43, 24);
            this.NextHop4.TabIndex = 12;
            this.NextHop4.Text = "7";
            this.NextHop4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NextHop4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IP_MASK_KeyPress);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridView1.Location = new System.Drawing.Point(12, 131);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1023, 278);
            this.dataGridView1.TabIndex = 18;
            // 
            // StaticRoutingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1047, 416);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StaticRoutingForm";
            this.Text = "Routing Table";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Button addButton;
        private Button removeButton;
        private GroupBox groupBox1;
        private TextBox IP1_1;
        private TextBox IP1_2;
        private TextBox IP1_3;
        private TextBox IP1_4;
        private TextBox Mask1;
        private TextBox Mask2;
        private TextBox Mask3;
        private TextBox Mask4;
        private Label label1;
        private TextBox NextHop1;
        private TextBox NextHop2;
        private TextBox NextHop3;
        private TextBox NextHop4;
        private Label label2;
        private DataGridView dataGridView1;
        private ComboBox comboBox1;
        private Button button1;
    }
}