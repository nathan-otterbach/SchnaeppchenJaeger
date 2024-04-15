using SchnaeppchenJaeger.Client;
using SchnaeppchenJaeger.Database;
using SchnaeppchenJaeger.Utility;
using System.Configuration;
using System.Windows.Forms;

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
            comboBox_lists.SelectedIndexChanged += comboBox_lists_SelectedIndexChanged;

            checkedListBox_select_shop.Items.AddRange(new string[]
            {
                "Aldi",
                "Edeka",
                "Kaufland",
                "Lidl",
                "Netto",
                "Penny",
                "Rewe"
            });
            Load();

            FormClosing += Form1_FormClosing;
        }

        // use method before get property method, so we update the list of selected shops
        private void GetSelectedShops()
        {
            for (int i = 0; i < checkedListBox_select_shop.Items.Count; i++)
            {
                if (checkedListBox_select_shop.GetItemChecked(i))
                {
                    Program._utils.selectedShops.Add(checkedListBox_select_shop.Items[i].ToString());
                }
            }
        }

        private async void button_test_Click(object sender, EventArgs e)
        {
            GetSelectedShops();

            using (var client = new ApiClient(Convert.ToUInt32(textBox_zipCode.Text.Trim()), textBox_product.Text.Trim()))
            {
                await client.GetOffersAsync(_cancellationTokenSource.Token);
            }

            richTextBox_bill.Clear();

            foreach (var entry in Program._utils.populatedData)
            {
                // Format the text as desired
                string formattedText = $"{entry.Key}: {entry.Value}\n";

                // Add the formatted text to the RichTextBox
                richTextBox_bill.AppendText(formattedText);
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
            comboBox_db_shopping_lists.Items.Clear();

            List<string> tableNames = _dbHelper.GetShoppingListTables();

            if (tableNames.Any())
            {
                comboBox_db_shopping_lists.Items.AddRange(tableNames.ToArray());
                comboBox_db_shopping_lists.SelectedIndex = 0;

                comboBox_lists.Items.AddRange(tableNames.ToArray());
                comboBox_lists.SelectedIndex = 0;
            }
        }

        private void PopulateProductListbox()
        {
            listBox_products.Items.Clear();

            if (comboBox_lists.SelectedIndex != -1)
            {
                string tableName = comboBox_lists.SelectedItem.ToString();
                List<string> products = _dbHelper.GetAllProductsFromShoppingList(tableName);
                listBox_products.Items.AddRange(products.ToArray());
            }
        }

        private void Load()
        {
            string selectedShops = ConfigurationManager.AppSettings["SelectedShops"];
            string zipCode = ConfigurationManager.AppSettings["ZipCode"];

            if (!string.IsNullOrEmpty(selectedShops))
            {
                string[] selectedShopArray = selectedShops.Split(',');

                foreach (string shop in selectedShopArray)
                {
                    int index = checkedListBox_select_shop.Items.IndexOf(shop);
                    if (index != -1)
                    {
                        checkedListBox_select_shop.SetItemChecked(index, true);
                    }
                }
            }

            textBox_zipCode.Text = zipCode;
        }

        private void Save()
        {
            List<string> selectedShops = new List<string>();

            foreach (var shop in checkedListBox_select_shop.CheckedItems)
            {
                selectedShops.Add(shop.ToString());
            }

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["SelectedShops"].Value = string.Join(",", selectedShops);
            config.AppSettings.Settings["ZipCode"].Value = textBox_zipCode.Text.Trim();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
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
            textBox_list_name.Clear();
        }

        private void button_delete_list_Click(object sender, EventArgs e)
        {
            if (comboBox_lists.SelectedIndex != -1)
            {
                string tableName = comboBox_lists.SelectedItem.ToString();
                _dbHelper.DeleteShoppingLists(tableName);

                PopulateShoppingListComboBox();
            }

            textBox_list_name.Clear();
        }

        private void button_add_product_to_list_Click(object sender, EventArgs e)
        {
            if (comboBox_lists.SelectedIndex != -1)
            {
                string tableName = comboBox_lists.SelectedItem.ToString();
                string productName = textBox_product_name.Text.Trim();

                if (!string.IsNullOrWhiteSpace(productName))
                {
                    _dbHelper.InsertItemIntoShoppingList(tableName, productName);
                }
                else
                {
                    MessageBox.Show("Please enter a product name.");
                }

                PopulateProductListbox();

                textBox_product_name.Clear();
            }
            else
            {
                MessageBox.Show("Please select a shopping list.");
            }
        }

        private void button_remove_product_from_list_Click(object sender, EventArgs e)
        {
            if (comboBox_lists.SelectedIndex != -1 &&
                listBox_products.SelectedIndex != -1)
            {
                string tableName = comboBox_lists.SelectedItem.ToString();
                string productName = listBox_products.SelectedItem.ToString();

                if (!string.IsNullOrWhiteSpace(productName))
                {
                    _dbHelper.RemoveItemFromShoppingList(tableName, productName);
                }
                else
                {
                    MessageBox.Show("Please enter a product name.");
                }

                PopulateProductListbox();

                textBox_product_name.Clear();
            }
            else
            {
                MessageBox.Show("Please select a shopping list.");
            }
        }

        private void comboBox_lists_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProductListbox();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }
    }
}