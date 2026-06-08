namespace SchedulerDBManager.Presentation
{
    partial class SectionForm
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
            components = new System.ComponentModel.Container();
            dgvSections = new DataGridView();
            splitContainer = new SplitContainer();
            tableLayoutPanel = new TableLayoutPanel();
            btnHelp = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            pnlSearch = new Panel();
            tlpSearch = new TableLayoutPanel();
            sortLbl = new Label();
            cmbSortBy = new ComboBox();
            filterLbl = new Label();
            cmbFilterDepartment = new ComboBox();
            searchLbl = new Label();
            searchField = new TextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ((System.ComponentModel.ISupportInitialize)dgvSections).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            pnlSearch.SuspendLayout();
            tlpSearch.SuspendLayout();
            SuspendLayout();
            // 
            // dgvSections
            // 
            dgvSections.BackgroundColor = SystemColors.Control;
            dgvSections.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSections.Dock = DockStyle.Fill;
            dgvSections.Location = new Point(0, 70);
            dgvSections.Name = "dgvSections";
            dgvSections.RowHeadersWidth = 51;
            dgvSections.Size = new Size(546, 380);
            dgvSections.TabIndex = 0;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(tableLayoutPanel);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(dgvSections);
            splitContainer.Panel2.Controls.Add(pnlSearch);
            splitContainer.Size = new Size(800, 450);
            splitContainer.SplitterDistance = 250;
            splitContainer.TabIndex = 1;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(btnHelp, 0, 3);
            tableLayoutPanel.Controls.Add(btnDelete, 0, 2);
            tableLayoutPanel.Controls.Add(btnEdit, 0, 1);
            tableLayoutPanel.Controls.Add(btnAdd, 0, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.Size = new Size(250, 450);
            tableLayoutPanel.TabIndex = 0;
            // 
            // btnHelp
            // 
            btnHelp.Dock = DockStyle.Bottom;
            btnHelp.Location = new Point(30, 391);
            btnHelp.Margin = new Padding(30);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(190, 29);
            btnHelp.TabIndex = 3;
            btnHelp.Text = "Справка";
            btnHelp.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Dock = DockStyle.Bottom;
            btnDelete.Location = new Point(30, 277);
            btnDelete.Margin = new Padding(30);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(190, 29);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Удалить участок";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Dock = DockStyle.Bottom;
            btnEdit.Location = new Point(30, 165);
            btnEdit.Margin = new Padding(30);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(190, 29);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Редактировать участок";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Dock = DockStyle.Bottom;
            btnAdd.Location = new Point(30, 53);
            btnAdd.Margin = new Padding(30);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(190, 29);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Создать участок";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = SystemColors.ControlLight;
            pnlSearch.Controls.Add(tlpSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(546, 70);
            pnlSearch.TabIndex = 1;
            // 
            // tlpSearch
            // 
            tlpSearch.ColumnCount = 3; // 3 колонки
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpSearch.RowCount = 2;
            tlpSearch.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpSearch.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpSearch.Controls.Add(searchLbl, 0, 0);
            tlpSearch.Controls.Add(searchField, 0, 1);
            tlpSearch.Controls.Add(filterLbl, 1, 0);
            tlpSearch.Controls.Add(cmbFilterDepartment, 1, 1);
            tlpSearch.Controls.Add(sortLbl, 2, 0);
            tlpSearch.Controls.Add(cmbSortBy, 2, 1);
            tlpSearch.Dock = DockStyle.Fill;
            tlpSearch.Location = new Point(0, 0);
            tlpSearch.Name = "tlpSearch";
            tlpSearch.Padding = new Padding(10, 5, 10, 5);
            tlpSearch.Size = new Size(546, 70);
            tlpSearch.TabIndex = 0;
            // 
            // sortLbl
            // 
            searchLbl.AutoSize = true;
            searchLbl.Dock = DockStyle.Bottom;
            searchLbl.Margin = new Padding(0);
            sortLbl.Location = new Point(340, 10);
            sortLbl.Name = "sortLbl";
            sortLbl.Size = new Size(150, 20);
            sortLbl.TabIndex = 0;
            sortLbl.Text = "Сортировка:";
            // 
            // cmbSortBy
            // 
            cmbSortBy.Dock = DockStyle.Top;
            cmbSortBy.Margin = new Padding(0);
            cmbSortBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSortBy.Items.AddRange(new object[] { "Без сортировки", "По адресу", "По телефону" });
            cmbSortBy.Location = new Point(340, 32);
            cmbSortBy.Name = "cmbSortBy";
            cmbSortBy.Size = new Size(160, 28);
            cmbSortBy.TabIndex = 1;
            // 
            // filterLbl
            //
            filterLbl.AutoSize = true;
            filterLbl.Dock = DockStyle.Bottom;
            filterLbl.Margin = new Padding(0);
            filterLbl.Location = new Point(170, 10);
            filterLbl.Name = "filterLbl";
            filterLbl.Size = new Size(150, 20);
            filterLbl.TabIndex = 2;
            filterLbl.Text = "Подразделение:";
            // 
            // cmbFilterDepartment
            // 
            cmbFilterDepartment.Dock = DockStyle.Top;
            cmbFilterDepartment.Margin = new Padding(0, 0, 10, 0);
            cmbFilterDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterDepartment.Location = new Point(170, 32);
            cmbFilterDepartment.Name = "cmbFilterDepartment";
            cmbFilterDepartment.Size = new Size(160, 28);
            cmbFilterDepartment.TabIndex = 3;
            // 
            // searchLbl
            // 
            searchLbl.AutoSize = true;
            searchLbl.Dock = DockStyle.Bottom;
            searchLbl.Margin = new Padding(0);
            searchLbl.Location = new Point(10, 10);
            searchLbl.Name = "searchLbl";
            searchLbl.Size = new Size(150, 20);
            searchLbl.TabIndex = 4;
            searchLbl.Text = "Поиск:";
            // 
            // searchField
            // 
            searchField.Dock = DockStyle.Top;
            searchField.Margin = new Padding(0, 0, 10, 0);
            searchField.Location = new Point(10, 32);
            searchField.Name = "searchField";
            searchField.PlaceholderText = "Введите адрес...";
            searchField.Size = new Size(150, 27);
            searchField.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // SectionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer);
            Name = "SectionForm";
            Text = "Участки";
            ((System.ComponentModel.ISupportInitialize)dgvSections).EndInit();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            tableLayoutPanel.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            tlpSearch.ResumeLayout(false);
            tlpSearch.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvSections;
        private SplitContainer splitContainer;
        private TableLayoutPanel tableLayoutPanel;
        private ContextMenuStrip contextMenuStrip1;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnAdd;
        private Button btnHelp;
        private Panel pnlSearch;
        private TextBox searchField;
        private ComboBox cmbFilterDepartment;
        private ComboBox cmbSortBy;
        private Label searchLbl;
        private Label filterLbl;
        private Label sortLbl;
        private TableLayoutPanel tlpSearch;
    }
}