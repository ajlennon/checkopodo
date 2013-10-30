namespace DynamicDevices.DataRepurposing.CheckOpodo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCheckIncDays = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxStay = new System.Windows.Forms.TextBox();
            this.textBoxEndPoint = new System.Windows.Forms.TextBox();
            this.textBoxStartPoint = new System.Windows.Forms.TextBox();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.buttonGo = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelCentre = new System.Windows.Forms.Panel();
            this.zgOutboundFlightsChange = new ZedGraph.ZedGraphControl();
            this.zgReturnFlightsChange = new ZedGraph.ZedGraphControl();
            this.panel1.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelCentre.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonAbout);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBoxCheckIncDays);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxStay);
            this.panel1.Controls.Add(this.textBoxEndPoint);
            this.panel1.Controls.Add(this.textBoxStartPoint);
            this.panel1.Controls.Add(this.dateTimePickerEnd);
            this.panel1.Controls.Add(this.dateTimePickerStart);
            this.panel1.Controls.Add(this.buttonGo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1308, 53);
            this.panel1.TabIndex = 0;
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(852, 18);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(71, 28);
            this.buttonAbout.TabIndex = 15;
            this.buttonAbout.Text = "About";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(637, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Check inc days";
            // 
            // textBoxCheckIncDays
            // 
            this.textBoxCheckIncDays.Location = new System.Drawing.Point(640, 27);
            this.textBoxCheckIncDays.Name = "textBoxCheckIncDays";
            this.textBoxCheckIncDays.Size = new System.Drawing.Size(74, 20);
            this.textBoxCheckIncDays.TabIndex = 13;
            this.textBoxCheckIncDays.Text = "7";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(723, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Days away";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(488, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Latest Return Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Earliest Departure Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "End Airport";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Start Airport";
            // 
            // textBoxStay
            // 
            this.textBoxStay.Location = new System.Drawing.Point(720, 28);
            this.textBoxStay.Name = "textBoxStay";
            this.textBoxStay.Size = new System.Drawing.Size(74, 20);
            this.textBoxStay.TabIndex = 6;
            this.textBoxStay.Text = "7";
            // 
            // textBoxEndPoint
            // 
            this.textBoxEndPoint.Location = new System.Drawing.Point(236, 27);
            this.textBoxEndPoint.Name = "textBoxEndPoint";
            this.textBoxEndPoint.Size = new System.Drawing.Size(100, 20);
            this.textBoxEndPoint.TabIndex = 4;
            this.textBoxEndPoint.Text = "BHZ";
            this.textBoxEndPoint.TextChanged += new System.EventHandler(this.textBoxEndPoint_TextChanged);
            // 
            // textBoxStartPoint
            // 
            this.textBoxStartPoint.Location = new System.Drawing.Point(130, 27);
            this.textBoxStartPoint.Name = "textBoxStartPoint";
            this.textBoxStartPoint.Size = new System.Drawing.Size(100, 20);
            this.textBoxStartPoint.TabIndex = 3;
            this.textBoxStartPoint.Text = "MAN";
            this.textBoxStartPoint.TextChanged += new System.EventHandler(this.textBoxStartPoint_TextChanged);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Location = new System.Drawing.Point(491, 27);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(143, 20);
            this.dateTimePickerEnd.TabIndex = 2;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Location = new System.Drawing.Point(342, 27);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(143, 20);
            this.dateTimePickerStart.TabIndex = 1;
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(12, 18);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(71, 28);
            this.buttonGo.TabIndex = 0;
            this.buttonGo.Text = "Search";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.textBoxLog);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 573);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(4);
            this.panelBottom.Size = new System.Drawing.Size(1308, 180);
            this.panelBottom.TabIndex = 1;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxLog.Location = new System.Drawing.Point(4, 4);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(1300, 172);
            this.textBoxLog.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelCentre);
            this.panel2.Controls.Add(this.panelBottom);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1308, 753);
            this.panel2.TabIndex = 1;
            // 
            // panelCentre
            // 
            this.panelCentre.Controls.Add(this.zgOutboundFlightsChange);
            this.panelCentre.Controls.Add(this.zgReturnFlightsChange);
            this.panelCentre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentre.Location = new System.Drawing.Point(0, 0);
            this.panelCentre.Name = "panelCentre";
            this.panelCentre.Size = new System.Drawing.Size(1308, 573);
            this.panelCentre.TabIndex = 3;
            // 
            // zgOutboundFlightsChange
            // 
            this.zgOutboundFlightsChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgOutboundFlightsChange.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgOutboundFlightsChange.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgOutboundFlightsChange.IsAutoScrollRange = false;
            this.zgOutboundFlightsChange.IsEnableHEdit = false;
            this.zgOutboundFlightsChange.IsEnableHPan = true;
            this.zgOutboundFlightsChange.IsEnableHZoom = true;
            this.zgOutboundFlightsChange.IsEnableVEdit = false;
            this.zgOutboundFlightsChange.IsEnableVPan = false;
            this.zgOutboundFlightsChange.IsEnableVZoom = true;
            this.zgOutboundFlightsChange.IsPrintFillPage = true;
            this.zgOutboundFlightsChange.IsPrintKeepAspectRatio = true;
            this.zgOutboundFlightsChange.IsScrollY2 = false;
            this.zgOutboundFlightsChange.IsShowContextMenu = true;
            this.zgOutboundFlightsChange.IsShowCopyMessage = true;
            this.zgOutboundFlightsChange.IsShowCursorValues = false;
            this.zgOutboundFlightsChange.IsShowHScrollBar = false;
            this.zgOutboundFlightsChange.IsShowPointValues = true;
            this.zgOutboundFlightsChange.IsShowVScrollBar = false;
            this.zgOutboundFlightsChange.IsSynchronizeXAxes = false;
            this.zgOutboundFlightsChange.IsSynchronizeYAxes = false;
            this.zgOutboundFlightsChange.IsZoomOnMouseCenter = false;
            this.zgOutboundFlightsChange.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgOutboundFlightsChange.LinkModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgOutboundFlightsChange.Location = new System.Drawing.Point(0, 0);
            this.zgOutboundFlightsChange.Name = "zgOutboundFlightsChange";
            this.zgOutboundFlightsChange.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgOutboundFlightsChange.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zgOutboundFlightsChange.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zgOutboundFlightsChange.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgOutboundFlightsChange.PointDateFormat = "g";
            this.zgOutboundFlightsChange.PointValueFormat = "G";
            this.zgOutboundFlightsChange.ScrollMaxX = 0;
            this.zgOutboundFlightsChange.ScrollMaxY = 0;
            this.zgOutboundFlightsChange.ScrollMaxY2 = 0;
            this.zgOutboundFlightsChange.ScrollMinX = 0;
            this.zgOutboundFlightsChange.ScrollMinY = 0;
            this.zgOutboundFlightsChange.ScrollMinY2 = 0;
            this.zgOutboundFlightsChange.Size = new System.Drawing.Size(1308, 573);
            this.zgOutboundFlightsChange.TabIndex = 7;
            this.zgOutboundFlightsChange.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgOutboundFlightsChange.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zgOutboundFlightsChange.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zgOutboundFlightsChange.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgOutboundFlightsChange.ZoomStepFraction = 0.1;
            // 
            // zgReturnFlightsChange
            // 
            this.zgReturnFlightsChange.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgReturnFlightsChange.EditModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgReturnFlightsChange.IsAutoScrollRange = false;
            this.zgReturnFlightsChange.IsEnableHEdit = false;
            this.zgReturnFlightsChange.IsEnableHPan = true;
            this.zgReturnFlightsChange.IsEnableHZoom = false;
            this.zgReturnFlightsChange.IsEnableVEdit = false;
            this.zgReturnFlightsChange.IsEnableVPan = true;
            this.zgReturnFlightsChange.IsEnableVZoom = false;
            this.zgReturnFlightsChange.IsPrintFillPage = true;
            this.zgReturnFlightsChange.IsPrintKeepAspectRatio = true;
            this.zgReturnFlightsChange.IsScrollY2 = false;
            this.zgReturnFlightsChange.IsShowContextMenu = true;
            this.zgReturnFlightsChange.IsShowCopyMessage = true;
            this.zgReturnFlightsChange.IsShowCursorValues = false;
            this.zgReturnFlightsChange.IsShowHScrollBar = false;
            this.zgReturnFlightsChange.IsShowPointValues = true;
            this.zgReturnFlightsChange.IsShowVScrollBar = false;
            this.zgReturnFlightsChange.IsSynchronizeXAxes = false;
            this.zgReturnFlightsChange.IsSynchronizeYAxes = false;
            this.zgReturnFlightsChange.IsZoomOnMouseCenter = false;
            this.zgReturnFlightsChange.LinkButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgReturnFlightsChange.LinkModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None)));
            this.zgReturnFlightsChange.Location = new System.Drawing.Point(469, 3);
            this.zgReturnFlightsChange.Name = "zgReturnFlightsChange";
            this.zgReturnFlightsChange.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgReturnFlightsChange.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zgReturnFlightsChange.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zgReturnFlightsChange.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgReturnFlightsChange.PointDateFormat = "g";
            this.zgReturnFlightsChange.PointValueFormat = "G";
            this.zgReturnFlightsChange.ScrollMaxX = 0;
            this.zgReturnFlightsChange.ScrollMaxY = 0;
            this.zgReturnFlightsChange.ScrollMaxY2 = 0;
            this.zgReturnFlightsChange.ScrollMinX = 0;
            this.zgReturnFlightsChange.ScrollMinY = 0;
            this.zgReturnFlightsChange.ScrollMinY2 = 0;
            this.zgReturnFlightsChange.Size = new System.Drawing.Size(460, 333);
            this.zgReturnFlightsChange.TabIndex = 8;
            this.zgReturnFlightsChange.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zgReturnFlightsChange.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zgReturnFlightsChange.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zgReturnFlightsChange.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zgReturnFlightsChange.ZoomStepFraction = 0.1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1308, 806);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Dynamic Devices Ltd - Flight Pricing Graph Tool ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panelCentre.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.TextBox textBoxEndPoint;
        private System.Windows.Forms.TextBox textBoxStartPoint;
        private System.Windows.Forms.TextBox textBoxStay;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelCentre;
        private ZedGraph.ZedGraphControl zgOutboundFlightsChange;
        private ZedGraph.ZedGraphControl zgReturnFlightsChange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCheckIncDays;
        private System.Windows.Forms.Button buttonAbout;
    }
}

