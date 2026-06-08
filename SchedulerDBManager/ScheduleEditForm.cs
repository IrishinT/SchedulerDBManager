using SchedulerDBManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class ScheduleEditForm : Form
    {
        public Schedule CurrentSchedule { get; private set; }

        public ScheduleEditForm(IEnumerable<Section> sections, Schedule schedule = null)
        {
            InitializeComponent();
            SetupFormBehavior();
            BindSectionsCombo(sections);
            InitializeFormData(schedule);
        }

        private void SetupFormBehavior()
        {
            btnSave.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            btnSave.Click += BtnSave_Click;
        }

        private void BindSectionsCombo(IEnumerable<Section> sections)
        {
            var displaySections = sections.Select(s => new { Id = s.SectionId, Name = s.Address.Trim() }).ToList();
            sectionAddress.DataSource = displaySections;
            sectionAddress.DisplayMember = "Name";
            sectionAddress.ValueMember = "Id";
        }

        private void InitializeFormData(Schedule schedule)
        {
            if (schedule == null)
            {
                this.Text = "Добавление смены";
                CurrentSchedule = new Schedule();
                startTimeDate.Value = DateTime.Now;
                endTimeDate.Value = DateTime.Now.AddHours(8);
                workersCount.Value = 5;
            }
            else
            {
                this.Text = "Редактирование смены";
                CurrentSchedule = new Schedule { ShiftId = schedule.ShiftId, SectionId = schedule.SectionId };

                startTimeDate.Value = schedule.StartTime;
                endTimeDate.Value = schedule.EndTime;
                supervisor.Text = schedule.SupervisorFullname;
                workersCount.Value = schedule.WorkerCount;
                sectionAddress.SelectedValue = schedule.SectionId;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            CurrentSchedule.StartTime = startTimeDate.Value;
            CurrentSchedule.EndTime = endTimeDate.Value;
            CurrentSchedule.SupervisorFullname = supervisor.Text;
            CurrentSchedule.WorkerCount = (int)workersCount.Value;
            if (sectionAddress.SelectedValue != null)
                CurrentSchedule.SectionId = (int)sectionAddress.SelectedValue;
        }
    }
}