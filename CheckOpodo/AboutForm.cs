using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DynamicDevices.DataRepurposing.CheckOpodo
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            labelVersion.Text = "Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            richTextBoxInfo.Text = "CheckOpodo allows to you graph costs for return plane flights across a range of dates. The tool queries Opodo with different start dates. Set the maximum range using the Departure Date and Return Date calendar pickers. Then select the number of days to increment for each Opodo query, and the minimum length of stay. Click 'Search' and the utility will start building a graph of return ticket prices. For more details, to request enhancements or to report issues with the software please contact Dynamic Devices at info@dynamicdevices.co.uk"; 
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
        }
    }
}