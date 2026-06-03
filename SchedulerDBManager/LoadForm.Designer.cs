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
            filePathField.Location = new Point(152, 93);
            filePathField.Name = "filePathField";
            filePathField.Size = new Size(370, 27);
            filePathField.TabIndex = 0;
            // 
            // explorerOpenBtn
            // 
            explorerOpenBtn.Location = new Point(528, 93);
            explorerOpenBtn.Name = "explorerOpenBtn";
            explorerOpenBtn.Size = new Size(94, 29);
            explorerOpenBtn.TabIndex = 1;
            explorerOpenBtn.Text = "...";
            explorerOpenBtn.UseVisualStyleBackColor = true;
            // 
            // connectBtn
            // 
            connectBtn.Location = new Point(275, 146);
            connectBtn.Name = "connectBtn";
            connectBtn.Size = new Size(226, 29);
            connectBtn.TabIndex = 2;
            connectBtn.Text = "Подключение";
            connectBtn.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(connectBtn);
            Controls.Add(explorerOpenBtn);
            Controls.Add(filePathField);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox filePathField;
        private Button explorerOpenBtn;
        private Button connectBtn;
    }
}
