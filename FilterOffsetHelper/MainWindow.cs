using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FilterOffsetHelper
{
    public partial class MainWindow : Form
    {
        private HelperState helperState;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            helperState = new HelperState();
        }

        private void focusmaxConnectButton_Click(object sender, EventArgs e)
        {
            if (!helperState.focusmaxConnected)
            {
                helperState.focusmaxConnect();
                focusmaxConnectButton.Text = "Disconnect FocusMax";

                if (helperState.filterwheelConnected)
                {
                    measureButton.Enabled = true;
                }
            }
            else
            {
                measureButton.Enabled = false;
                helperState.focusmaxDisconnect();
                focusmaxConnectButton.Text = "Connect to FocusMax";
            }
        }

        private void filterwheelConnectButton_Click(object sender, EventArgs e)
        {
            if (!helperState.filterwheelConnected)
            {
                helperState.filterwheelConnect();
                filterwheelConnectButton.Text = "Disconnect filterwheel";
                filterListBox.Enabled = true;
                referenceComboBox.Enabled = true;
                string[] filters = helperState.getFilters();
                if (filters != null)
                {
                    filterListBox.Items.AddRange(filters);
                    referenceComboBox.Items.AddRange(filters);
                }

                if (helperState.focusmaxConnected)
                {
                    measureButton.Enabled = true;
                }
            }
            else
            {
                measureButton.Enabled = false;
                helperState.filterwheelDisconnect();
                filterListBox.Items.Clear();
                referenceComboBox.Items.Clear();
                filterListBox.Enabled = false;
                referenceComboBox.Enabled = false;
                filterwheelConnectButton.Text = "Connect to filterwheel";
            }
        }

        private void measureButton_Click(object sender, EventArgs e)
        {
            if (referenceComboBox.SelectedIndex == -1)
            {
                // Nothing selected as reference
                MessageBox.Show("Please select a reference filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }
    }
}
