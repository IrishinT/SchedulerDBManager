namespace SchedulerDBManager.Presentation
{
    partial class ScheduleForm
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
            dvgSchedules = new DataGridView();
            splitContainer = new SplitContainer();
            tableLayoutPanel = new TableLayoutPanel();
            btnHelp = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            pnlSearch = new Panel();
            sortLbl = new Label();
            cmbSortBy = new ComboBox();
            addressLbl = new Label();
            cmbFilterAddress = new ComboBox();
            supervisorLbl = new Label();
            cmbFilterSupervisor = new ComboBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ((System.ComponentModel.ISupportInitialize)dvgSchedules).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            pnlSearch.SuspendLayout();
            SuspendLayout();
            // 
            // dvgSchedules
            // 
            dvgSchedules.BackgroundColor = SystemColors.Control;
            dvgSchedules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgSchedules.Dock = DockStyle.Fill;
            dvgSchedules.Location = new Point(0, 70);
            dvgSchedules.Name = "dvgSchedules";
            dvgSchedules.RowHeadersWidth = 51;
            dvgSchedules.Size = new Size(546, 380);
            dvgSchedules.TabIndex = 0;
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
            splitContainer.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(dvgSchedules);
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
            tableLayoutPanel.Paint += tableLayoutPanel_Paint;
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
            btnDelete.Text = "Удалить смену";
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
            btnEdit.Text = "Редактировать смену";
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
            btnAdd.Text = "Создать смену";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = SystemColors.ControlLight;
            pnlSearch.Controls.Add(sortLbl);
            pnlSearch.Controls.Add(cmbSortBy);
            pnlSearch.Controls.Add(addressLbl);
            pnlSearch.Controls.Add(cmbFilterAddress);
            pnlSearch.Controls.Add(supervisorLbl);
            pnlSearch.Controls.Add(cmbFilterSupervisor);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(546, 70);
            pnlSearch.TabIndex = 1;
            // 
            // sortLbl
            // 
            sortLbl.Location = new Point(335, 9);
            sortLbl.Name = "sortLbl";
            sortLbl.Size = new Size(140, 20);
            sortLbl.TabIndex = 0;
            sortLbl.Text = "Сортировка:";
            // 
            // cmbSortBy
            // 
            cmbSortBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSortBy.Items.AddRange(new object[] { "По дате", "По количеству рабочих", "По длительности" });
            cmbSortBy.Location = new Point(335, 30);
            cmbSortBy.Name = "cmbSortBy";
            cmbSortBy.Size = new Size(199, 28);
            cmbSortBy.TabIndex = 1;
            // 
            // addressLbl
            // 
            addressLbl.Location = new Point(160, 8);
            addressLbl.Name = "addressLbl";
            addressLbl.Size = new Size(140, 20);
            addressLbl.TabIndex = 2;
            addressLbl.Text = "Адрес участка:";
            // 
            // cmbFilterAddress
            // 
            cmbFilterAddress.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterAddress.Location = new Point(160, 30);
            cmbFilterAddress.Name = "cmbFilterAddress";
            cmbFilterAddress.Size = new Size(169, 28);
            cmbFilterAddress.TabIndex = 3;
            // 
            // supervisorLbl
            // 
            supervisorLbl.Location = new Point(10, 8);
            supervisorLbl.Name = "supervisorLbl";
            supervisorLbl.Size = new Size(140, 20);
            supervisorLbl.TabIndex = 4;
            supervisorLbl.Text = "Начальник:";
            // 
            // cmbFilterSupervisor
            // 
            cmbFilterSupervisor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterSupervisor.Location = new Point(10, 30);
            cmbFilterSupervisor.Name = "cmbFilterSupervisor";
            cmbFilterSupervisor.Size = new Size(140, 28);
            cmbFilterSupervisor.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // ScheduleForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer);
            Name = "ScheduleForm";
            Text = "Смены";
            ((System.ComponentModel.ISupportInitialize)dvgSchedules).EndInit();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            tableLayoutPanel.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dvgSchedules;
        private SplitContainer splitContainer;
        private TableLayoutPanel tableLayoutPanel;
        private ContextMenuStrip contextMenuStrip1;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnAdd;
        private Button btnHelp;
        private Panel pnlSearch;
        private ComboBox cmbFilterSupervisor;
        private ComboBox cmbFilterAddress;
        private ComboBox cmbSortBy;
        private Label supervisorLbl;
        private Label addressLbl;
        private Label sortLbl;
    }
}