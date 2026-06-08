    using SchedulerDBManager.DataAccess.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using static System.Collections.Specialized.BitVector32;

    namespace SchedulerDBManager.Presentation
    {
        public partial class ScheduleEditForm : Form
        {
            public Schedule CurrentSchedule { get; private set; }

            public ScheduleEditForm(IEnumerable<DataAccess.Models.Section> sections, Schedule schedule = null)
            {
                InitializeComponent();

                btnSave.DialogResult = DialogResult.OK;
                btnCancel.DialogResult = DialogResult.Cancel;

                this.AcceptButton = btnSave;
                this.CancelButton = btnCancel;

                btnSave.Click += BtnSave_Click;

                var cleanSections = sections.Select(s => new DataAccess.Models.Section
                {
                    SectionId = s.SectionId,
                    Address = s.Address.Replace("\"", "").Trim()
                }).ToList();

                sectionAddress.DataSource = cleanSections;
                sectionAddress.DisplayMember = "Address"; // Что видит пользователь
                sectionAddress.ValueMember = "SectionId"; // Что сохранится в базу


                if (schedule == null)
                {
                    this.Text = "Добавление смены";
                    CurrentSchedule = new Schedule();
                    // Дефолтные значения
                    startTimeDate.Value = DateTime.Now;
                    endTimeDate.Value = DateTime.Now.AddHours(8);
                    workersCount.Value = 5;
                }
                else
                {
                    this.Text = "Редактирование смены";
                    // Копируем данные, чтобы не менять оригинал до нажатия "Сохранить"
                    CurrentSchedule = new Schedule
                    {
                        ShiftId = schedule.ShiftId,
                        Duration = schedule.Duration,
                        StartTime = schedule.StartTime,
                        EndTime = schedule.EndTime,
                        WorkerCount = schedule.WorkerCount,
                        SupervisorFullname = schedule.SupervisorFullname,
                        SectionId = schedule.SectionId
                    };

                    // Заполняем поля на форме
                    startTimeDate.Value = CurrentSchedule.StartTime;
                    endTimeDate.Value = CurrentSchedule.EndTime;
                    supervisor.Text = CurrentSchedule.SupervisorFullname;
                    workersCount.Value = CurrentSchedule.WorkerCount;
                }
            }

            private void BtnSave_Click(object sender, EventArgs e)
            {
                // Считываем данные с элементов управления обратно в объект
                CurrentSchedule.StartTime = startTimeDate.Value;
                CurrentSchedule.EndTime = endTimeDate.Value;
                CurrentSchedule.SupervisorFullname = supervisor.Text.Trim();
                CurrentSchedule.WorkerCount = (int)workersCount.Value;

                CurrentSchedule.ShiftDate = CurrentSchedule.StartTime.Date;
                // Сразу пересчитываем длительность в часах
                CurrentSchedule.Duration = (int)(CurrentSchedule.EndTime - CurrentSchedule.StartTime).TotalHours;

                // Забираем ID выбранного участка из ComboBox
                if (sectionAddress.SelectedValue != null)
                {
                    CurrentSchedule.SectionId = (int)sectionAddress.SelectedValue;
                }
            }

        }
    }
