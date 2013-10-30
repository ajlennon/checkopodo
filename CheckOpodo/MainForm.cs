using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using DynamicDevices.DataRepurposing.Queries;

using ZedGraph;

namespace DynamicDevices.DataRepurposing.CheckOpodo
{
    public partial class MainForm : Form
    {
        public delegate void UpdateTextCallback(string text);

        PointPairList arrOutboundChangeCostList = new PointPairList();
        PointPairList arrReturnChangeCostList = new PointPairList();

        public delegate void UpdateGraphCallback();

        private Thread objQueryThread;

        public ArrayList _arrQueries = new ArrayList();

        public MainForm()
        {
            InitializeComponent();

//            webBrowser1.ScriptErrorsSuppressed = true;

            dateTimePickerStart.Value = DateTime.Now.AddDays(7);
            dateTimePickerEnd.Value = DateTime.Now.AddMonths(11);
//            dateTimePickerStart.Value = DateTime.Parse("25/09/2006");
//            dateTimePickerEnd.Value = DateTime.Parse("26/11/2006");

            zgOutboundFlightsChange.Invoke(new UpdateGraphCallback(UpdateGraph));
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            if (buttonGo.Text == "Search")
            {
                DateTime objSDateTime = dateTimePickerStart.Value;
                DateTime objEDateTime = dateTimePickerEnd.Value;

                if (objSDateTime.Ticks > objEDateTime.Ticks)
                {
                    MessageBox.Show("Start date must be before end date !");
                    return;
                }

                if( DateTime.Now.AddDays(360) <= objEDateTime)
                {
                    MessageBox.Show("End date must be within 360 days !");
                    return;
                }


                SafeLog("Starting Search on Opodo");

                _arrQueries.Clear();
                arrOutboundChangeCostList.Clear();
                arrReturnChangeCostList.Clear();

                UpdateGraph();

                objQueryThread = new Thread(new ThreadStart(QueryWorker));
                objQueryThread.Start();
//                checkBoxReturnDates.Enabled = false;
                buttonGo.Text = "Abort";
            }
            else
            {
                if (_arrQueries.Count > 0)
                {
                    SafeLog("Aborting Search on Opodo");

                    if (objQueryThread != null)
                        objQueryThread.Abort();

                    foreach (Query objQuery in _arrQueries)
                    {
                        objQuery.Dispose();
                    }
                    _arrQueries.Clear();
                
                    SafeLog("Aborted");
                }

                buttonGo.Text = "Search";
//                checkBoxReturnDates.Enabled = true;
            }
        }

        private void QueryWorker()
        {
            DateTime objSDateTime = dateTimePickerStart.Value;
            DateTime objEDateTime = dateTimePickerEnd.Value;

            Query objQuery;

            //          buttonGo.Enabled = false;

            DateTime myStartDateTime = objSDateTime;
            DateTime myEndDateTime = objSDateTime.AddDays(int.Parse(textBoxStay.Text));
                        
            while (myEndDateTime <= objEDateTime)
            {
                SafeLog("Checking " + myStartDateTime.ToShortDateString() + " - " + myEndDateTime.ToShortDateString());

                objQuery = new OpodoQuery(textBoxStartPoint.Text, textBoxEndPoint.Text,
                    myStartDateTime.Day.ToString(), myStartDateTime.Month.ToString(), myStartDateTime.Year.ToString(),
                    myEndDateTime.Day.ToString(), myEndDateTime.Month.ToString(), myEndDateTime.Year.ToString()
                );

                _arrQueries.Add(objQuery);

                objQuery.OnReceivedPage += OutChangeRxPageHandler;
                objQuery.OnError += SafeLogException;

                // Server seems to handle 4 concurrent connections
                // FIXME: Mutexes and such                
                while (_arrQueries.Count >= 10)
                    Thread.Sleep(1000);

                objQuery.Start();

#if false
                if (checkBoxReturnDates.Checked)
                {
                    SafeLog("Checking " + objSDateTime.ToShortDateString() + " - " + myEndDateTime.ToShortDateString());

                    objQuery = new OpodoQuery(textBoxStartPoint.Text, textBoxEndPoint.Text,
                            objSDateTime.Day.ToString(), objSDateTime.Month.ToString(), objSDateTime.Year.ToString(),
                            myEndDateTime.Day.ToString(), myEndDateTime.Month.ToString(), myEndDateTime.Year.ToString()
                        );

                    _arrQueries.Add(objQuery);

                    objQuery.ReceivedPage += ReturnChangeRxPageHandler;

                    objQuery.Start();

                    SafeLog("Checked " + objSDateTime.ToShortDateString() + " - " + myEndDateTime.ToShortDateString());
                }
#endif

                myStartDateTime = myStartDateTime.AddDays(int.Parse(textBoxCheckIncDays.Text));
                myEndDateTime = myEndDateTime.AddDays(int.Parse(textBoxCheckIncDays.Text));
            }

            //            buttonGo.Enabled = true;
        }

        public void OutChangeRxPageHandler(Query objQuery, string strRxData)
        {
            if (objQuery is OpodoQuery)
            {
                int iStartIndex = 0;
                int iEndIndex;
                string strSubstring;
                DateTime objSDateTime = DateTime.Now;
                DateTime objEDateTime = DateTime.Now;

                bool fFoundLowCost = false;
                float fLowCost = 99999;

                while ((iStartIndex = strRxData.IndexOf("JavaScript:document", iStartIndex)) >= 0)
                {
                    // Got a price...

                    iStartIndex = strRxData.IndexOf("&pound;", iStartIndex) + 8;

                    iEndIndex = strRxData.IndexOf("</span>", iStartIndex);

                    strSubstring = strRxData.Substring(iStartIndex, iEndIndex-iStartIndex);
                 
                    // Parse data
//                    strSubstring = strSubstring.Replace('\"', ' ');
                    strSubstring = strSubstring.Trim();

                    objEDateTime = ((OpodoQuery)objQuery).EndDateTime;
                    objSDateTime = ((OpodoQuery)objQuery).StartDateTime;
                    float fCost = float.Parse(strSubstring);

                    if (fLowCost > fCost)
                    {
                        fLowCost = fCost;
                        fFoundLowCost = true;
                    }

                }

                if (fFoundLowCost)
                {
                    SafeLog("Got: " + objSDateTime.ToShortDateString() + "," + objEDateTime.ToShortDateString() + ",£" + fLowCost);
                    arrOutboundChangeCostList.Add((double)new XDate(objSDateTime), (double)fLowCost);
                    zgOutboundFlightsChange.Invoke(new UpdateGraphCallback(UpdateGraph));
                }
                else
                {
                    if (strRxData.Contains("Sorry, we are unable to find any results for your search."))
                    {
                        SafeLog("Got: " + objSDateTime.ToShortDateString() + "," + objEDateTime.ToShortDateString() + ",No results");
                    }
                    else if (strRxData.Contains("essential maintenance"))
                      SafeLog("Site is down for essential maintenance");
            //    Console.WriteLine(strRxData);
            }
            }
            else
            {
//                webBrowser1.DocumentText = strRxData;
            }

            if (_arrQueries.Contains(objQuery))
                _arrQueries.Remove(objQuery);

            if (_arrQueries.Count == 0)
            {
                SafeLog("Finished Search");
                buttonGo_Click(null, null);
            }
        }

        public void ReturnChangeRxPageHandler(Query objQuery, string strRxData)
        {
            if (objQuery is OpodoQuery)
            {
                int iStartIndex = 0;
                int iEndIndex;
                string strSubstring;
                DateTime objSDateTime = DateTime.Now;
                DateTime objEDateTime = DateTime.Now;

                bool fFoundLowCost = false;
                float fLowCost = 99999;

                while ((iStartIndex = strRxData.IndexOf("displayAvail(", iStartIndex)) >= 0)
                {
                    // Got a price...
                    iStartIndex += "displayAvail(".Length;
                    iEndIndex = strRxData.IndexOf(")", iStartIndex);

                    strSubstring = strRxData.Substring(iStartIndex, iEndIndex - iStartIndex);
                    if (!strSubstring.Contains("false") && !strSubstring.Contains("true"))
                        continue;

                    // Parse data
                    //                    strSubstring = strSubstring.Replace('\"', ' ');
                    strSubstring = strSubstring.Trim();

                    char[] arrSeparators = { '\"' };
                    string[] strData = strSubstring.Split(arrSeparators);

                    objEDateTime = DateTime.Parse(strData[3]);
                    objSDateTime = DateTime.Parse(strData[5]);
                    float fCost = float.Parse(strData[13].Substring(4));

                    if (fLowCost > fCost)
                    {
                        fLowCost = fCost;
                        fFoundLowCost = true;
                    }
                }

                if (fFoundLowCost)
                {
                    SafeLog("Got: " + objSDateTime.ToShortDateString() + "," + objEDateTime.ToShortDateString() + ",£" + fLowCost);
                    arrReturnChangeCostList.Add((double)new XDate(objEDateTime), (double)fLowCost);
                    zgReturnFlightsChange.Invoke(new UpdateGraphCallback(UpdateGraph));
                }
                else 
                    SafeLog("Got: " + objSDateTime.ToShortDateString() + "," + objEDateTime.ToShortDateString() + ",No valid prices");

            }
            else
            {
                //                webBrowser1.DocumentText = strRxData;
            }
            if (_arrQueries.Contains(objQuery))
                _arrQueries.Remove(objQuery);
            if (_arrQueries.Count == 0)
            {
                SafeLog("Finished Search");
                buttonGo_Click(null, null);
            }
        }

        private void SafeLogException(Query q, Exception e)
        {
            if (_arrQueries.Contains(q))
                _arrQueries.Remove(q);

            textBoxLog.Invoke(new UpdateTextCallback(this.Log), new object[] { "Query returned exception: " + e.Message + "\r\n" });
        }

        private void SafeLog(string s)
        {
            textBoxLog.Invoke(new UpdateTextCallback(this.Log), new object[] { s + "\r\n" });
        }

        private void Log(string s)
        {
            textBoxLog.AppendText(DateTime.Now.ToLongTimeString() + "\t" + s);
        }

        private void UpdateGraph()
        {
            zgReturnFlightsChange.GraphPane = DoGraphPane();
          
#if false
            if (arrReturnChangeCostList.Count > 0)
            {
                arrReturnChangeCostList.Sort(SortType.YValues);
                
                PointPairList iplowest = new PointPairList();
                iplowest.Add(arrReturnChangeCostList[0]);

                PointPairList iprest = arrReturnChangeCostList.Clone();
                iprest.RemoveAt(0);

                zgReturnFlightsChange.GraphPane.AddBar("Return Flight Cost", iprest, Color.Red);
                zgReturnFlightsChange.GraphPane.AddBar("Min. Flight Cost", iplowest, Color.Green);
            }
#endif
            arrReturnChangeCostList.Sort(SortType.XValues);
            zgReturnFlightsChange.GraphPane.AddCurve("Return Flight Cost", arrReturnChangeCostList, Color.Red, SymbolType.Diamond);

            zgReturnFlightsChange.GraphPane.Title.Text = "Return flight prices for " + textBoxStartPoint.Text + " to " + textBoxEndPoint.Text;
            zgReturnFlightsChange.GraphPane.XAxis.Title.Text = "Return Date";

            // Enable scrollbars if needed
            zgReturnFlightsChange.IsShowHScrollBar = false;
            zgReturnFlightsChange.IsShowVScrollBar = false;
            zgReturnFlightsChange.IsAutoScrollRange = true;
           
            // Tell ZedGraph to calculate the axis ranges
            // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
            // up the proper scrolling parameters
            zgReturnFlightsChange.AxisChange();
            // Make sure the Graph gets redrawn
            zgReturnFlightsChange.Invalidate();

            zgOutboundFlightsChange.GraphPane = DoGraphPane();

#if false
            if (arrOutboundChangeCostList.Count > 0)
            {
                arrOutboundChangeCostList.Sort(SortType.YValues);

                PointPairList iplowest = new PointPairList();
                iplowest.Add(arrOutboundChangeCostList[0]);

                PointPairList iprest = arrOutboundChangeCostList.Clone();
                iprest.RemoveAt(0);

                zgOutboundFlightsChange.GraphPane.AddBar("Min. Flight Cost", iplowest, Color.Green);
                zgOutboundFlightsChange.GraphPane.AddBar("Return Flight Cost", iprest, Color.Red);
            }
#endif
            arrOutboundChangeCostList.Sort(SortType.XValues);
            zgOutboundFlightsChange.GraphPane.AddCurve("Return Flight Cost", arrOutboundChangeCostList, Color.Red, SymbolType.Diamond);

            zgOutboundFlightsChange.GraphPane.Title.Text = "Return flight prices for " + textBoxStartPoint.Text + " to " + textBoxEndPoint.Text;
            zgOutboundFlightsChange.GraphPane.XAxis.Title.Text = "Outbound Date";

            // Enable scrollbars if needed
            zgOutboundFlightsChange.IsShowHScrollBar = false;
            zgOutboundFlightsChange.IsShowVScrollBar = false;
            zgOutboundFlightsChange.IsAutoScrollRange = true;

            // Tell ZedGraph to calculate the axis ranges
            // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
            // up the proper scrolling parameters
            zgOutboundFlightsChange.AxisChange();

            // Make sure the Graph gets redrawn
            zgOutboundFlightsChange.Invalidate();
        }

        private void textBoxStartPoint_TextChanged(object sender, EventArgs e)
        {
            zgReturnFlightsChange.GraphPane.Title.Text = "Flight costs for " + textBoxStartPoint.Text + " to " + textBoxEndPoint.Text;
            zgOutboundFlightsChange.GraphPane.Title.Text = "Flight costs for " + textBoxStartPoint.Text + " to " + textBoxEndPoint.Text;
            zgOutboundFlightsChange.Invoke(new UpdateGraphCallback(UpdateGraph));
        }

        private void textBoxEndPoint_TextChanged(object sender, EventArgs e)
        {
            zgReturnFlightsChange.GraphPane.Title.Text = "Flight costs for " + textBoxStartPoint.Text + " to " + textBoxEndPoint.Text;
            zgOutboundFlightsChange.GraphPane.Title.Text = "Flight costs for " + textBoxStartPoint.Text + " to " + textBoxEndPoint.Text;
            zgOutboundFlightsChange.Invoke(new UpdateGraphCallback(UpdateGraph));
        }

        private GraphPane DoGraphPane()
        {
            DateTime objSDateTime = dateTimePickerStart.Value.AddDays(-1);
            DateTime objEDateTime = dateTimePickerEnd.Value.AddDays(1);

            GraphPane myPane = new GraphPane();

            // Set the titles and axis labels
            myPane.YAxis.Title.Text = "Price (£)";

//            myPane.XAxis.Scale.MajorUnit = DateUnit.Day;

            myPane.XAxis.Type = AxisType.Date;

//            myPane.BarSettings.Type = BarType.SortedOverlay;
//            myPane.XAxis.Scale.MinAuto = false;
//            myPane.XAxis.Scale.MaxAuto = false;
            myPane.XAxis.Scale.Format = "dd-MM";
//            myPane.XAxis.Scale.Min = (double)new XDate(objSDateTime);
//            myPane.XAxis.Scale.Max = (double)new XDate(objEDateTime);

#if false
            // Generate a red curve with diamond symbols, and "Alpha" in the legend
            LineItem myCurve = myPane.AddCurve("Alpha",
                list, Color.Red, SymbolType.Diamond);
            // Fill the symbols with white
            myCurve.Symbol.Fill = new Fill(Color.White);

            // Generate a blue curve with circle symbols, and "Beta" in the legend
            myCurve = myPane.AddCurve("Beta",
                list2, Color.Blue, SymbolType.Circle);
            // Fill the symbols with white
            myCurve.Symbol.Fill = new Fill(Color.White);
            // Associate this curve with the Y2 axis
            myCurve.IsY2Axis = true;

            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = true;

            // Make the Y axis scale red
            myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
            myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
            // turn off the opposite tics so the Y tics don't show up on the Y2 axis
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.MinorTic.IsOpposite = false;
            // Don't display the Y zero line
            myPane.YAxis.MajorGrid.IsZeroLine = false;
            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.Scale.Align = AlignP.Inside;
            // Manually set the axis range
            myPane.YAxis.Scale.Min = -30;
            myPane.YAxis.Scale.Max = 30;

            // Enable the Y2 axis display
            myPane.Y2Axis.IsVisible = true;
            // Make the Y2 axis scale blue
            myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
            myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            myPane.Y2Axis.MajorTic.IsOpposite = false;
            myPane.Y2Axis.MinorTic.IsOpposite = false;
            // Display the Y2 axis grid lines
            myPane.Y2Axis.MajorGrid.IsVisible = true;
            // Align the Y2 axis labels so they are flush to the axis
            myPane.Y2Axis.Scale.Align = AlignP.Inside;

            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

#endif
            // Add a text box with instructions
            TextObj text = new TextObj(
                "Zoom: left mouse & drag\nPan: middle mouse & drag\nContext Menu: right mouse\nSave As Image: On Context Menu\nCopyright Dynamic Devices 2006",
                0.02f, 0.95f, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom);
            text.FontSpec.StringAlignment = StringAlignment.Near;
  //          myPane.GraphObjList.Add(text);


            // OPTIONAL: Show tooltips when the mouse hovers over a point
            //            zg1.IsShowPointValues = true;
            //            zg1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);

            // OPTIONAL: Add a custom context menu item
            //            zg1.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(
            //                            MyContextMenuBuilder);

            // OPTIONAL: Handle the Zoom Event
            //            zg1.ZoomEvent += new ZedGraphControl.ZoomEventHandler(MyZoomEvent);

            // Size the control to fit the window
            //            SetSetSize();

            return myPane;
        }

        private void checkBoxReturnDates_CheckedChanged(object sender, EventArgs e)
        {
//            if (checkBoxReturnDates.Checked)
//                zgOutboundFlightsChange.Width = 453;
//            else
//                zgOutboundFlightsChange.Width = 926;
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            AboutForm objAboutForm = new AboutForm();

            objAboutForm.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (objQueryThread != null)
                objQueryThread.Abort();

            foreach (Query objQuery in _arrQueries)
            {
                objQuery.Dispose();
            }
            _arrQueries.Clear();
        }
    }
}