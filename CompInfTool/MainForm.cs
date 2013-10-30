using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Net;
using System.Web;

using DynamicDevices.Utilities;
using DynamicDevices.DataRepurposing.Queries;

namespace DynamicDevices.DataRepurposing.CompInfTool
{
    public partial class MainForm : Form
    {
        private delegate void QueryFinishedHandler();
        private QueryFinishedHandler OnQueryFinished;

        private delegate void AddDataHandler(string strDealerName, string strDetail, string strColor, string strMileage, string strPrice, string InfoURL);
        private AddDataHandler OnAddData;

        private Thread _objWorker;
        private bool _bIsRunning = false;
        private CarQuery.EnumCarType _eCarType;
        private string _strPostCode;

        public MainForm()
        {
            InitializeComponent();

            OnQueryFinished = new QueryFinishedHandler(QueryFinished);
            OnAddData = new AddDataHandler(AddToDataGridView);
            comboBoxCarType.SelectedIndex = 0;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            if (buttonGo.Text == "GO")
            {
                if (textBoxPostCode.Text.Length == 0)
                    return;

                //textBoxPostCode.Enabled = false;

                buttonGo.Text = "Abort";

                _eCarType = CarQuery.EnumCarType.NEW;

                if (comboBoxCarType.SelectedText == "New")
                {
                    _eCarType = CarQuery.EnumCarType.NEW;
                }
                else if (comboBoxCarType.SelectedText == "Used")
                {
                    _eCarType = CarQuery.EnumCarType.USED;
                }
                else if (comboBoxCarType.SelectedText == "Service")
                {
                    _eCarType = CarQuery.EnumCarType.SERVICE;
                }

                _strPostCode = textBoxPostCode.Text;

                _objWorker = new Thread(Worker);
                _objWorker.Start();
            }
            else
            {
                buttonGo.Enabled = false;

                _bIsRunning = false;

                //_objWorker.Join();

                textBoxPostCode.Enabled = true;
                buttonGo.Text = "GO";
                buttonGo.Enabled = true;
            }
        }

        private void SafeQueryFinished()
        {
            this.Invoke( OnQueryFinished );
        }

        private void QueryFinished()
        {
            textBoxPostCode.Enabled = true;
            buttonGo.Text = "GO";
            buttonGo.Enabled = true;
        }

        private void Worker()
        {
            _bIsRunning = true;

            // Query dealers around a given postcode
            VolkswagenRetailerQuery objQuery = new VolkswagenRetailerQuery(_eCarType, _strPostCode);

            try
            {
                QueryResponse[] arrRetailerResponses = objQuery.GetResponses();

                foreach (QueryResponse objRetailer in arrRetailerResponses)
                {
                    if (!_bIsRunning)
                    {
                        SafeQueryFinished();

                        return;
                    }

                    try
                    {
                        int iID = int.Parse((string)objRetailer["ID"]);

                        QueryResponse[] arrCarResponses = queryRetailer(_eCarType, iID);

                        objRetailer.Add("CARS", arrCarResponses);

                        SafeAddToDataGridView(objRetailer);
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine("Got exception: " + e.Message);
                    }
                }

                // Now we've got all the data so write it out (somehow!)                
                string strFilename = "Retailer_NewCarInfo_" + _strPostCode + "_" + DateTime.Now.Ticks + ".xml";
                storeToXML(strFilename, _strPostCode, arrRetailerResponses);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Got error: " + e.Message);
            }

            if (OnQueryFinished != null)
                SafeQueryFinished();
        }

        private void SafeAddToDataGridView(QueryResponse objRetailer)
        {
            string strDealerName = (string)objRetailer["NAME"];

            foreach (QueryResponse objCar in (QueryResponse[])objRetailer["CARS"])
            {

                string strDetail = (string)objCar["DETAILS"];
                string strColor = (string)objCar["COLOR"];
                string strMileage = (string)objCar["MILEAGE"];
                string strPrice = (string)objCar["PRICE"];
                string strInfoUrl = (string)objCar["INFOURL"];

//                Image objImage = GetImage(strImageUrl);

                this.Invoke(OnAddData, new object[] { strDealerName, strDetail, strColor, strMileage, strPrice, strInfoUrl });
            }
        }

        private void AddToDataGridView(string strDealerName, string strDetail, string strColor, string strMileage, string strPrice, string strInfoURL)
        {
            dataGridViewCars.Rows.Add(new object[] { strDealerName, strDetail, strColor, strMileage, strPrice, strInfoURL});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eCarType"></param>
        /// <param name="iID"></param>
        /// <returns></returns>
        private QueryResponse[] queryRetailer(CarQuery.EnumCarType eCarType, int iID)
        {
            QueryResponse[] arrResponses = new QueryResponse[0];

            // Query cars from each dealer
            VolkswagenCarQuery objQuery = new VolkswagenCarQuery(eCarType, iID);

            try
            {
                arrResponses = objQuery.GetResponses();

#if false
                foreach (QueryResponse objResponse in arrResponses)
                {
                    Console.Out.WriteLine("Got QueryResponse: " + objResponse.GetType());

                    foreach (DictionaryEntry d in objResponse)
                    {
                        Console.Out.WriteLine(d.Key + " = " + d.Value);
                    }
                }
#endif
            }
            catch (Exception e)
            {
                Debug.WriteLine("Got error: " + e.Message);
            }

            return arrResponses;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFilename"></param>
        /// <param name="strPostCode"></param>
        /// <param name="arrRetailers"></param>
        private void storeToXML(string strFilename, string strPostCode, QueryResponse[] arrRetailers)
        {
            Debug.WriteLine("Storing to XML file: " + strFilename);

            StringWriter objWriter = new StringWriter();

            objWriter.WriteLine("<query>");

            objWriter.WriteLine("");

            objWriter.WriteLine("<date>" + DateTime.Now + "</date>");

            objWriter.WriteLine("");

            objWriter.WriteLine("<postcode>" + strPostCode + "</postcode>");

            objWriter.WriteLine("");

            objWriter.WriteLine("<retailers>");

            foreach (QueryResponse objRetailer in arrRetailers)
            {
                objWriter.WriteLine("");

                objWriter.WriteLine("   <retailer"
                    + " name='" + Query.XmlEscape((string)objRetailer["NAME"]) + "'"
                    + " id='" + Query.XmlEscape((string)objRetailer["ID"]) + "'"
                    + " distance='" + Query.XmlEscape((string)objRetailer["DISTANCE"]) + "'"
                    + " url='" + Query.XmlEscape((string)objRetailer["URL"]) 
                    + "'>");

                foreach (QueryResponse objCar in (QueryResponse[])objRetailer["CARS"])
                {
                    objWriter.WriteLine("");

                    objWriter.WriteLine("       <car type='new'>");

                    foreach (DictionaryEntry objDE in objCar)
                    {
                        objWriter.Write("           <" + Query.XmlEscape(((string)objDE.Key).ToLower()) + ">");

                        objWriter.Write(Query.XmlEscape((string)objDE.Value));


                        objWriter.WriteLine("</" + Query.XmlEscape(((string)objDE.Key).ToLower()) + ">");
                    }

                    objWriter.WriteLine("       </car>");
                }

                objWriter.WriteLine("   </retailer>");
            }

            objWriter.WriteLine("</retailers>");

            objWriter.WriteLine("</query>");
            objWriter.Close();

            string strOutput = objWriter.ToString();

            StreamWriter objFileWriter = new StreamWriter(strFilename);
            objFileWriter.Write(strOutput);
            objFileWriter.Close();

        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            AboutForm objForm = new AboutForm();
            objForm.ShowDialog();
        }

        public Bitmap GetImage(string url)
        {
            HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse wRes = (HttpWebResponse)(wReq).GetResponse();
            Stream wStr = wRes.GetResponseStream();
            return new Bitmap(wStr);
        }

        private void dataGridViewCars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            object o = dataGridViewCars.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (o is DataGridViewLinkCell)
            {
                DataGridViewLinkCell cell = (DataGridViewLinkCell)this.dataGridViewCars.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if ( ((string)cell.Value).StartsWith("http://"))
                 {
                     Process.Start(((string)cell.Value));
                 }
            }
            
        }
    }
}