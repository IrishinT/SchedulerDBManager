namespace SchedulerDBManager.Presentation
{
    partial class ScheduleForm
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
            dvgSchedules = new DataGridView();
            splitContainer = new SplitContainer();
            tableLayoutPanel = new TableLayoutPanel();
            durationLbl = new Label();
            startTimeLbl = new Label();
            durationField = new TextBox();
            startTimeDate = new DateTimePicker();
            endTimeLbl = new Label();
            endTimeDate = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dvgSchedules).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // dvgSchedules
            // 
            dvgSchedules.BackgroundColor = SystemColors.Control;
            dvgSchedules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvgSchedules.Dock = DockStyle.Fill;
            dvgSchedules.Location = new Point(0, 0);
            dvgSchedules.Name = "dvgSchedules";
            dvgSchedules.RowHeadersWidth = 51;
            dvgSchedules.Size = new Size(446, 450);
            dvgSchedules.TabIndex = 0;
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
            splitContainer.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(dvgSchedules);
            splitContainer.Size = new Size(800, 450);
            splitContainer.SplitterDistance = 350;
            splitContainer.TabIndex = 1;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.8571434F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57.1428566F));
            tableLayoutPanel.Controls.Add(endTimeDate, 1, 2);
            tableLayoutPanel.Controls.Add(endTimeLbl, 0, 2);
            tableLayoutPanel.Controls.Add(startTimeLbl, 0, 1);
            tableLayoutPanel.Controls.Add(durationLbl, 0, 0);
            tableLayoutPanel.Controls.Add(durationField, 1, 0);
            tableLayoutPanel.Controls.Add(startTimeDate, 1, 1);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 9;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel.Size = new Size(350, 450);
            tableLayoutPanel.TabIndex = 0;
            tableLayoutPanel.Paint += tableLayoutPanel_Paint;
            // 
            // durationLbl
            // 
            durationLbl.AutoSize = true;
            durationLbl.Dock = DockStyle.Bottom;
            durationLbl.Location = new Point(3, 25);
            durationLbl.Name = "durationLbl";
            durationLbl.Size = new Size(144, 20);
            durationLbl.TabIndex = 0;
            durationLbl.Text = "Длительность";
            // 
            // startTimeLbl
            // 
            startTimeLbl.AutoSize = true;
            startTimeLbl.Dock = DockStyle.Bottom;
            startTimeLbl.Location = new Point(3, 70);
            startTimeLbl.Name = "startTimeLbl";
            startTimeLbl.Size = new Size(144, 20);
            startTimeLbl.TabIndex = 2;
            startTimeLbl.Text = "Начало";
            // 
            // durationField
            // 
            durationField.Dock = DockStyle.Bottom;
            durationField.Location = new Point(153, 15);
            durationField.Name = "durationField";
            durationField.PlaceholderText = "12 часов";
            durationField.Size = new Size(194, 27);
            durationField.TabIndex = 3;
            // 
            // startTimeDate
            // 
            startTimeDate.Dock = DockStyle.Bottom;
            startTimeDate.Location = new Point(153, 60);
            startTimeDate.MaxDate = new DateTime(2070, 12, 31, 0, 0, 0, 0);
            startTimeDate.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            startTimeDate.Name = "startTimeDate";
            startTimeDate.Size = new Size(194, 27);
            startTimeDate.TabIndex = 4;
            // 
            // endTimeLbl
            // 
            endTimeLbl.AutoSize = true;
            endTimeLbl.Dock = DockStyle.Bottom;
            endTimeLbl.Location = new Point(3, 115);
            endTimeLbl.Name = "endTimeLbl";
            endTimeLbl.Size = new Size(144, 20);
            endTimeLbl.TabIndex = 5;
            endTimeLbl.Text = "Конец";
            // 
            // endTimeDate
            // 
            endTimeDate.Dock = DockStyle.Bottom;
            endTimeDate.Location = new Point(153, 105);
            endTimeDate.MaxDate = new DateTime(2070, 12, 31, 0, 0, 0, 0);
            endTimeDate.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            endTimeDate.Name = "endTimeDate";
            endTimeDate.Size = new Size(194, 27);
            endTimeDate.TabIndex = 6;
            endTimeDate.Value = new DateTime(2026, 5, 28, 15, 0, 0, 0);
            // 
            // Schedule
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer);
            Name = "Schedule";
            Text = "Schedule";
            ((System.ComponentModel.ISupportInitialize)dvgSchedules).EndInit();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dvgSchedules;
        private SplitContainer splitContainer;
        private TableLayoutPanel tableLayoutPanel;
        private Label durationLbl;
        private Label startTimeLbl;
        private Label endTimeLbl;
        private TextBox durationField;
        private DateTimePicker startTimeDate;
        private DateTimePicker endTimeDate;
    }
}