namespace SchnaeppchenJaeger
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_search_manual = new Button();
            textBox_zipCode = new TextBox();
            textBox_product = new TextBox();
            label_PLZ = new Label();
            label_product = new Label();
            checkBox_modus = new CheckBox();
            groupBox_modus = new GroupBox();
            label_modus_indicator = new Label();
            groupBox_modus_manual = new GroupBox();
            button_cancel_manual = new Button();
            groupBox_modus_automatic = new GroupBox();
            button_cancel_automatic = new Button();
            button_search_automatic = new Button();
            label1 = new Label();
            comboBox_db_shopping_lists = new ComboBox();
            groupBox_select_shop = new GroupBox();
            checkedListBox_select_shop = new CheckedListBox();
            groupBox_create_list = new GroupBox();
            textBox_product_name = new TextBox();
            label_product_name = new Label();
            textBox_list_name = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label_list_name = new Label();
            groupBox1 = new GroupBox();
            label2 = new Label();
            groupBox_bill = new GroupBox();
            richTextBox_bill = new RichTextBox();
            groupBox_modus.SuspendLayout();
            groupBox_modus_manual.SuspendLayout();
            groupBox_modus_automatic.SuspendLayout();
            groupBox_select_shop.SuspendLayout();
            groupBox_create_list.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox_bill.SuspendLayout();
            SuspendLayout();
            // 
            // button_search_manual
            // 
            button_search_manual.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_search_manual.Location = new Point(15, 131);
            button_search_manual.Name = "button_search_manual";
            button_search_manual.Size = new Size(153, 28);
            button_search_manual.TabIndex = 0;
            button_search_manual.Text = "Suche starten";
            button_search_manual.Click += button_test_Click;
            // 
            // textBox_zipCode
            // 
            textBox_zipCode.Location = new Point(15, 61);
            textBox_zipCode.Name = "textBox_zipCode";
            textBox_zipCode.Size = new Size(88, 33);
            textBox_zipCode.TabIndex = 1;
            // 
            // textBox_product
            // 
            textBox_product.Location = new Point(194, 61);
            textBox_product.Name = "textBox_product";
            textBox_product.Size = new Size(153, 33);
            textBox_product.TabIndex = 2;
            // 
            // label_PLZ
            // 
            label_PLZ.AutoSize = true;
            label_PLZ.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_PLZ.Location = new Point(15, 37);
            label_PLZ.Name = "label_PLZ";
            label_PLZ.Size = new Size(88, 21);
            label_PLZ.TabIndex = 3;
            label_PLZ.Text = "Postleitzahl";
            // 
            // label_product
            // 
            label_product.AutoSize = true;
            label_product.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_product.Location = new Point(194, 37);
            label_product.Name = "label_product";
            label_product.Size = new Size(65, 21);
            label_product.TabIndex = 4;
            label_product.Text = "Produkt";
            // 
            // checkBox_modus
            // 
            checkBox_modus.AutoSize = true;
            checkBox_modus.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox_modus.Location = new Point(12, 33);
            checkBox_modus.Name = "checkBox_modus";
            checkBox_modus.Size = new Size(116, 25);
            checkBox_modus.TabIndex = 5;
            checkBox_modus.Text = "Automatisch";
            checkBox_modus.UseVisualStyleBackColor = true;
            checkBox_modus.CheckedChanged += checkBox_modus_CheckedChanged;
            // 
            // groupBox_modus
            // 
            groupBox_modus.Controls.Add(label_modus_indicator);
            groupBox_modus.Controls.Add(checkBox_modus);
            groupBox_modus.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox_modus.Location = new Point(378, 12);
            groupBox_modus.Name = "groupBox_modus";
            groupBox_modus.Size = new Size(134, 94);
            groupBox_modus.TabIndex = 7;
            groupBox_modus.TabStop = false;
            groupBox_modus.Text = "Modus";
            // 
            // label_modus_indicator
            // 
            label_modus_indicator.AutoSize = true;
            label_modus_indicator.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_modus_indicator.ForeColor = Color.Red;
            label_modus_indicator.Location = new Point(12, 61);
            label_modus_indicator.Name = "label_modus_indicator";
            label_modus_indicator.Size = new Size(66, 21);
            label_modus_indicator.TabIndex = 9;
            label_modus_indicator.Text = "Manuell";
            // 
            // groupBox_modus_manual
            // 
            groupBox_modus_manual.Controls.Add(button_cancel_manual);
            groupBox_modus_manual.Controls.Add(textBox_zipCode);
            groupBox_modus_manual.Controls.Add(button_search_manual);
            groupBox_modus_manual.Controls.Add(label_product);
            groupBox_modus_manual.Controls.Add(textBox_product);
            groupBox_modus_manual.Controls.Add(label_PLZ);
            groupBox_modus_manual.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox_modus_manual.Location = new Point(12, 12);
            groupBox_modus_manual.Name = "groupBox_modus_manual";
            groupBox_modus_manual.Size = new Size(360, 171);
            groupBox_modus_manual.TabIndex = 8;
            groupBox_modus_manual.TabStop = false;
            groupBox_modus_manual.Text = "Paramater Suche";
            // 
            // button_cancel_manual
            // 
            button_cancel_manual.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_cancel_manual.Location = new Point(194, 131);
            button_cancel_manual.Name = "button_cancel_manual";
            button_cancel_manual.Size = new Size(153, 28);
            button_cancel_manual.TabIndex = 5;
            button_cancel_manual.Text = "Suche abbrechen";
            // 
            // groupBox_modus_automatic
            // 
            groupBox_modus_automatic.Controls.Add(button_cancel_automatic);
            groupBox_modus_automatic.Controls.Add(button_search_automatic);
            groupBox_modus_automatic.Controls.Add(label1);
            groupBox_modus_automatic.Controls.Add(comboBox_db_shopping_lists);
            groupBox_modus_automatic.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox_modus_automatic.Location = new Point(12, 12);
            groupBox_modus_automatic.Name = "groupBox_modus_automatic";
            groupBox_modus_automatic.Size = new Size(360, 171);
            groupBox_modus_automatic.TabIndex = 9;
            groupBox_modus_automatic.TabStop = false;
            groupBox_modus_automatic.Text = "Einkaufslisten Suche";
            groupBox_modus_automatic.Visible = false;
            // 
            // button_cancel_automatic
            // 
            button_cancel_automatic.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_cancel_automatic.Location = new Point(194, 131);
            button_cancel_automatic.Name = "button_cancel_automatic";
            button_cancel_automatic.Size = new Size(153, 28);
            button_cancel_automatic.TabIndex = 7;
            button_cancel_automatic.Text = "Suche abbrechen";
            // 
            // button_search_automatic
            // 
            button_search_automatic.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_search_automatic.Location = new Point(15, 131);
            button_search_automatic.Name = "button_search_automatic";
            button_search_automatic.Size = new Size(153, 28);
            button_search_automatic.TabIndex = 6;
            button_search_automatic.Text = "Suche starten";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 37);
            label1.Name = "label1";
            label1.Size = new Size(158, 21);
            label1.TabIndex = 1;
            label1.Text = "Einkaufsliste waehlen";
            // 
            // comboBox_db_shopping_lists
            // 
            comboBox_db_shopping_lists.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_db_shopping_lists.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox_db_shopping_lists.FormattingEnabled = true;
            comboBox_db_shopping_lists.Location = new Point(15, 63);
            comboBox_db_shopping_lists.Name = "comboBox_db_shopping_lists";
            comboBox_db_shopping_lists.Size = new Size(332, 29);
            comboBox_db_shopping_lists.TabIndex = 0;
            // 
            // groupBox_select_shop
            // 
            groupBox_select_shop.Controls.Add(checkedListBox_select_shop);
            groupBox_select_shop.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox_select_shop.Location = new Point(518, 12);
            groupBox_select_shop.Name = "groupBox_select_shop";
            groupBox_select_shop.Size = new Size(259, 221);
            groupBox_select_shop.TabIndex = 10;
            groupBox_select_shop.TabStop = false;
            groupBox_select_shop.Text = "Einkaufsladen auswaehlen";
            // 
            // checkedListBox_select_shop
            // 
            checkedListBox_select_shop.BorderStyle = BorderStyle.FixedSingle;
            checkedListBox_select_shop.CheckOnClick = true;
            checkedListBox_select_shop.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkedListBox_select_shop.FormattingEnabled = true;
            checkedListBox_select_shop.Items.AddRange(new object[] { "ALDI SÜD", "EDEKA", "Kaufland", "Lidl", "Netto Marken-Discount", "PENNY", "REWE" });
            checkedListBox_select_shop.Location = new Point(11, 37);
            checkedListBox_select_shop.Name = "checkedListBox_select_shop";
            checkedListBox_select_shop.Size = new Size(238, 170);
            checkedListBox_select_shop.TabIndex = 0;
            // 
            // groupBox_create_list
            // 
            groupBox_create_list.Controls.Add(textBox_product_name);
            groupBox_create_list.Controls.Add(label_product_name);
            groupBox_create_list.Controls.Add(textBox_list_name);
            groupBox_create_list.Controls.Add(button1);
            groupBox_create_list.Controls.Add(button2);
            groupBox_create_list.Controls.Add(label_list_name);
            groupBox_create_list.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox_create_list.Location = new Point(12, 189);
            groupBox_create_list.Name = "groupBox_create_list";
            groupBox_create_list.Size = new Size(500, 360);
            groupBox_create_list.TabIndex = 11;
            groupBox_create_list.TabStop = false;
            groupBox_create_list.Text = "Einkaufslisten Anlegen";
            // 
            // textBox_product_name
            // 
            textBox_product_name.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_product_name.Location = new Point(262, 61);
            textBox_product_name.Name = "textBox_product_name";
            textBox_product_name.Size = new Size(232, 29);
            textBox_product_name.TabIndex = 10;
            // 
            // label_product_name
            // 
            label_product_name.AutoSize = true;
            label_product_name.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_product_name.Location = new Point(262, 37);
            label_product_name.Name = "label_product_name";
            label_product_name.Size = new Size(111, 21);
            label_product_name.TabIndex = 9;
            label_product_name.Text = "Produkt Name";
            // 
            // textBox_list_name
            // 
            textBox_list_name.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_list_name.Location = new Point(15, 61);
            textBox_list_name.Name = "textBox_list_name";
            textBox_list_name.Size = new Size(232, 29);
            textBox_list_name.TabIndex = 8;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(194, 212);
            button1.Name = "button1";
            button1.Size = new Size(153, 28);
            button1.TabIndex = 7;
            button1.Text = "Suche abbrechen";
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(15, 212);
            button2.Name = "button2";
            button2.Size = new Size(153, 28);
            button2.TabIndex = 6;
            button2.Text = "Suche starten";
            // 
            // label_list_name
            // 
            label_list_name.AutoSize = true;
            label_list_name.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_list_name.Location = new Point(15, 37);
            label_list_name.Name = "label_list_name";
            label_list_name.Size = new Size(142, 21);
            label_list_name.TabIndex = 1;
            label_list_name.Text = "Einkaufsliste Name";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(378, 112);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(134, 71);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Status";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(12, 35);
            label2.Name = "label2";
            label2.Size = new Size(103, 21);
            label2.TabIndex = 9;
            label2.Text = "Disconnected";
            // 
            // groupBox_bill
            // 
            groupBox_bill.Controls.Add(richTextBox_bill);
            groupBox_bill.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox_bill.Location = new Point(518, 239);
            groupBox_bill.Name = "groupBox_bill";
            groupBox_bill.Size = new Size(259, 310);
            groupBox_bill.TabIndex = 13;
            groupBox_bill.TabStop = false;
            groupBox_bill.Text = "Rechnung";
            // 
            // richTextBox_bill
            // 
            richTextBox_bill.BorderStyle = BorderStyle.FixedSingle;
            richTextBox_bill.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox_bill.Location = new Point(11, 32);
            richTextBox_bill.Name = "richTextBox_bill";
            richTextBox_bill.Size = new Size(238, 272);
            richTextBox_bill.TabIndex = 0;
            richTextBox_bill.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(789, 561);
            Controls.Add(groupBox_bill);
            Controls.Add(groupBox1);
            Controls.Add(groupBox_create_list);
            Controls.Add(groupBox_select_shop);
            Controls.Add(groupBox_modus_automatic);
            Controls.Add(groupBox_modus_manual);
            Controls.Add(groupBox_modus);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Text = "SchnaeppchenJaeger";
            groupBox_modus.ResumeLayout(false);
            groupBox_modus.PerformLayout();
            groupBox_modus_manual.ResumeLayout(false);
            groupBox_modus_manual.PerformLayout();
            groupBox_modus_automatic.ResumeLayout(false);
            groupBox_modus_automatic.PerformLayout();
            groupBox_select_shop.ResumeLayout(false);
            groupBox_create_list.ResumeLayout(false);
            groupBox_create_list.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox_bill.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button_search_manual;
        private TextBox textBox_zipCode;
        private TextBox textBox_product;
        private Label label_PLZ;
        private Label label_product;
        private CheckBox checkBox_modus;
        private GroupBox groupBox_modus;
        private GroupBox groupBox_modus_manual;
        private Button button_cancel_manual;
        private Label label_modus_indicator;
        private GroupBox groupBox_modus_automatic;
        private ComboBox comboBox_db_shopping_lists;
        private Label label1;
        private Button button_cancel_automatic;
        private Button button_search_automatic;
        private GroupBox groupBox_select_shop;
        private CheckedListBox checkedListBox_select_shop;
        private GroupBox groupBox_create_list;
        private Button button1;
        private Button button2;
        private Label label_list_name;
        private TextBox textBox_list_name;
        private TextBox textBox_product_name;
        private Label label_product_name;
        private GroupBox groupBox1;
        private Label label2;
        private GroupBox groupBox_bill;
        private RichTextBox richTextBox_bill;
    }
}
