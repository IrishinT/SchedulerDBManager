namespace SchedulerDBManager.Presentation
{
    partial class AuthForm
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
            splitContainer1 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            passField = new TextBox();
            passLbl = new Label();
            loginLbl = new Label();
            loginField = new TextBox();
            btnAuth = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnAuth);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.Controls.Add(passField, 1, 1);
            tableLayoutPanel1.Controls.Add(passLbl, 0, 1);
            tableLayoutPanel1.Controls.Add(loginLbl, 0, 0);
            tableLayoutPanel1.Controls.Add(loginField, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(800, 266);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // passField
            // 
            passField.BackColor = Color.White;
            passField.Dock = DockStyle.Bottom;
            passField.Location = new Point(355, 204);
            passField.Margin = new Padding(35, 35, 100, 35);
            passField.Name = "passField";
            passField.PlaceholderText = "Введите пароль";
            passField.Size = new Size(345, 27);
            passField.TabIndex = 3;
            passField.UseSystemPasswordChar = true;
            // 
            // passLbl
            // 
            passLbl.AutoSize = true;
            passLbl.Dock = DockStyle.Bottom;
            passLbl.Location = new Point(35, 211);
            passLbl.Margin = new Padding(35);
            passLbl.Name = "passLbl";
            passLbl.Size = new Size(250, 20);
            passLbl.TabIndex = 2;
            passLbl.Text = "Пароль:";
            passLbl.TextAlign = ContentAlignment.TopRight;
            // 
            // loginLbl
            // 
            loginLbl.AutoSize = true;
            loginLbl.Dock = DockStyle.Bottom;
            loginLbl.Location = new Point(35, 78);
            loginLbl.Margin = new Padding(35);
            loginLbl.Name = "loginLbl";
            loginLbl.Size = new Size(250, 20);
            loginLbl.TabIndex = 0;
            loginLbl.Text = "Логин:";
            loginLbl.TextAlign = ContentAlignment.TopRight;
            // 
            // loginField
            // 
            loginField.BackColor = Color.White;
            loginField.Dock = DockStyle.Bottom;
            loginField.Location = new Point(355, 71);
            loginField.Margin = new Padding(35, 35, 100, 35);
            loginField.Name = "loginField";
            loginField.PlaceholderText = "Введите логин";
            loginField.Size = new Size(345, 27);
            loginField.TabIndex = 1;
            // 
            // btnAuth
            // 
            btnAuth.Anchor = AnchorStyles.None;
            btnAuth.BackColor = Color.DodgerBlue;
            btnAuth.FlatAppearance.BorderColor = Color.White;
            btnAuth.FlatAppearance.MouseDownBackColor = Color.CornflowerBlue;
            btnAuth.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btnAuth.FlatStyle = FlatStyle.Flat;
            btnAuth.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnAuth.ForeColor = Color.White;
            btnAuth.Location = new Point(324, 20);
            btnAuth.Margin = new Padding(20);
            btnAuth.Name = "btnAuth";
            btnAuth.Size = new Size(151, 54);
            btnAuth.TabIndex = 0;
            btnAuth.Text = "Войти";
            btnAuth.UseVisualStyleBackColor = false;
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.GhostWhite;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AuthForm";
            Text = "Авторизация";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox passField;
        private Label passLbl;
        private Label loginLbl;
        private TextBox loginField;
        private Button btnAuth;
    }
}