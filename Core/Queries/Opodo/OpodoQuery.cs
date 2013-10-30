using System;
using System.Text;
using System.Net;
using System.Threading;
using System.Web;
using System.IO;

using System.Diagnostics;

namespace DynamicDevices.DataRepurposing.Queries
{
    public class OpodoQuery : Query
    {
        #region Fields

        private string _strStartPoint;
        private string _strEndPoint;
        private string _strSDate;
        private string _strEDate;
        private string _strSYearMonth;
        private string _strEYearMonth;

        #endregion

        #region Constructor

        public OpodoQuery(string strStartPoint, string strEndPoint, string strSDate, string strSMonth, string strSYear, string strEDate, string strEMonth, string strEYear)
        {

            if (strSDate.Length == 1)
                strSDate = "0" + strSDate;
            if (strEDate.Length == 1)
                strEDate = "0" + strEDate;
            if (strSMonth.Length == 1)
                strSMonth = "0" + strSMonth;
            if (strEMonth.Length == 1)
                strEMonth = "0" + strEMonth;
            
            _strSYearMonth = strSYear + strSMonth;
            _strEYearMonth = strEYear + strEMonth;
            _strStartPoint = strStartPoint;
            _strEndPoint = strEndPoint;
            _strSDate = strSDate;
            _strEDate = strEDate;
        }

        #endregion

        #region Properties

        public override string QueryString
        {
            get
            {
//                return "http://" + Domain + Path + "?RMSCHID=RMSCHID&RTRIP_TYPE=R&DEFAULT_FIELDS=&MULTISTOP_SUBMIT=N&TRIP_TYPE=R&B_LOCATION_1=" + _strStartPoint + "&E_LOCATION_1=" + _strEndPoint + "&B1_DAY=" + _strSDate + "&B1_MONTH=" + _strSYearMonth + "&B_TIME_TO_PROCESS_1=ANY&B2_DAY=" + _strEDate + "&B2_MONTH=" + _strEYearMonth + "&B_TIME_TO_PROCESS_2=ANY&CABIN=E&PREF_AIRLINE_1_DDN_EXISTS=true&INC_AIRLINE_1=&AIRLINE_SELECTED_IN_POPUP=&SELECTED_AIRLINE_NAME=&NUM_OF_ADTS=1&NUM_OF_CHD=0&NUM_OF_INFS=0";

                return "http://" + Domain + Path + "?reset=true&departureAirportCode=" + _strStartPoint + "&arrivalAirportCode=" + _strEndPoint + "&tripType=R&searchLowCost=true"
                    + "&departureDay=" + _strSDate + "&departureMonth=" + _strSYearMonth + "&departureTime=ANY"
                    + "&returnDay=" + _strEDate + "&returnMonth=" + _strEYearMonth + "&returnTime=ANY&flexible=true&numberOfAdults=1&numberOfChildren=0&numberOfInfants=0&cabinType=E";
            }
        }

        public override string Domain
        {
            get
            {
                return "www.opodo.co.uk";
            }
        }

        public override string Path
        {
            get
            {
                return "/opodo/flights/search";
            }
        }

        public DateTime StartDateTime
        {
            get
            {
                return new DateTime( int.Parse(_strSYearMonth.Substring(0,4)), int.Parse(_strSYearMonth.Substring(4, 2)), int.Parse(_strSDate));
            }
        }

        public DateTime EndDateTime
        {
            get
            {
                return new DateTime( int.Parse(_strEYearMonth.Substring(0,4)), int.Parse(_strEYearMonth.Substring(4, 2)), int.Parse(_strEDate));
            }
        }

        #endregion

        #region Implementation

        #endregion

        }
}
