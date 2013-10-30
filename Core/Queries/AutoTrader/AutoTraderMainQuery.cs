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
    public class AutoTraderMainQuery : Query
    {
        #region Constants

        private int MAX_RECORDS = 200;

        #endregion

        #region Fields

        private int _iPageNo = 0;
        private string _strPostCode = "";
        private int _iMiles;

        #endregion

        #region Constructor

        public AutoTraderMainQuery(string strPostCode, int iMiles)
        {
            _strPostCode = strPostCode;
            _iMiles = iMiles;

            // Search all trade cars sold within x miles of y postcode

            OnTidiedData += new TidiedDataHandler(tidiedData);
        }

        #endregion

        #region Properties

        public override string QueryString
        {
            get
            {
                return "http://" + Domain + Path + "page=" + _iPageNo + "&modelexact=1&lid=search_used_cars_full&make=ANY&model=ANY&min_pr=500&max_pr=&mileage=&agerange=&postcode=" + _strPostCode + "&miles=" + _iMiles + "&max_records=" + MAX_RECORDS + "&source=2&photo=1&sort=4&ukcarsearch_full=SEARCH";
            }
        }

        public override string Domain
        {
            get
            {
                return "atsearch.autotrader.co.uk";
            }
        }

        public override string Path
        {
            get
            {
                return "/WWW/cars_search.asp?";
            }
        }

        #endregion

        #region Implementation

        private void tidiedData(Query q, XmlDocument objXmlDocument)
        {
            // Grab results number
            XmlNode objNodeIndex = objXmlDocument.SelectSingleNode("/html/body/div/table[2]/tr[1]/td[1]/b");
            string strIndex = objNodeIndex.InnerText;
            string[] strIndexInfo = strIndex.Split(new char[] { ' ' });

            int iFirstResult;
            int iLastResult; 
            int iTotalResults;

            iFirstResult = int.Parse(strIndexInfo[1]);
            iLastResult = int.Parse(strIndexInfo[3]);
            iTotalResults = int.Parse(strIndexInfo[5]);

            if (iTotalResults == MAX_RECORDS)
            {
                // FIXME: Throw an exception ?
                Debug.WriteLine("Warning: returned record count is equal to MAX_RECORDS. Query is too general");
            }

            if (iLastResult < iTotalResults)
                _bMoreResults = true;

            // Grab list from this page
            XmlNodeList objNodeShortformList = objXmlDocument.SelectNodes("//div[@class='results']//table[@class='standardListAd']/tr/td/b/a");
            XmlNodeList objNodeLongformList = objXmlDocument.SelectNodes("//div[@class='results']//table[@class='standardListAd']/tr/td[@class='listAdTextAreaPhoto']");
            XmlNodeList objNodePriceList = objXmlDocument.SelectNodes("//div[@class='results']//table[@class='standardListAd']/tr/td[@class='priceCol']/b");
            XmlNodeList objNodePicList = objXmlDocument.SelectNodes("//div[@class='results']//table[@class='standardListAd']/tr/td[@class='photoColWithPhoto']");
            XmlNodeList objMoreInfoList = objXmlDocument.SelectNodes("//div[@class='results']//table[@class='standardListAd']/tr/td/b/a/@href");

            for(int i = 0; i < objNodeLongformList.Count; i++)
            {
                QueryResponse objResponse = new QueryResponse();
    
                string strPicURL;
                XmlNode objNode = objNodePicList[i]["img"];
                if(objNode != null)
                    strPicURL = objNode.Attributes["src"].Value;
                else
                    strPicURL = "";

                string strShortform = objNodeShortformList[i].InnerText;
                string strLongform = objNodeLongformList[i].InnerText;
                
                string [] strMoreInfoElements = objMoreInfoList[i].Value.Split( new char[] { ',', '(', ')' } );

                string strAdID = strMoreInfoElements[1];
                string strDist = strMoreInfoElements[2];
                string strCat = strMoreInfoElements[3];
                string strPos = strMoreInfoElements[4];
                
                string strURL = "http://" + _strDomain + "/WWW/CARS_popup.asp?modelexact=1&lid=search_used_cars_full&make=ANY&model=ANY&min_pr=500&max_pr=&mileage=&agerange=&postcode=L23%203DP&miles=10&max_records=&source=2&photo=1&sort=4&ukcarsearch_full=SEARCH&start="
                                + strPos + "&distance=" + strDist + "&adcategory=" + strCat +
                                "&channel=CARS&id=" + strAdID;

                // Knock out all superfluous spaces
                while (strShortform.IndexOf("  ") >= 0)
                    strShortform = strShortform.Replace("  ", " ");
                while (strLongform.IndexOf("  ") >= 0)
                    strLongform = strLongform.Replace("  ", " ");

                // Knock out new lines
                strShortform = strShortform.Replace("\r\n", "");
                strLongform = strLongform.Replace("\r\n", "");

                string strNodePrice = objNodePriceList[i].InnerText;
                strNodePrice = strNodePrice.Replace(",", "");

#if false
                Debug.WriteLine("Got: " + strPicURL + ", " + strLongform + ", " + strNodePrice);
#endif

                objResponse.Add("PictureURL", strPicURL);
                objResponse.Add("ShortForm", strShortform);
                objResponse.Add("Details", strLongform);
                objResponse.Add("Price", strNodePrice);
                objResponse.Add("MoreInfoURL", strURL);

                _arrResponses.Add(objResponse);

            }

            // Go get next set of data ?
            if (_bMoreResults)
            {
                _iPageNo++;

                // Increment the page number and do the query again
                doQuery();
            }

            if (OnResults != null)
                OnResults(this, Responses);
        }

        #endregion

    }
}
