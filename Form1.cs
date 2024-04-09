using SchnaeppchenJaeger.Client;
using SchnaeppchenJaeger.Database;

namespace SchnaeppchenJaeger
{
    public partial class Form1 : Form
    {
        private DatabaseHelper _dbHelper;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private Mode _currentMode = Mode.Manual;
        private Status_DB _statusDB = Status_DB.Disconnected;

        private enum Mode
        {
            Manual,
            Automatic
        }

        private enum Status_DB
        {
            Connected,
            Disconnected
        }

        public Form1()
        {
            InitializeComponent();

            _dbHelper = DatabaseHelper.Instance;
            UpdateDatabaseConnectionStatus();

            _cancellationTokenSource = new CancellationTokenSource();
            UpdateUIForMode();

            PopulateShoppingListComboBox();
        }

        private async void button_test_Click(object sender, EventArgs e)
        {
            // grab zip from user input
            // grab search term from user input or sql database
            var searchTerm = textBox_product.Text; // Example: Fetch search term from a text box

            using (var client = new ApiClient(74564, "coca cola"))
            {
                await client.GetOffersAsync(_cancellationTokenSource.Token);
            }
        }

        private void checkBox_modus_CheckedChanged(object sender, EventArgs e)
        {
            ToggleMode();
            UpdateUIForMode();
        }

        #region Helper Methods

        private void UpdateDatabaseConnectionStatus()
        {
            if (_dbHelper.IsDatabaseConnected())
            {
                _statusDB = Status_DB.Connected;
                label_db_status.Text = "Connected";
                label_db_status.ForeColor = Color.Green;
            }
            else
            {
                _statusDB = Status_DB.Disconnected;
                label_db_status.Text = "Disconnected";
                label_db_status.ForeColor = Color.Red;
            }
        }


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

        private string ReplaceSpecialCharacters(string tableName)
        {
            char[] specialCharacters = new char[] { ' ', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '{', '}', '[', ']', ';', ':', '\'', '"', ',', '.', '/', '\\', '<', '>', '?' };
            for (int i = 0; i < specialCharacters.Length; i++)
            {
                tableName = tableName.Replace(specialCharacters[i], '_');
            }

            return tableName;
        }

        private void PopulateShoppingListComboBox()
        {
            comboBox_lists.Items.Clear();
            List<string> tableNames = _dbHelper.GetShoppingListTables();
            comboBox_lists.Items.AddRange(tableNames.ToArray());
            comboBox_lists.SelectedIndex = 0;
        }


        #endregion

        private async void button_search_automatic_Click(object sender, EventArgs e)
        {

        }

        private void button_create_list_Click(object sender, EventArgs e)
        {
            string tableName = ReplaceSpecialCharacters(textBox_list_name.Text.Trim());
            _dbHelper.CreateShoppingListTable(tableName);

            PopulateShoppingListComboBox();
        }

        private void button_delete_list_Click(object sender, EventArgs e)
        {
            if (comboBox_lists.SelectedIndex != -1)
            {
                string tableName = comboBox_lists.SelectedItem.ToString();
                _dbHelper.DeleteShoppingLists(tableName);

                PopulateShoppingListComboBox();
            }
        }
    }
}