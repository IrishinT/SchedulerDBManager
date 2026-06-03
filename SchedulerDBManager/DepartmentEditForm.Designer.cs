namespace SchedulerDBManager.Presentation
{
    partial class DepartmentEditForm
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
            addressField = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            tableLayout.SuspendLayout();
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
            tableLayout.Controls.Add(btnCancel, 0, 2);
            tableLayout.Controls.Add(btnSave, 1, 2);
            tableLayout.Controls.Add(addressField, 1, 0);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 3;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33334F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayout.Size = new Size(800, 450);
            tableLayout.TabIndex = 0;
            // 
            // phoneField
            // 
            phoneField.Dock = DockStyle.Bottom;
            phoneField.Location = new Point(403, 269);
            phoneField.Name = "phoneField";
            phoneField.Size = new Size(394, 27);
            phoneField.TabIndex = 13;
            // 
            // addressLbl
            // 
            addressLbl.AutoSize = true;
            addressLbl.Dock = DockStyle.Bottom;
            addressLbl.Location = new Point(3, 129);
            addressLbl.Name = "addressLbl";
            addressLbl.Size = new Size(394, 20);
            addressLbl.TabIndex = 0;
            addressLbl.Text = "Название:";
            // 
            // phoneLbl
            // 
            phoneLbl.AutoSize = true;
            phoneLbl.Dock = DockStyle.Bottom;
            phoneLbl.Location = new Point(3, 279);
            phoneLbl.Name = "phoneLbl";
            phoneLbl.Size = new Size(394, 20);
            phoneLbl.TabIndex = 1;
            phoneLbl.Text = "ФИО руководителя:";
            // 
            // addressField
            // 
            addressField.Dock = DockStyle.Bottom;
            addressField.Location = new Point(403, 119);
            addressField.Name = "addressField";
            addressField.Size = new Size(394, 27);
            addressField.TabIndex = 12;
            // 
            // btnSave
            // 
            btnSave.Dock = DockStyle.Bottom;
            btnSave.Location = new Point(440, 387);
            btnSave.Margin = new Padding(40, 10, 40, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(320, 53);
            btnSave.TabIndex = 6;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Dock = DockStyle.Bottom;
            btnCancel.Location = new Point(40, 387);
            btnCancel.Margin = new Padding(40, 10, 40, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(320, 53);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // DepartmentEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayout);
            Name = "DepartmentEditForm";
            Text = "ScheduleEditForm";
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayout;
        private Label addressLbl;
        private Label phoneLbl;
        private TextBox addressField;
        private TextBox phoneField;
        private Button btnCancel;
        private Button btnSave;
    }
}