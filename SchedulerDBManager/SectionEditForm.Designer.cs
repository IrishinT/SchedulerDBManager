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
            tableLayout.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayout.Controls.Add(phoneField, 1, 1);
            tableLayout.Controls.Add(addressLbl, 0, 0);
            tableLayout.Controls.Add(phoneLbl, 0, 1);
            tableLayout.Controls.Add(sectionLbl, 0, 2);
            tableLayout.Controls.Add(btnCancel, 0, 3);
            tableLayout.Controls.Add(btnSave, 1, 3);
            tableLayout.Controls.Add(sectionAddress, 1, 2);
            tableLayout.Controls.Add(addressField, 1, 0);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 4;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0000038F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0000038F));
            tableLayout.Size = new Size(800, 450);
            tableLayout.TabIndex = 0;
            // 
            // phoneField
            // 
            phoneField.Dock = DockStyle.Bottom;
            phoneField.Location = new Point(323, 194);
            phoneField.Margin = new Padding(3, 3, 30, 3);
            phoneField.MaximumSize = new Size(600, 0);
            phoneField.Name = "phoneField";
            phoneField.PlaceholderText = "+79998887766";
            phoneField.Size = new Size(447, 27);
            phoneField.TabIndex = 13;
            // 
            // addressLbl
            // 
            addressLbl.AutoSize = true;
            addressLbl.Dock = DockStyle.Bottom;
            addressLbl.Location = new Point(3, 92);
            addressLbl.Margin = new Padding(3, 0, 10, 0);
            addressLbl.Name = "addressLbl";
            addressLbl.Size = new Size(307, 20);
            addressLbl.TabIndex = 0;
            addressLbl.Text = "Адрес:";
            addressLbl.TextAlign = ContentAlignment.TopRight;
            // 
            // phoneLbl
            // 
            phoneLbl.AutoSize = true;
            phoneLbl.Dock = DockStyle.Bottom;
            phoneLbl.Location = new Point(3, 204);
            phoneLbl.Margin = new Padding(3, 0, 10, 0);
            phoneLbl.Name = "phoneLbl";
            phoneLbl.Size = new Size(307, 20);
            phoneLbl.TabIndex = 1;
            phoneLbl.Text = "Номер телефона:";
            phoneLbl.TextAlign = ContentAlignment.TopRight;
            // 
            // sectionLbl
            // 
            sectionLbl.AutoSize = true;
            sectionLbl.Dock = DockStyle.Bottom;
            sectionLbl.Location = new Point(3, 316);
            sectionLbl.Margin = new Padding(3, 0, 10, 0);
            sectionLbl.Name = "sectionLbl";
            sectionLbl.Size = new Size(307, 20);
            sectionLbl.TabIndex = 4;
            sectionLbl.Text = "Подразделение:";
            sectionLbl.TextAlign = ContentAlignment.TopRight;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.PaleVioletRed;
            btnCancel.Dock = DockStyle.Bottom;
            btnCancel.FlatAppearance.BorderColor = Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(40, 387);
            btnCancel.Margin = new Padding(40, 10, 40, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(240, 53);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.DodgerBlue;
            btnSave.Dock = DockStyle.Bottom;
            btnSave.FlatAppearance.BorderColor = Color.Black;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(360, 387);
            btnSave.Margin = new Padding(40, 10, 40, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(400, 53);
            btnSave.TabIndex = 6;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // sectionAddress
            // 
            sectionAddress.Dock = DockStyle.Bottom;
            sectionAddress.FormattingEnabled = true;
            sectionAddress.Location = new Point(323, 305);
            sectionAddress.Margin = new Padding(3, 3, 30, 3);
            sectionAddress.MaximumSize = new Size(600, 0);
            sectionAddress.Name = "sectionAddress";
            sectionAddress.Size = new Size(447, 28);
            sectionAddress.TabIndex = 11;
            // 
            // addressField
            // 
            addressField.Dock = DockStyle.Bottom;
            addressField.Location = new Point(323, 82);
            addressField.Margin = new Padding(3, 3, 30, 3);
            addressField.MaximumSize = new Size(600, 0);
            addressField.Name = "addressField";
            addressField.PlaceholderText = "г.Тверь, ул. ул. Маршала Буденного, 10";
            addressField.Size = new Size(447, 27);
            addressField.TabIndex = 12;
            // 
            // SectionEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.GhostWhite;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayout);
            Name = "SectionEditForm";
            Text = "ScheduleEditForm";
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
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
    }
}