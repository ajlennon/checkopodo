using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicDevices.DataRepurposing.Queries
{
    public class CarQuery : Query
    {
        #region Constants

        public enum EnumCarType
        {
            NEW,
            USED,
            SERVICE,
        }

        #endregion

        #region Fields

        protected EnumCarType _eCarType;

        #endregion

        #region Constructor

        public CarQuery(EnumCarType eCarType)
        {
            _eCarType = eCarType;
        }

        #endregion

        #region Properties

        public EnumCarType CarType
        {
            get
            {
                return _eCarType;
            }
            set
            {
                _eCarType = value;
            }
        }

        #endregion

    }
}
