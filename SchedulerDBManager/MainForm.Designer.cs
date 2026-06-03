namespace SchedulerDBManager.Presentation
{
    partial class MainForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            btnSchedule = new Button();
            btnSections = new Button();
            btnDepartments = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnSchedule, 0, 1);
            tableLayoutPanel1.Controls.Add(btnSections, 1, 1);
            tableLayoutPanel1.Controls.Add(btnDepartments, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnSchedule
            // 
            btnSchedule.Dock = DockStyle.Fill;
            btnSchedule.Location = new Point(35, 147);
            btnSchedule.Margin = new Padding(35);
            btnSchedule.Name = "btnSchedule";
            btnSchedule.Size = new Size(330, 42);
            btnSchedule.TabIndex = 0;
            btnSchedule.Text = "Смены";
            btnSchedule.UseVisualStyleBackColor = true;
            // 
            // btnSections
            // 
            btnSections.Dock = DockStyle.Fill;
            btnSections.Location = new Point(435, 147);
            btnSections.Margin = new Padding(35);
            btnSections.Name = "btnSections";
            btnSections.Size = new Size(330, 42);
            btnSections.TabIndex = 1;
            btnSections.Text = "Участки";
            btnSections.UseVisualStyleBackColor = true;
            // 
            // btnDepartments
            // 
            btnDepartments.Dock = DockStyle.Fill;
            btnDepartments.Location = new Point(35, 259);
            btnDepartments.Margin = new Padding(35);
            btnDepartments.Name = "btnDepartments";
            btnDepartments.Size = new Size(330, 42);
            btnDepartments.TabIndex = 2;
            btnDepartments.Text = "Подразделения";
            btnDepartments.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            Text = "MainForm";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button btnSchedule;
        private Button btnSections;
        private Button btnDepartments;
    }
}