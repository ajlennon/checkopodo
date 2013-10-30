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
    public class EdwardsAndBrazierMainQuery : Query
    {
        #region Constants

        private int MAX_RECORDS = 200;

        #endregion

        #region Fields

        private int _iPageNo = 0;
        private string _strSalesRental = "S";
        private string _strQType = "2";
        private string _strArea = "Crosby";
        private string _strNumBeds = "1";
        private string _strPriceLow = "40000";
        private string _strPriceHigh = "250000";
        private string _strKeywords = "";
        private string _strXstyle = "";
                
        #endregion

        #region Constructor

        public EdwardsAndBrazierMainQuery()
        {
            OnTidiedData += new TidiedDataHandler(tidiedData);

            _strType = "POST";
            _strPostData = "sales_rental=S&type=&area=Crosby&num_beds=1&price_low=40000&price_high=250000&keywords=&xstyle=";
        }

        #endregion

        #region Properties

        public override string QueryString
        {
            get
            {
                return "http://" + Domain + Path + "elb1&imagesize=120,fo=s,fof=iarea,fof=ikeyword&1";
            }
        }

        public override string Domain
        {
            get
            {
                return "housescape.org.uk";
            }
        }

        public override string Path
        {
            get
            {
                return "/cgi-bin/search.pl?";
            }
        }

        #endregion

        #region Implementation

        private void tidiedData(Query q, XmlDocument objXmlDocument)
        {
            // Grab results number
            XmlNode objNodeIndex = objXmlDocument.SelectSingleNode("/html/body/table[1]/tr[1]/td[1]/font/span");
            string strIndex = objNodeIndex.InnerText;
            string[] strIndexInfo = strIndex.Split(new char[] { ' ' });

            int iFirstResult;
            int iLastResult; 
            int iTotalResults;

            iFirstResult = int.Parse(strIndexInfo[27].TrimEnd());
            iLastResult  = int.Parse(strIndexInfo[29].TrimEnd());
            iTotalResults = int.Parse(strIndexInfo[3].TrimEnd());

            if (iTotalResults == MAX_RECORDS)
            {
                // FIXME: Throw an exception ?
                Debug.WriteLine("Warning: returned record count is equal to MAX_RECORDS. Query is too general");
            }

            if (iLastResult < iTotalResults)
                _bMoreResults = true;

            // Grab list from this page
            XmlNodeList objNodeFullDetails = objXmlDocument.SelectNodes("/html/body/table/tr/td[2]/a[1]");

            for(int i = 0; i < objNodeFullDetails.Count; i++)
            {
                XmlNode objHouseNode = objNodeFullDetails[i];

                string strDetailsURL = objHouseNode.Attributes["href"].Value;

                Console.WriteLine("Got href: " + strDetailsURL);

#if false
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
#endif
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
