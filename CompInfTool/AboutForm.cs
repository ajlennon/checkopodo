using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DynamicDevices.DataRepurposing.CompInfTool
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            richTextBoxInfo.Text = "[TBD]. For more details, to request enhancements or to report issues with the software please contact Dynamic Devices at info@dynamicdevices.co.uk"; 
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
        }
    }
}