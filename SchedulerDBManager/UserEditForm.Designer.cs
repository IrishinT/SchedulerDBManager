namespace SchedulerDBManager.Presentation
{
    partial class UserEditForm
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
            passField = new TextBox();
            loginLbl = new Label();
            passLbl = new Label();
            roleLbl = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            roleField = new ComboBox();
            loginField = new TextBox();
            tableLayout.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.Controls.Add(passField, 1, 1);
            tableLayout.Controls.Add(loginLbl, 0, 0);
            tableLayout.Controls.Add(passLbl, 0, 1);
            tableLayout.Controls.Add(roleLbl, 0, 2);
            tableLayout.Controls.Add(btnCancel, 0, 3);
            tableLayout.Controls.Add(btnSave, 1, 3);
            tableLayout.Controls.Add(roleField, 1, 2);
            tableLayout.Controls.Add(loginField, 1, 0);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 4;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0000038F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0000038F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayout.Size = new Size(800, 450);
            tableLayout.TabIndex = 0;
            // 
            // passField
            // 
            passField.Dock = DockStyle.Bottom;
            passField.Location = new Point(403, 194);
            passField.Name = "passField";
            passField.PlaceholderText = "Введите пароль пользователя";
            passField.Size = new Size(394, 27);
            passField.TabIndex = 13;
            passField.UseSystemPasswordChar = true;
            // 
            // loginLbl
            // 
            loginLbl.AutoSize = true;
            loginLbl.Dock = DockStyle.Bottom;
            loginLbl.Location = new Point(3, 92);
            loginLbl.Name = "loginLbl";
            loginLbl.Size = new Size(394, 20);
            loginLbl.TabIndex = 0;
            loginLbl.Text = "Логин:";
            // 
            // passLbl
            // 
            passLbl.AutoSize = true;
            passLbl.Dock = DockStyle.Bottom;
            passLbl.Location = new Point(3, 204);
            passLbl.Name = "passLbl";
            passLbl.Size = new Size(394, 20);
            passLbl.TabIndex = 1;
            passLbl.Text = "Пароль:";
            // 
            // roleLbl
            // 
            roleLbl.AutoSize = true;
            roleLbl.Dock = DockStyle.Bottom;
            roleLbl.Location = new Point(3, 316);
            roleLbl.Name = "roleLbl";
            roleLbl.Size = new Size(394, 20);
            roleLbl.TabIndex = 4;
            roleLbl.Text = "Роль:";
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
            // roleField
            // 
            roleField.Dock = DockStyle.Bottom;
            roleField.FormattingEnabled = true;
            roleField.Location = new Point(403, 305);
            roleField.Name = "roleField";
            roleField.Size = new Size(394, 28);
            roleField.TabIndex = 11;
            // 
            // loginField
            // 
            loginField.Dock = DockStyle.Bottom;
            loginField.Location = new Point(403, 82);
            loginField.Name = "loginField";
            loginField.PlaceholderText = "Введите логин пользователя";
            loginField.Size = new Size(394, 27);
            loginField.TabIndex = 12;
            // 
            // UserEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayout);
            Name = "UserEditForm";
            Text = "ScheduleEditForm";
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayout;
        private Label loginLbl;
        private Label passLbl;
        private Label roleLbl;
        private ComboBox roleField;
        private TextBox loginField;
        private TextBox passField;
        private Button btnCancel;
        private Button btnSave;
    }
}