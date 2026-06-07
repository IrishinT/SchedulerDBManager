namespace SchedulerDBManager.Presentation
{
    partial class SectionEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayout = new TableLayoutPanel();
            phoneField = new TextBox();
            addressLbl = new Label();
            phoneLbl = new Label();
            sectionLbl = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            sectionAddress = new ComboBox();
            addressField = new TextBox();
            panelTopActions = new Panel();
            sortLbl = new Label();
            filterCmb = new ComboBox();
            filterLbl = new Label();
            cmbSortBy = new ComboBox();
            searchLbl = new Label();
            searchField = new TextBox();
            tableLayout.SuspendLayout();
            panelTopActions.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.Controls.Add(phoneField, 1, 1);
            tableLayout.Controls.Add(addressLbl, 0, 0);
            tableLayout.Controls.Add(phoneLbl, 0, 1);
            tableLayout.Controls.Add(sectionLbl, 0, 2);
            tableLayout.Controls.Add(btnCancel, 0, 3);
            tableLayout.Controls.Add(btnSave, 1, 3);
            tableLayout.Controls.Add(sectionAddress, 1, 2);
            tableLayout.Controls.Add(addressField, 1, 0);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 60);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 4;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0000038F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0000038F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayout.Size = new Size(800, 390);
            tableLayout.TabIndex = 0;
            // 
            // phoneField
            // 
            phoneField.Dock = DockStyle.Bottom;
            phoneField.Location = new Point(403, 164);
            phoneField.Name = "phoneField";
            phoneField.Size = new Size(394, 27);
            phoneField.TabIndex = 13;
            // 
            // addressLbl
            // 
            addressLbl.AutoSize = true;
            addressLbl.Dock = DockStyle.Bottom;
            addressLbl.Location = new Point(3, 77);
            addressLbl.Name = "addressLbl";
            addressLbl.Size = new Size(394, 20);
            addressLbl.TabIndex = 0;
            addressLbl.Text = "Адрес:";
            // 
            // phoneLbl
            // 
            phoneLbl.AutoSize = true;
            phoneLbl.Dock = DockStyle.Bottom;
            phoneLbl.Location = new Point(3, 174);
            phoneLbl.Name = "phoneLbl";
            phoneLbl.Size = new Size(394, 20);
            phoneLbl.TabIndex = 1;
            phoneLbl.Text = "Номер телефона:";
            // 
            // sectionLbl
            // 
            sectionLbl.AutoSize = true;
            sectionLbl.Dock = DockStyle.Bottom;
            sectionLbl.Location = new Point(3, 271);
            sectionLbl.Name = "sectionLbl";
            sectionLbl.Size = new Size(394, 20);
            sectionLbl.TabIndex = 4;
            sectionLbl.Text = "Подразделение:";
            // 
            // btnCancel
            // 
            btnCancel.Dock = DockStyle.Bottom;
            btnCancel.Location = new Point(40, 327);
            btnCancel.Margin = new Padding(40, 10, 40, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(320, 53);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Dock = DockStyle.Bottom;
            btnSave.Location = new Point(440, 327);
            btnSave.Margin = new Padding(40, 10, 40, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(320, 53);
            btnSave.TabIndex = 6;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // sectionAddress
            // 
            sectionAddress.Dock = DockStyle.Bottom;
            sectionAddress.FormattingEnabled = true;
            sectionAddress.Location = new Point(403, 260);
            sectionAddress.Name = "sectionAddress";
            sectionAddress.Size = new Size(394, 28);
            sectionAddress.TabIndex = 11;
            // 
            // addressField
            // 
            addressField.Dock = DockStyle.Bottom;
            addressField.Location = new Point(403, 67);
            addressField.Name = "addressField";
            addressField.Size = new Size(394, 27);
            addressField.TabIndex = 12;
            // 
            // panelTopActions
            // 
            panelTopActions.BackColor = Color.WhiteSmoke;
            panelTopActions.Controls.Add(sortLbl);
            panelTopActions.Controls.Add(filterCmb);
            panelTopActions.Controls.Add(filterLbl);
            panelTopActions.Controls.Add(cmbSortBy);
            panelTopActions.Controls.Add(searchLbl);
            panelTopActions.Controls.Add(searchField);
            panelTopActions.Dock = DockStyle.Top;
            panelTopActions.Location = new Point(0, 0);
            panelTopActions.Name = "panelTopActions";
            panelTopActions.Size = new Size(800, 60);
            panelTopActions.TabIndex = 1;
            // 
            // sortLbl
            // 
            sortLbl.Location = new Point(559, 3);
            sortLbl.Name = "sortLbl";
            sortLbl.Size = new Size(120, 20);
            sortLbl.TabIndex = 0;
            sortLbl.Text = "Сортировать по:";
            // 
            // filterCmb
            // 
            filterCmb.DropDownStyle = ComboBoxStyle.DropDownList;
            filterCmb.Items.AddRange(new object[] { "Адресу", "Телефону" });
            filterCmb.Location = new Point(324, 26);
            filterCmb.Name = "filterCmb";
            filterCmb.Size = new Size(229, 28);
            filterCmb.TabIndex = 1;
            filterCmb.SelectedIndexChanged += cmbSortBy_SelectedIndexChanged;
            // 
            // filterLbl
            // 
            filterLbl.Location = new Point(324, 3);
            filterLbl.Name = "filterLbl";
            filterLbl.Size = new Size(120, 20);
            filterLbl.TabIndex = 2;
            filterLbl.Text = "Подразделение:";
            // 
            // cmbSortBy
            // 
            cmbSortBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSortBy.Location = new Point(559, 26);
            cmbSortBy.Name = "cmbSortBy";
            cmbSortBy.Size = new Size(229, 28);
            cmbSortBy.TabIndex = 3;
            // 
            // searchLbl
            // 
            searchLbl.Location = new Point(12, 3);
            searchLbl.Name = "searchLbl";
            searchLbl.Size = new Size(60, 20);
            searchLbl.TabIndex = 4;
            searchLbl.Text = "Поиск:";
            // 
            // searchField
            // 
            searchField.Location = new Point(12, 26);
            searchField.Name = "searchField";
            searchField.PlaceholderText = "Введите текст...";
            searchField.Size = new Size(306, 27);
            searchField.TabIndex = 5;
            // 
            // SectionEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayout);
            Controls.Add(panelTopActions);
            Name = "SectionEditForm";
            Text = "ScheduleEditForm";
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            panelTopActions.ResumeLayout(false);
            panelTopActions.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayout;
        private Label addressLbl;
        private Label phoneLbl;
        private Label sectionLbl;
        private ComboBox sectionAddress;
        private TextBox addressField;
        private TextBox phoneField;
        private Button btnCancel;
        private Button btnSave;
        private Panel panelTopActions;
        private TextBox searchField;
        private Label searchLbl;
        private ComboBox cmbSortBy;
        private Label filterLbl;
        private ComboBox filterCmb;
        private Label sortLbl;
    }
}