namespace SchedulerDBManager.Presentation
{
    partial class ScheduleEditForm
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
            endTimeDate = new DateTimePicker();
            shiftStartDateLbl = new Label();
            shiftEndDateLbl = new Label();
            supervisorLbl = new Label();
            workerCountLbl = new Label();
            sectionLbl = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            startTimeDate = new DateTimePicker();
            supervisor = new TextBox();
            workersCount = new NumericUpDown();
            sectionAddress = new ComboBox();
            tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)workersCount).BeginInit();
            SuspendLayout();
            // 
            // tableLayout
            // 
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.Controls.Add(endTimeDate, 1, 1);
            tableLayout.Controls.Add(shiftStartDateLbl, 0, 0);
            tableLayout.Controls.Add(shiftEndDateLbl, 0, 1);
            tableLayout.Controls.Add(supervisorLbl, 0, 2);
            tableLayout.Controls.Add(workerCountLbl, 0, 3);
            tableLayout.Controls.Add(sectionLbl, 0, 4);
            tableLayout.Controls.Add(btnCancel, 0, 5);
            tableLayout.Controls.Add(btnSave, 1, 5);
            tableLayout.Controls.Add(startTimeDate, 1, 0);
            tableLayout.Controls.Add(supervisor, 1, 2);
            tableLayout.Controls.Add(workersCount, 1, 3);
            tableLayout.Controls.Add(sectionAddress, 1, 4);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 6;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayout.Size = new Size(800, 450);
            tableLayout.TabIndex = 0;
            // 
            // endTimeDate
            // 
            endTimeDate.CustomFormat = "dd.MM.yyyy HH:00";
            endTimeDate.Dock = DockStyle.Bottom;
            endTimeDate.Format = DateTimePickerFormat.Custom;
            endTimeDate.ImeMode = ImeMode.NoControl;
            endTimeDate.Location = new Point(403, 120);
            endTimeDate.MinDate = new DateTime(1999, 1, 1, 0, 0, 0, 0);
            endTimeDate.Name = "endTimeDate";
            endTimeDate.Size = new Size(394, 27);
            endTimeDate.TabIndex = 8;
            endTimeDate.Value = new DateTime(2026, 6, 1, 23, 15, 0, 0);
            // 
            // shiftStartDateLbl
            // 
            shiftStartDateLbl.AutoSize = true;
            shiftStartDateLbl.Dock = DockStyle.Bottom;
            shiftStartDateLbl.Location = new Point(3, 55);
            shiftStartDateLbl.Name = "shiftStartDateLbl";
            shiftStartDateLbl.Size = new Size(394, 20);
            shiftStartDateLbl.TabIndex = 0;
            shiftStartDateLbl.Text = "Начало смены:";
            // 
            // shiftEndDateLbl
            // 
            shiftEndDateLbl.AutoSize = true;
            shiftEndDateLbl.Dock = DockStyle.Bottom;
            shiftEndDateLbl.Location = new Point(3, 130);
            shiftEndDateLbl.Name = "shiftEndDateLbl";
            shiftEndDateLbl.Size = new Size(394, 20);
            shiftEndDateLbl.TabIndex = 1;
            shiftEndDateLbl.Text = "Конец смены:";
            // 
            // supervisorLbl
            // 
            supervisorLbl.AutoSize = true;
            supervisorLbl.Dock = DockStyle.Bottom;
            supervisorLbl.Location = new Point(3, 205);
            supervisorLbl.Name = "supervisorLbl";
            supervisorLbl.Size = new Size(394, 20);
            supervisorLbl.TabIndex = 2;
            supervisorLbl.Text = "Начальник:";
            // 
            // workerCountLbl
            // 
            workerCountLbl.AutoSize = true;
            workerCountLbl.Dock = DockStyle.Bottom;
            workerCountLbl.Location = new Point(3, 280);
            workerCountLbl.Name = "workerCountLbl";
            workerCountLbl.Size = new Size(394, 20);
            workerCountLbl.TabIndex = 3;
            workerCountLbl.Text = "Количество рабочих:";
            // 
            // sectionLbl
            // 
            sectionLbl.AutoSize = true;
            sectionLbl.Dock = DockStyle.Bottom;
            sectionLbl.Location = new Point(3, 355);
            sectionLbl.Name = "sectionLbl";
            sectionLbl.Size = new Size(394, 20);
            sectionLbl.TabIndex = 4;
            sectionLbl.Text = "Участок:";
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
            // startTimeDate
            // 
            startTimeDate.CustomFormat = "dd.MM.yyyy HH:00";
            startTimeDate.Dock = DockStyle.Bottom;
            startTimeDate.Format = DateTimePickerFormat.Custom;
            startTimeDate.ImeMode = ImeMode.NoControl;
            startTimeDate.Location = new Point(403, 45);
            startTimeDate.MinDate = new DateTime(1999, 1, 1, 0, 0, 0, 0);
            startTimeDate.Name = "startTimeDate";
            startTimeDate.Size = new Size(394, 27);
            startTimeDate.TabIndex = 7;
            startTimeDate.Value = new DateTime(2026, 6, 1, 9, 10, 0, 0);
            // 
            // supervisor
            // 
            supervisor.Dock = DockStyle.Bottom;
            supervisor.Location = new Point(403, 195);
            supervisor.Name = "supervisor";
            supervisor.PlaceholderText = "Введите ФИО начальника смены";
            supervisor.Size = new Size(394, 27);
            supervisor.TabIndex = 9;
            // 
            // workersCount
            // 
            workersCount.Dock = DockStyle.Bottom;
            workersCount.Location = new Point(403, 270);
            workersCount.Name = "workersCount";
            workersCount.Size = new Size(394, 27);
            workersCount.TabIndex = 10;
            // 
            // sectionAddress
            // 
            sectionAddress.Dock = DockStyle.Bottom;
            sectionAddress.FormattingEnabled = true;
            sectionAddress.Location = new Point(403, 344);
            sectionAddress.Name = "sectionAddress";
            sectionAddress.Size = new Size(394, 28);
            sectionAddress.TabIndex = 11;
            // 
            // ScheduleEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayout);
            Name = "ScheduleEditForm";
            Text = "ScheduleEditForm";
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)workersCount).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayout;
        private Label shiftStartDateLbl;
        private Label shiftEndDateLbl;
        private Label supervisorLbl;
        private Label workerCountLbl;
        private Label sectionLbl;
        private Button btnCancel;
        private Button btnSave;
        private DateTimePicker startTimeDate;
        private DateTimePicker endTimeDate;
        private TextBox supervisor;
        private NumericUpDown workersCount;
        private ComboBox sectionAddress;
    }
}