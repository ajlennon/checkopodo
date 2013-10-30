using System;
using System.Text;
using System.Net;
using System.Threading;
using System.Web;
using System.IO;
using System.Xml;
using System.Collections;
using System.Diagnostics;

namespace DynamicDevices.DataRepurposing.Queries
{
    public class VolkswagenCarQuery : CarQuery
    {
        

        #region Fields

        private int _iPageNo;
        private int _iDealerNo; 
        private string _strSubFrameQuery;

        #endregion

        #region Constructor

        /// <summary>
        /// Search on specific dealer
        /// </summary>
        /// <param name="iDealerNo"></param>
        public VolkswagenCarQuery(EnumCarType eType, int iDealerNo) : base(eType)
        {
            _iDealerNo = iDealerNo;
            _iPageNo = 0;

            OnTidiedData += new TidiedDataHandler(tidiedData);
        }
        
        #endregion

        #region Properties


        public override string QueryString
        {
            get
            {
                if (_strSubFrameQuery == null)
                    return "http://" + Domain + Path + "?page=" + _iPageNo + "&dealer_no=" + _iDealerNo;
                else
                    return _strSubFrameQuery;
            }
        }

        public override string Domain
        {
            get
            {
                return "www.volkswagen.co.uk";
            }
        }

        public override string Path
        {
            get
            {
                return "/templates/NewRetailer/PFC_frame.jsp";
            }
        }

        #endregion

        #region Implementation

        private void tidiedData(Query q, XmlDocument objXmlDocument)
        {
            // Grab sub frame
            XmlNode objNodeIndex = objXmlDocument.SelectSingleNode("/html/frameset/frameset/frame[@name='PFC']");

            // Are we in the main page - i.e. do we need to jump into the sub-frame ?
            if (objNodeIndex != null)
            {
                if (objNodeIndex.Attributes["src"] != null)
                {
                    _strSubFrameQuery = objNodeIndex.Attributes["src"].Value + "&page=" + ++_iPageNo;

                    objXmlDocument = GetTidiedData(_strSubFrameQuery);
                }
            }

            // Increment the page number and do the query again with a sub-frame query
            // FIXME: Do this better
            _strSubFrameQuery = _strSubFrameQuery.Replace("page=" + _iPageNo, "page=" + ++_iPageNo);
        
            // Grab images
            XmlNodeList objNodeListImages = objXmlDocument.SelectNodes("/html/body/form/table/tr/td/table/tr/td/table/tr/td/table/tr/td/a");
            XmlNodeList objNodeListDate = objXmlDocument.SelectNodes("/html/body/form/table/tr/td/table/tr/td/table/tr/td/table/tr/td/a/../../td[3]");
            XmlNodeList objNodeListMileage = objXmlDocument.SelectNodes("/html/body/form/table/tr/td/table/tr/td/table/tr/td/table/tr/td/a/../../td[4]");
            XmlNodeList objNodeListColor = objXmlDocument.SelectNodes("/html/body/form/table/tr/td/table/tr/td/table/tr/td/table/tr/td/a/../../td[5]");
            XmlNodeList objNodeListPrice = objXmlDocument.SelectNodes("/html/body/form/table/tr/td/table/tr/td/table/tr/td/table/tr/td/a/../../td[6]");
            XmlNode objNextNode = objXmlDocument.SelectSingleNode("/html/body/form/table/tr/td/table/tr/td/table/tr/td/p/a[@title='Next Page']");

            if (objNextNode != null)
                _bMoreResults = true;
            else
                _bMoreResults = false;

            for (int i = 0; i < objNodeListImages.Count; i++)
            {
                string strPicURL       = TidyString("/" + objNodeListImages[i].Attributes["href"].Value).Trim();
                string strShortform = TidyString(objNodeListImages[i].InnerText).Trim();
                string strDate      = TidyString(objNodeListDate[i].InnerText).Trim();
                string strMileage = TidyString(objNodeListMileage[i].InnerText).Trim();
                string strColor = TidyString(objNodeListColor[i].InnerText).Trim();
                string strPrice = TidyString(objNodeListPrice[i].InnerText).Trim();

                QueryResponse objResponse = new QueryResponse();

                Uri objUri = new Uri(_strSubFrameQuery);

                objResponse.Add("INFOURL", "http://" + objUri.Host  + strPicURL);
                objResponse.Add("DETAILS", strShortform);
                objResponse.Add("DATE", strDate);
                objResponse.Add("MILEAGE", strMileage);
                objResponse.Add("COLOR", strColor);
                objResponse.Add("PRICE", strPrice);

                _arrResponses.Add(objResponse);
            }

            // Go get next set of data ?
            if (_bMoreResults)
            {
                doQuery();
            }

            if (OnResults != null)
                OnResults(this, Responses);
        }

#endregion
        
    }
}
