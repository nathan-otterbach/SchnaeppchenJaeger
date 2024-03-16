using SchnaeppchenJaeger.Client;
using System;

namespace SchnaeppchenJaeger
{
    public partial class Form1 : Form
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private Mode _currentMode = Mode.Manual;

        private enum Mode
        {
            Manual,
            Automatic
        }

        public Form1()
        {
            InitializeComponent();

            _cancellationTokenSource = new CancellationTokenSource();
            UpdateUIForMode();
        }

        private async void button_test_Click(object sender, EventArgs e)
        {
            // grab zip from user input
            // grab search term from user input or sql database
            var searchTerm = textBox_product.Text; // Example: Fetch search term from a text box
            var _client = new ApiClient(74564, searchTerm);

            var result = await _client.GetOffersAsync(_cancellationTokenSource.Token);
        }

        private void checkBox_modus_CheckedChanged(object sender, EventArgs e)
        {
            ToggleMode();
            UpdateUIForMode();
        }

        #region Helper Methods

        private void ToggleMode()
        {
            switch (_currentMode)
            {
                case Mode.Manual:
                    _currentMode = Mode.Automatic;
                    break;
                case Mode.Automatic:
                    _currentMode = Mode.Manual;
                    break;
            }
        }

        private void UpdateUIForMode()
        {
            switch (_currentMode)
            {
                case Mode.Manual:
                    label_modus_indicator.Text = "Manuell";
                    label_modus_indicator.ForeColor = Color.Red;

                    groupBox_modus_manual.Enabled = true;
                    groupBox_modus_manual.Visible = true;
                    groupBox_modus_automatic.Enabled = false;
                    groupBox_modus_automatic.Visible = false;
                    break;

                case Mode.Automatic:
                    label_modus_indicator.Text = "Automatik";
                    label_modus_indicator.ForeColor = Color.Green;

                    groupBox_modus_manual.Enabled = false;
                    groupBox_modus_manual.Visible = false;
                    groupBox_modus_automatic.Enabled = true;
                    groupBox_modus_automatic.Visible = true;
                    break;
            }
        }

        #endregion
    }
}