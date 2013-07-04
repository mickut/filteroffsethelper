﻿using System;
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
        private bool _focusmaxConnected;
        private bool _filterwheelConnected;
        private FocusControl focusmaxControl;
        private FilterWheel filterWheel;
        private string filterWheelProgId;
        public HelperState()
        {
            _focusmaxConnected = false;
            _filterwheelConnected = false;
        }

        public bool focusmaxConnected
        {
            get { return _focusmaxConnected; }
        }

        public bool filterwheelConnected
        {
            get { return _filterwheelConnected; }
        }

        public void focusmaxConnect()
        {
            if (!_focusmaxConnected)
            {
                focusmaxControl = new FocusControl();
                _focusmaxConnected = true;
            }
        }

        public void focusmaxDisconnect()
        {
            if (_focusmaxConnected)
            {
                focusmaxControl = null;
                _focusmaxConnected = false;
            }
        }

        public void filterwheelConnect()
        {
            if (!_filterwheelConnected)
            {
                filterWheelProgId = FilterWheel.Choose(null);
                filterWheel = new FilterWheel(filterWheelProgId);
                filterWheel.Connected = true;
                _filterwheelConnected = true;
            }
        }

        public void filterwheelDisconnect()
        {
            if (_filterwheelConnected)
            {
                filterWheel.Connected = false;
                filterWheel.Dispose();
                filterWheel = null;
                _filterwheelConnected = false;
            }
        }

        public string[] getFilters()
        {
            if (_filterwheelConnected)
            {
                return filterWheel.Names;
            }
            else
            {
                return null;
            }
        }

        public void setCurrentFilter(int filterIndex)
        {
            if (_filterwheelConnected)
            {
                try
                {
                    filterWheel.Position = (short)filterIndex;
                }
                catch (ASCOM.InvalidValueException e)
                {
                    return;
                }
            }
        }
    }
}
