using SchnaeppchenJaeger.Client;
using SchnaeppchenJaeger.Database;
using SchnaeppchenJaeger.Utility;
using System.Configuration;

namespace SchnaeppchenJaeger
{
    public partial class Form1 : Form
    {
        private DatabaseHelper _dbHelper;
        private CancellationTokenSource _cancellationTokenSource;
        private Mode _currentMode = Mode.Manual;
        private Status_DB _statusDB = Status_DB.Disconnected;
        private Resizer _resizer;

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
                "PENNY",
                "REWE"
            });
            Load();

            _resizer = new Resizer(this);

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

        private async void button_search_manual_Click(object sender, EventArgs e)
        {
            GetSelectedShops();
            richTextBox_bill.Clear();
            Program._utils.populatedData.Clear();
            _cancellationTokenSource = new CancellationTokenSource();

            string zipCode = textBox_zipCode.Text.Trim();
            string product = textBox_product.Text.Trim();

            if (string.IsNullOrWhiteSpace(zipCode) || string.IsNullOrWhiteSpace(product))
            {
                MessageBox.Show("Please enter a zip code and product");
                return;
            }

            using (var client = new ApiClient(Convert.ToUInt32(zipCode), product))
            {
                await client.GetOffersAsync(_cancellationTokenSource.Token);
            }

            await GetAndDisplayOffersAsync(product);
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
                listBox_products.Items.AddRange(_dbHelper.GetAllProductsFromShoppingList(tableName).ToArray());
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
            textBox_zipCode_automatic.Text = zipCode;
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
            GetSelectedShops();
            richTextBox_bill.Clear();
            _cancellationTokenSource = new CancellationTokenSource();

            if (string.IsNullOrWhiteSpace(textBox_zipCode.Text.Trim()) || comboBox_db_shopping_lists.SelectedIndex == -1)
            {
                MessageBox.Show("Please enter a zip code and select a shopping list");
                return;
            }

            string selectedShoppingList = comboBox_db_shopping_lists.SelectedItem.ToString();
            Program._utils.products = _dbHelper.GetAllProductsFromShoppingList(selectedShoppingList);

            foreach (var product in Program._utils.products)
            {
                Program._utils.populatedData.Clear(); // Clear data for each product

                await GetAndDisplayOffersAsync(product);
            }
        }

        private async Task GetAndDisplayOffersAsync(string product)
        {
            using (var client = new ApiClient(Convert.ToUInt32(textBox_zipCode.Text.Trim()), product))
            {
                await client.GetOffersAsync(_cancellationTokenSource.Token);
            }

            if (!Program._utils.populatedData.Any())
            {
                return; // No valid offers for this product, skip displaying
            }

            richTextBox_bill.AppendText($"{product.ToUpper()}:\n\n");

            foreach (var entry in Program._utils.populatedData)
            {
                string key = entry.Key;
                string value = entry.Value;

                if (key.Contains("Price_") && !key.Contains("ReferencePrice_"))
                {
                    richTextBox_bill.AppendText($"{value} €\n");
                }
                else if (key.Contains("ReferencePrice_"))
                {
                    richTextBox_bill.AppendText($"{value} € pro ");
                }
                else if (key.Contains("FromDate_"))
                {
                    richTextBox_bill.AppendText($"Gültig von: {value} bis ");
                }
                else if (key.Contains("RequiresLoyaltyMembership_"))
                {
                    string loyaltyMembershipText = value.Equals("True", StringComparison.OrdinalIgnoreCase)
                        ? "Kundenkarte benötigt\n\n"
                        : "Kundenkarte nicht benötigt\n\n";
                    richTextBox_bill.AppendText(loyaltyMembershipText);
                }
                else
                {
                    richTextBox_bill.AppendText($"{value}\n");
                }
            }
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

        private void button_cancel_manual_Click(object sender, EventArgs e)
        {
            if (_cancellationTokenSource.Token.CanBeCanceled)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private void button_cancel_automatic_Click(object sender, EventArgs e)
        {
            if (_cancellationTokenSource.Token.CanBeCanceled)
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}