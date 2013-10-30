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
    public class VolkswagenRetailerQuery : CarQuery
    {
        #region Constants

        #endregion

        #region Fields

        private string _strPostCode = "";

        #endregion

        #region Constructor

        /// <summary>
        /// Search on specific dealer
        /// </summary>
        /// <param name="iDealerNo"></param>
        public VolkswagenRetailerQuery(EnumCarType eCarType, string strPostCode) : base(eCarType)
        {
            _strPostCode = strPostCode;

            OnTidiedData += new TidiedDataHandler(tidiedData);
            OnCustomRxData += new CustomRxDataFilter(filterRxData);
        }

        #endregion

        #region Properties

        public override string QueryString
        {
            get
            {
                    string strQuery = "http://" + Domain + Path + "?search_type=new";
                    if (_eCarType == EnumCarType.NEW)
                        strQuery += "&search_main=new";
                    else if (_eCarType == EnumCarType.SERVICE)
                        strQuery += "&search_main=service";
                    else if (_eCarType == EnumCarType.USED)
                        strQuery += "&search_main=used";

                    strQuery += "&postcode=" + System.Web.HttpUtility.UrlEncode(_strPostCode);

                    return strQuery;
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
                return "/RetailerSearch";
            }
        }

        #endregion

        #region Implementation

        private void filterRxData(string strRxData)
        {
            // Replace duplicate Volkswagen attribute
            int iVolkIndex1 = strRxData.IndexOf("Volkswagen=");
            if (iVolkIndex1 >= 0)
                strRxData = strRxData.Substring(0, iVolkIndex1) + "X" + strRxData.Substring(iVolkIndex1);
        }

        private void tidiedData(Query q, XmlDocument objXmlDocument)
        {
            // Only deal with one page for now (I think there is only one)
            _bMoreResults = false;
            
            // Grab sub frame
            XmlNodeList objNodeListRetailerNames = objXmlDocument.SelectNodes("/html/body/div/table/tr/td/table/tr/td/table/tr/td/a[@href!='/retailers']");
            XmlNodeList objNodeListDistances = objXmlDocument.SelectNodes("/html/body/div/table/tr/td/table/tr/td/table/tr/td/a[@href!='/retailers']/../../td[@class='indent'][@align='right']");

            // Check
            if (objNodeListDistances == null || objNodeListRetailerNames == null)
                throw new Exception("Big problems - can't extract data");

            for (int i = 0; i < objNodeListRetailerNames.Count; i++)
            {
                string strURL = TidyString(objNodeListRetailerNames[i].Attributes["href"].Value);
                string strName = TidyString(objNodeListRetailerNames[i].InnerText);
                string strDistance = TidyString(objNodeListDistances[i].InnerText);

                // CHANGE: Need a better way to extract ID
                string strID = strURL.Substring(strURL.IndexOf("dealer_no=") + 10);

                QueryResponse objResponse = new QueryResponse();

                objResponse.Add("URL", "http://" + Domain + strURL);
                objResponse.Add("NAME", strName);
                objResponse.Add("DISTANCE", strDistance);
                objResponse.Add("ID", strID);

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
