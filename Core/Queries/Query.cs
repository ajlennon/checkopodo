using System;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Xml;
using Tidy;

namespace DynamicDevices.DataRepurposing.Queries
{
    public class Query
    {
        #region Fields

        protected string _strDomain;
        
        private string _strQuery;

        protected string _strType = "GET";
        protected string _strPostData = "";

        protected ArrayList _arrResponses = new ArrayList();

        protected bool _bMoreResults;

        private int _iQueryTimeoutMs = 60000;
        private int _iQueriesMax = 1;

        private Exception _objLastException;

        private Thread _objWorker;

        private bool _bDebugHtmlTidyMessages = false;

        private bool _bStoreReponseFiles = false;

        #endregion

        #region Constructor

        #endregion

        #region Events

        public delegate void ErrorHandler(Query thisQuery, Exception e);
		public ErrorHandler OnError;

		public delegate void RxPageHandler(Query thisQuery, string strRxData);
        public RxPageHandler OnReceivedPage;

        public delegate void TidiedDataHandler(Query thisQuery, XmlDocument objXmlDoc);
        public TidiedDataHandler OnTidiedData;

        public delegate void ResultsHandler(Query thisQuery, QueryResponse[] arrResponses);
        public ResultsHandler OnResults;

        public delegate void CustomRxDataFilter(string strRxData);
        public CustomRxDataFilter OnCustomRxData;

//        public delegate void ParsedDataHandler(Query thisQuery, Hashtable htRxData);
//        public RxPageHandler OnParsedData;

        #endregion

        #region Methods

        /**
         * Start the query running and return
         */
        public void Start()
        {
            if (_objWorker != null)
                throw new Exception("Already started");

            _objWorker = new Thread(new ThreadStart(Worker));
            _objWorker.Start();
        }

        /**
         * Start the query running and block until it completes
         */
        public QueryResponse[] GetResponses()
        {
            OnError = new ErrorHandler(objDoc_OnError);

            Start();

            _objWorker.Join();

            if (_objLastException != null)
                throw _objLastException;

            return Responses;
        }

        #endregion

        #region Properties

        public bool StoreReponseFiles
        {
            get
            {
                return _bStoreReponseFiles;
            }
            set
            {
                _bStoreReponseFiles = value;
            }
        }

        public virtual string QueryString
        {
            get
            {
                return _strQuery;
            }
            set
            {
                _strQuery = value;
            }
        }

        public virtual string Domain
        {
            get
            {
                throw new Exception("Not implemented");
            }
            set
            {
                throw new Exception("Not implemented");
            }
        }

        public virtual string Path
        {
            get
            {
                throw new Exception("Not implemented");
            }
            set
            {
                throw new Exception("Not implemented");
            }
        }

        public bool DebugHtmlTidyMessages
        {
            get
            {
                return _bDebugHtmlTidyMessages;
            }
            set
            {
                _bDebugHtmlTidyMessages = value;
            }
        }

        public QueryResponse[] Responses
        {
            get
            {
                return (QueryResponse[])_arrResponses.ToArray(typeof(QueryResponse));
            }
        }

        private int TimeoutMs
        {
            get
            {
                return _iQueryTimeoutMs;
            }
            set
            {
                _iQueryTimeoutMs = value;
            }
        }

        private string Type
        {
            get
            {
                return _strType;
            }
            set
            {
                _strType = value;
            }
        }

        /**
         * Dispose of the query, closing down any running threads
         */
        public void Dispose()
        {
            if (_objWorker != null)
                _objWorker.Abort();
        }

        public static string XmlEscape(string s)
        {
            s = s.Replace("&", "&amp;");
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&gt;");
            s = s.Replace("'", "&apos;");
            s = s.Replace("\"", "&quot;");

            return s;
        }

        public static string TidyString(string s)
        {
            // Knock out all superfluous spaces
            while (s.IndexOf("  ") >= 0)
                s = s.Replace("  ", " ");
            s = s.Replace("\r\n", "");

            return s;
        }

        public static XmlDocument GetTidiedData(string strURL)
        {
            XmlDocument objXmlResponseDoc = null;

            Query objQuery = new Query();
            objQuery.QueryString = strURL;
            objQuery.OnTidiedData = new TidiedDataHandler( delegate(Query q, XmlDocument objDoc) 
            {
                objXmlResponseDoc = objDoc;
            });

            objQuery.Start();

            objQuery._objWorker.Join();

            return objXmlResponseDoc;
        }

        #endregion

        #region Implementation

        private void Worker()
        {
            OnReceivedPage += new RxPageHandler(receivedPage);

            doQuery();
        }

        protected bool doQuery()
        {
            bool fIsDone = false;
            int iQueryRetries = 0;
            
            while (!fIsDone)
            {
                // Have we hit the maximum number of retries?
                if (_iQueriesMax > 0 && iQueryRetries++ >= _iQueriesMax)
                {
                    fIsDone = true;

                    if (OnError != null)
                        OnError(this, new Exception("Timed out"));

                    continue;
                }

                try
                {
                    Debug.WriteLine("Querying: " + QueryString);

                    HttpWebRequest objReq = (HttpWebRequest)WebRequest.Create(QueryString);
                    objReq.ReadWriteTimeout = _iQueryTimeoutMs;
                    objReq.Method = _strType;
                    
                    // TODO: Investigate why we need to add cookie containers for some sites
                    objReq.CookieContainer = new CookieContainer();

                    if (_strType == "POST")
                    {
                        byte[] bArrTxBytes = System.Text.Encoding.ASCII.GetBytes(_strPostData);

                        objReq.ContentType = "application/x-www-form-urlencoded";
                        objReq.ContentLength = bArrTxBytes.Length;

                        Stream objTxStream = objReq.GetRequestStream();
                        objTxStream.Write(bArrTxBytes, 0, bArrTxBytes.Length);
                        objTxStream.Close();
                    }

                    // Get response stream
                    HttpWebResponse objRsp = (HttpWebResponse)objReq.GetResponse();

                    // Check that response type is correct
                    if (objRsp.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("Got response error: " + objRsp.StatusCode);
                    }

                    StreamReader objReader = new StreamReader(objRsp.GetResponseStream());

                    string strRsp = objReader.ReadToEnd();

                    objReader.Close();

                    if (OnReceivedPage != null)
                    {
                        fIsDone = true;
                        OnReceivedPage(this, strRsp);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Got exception " + e.Message);
                }
            }
            return fIsDone;
        }

        private void receivedPage(Query q, string strRxData)
        {
            string strFilename = GetType() + "_" + DateTime.Now.Ticks;

            // FIXME: Why can't we simply save the string?
            if (_bStoreReponseFiles)
            {
                Debug.WriteLine("Saving to: " + strFilename + ".html");

                FileStream outStream = File.Create(strFilename + ".html");
                StreamWriter objWriter = new StreamWriter(outStream);
                objWriter.Write(strRxData);
                objWriter.Close();
                outStream.Close();
            }

            // Discard non-breaking space
            strRxData = strRxData.Replace("&nbsp;", " ");
            
            // CHECK: Best way to do this? Strip out to first <HTML>
            int iStart = strRxData.ToLower().IndexOf("<html>");
            if(iStart > 0)
                strRxData = strRxData.Substring(iStart);
                
            // Custom fixes?
   //         if (OnCustomRxData != null)
   //             OnCustomRxData(strRxData);

            // Got the data so now clean it.
            var objDoc = new Document();

            // Set message handler
            objDoc.OnMessage += objDoc_OnMessage;
                
               
            int err_code = objDoc.ParseString(strRxData);
            if (err_code < 0)
                throw new Exception("Unable to parse data: ");

            objDoc.SetOptBool(TidyOptionId.TidyXmlPIs, 1);
            objDoc.SetOptBool(TidyOptionId.TidyXmlOut, 1);
            objDoc.SetOptInt(TidyOptionId.TidyDoctypeMode, 0);

            // Custom fix for Edwards and Brazier...
            objDoc.SetOptValue(TidyOptionId.TidyCharEncoding, "utf8");

            // Set option to indent blocks automatically
            objDoc.SetOptInt(TidyOptionId.TidyIndentContent,
               2);

            // Set indent to 4 chars (as an example)
            objDoc.SetOptInt(TidyOptionId.TidyIndentSpaces,
               4);

            // Force an output
            objDoc.SetOptBool(TidyOptionId.TidyForceOutput, 1);

            // Parse the file
            err_code = objDoc.CleanAndRepair();

            if (err_code < 0)
                throw new Exception(
                   "Unable to clean/repair string: ");

            err_code = objDoc.RunDiagnostics();

            if (err_code < 0)
                throw new Exception(
                   "Unable to run diagnostics on string: ");

            XmlDocument objXMLDoc = new XmlDocument();

            // Store a query file
            Debug.WriteLine("Saving to: " + strFilename + ".xml");

            objDoc.SaveFile(strFilename + ".xml");

            objXMLDoc.Load(strFilename + ".xml");

            if(!_bStoreReponseFiles)
                File.Delete(strFilename + ".xml");

            if (OnTidiedData != null)
                OnTidiedData(this, objXMLDoc);

        }

        void objDoc_OnMessage(TidyReportLevel level, int line, int col, string message)
        {
            if(_bDebugHtmlTidyMessages)
                Debug.WriteLine(message);
        }

        void objDoc_OnError(Query q, Exception e)
        {
            _objLastException = e;
        }

        #endregion

    }
}