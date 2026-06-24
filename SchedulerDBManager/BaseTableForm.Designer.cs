namespace SchedulerDBManager.Presentation
{
    partial class BaseTableForm
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
            dgvTable = new DataGridView();
            splitContainer = new SplitContainer();
            tableLayoutPanel = new TableLayoutPanel();
            btnExportPDF = new Button();
            btnExport = new Button();
            btnHelp = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            pnlSearch = new Panel();
            tlpSearch = new TableLayoutPanel();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ((System.ComponentModel.ISupportInitialize)dgvTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            pnlSearch.SuspendLayout();
            SuspendLayout();
            // 
            // dgvTable
            // 
            dgvTable.BackgroundColor = Color.Snow;
            dgvTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTable.Dock = DockStyle.Fill;
            dgvTable.Location = new Point(0, 70);
            dgvTable.Name = "dgvTable";
            dgvTable.RowHeadersWidth = 51;
            dgvTable.Size = new Size(546, 380);
            dgvTable.TabIndex = 0;
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
            splitContainer.Panel2.Controls.Add(dgvTable);
            splitContainer.Panel2.Controls.Add(pnlSearch);
            splitContainer.Size = new Size(800, 450);
            splitContainer.SplitterDistance = 250;
            splitContainer.TabIndex = 1;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(btnExportPDF, 0, 5);
            tableLayoutPanel.Controls.Add(btnExport, 0, 4);
            tableLayoutPanel.Controls.Add(btnHelp, 0, 3);
            tableLayoutPanel.Controls.Add(btnDelete, 0, 2);
            tableLayoutPanel.Controls.Add(btnEdit, 0, 1);
            tableLayoutPanel.Controls.Add(btnAdd, 0, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.Size = new Size(250, 450);
            tableLayoutPanel.TabIndex = 0;
            // 
            // btnExportPDF
            // 
            btnExportPDF.BackColor = Color.Snow;
            btnExportPDF.Dock = DockStyle.Bottom;
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnExportPDF.Location = new Point(30, 416);
            btnExportPDF.Margin = new Padding(30, 5, 30, 5);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new Size(190, 29);
            btnExportPDF.TabIndex = 5;
            btnExportPDF.Text = "Экспорт в PDF";
            btnExportPDF.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.Snow;
            btnExport.Dock = DockStyle.Bottom;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnExport.Location = new Point(30, 371);
            btnExport.Margin = new Padding(30, 5, 30, 5);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(190, 29);
            btnExport.TabIndex = 4;
            btnExport.Text = "Экспорт в CSV";
            btnExport.UseVisualStyleBackColor = false;
            // 
            // btnHelp
            // 
            btnHelp.BackColor = Color.Snow;
            btnHelp.Dock = DockStyle.Bottom;
            btnHelp.FlatStyle = FlatStyle.Flat;
            btnHelp.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnHelp.Location = new Point(30, 311);
            btnHelp.Margin = new Padding(30, 20, 30, 20);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(190, 29);
            btnHelp.TabIndex = 3;
            btnHelp.Text = "Справка";
            btnHelp.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.PaleVioletRed;
            btnDelete.Dock = DockStyle.Bottom;
            btnDelete.FlatAppearance.BorderColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(30, 221);
            btnDelete.Margin = new Padding(30, 20, 30, 20);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(190, 29);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.DodgerBlue;
            btnEdit.Dock = DockStyle.Bottom;
            btnEdit.FlatAppearance.BorderColor = Color.White;
            btnEdit.FlatAppearance.MouseDownBackColor = Color.CornflowerBlue;
            btnEdit.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(30, 131);
            btnEdit.Margin = new Padding(30, 20, 30, 20);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(190, 29);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Редактировать";
            btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.DodgerBlue;
            btnAdd.Dock = DockStyle.Bottom;
            btnAdd.FlatAppearance.BorderColor = Color.White;
            btnAdd.FlatAppearance.MouseDownBackColor = Color.CornflowerBlue;
            btnAdd.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(30, 41);
            btnAdd.Margin = new Padding(30, 20, 30, 20);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(190, 29);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Создать";
            btnAdd.UseVisualStyleBackColor = false;
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
            tlpSearch.BackColor = Color.GhostWhite;
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tlpSearch.Dock = DockStyle.Fill;
            tlpSearch.Location = new Point(0, 0);
            tlpSearch.Name = "tlpSearch";
            tlpSearch.Padding = new Padding(10, 5, 10, 5);
            tlpSearch.RowCount = 2;
            tlpSearch.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            tlpSearch.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpSearch.Size = new Size(546, 70);
            tlpSearch.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // BaseTableForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.GhostWhite;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer);
            Name = "BaseTableForm";
            Text = "BaseTableForm";
            ((System.ComponentModel.ISupportInitialize)dgvTable).EndInit();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            tableLayoutPanel.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        protected DataGridView dgvTable;
        protected SplitContainer splitContainer;
        protected TableLayoutPanel tableLayoutPanel;
        protected ContextMenuStrip contextMenuStrip1;
        protected Button btnDelete;
        protected Button btnEdit;
        protected Button btnAdd;
        protected Button btnHelp;
        protected Panel pnlSearch;
        protected TableLayoutPanel tlpSearch;
        protected Button btnExport;
        protected Button btnExportPDF;
    }
}