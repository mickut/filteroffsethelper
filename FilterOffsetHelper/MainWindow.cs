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
        private bool measuringInProgress;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            helperState = new HelperState();
            measuringInProgress = false;
            /* Disabling CheckForIllegalCrossThreadCalls makes the code much simpler,
             * but risks thread safety. There shouldn't be any problems in this case, though. */
            Control.CheckForIllegalCrossThreadCalls = false;
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
            if (measuringInProgress)
            {
                this.measureBackgroundWorker.CancelAsync();
                this.measureButton.Text = "Stopping...";
                return;
            }

            if (referenceComboBox.SelectedIndex == -1)
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
            iterationNumeric.Enabled = false;
            measureButton.Text = "Stop measuring!";

            this.measuringInProgress = true;

            this.measureBackgroundWorker.RunWorkerAsync();            
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

        private void measureBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            /* Make a separate thread for the measurement, so the GUI won't be frozen. */
            BackgroundWorker worker = sender as BackgroundWorker;
            int referenceIndex = referenceComboBox.SelectedIndex;
            List<String> filters = helperState.getFilters().ToList();
            int iterations = (int)iterationNumeric.Value;

            foreach (string filter in filterListBox.CheckedItems)
            {
                // Main measurement loop
                int targetIndex = filters.IndexOf(filter);
                for (int iteration = 0; iteration < iterations; iteration++)
                {
                    helperState.setCurrentFilter(referenceIndex);
                    helperState.focus();
                    System.Threading.Thread.Sleep(100);
                    while (helperState.getFocuserStatus() != 1)
                    {
                        if (worker.CancellationPending || helperState.getFocuserStatus() == 0)
                        {
                            helperState.haltFocuser();
                            return;
                        }
                        System.Threading.Thread.Sleep(500);
                    }
                    int referencePosition = helperState.getFocuserPosition();
                    helperState.setCurrentFilter(targetIndex);
                    helperState.focus();
                    System.Threading.Thread.Sleep(100);
                    while (helperState.getFocuserStatus() != 1)
                    {
                        if (worker.CancellationPending || helperState.getFocuserStatus() == 0)
                        {
                            helperState.haltFocuser();
                            return;
                        }
                        System.Threading.Thread.Sleep(500);
                    }
                    int targetPosition = helperState.getFocuserPosition();
                    int difference = referencePosition - targetPosition; // Is this the right way around?
                    // The difference data needs to be saved in a file
                }
            }
        }

        private void measureBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            /* Run this function when the measuring process has ended. */
            focusmaxConnectButton.Enabled = true;
            filterwheelConnectButton.Enabled = true;
            filterListBox.Enabled = true;
            iterationNumeric.Enabled = true;
            measureButton.Text = "Measure!";
            this.measuringInProgress = false;
        }
    }
}