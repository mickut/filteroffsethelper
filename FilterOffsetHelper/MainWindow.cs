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
            int referenceIndex = referenceComboBox.SelectedIndex;
            if (referenceIndex == -1)
            {
                // Nothing selected as reference
                MessageBox.Show("Please select a reference filter!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (filterListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show("No measurable filters selected!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            focusmaxConnectButton.Enabled = false;
            filterwheelConnectButton.Enabled = false;
            filterListBox.Enabled = false;

            List<String> filters = helperState.getFilters().ToList();

            foreach (string filter in filterListBox.CheckedItems)
            {
                // Main measurement loop
                // TODO: Implement iterations
                int targetIndex = filters.IndexOf(filter);
                helperState.setCurrentFilter(referenceIndex);
                helperState.focus();
                int referencePosition = helperState.getFocuserPosition();
                helperState.setCurrentFilter(targetIndex);
                helperState.focus();
                int targetPosition = helperState.getFocuserPosition();
                int difference = referencePosition - targetPosition; // Is this the right way around?
                // The difference data needs to be saved in a file
            }

            focusmaxConnectButton.Enabled = true;
            filterwheelConnectButton.Enabled = true;
            filterListBox.Enabled = true;
        }

        private void referenceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox referenceBox = (ComboBox)sender;
            int index = referenceBox.SelectedIndex;
            string[] filters = helperState.getFilters();
            filterListBox.Items.Clear();
            filterListBox.Items.AddRange(filters);
            filterListBox.Items.RemoveAt(index);
        }
    }
}