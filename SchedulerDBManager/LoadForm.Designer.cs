namespace SchedulerDBManager.Presentaton
{
    partial class LoadForm
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
            filePathField = new TextBox();
            explorerOpenBtn = new Button();
            connectBtn = new Button();
            SuspendLayout();
            // 
            // filePathField
            // 
            filePathField.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            filePathField.ForeColor = SystemColors.WindowText;
            filePathField.Location = new Point(123, 91);
            filePathField.Name = "filePathField";
            filePathField.PlaceholderText = "Введите путь к файлу с БД (.accdb) ...";
            filePathField.Size = new Size(438, 30);
            filePathField.TabIndex = 0;
            // 
            // explorerOpenBtn
            // 
            explorerOpenBtn.BackColor = Color.DodgerBlue;
            explorerOpenBtn.FlatAppearance.BorderColor = Color.Black;
            explorerOpenBtn.FlatStyle = FlatStyle.Flat;
            explorerOpenBtn.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            explorerOpenBtn.ForeColor = Color.White;
            explorerOpenBtn.Location = new Point(567, 91);
            explorerOpenBtn.Name = "explorerOpenBtn";
            explorerOpenBtn.Size = new Size(78, 30);
            explorerOpenBtn.TabIndex = 1;
            explorerOpenBtn.Text = "📁";
            explorerOpenBtn.UseVisualStyleBackColor = false;
            // 
            // connectBtn
            // 
            connectBtn.BackColor = Color.DodgerBlue;
            connectBtn.FlatAppearance.BorderColor = Color.White;
            connectBtn.FlatAppearance.MouseDownBackColor = Color.CornflowerBlue;
            connectBtn.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            connectBtn.FlatStyle = FlatStyle.Flat;
            connectBtn.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            connectBtn.ForeColor = Color.White;
            connectBtn.Location = new Point(280, 264);
            connectBtn.Name = "connectBtn";
            connectBtn.Size = new Size(226, 48);
            connectBtn.TabIndex = 2;
            connectBtn.Text = "Подключиться";
            connectBtn.UseVisualStyleBackColor = false;
            // 
            // LoadForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.GhostWhite;
            ClientSize = new Size(800, 450);
            Controls.Add(connectBtn);
            Controls.Add(explorerOpenBtn);
            Controls.Add(filePathField);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LoadForm";
            Text = "Подключение к БД";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox filePathField;
        private Button explorerOpenBtn;
        private Button connectBtn;
    }
}
