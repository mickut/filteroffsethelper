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

        public void focus()
        {
            /* Focus asynchronously using FocusMax. */
            if (_focusmaxConnected)
            {
                focusmaxControl.FocusAsync();
            }
        }

        public int getFocuserStatus()
        {
            /* 0 means operation failed,
             * 1 means operation succeeded,
             * -1 means operation is in progress
             * and -2 means FocusMax is not connected. */
            if (_focusmaxConnected)
            {
                return (int)focusmaxControl.FocusAsyncStatus;
            }
            else
            {
                return -2;
            }
        }

        public void haltFocuser()
        {
            if (_focusmaxConnected)
            {
                focusmaxControl.Halt();
            }
        }

        public int getFocuserPosition()
        {
            if (_focusmaxConnected)
            {
                return focusmaxControl.Position;
            }
            else
            {
                return -1;
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
                    System.Windows.Forms.MessageBox.Show("Invalid index passed to the filter wheel!", "Error", System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Asterisk);
                    return;
                }
            }
        }
    }
}
