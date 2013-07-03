using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASCOM.Utilities;
using ASCOM.DriverAccess;
using FocusMax;

namespace FilterOffsetHelper
{
    class HelperState
    {
        private bool focusmaxConnected;
        private bool filterwheelConnected;
        private FocusControl focusmaxControl;
        private FilterWheel filterWheel;
        private string filterWheelProgId;
        public HelperState()
        {
            focusmaxConnected = false;
            filterwheelConnected = false;
        }

        public void focusmaxConnect()
        {
            if (!focusmaxConnected)
            {
                focusmaxControl = new FocusControl();
            }
        }

        public void filterwheelConnect()
        {
            if (!filterwheelConnected)
            {
                filterWheelProgId = FilterWheel.Choose(null);
                filterWheel = new FilterWheel(filterWheelProgId);
            }
        }
    }
}
