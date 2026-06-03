using SchedulerDBManager.BusinessLogic.Services;
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

namespace SchedulerDBManager.Presentation
{
    public partial class ScheduleForm : Form
    {
        private readonly ScheduleService scheduleService;
        private readonly SectionService sectionService;

        public ScheduleForm(ScheduleService service, SectionService sectionService)
        {
            InitializeComponent();
            this.scheduleService = service;
            this.sectionService = sectionService;
            this.Load += Schedule_Load;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnHelp.Click += btnHelp_Click;

            dvgSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgSchedules.MultiSelect = false;
            dvgSchedules.ReadOnly = true;
            dvgSchedules.AllowUserToAddRows = false;
        }

        private void Schedule_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                var schedules = scheduleService.GetAllSchedules().ToList();
                dvgSchedules.DataSource = null;
                dvgSchedules.DataSource = schedules;

                // Скрываем ненужные колонки: ID, section_id, shift_date
                if (dvgSchedules.Columns.Contains("ShiftId"))
                    dvgSchedules.Columns["ShiftId"].Visible = false;

                if (dvgSchedules.Columns.Contains("SectionId"))
                    dvgSchedules.Columns["SectionId"].Visible = false;

                if (dvgSchedules.Columns.Contains("ShiftDate"))
                    dvgSchedules.Columns["ShiftDate"].Visible = false;

                // Переименовываем заголовки на русский
                if (dvgSchedules.Columns.Contains("StartTime"))
                    dvgSchedules.Columns["StartTime"].HeaderText = "Начало смены";

                if (dvgSchedules.Columns.Contains("EndTime"))
                    dvgSchedules.Columns["EndTime"].HeaderText = "Конец смены";

                if (dvgSchedules.Columns.Contains("Duration"))
                    dvgSchedules.Columns["Duration"].HeaderText = "Длительность (ч)";

                if (dvgSchedules.Columns.Contains("WorkerCount"))
                    dvgSchedules.Columns["WorkerCount"].HeaderText = "Рабочих";

                if (dvgSchedules.Columns.Contains("SupervisorFullname"))
                    dvgSchedules.Columns["SupervisorFullname"].HeaderText = "Начальник смены";

                if (dvgSchedules.Columns.Contains("SectionAddress"))
                    dvgSchedules.Columns["SectionAddress"].HeaderText = "Адрес участка";


                // растянуть колонку с адресом по ширине
                dvgSchedules.Columns["SectionAddress"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var sections = sectionService.GetAllSections();

            using (var editForm = new ScheduleEditForm(sections)) // Вызываем без параметров = создание
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        scheduleService.CreateSchedule(editForm.CurrentSchedule);
                        RefreshGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dvgSchedules.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите смену для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем выбранный объект из таблицы
            var selectedSchedule = (Schedule)dvgSchedules.SelectedRows[0].DataBoundItem;
            var sections = sectionService.GetAllSections();

            using (var editForm = new ScheduleEditForm(sections, selectedSchedule)) // Вызываем с параметром = редактирование
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        scheduleService.UpdateSchedule(editForm.CurrentSchedule);
                        RefreshGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка обновления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dvgSchedules.SelectedRows.Count == 0) return;

            var selectedSchedule = (Schedule)dvgSchedules.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show("Вы уверены, что хотите удалить выбранную смену?",
                                         "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    scheduleService.RemoveSchedule(selectedSchedule.ShiftId);
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string helpText =
                "Руководство по работе с расписанием смен:\n\n" +
                "1. Просмотр данных:\n" +
                "- В главной таблице отображается список всех запланированных смен. \n\n" +
                "2. Добавление смены:\n" +
                "- Нажмите на кнопку 'Добавить'.\n" +
                "- В открывшемся окне укажите время начала и конца смены, \n" +
                "- введите ФИО ответственного начальника,\n" +
                "- выберите адрес участка из выпадающего списка.\n\n" +
                "3. Редактирование:\n" +
                "- Выберите нужную строку в таблице (кликните по ней) и нажмите 'Редактировать'.\n" +
                "- Все текущие данные подтянутся в форму автоматически.\n\n" +
                "4. Удаление:\n" +
                "- Выберите смену и нажмите 'Удалить'. Система попросит подтверждение.\n\n" +
                "Особенности:\n" +
                "- Длительность смены рассчитывается программой автоматически.";

            MessageBox.Show(helpText, "Справка пользователя", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
