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
            headField = new TextBox();
            nameLbl = new Label();
            headLbl = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            nameField = new TextBox();
            tableLayout.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayout.Controls.Add(headField, 1, 1);
            tableLayout.Controls.Add(nameLbl, 0, 0);
            tableLayout.Controls.Add(headLbl, 0, 1);
            tableLayout.Controls.Add(btnCancel, 0, 2);
            tableLayout.Controls.Add(btnSave, 1, 2);
            tableLayout.Controls.Add(nameField, 1, 0);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 3;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33334F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayout.Size = new Size(800, 450);
            tableLayout.TabIndex = 0;
            // 
            // headField
            // 
            headField.Dock = DockStyle.Bottom;
            headField.Location = new Point(323, 269);
            headField.Margin = new Padding(3, 3, 50, 3);
            headField.MaximumSize = new Size(400, 0);
            headField.Name = "headField";
            headField.PlaceholderText = "Иванов Иван Иванович";
            headField.Size = new Size(400, 27);
            headField.TabIndex = 13;
            // 
            // nameLbl
            // 
            nameLbl.AutoSize = true;
            nameLbl.Dock = DockStyle.Bottom;
            nameLbl.Location = new Point(20, 129);
            nameLbl.Margin = new Padding(20, 0, 10, 0);
            nameLbl.Name = "nameLbl";
            nameLbl.Size = new Size(290, 20);
            nameLbl.TabIndex = 0;
            nameLbl.Text = "Название:";
            nameLbl.TextAlign = ContentAlignment.TopRight;
            // 
            // headLbl
            // 
            headLbl.AutoSize = true;
            headLbl.Dock = DockStyle.Bottom;
            headLbl.Location = new Point(20, 279);
            headLbl.Margin = new Padding(20, 0, 10, 0);
            headLbl.Name = "headLbl";
            headLbl.Size = new Size(290, 20);
            headLbl.TabIndex = 1;
            headLbl.Text = "Руководитель:";
            headLbl.TextAlign = ContentAlignment.TopRight;
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
            // nameField
            // 
            nameField.Dock = DockStyle.Bottom;
            nameField.Location = new Point(323, 119);
            nameField.Margin = new Padding(3, 3, 50, 3);
            nameField.MaximumSize = new Size(400, 0);
            nameField.Name = "nameField";
            nameField.PlaceholderText = "Техническая Поддержка L3";
            nameField.Size = new Size(400, 27);
            nameField.TabIndex = 12;
            // 
            // DepartmentEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.GhostWhite;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayout);
            Name = "DepartmentEditForm";
            Text = "DepartmentEditForm";
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayout;
        private Label nameLbl;
        private Label headLbl;
        private TextBox nameField;
        private TextBox headField;
        private Button btnCancel;
        private Button btnSave;
    }
}