namespace DynamicDevices.DataRepurposing.CompInfTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCarType = new System.Windows.Forms.ComboBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.textBoxPostCode = new System.Windows.Forms.TextBox();
            this.labelPostcode = new System.Windows.Forms.Label();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.dataGridViewCars = new System.Windows.Forms.DataGridView();
            this.Retailer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InfoPage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MoreInfo = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panelTop.SuspendLayout();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.buttonAbout);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.comboBoxCarType);
            this.panelTop.Controls.Add(this.buttonGo);
            this.panelTop.Controls.Add(this.textBoxPostCode);
            this.panelTop.Controls.Add(this.labelPostcode);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(926, 66);
            this.panelTop.TabIndex = 0;
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(643, 12);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(95, 23);
            this.buttonAbout.TabIndex = 5;
            this.buttonAbout.Text = "About";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(279, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Choose Car Type";
            // 
            // comboBoxCarType
            // 
            this.comboBoxCarType.FormattingEnabled = true;
            this.comboBoxCarType.Items.AddRange(new object[] {
            "New",
            "Used",
            "Service"});
            this.comboBoxCarType.Location = new System.Drawing.Point(389, 12);
            this.comboBoxCarType.Name = "comboBoxCarType";
            this.comboBoxCarType.Size = new System.Drawing.Size(105, 21);
            this.comboBoxCarType.TabIndex = 3;
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(522, 12);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(95, 23);
            this.buttonGo.TabIndex = 2;
            this.buttonGo.Text = "GO";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // textBoxPostCode
            // 
            this.textBoxPostCode.Location = new System.Drawing.Point(118, 12);
            this.textBoxPostCode.Name = "textBoxPostCode";
            this.textBoxPostCode.Size = new System.Drawing.Size(146, 20);
            this.textBoxPostCode.TabIndex = 1;
            // 
            // labelPostcode
            // 
            this.labelPostcode.AutoSize = true;
            this.labelPostcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPostcode.Location = new System.Drawing.Point(13, 15);
            this.labelPostcode.Name = "labelPostcode";
            this.labelPostcode.Size = new System.Drawing.Size(99, 13);
            this.labelPostcode.TabIndex = 0;
            this.labelPostcode.Text = "Enter Post Code";
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.dataGridViewCars);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 66);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(926, 276);
            this.panelCenter.TabIndex = 1;
            // 
            // dataGridViewCars
            // 
            this.dataGridViewCars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Retailer,
            this.InfoPage,
            this.Color,
            this.Mileage,
            this.Price,
            this.MoreInfo});
            this.dataGridViewCars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCars.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCars.Name = "dataGridViewCars";
            this.dataGridViewCars.ReadOnly = true;
            this.dataGridViewCars.Size = new System.Drawing.Size(926, 276);
            this.dataGridViewCars.TabIndex = 0;
            this.dataGridViewCars.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCars_CellContentClick);
            // 
            // Retailer
            // 
            this.Retailer.HeaderText = "Retailer";
            this.Retailer.Name = "Retailer";
            this.Retailer.ReadOnly = true;
            // 
            // InfoPage
            // 
            this.InfoPage.HeaderText = "Details";
            this.InfoPage.Name = "InfoPage";
            this.InfoPage.ReadOnly = true;
            // 
            // Color
            // 
            this.Color.HeaderText = "Color";
            this.Color.Name = "Color";
            this.Color.ReadOnly = true;
            // 
            // Mileage
            // 
            this.Mileage.HeaderText = "Mileage";
            this.Mileage.Name = "Mileage";
            this.Mileage.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // MoreInfo
            // 
            this.MoreInfo.HeaderText = "More Info";
            this.MoreInfo.Name = "MoreInfo";
            this.MoreInfo.ReadOnly = true;
            this.MoreInfo.Text = "More Info";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 342);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CIT V0.1 - Dynamic Devices Ltd";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TextBox textBoxPostCode;
        private System.Windows.Forms.Label labelPostcode;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCarType;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.DataGridView dataGridViewCars;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.DataGridViewTextBoxColumn Retailer;
        private System.Windows.Forms.DataGridViewTextBoxColumn InfoPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Color;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mileage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewLinkColumn MoreInfo;
    }
}

