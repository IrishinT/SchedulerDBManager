using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.Presentation.Helpers;
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

        private List<Schedule> allSchedules = new List<Schedule>();

        // Флаг для предотвращения рекурсии при обновлении списков
        private bool isUpdatingFilters = false;

        private ToolTip toolTip;

        public ScheduleForm(ScheduleService service, SectionService sectionService)
        {
            InitializeComponent();
            this.scheduleService = service;
            this.sectionService = sectionService;

            SetupEventHandlers();
            InitializeToolTips();
        }

        private void SetupEventHandlers()
        {
            this.Load += (s, e) => { cmbSortBy.SelectedIndex = 0; RefreshData(); };
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnHelp.Click += btnHelp_Click;

            cmbFilterSupervisor.SelectedIndexChanged += FilterControl_Changed;
            cmbFilterAddress.SelectedIndexChanged += FilterControl_Changed;
            cmbSortBy.SelectedIndexChanged += (s, e) => ApplyFilters();
        }

        private void InitializeToolTips()
        {
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 100;
            toolTip.ShowAlways = true;

            // Элементы управления сверху
            toolTip.SetToolTip(cmbFilterSupervisor, "Выберите начальника смены для отображения только его расписания");
            toolTip.SetToolTip(cmbFilterAddress, "Выберите адрес производственного участка для фильтрации смен");
            toolTip.SetToolTip(cmbSortBy, "Выберите критерий для сортировки расписания (по дате, количеству рабочих или длительности)");

            // Кнопки действий
            toolTip.SetToolTip(btnAdd, "Создать новую рабочую смену и добавить её в расписание");
            toolTip.SetToolTip(btnEdit, "Изменить время, начальника или участок выбранной смены");
            toolTip.SetToolTip(btnDelete, "Безвозвратно удалить выбранную смену из расписания");
            toolTip.SetToolTip(btnHelp, "Открыть руководство по работе с расписанием");

            // Таблица
            toolTip.SetToolTip(dvgSchedules, "Кликните на строку, чтобы выбрать смену для редактирования или удаления");
        }

        private void RefreshData()
        {
            UIHelper.SafeExecute(() =>
            {
                allSchedules = scheduleService.GetAllSchedules().ToList();
                UpdateFilterControls();
                ApplyFilters();

                UIHelper.ConfigureGrid(
                    dvgSchedules,
                    hideColumns: ["ShiftId", "SectionId", "ShiftDate"],
                    renameColumns: new Dictionary<string, string> {
                        { "StartTime", "Начало смены" }, { "EndTime", "Конец смены" },
                        { "Duration", "Длительность (ч)" }, { "WorkerCount", "Рабочих" },
                        { "SupervisorFullname", "Начальник смены" }, { "SectionAddress", "Адрес участка" }
                    },
                    fillColumn: ["SectionAddress"] 
                );
            });
        }

        private void FilterControl_Changed(object sender, EventArgs e)
        {
            if (isUpdatingFilters) return;
            UpdateFilterControls();
            ApplyFilters();
        }

        private void UpdateFilterControls()
        {
            isUpdatingFilters = true;

            string selSuper = cmbFilterSupervisor.SelectedItem?.ToString() ?? "Все";
            string selAddress = cmbFilterAddress.SelectedItem?.ToString() ?? "Все";

            // Выделяем получение уникальных значений в отдельные методы-хелперы LINQ
            var supervisors = allSchedules.Where(s => selAddress == "Все" || s.SectionAddress == selAddress)
                                          .Select(s => s.SupervisorFullname).Distinct().OrderBy(x => x).ToArray();

            var addresses = allSchedules.Where(s => selSuper == "Все" || s.SupervisorFullname == selSuper)
                                        .Select(s => s.SectionAddress).Distinct().OrderBy(x => x).ToArray();

            // Перезаписываем комбобоксы
            UpdateComboBox(cmbFilterSupervisor, supervisors, selSuper);
            UpdateComboBox(cmbFilterAddress, addresses, selAddress);

            isUpdatingFilters = false;
        }

        private void UpdateComboBox(ComboBox cmb, string[] items, string selectedValue)
        {
            cmb.Items.Clear();
            cmb.Items.Add("Все");
            cmb.Items.AddRange(items);
            cmb.SelectedItem = cmb.Items.Contains(selectedValue) ? selectedValue : "Все";
        }

        private void ApplyFilters()
        {
            if (allSchedules == null) return;

            string super = cmbFilterSupervisor.SelectedItem?.ToString();
            string address = cmbFilterAddress.SelectedItem?.ToString();

            // Чистый и понятный LINQ pipeline
            var filtered = allSchedules
                .Where(s => super == "Все" || s.SupervisorFullname == super)
                .Where(s => address == "Все" || s.SectionAddress == address);

            filtered = cmbSortBy.SelectedItem?.ToString() switch
            {
                "По количеству рабочих" => filtered.OrderByDescending(s => s.WorkerCount),
                "По длительности" => filtered.OrderByDescending(s => s.Duration),
                _ => filtered.OrderBy(s => s.StartTime) // "По дате" по умолчанию
            };

            dvgSchedules.DataSource = filtered.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UIHelper.SafeExecute(() =>
            {
                var sections = sectionService.GetAllSections();
                using var form = new ScheduleEditForm(sections);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    scheduleService.CreateSchedule(form.CurrentSchedule);
                    RefreshData();
                }
            }, "Ошибка добавления");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dvgSchedules.SelectedRows.Count == 0) return;

            UIHelper.SafeExecute(() =>
            {
                var schedule = (Schedule)dvgSchedules.SelectedRows[0].DataBoundItem;
                using var form = new ScheduleEditForm(sectionService.GetAllSections(), schedule);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    scheduleService.UpdateSchedule(form.CurrentSchedule);
                    RefreshData();
                }
            }, "Ошибка обновления");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dvgSchedules.SelectedRows.Count == 0) return;
            var schedule = (Schedule)dvgSchedules.SelectedRows[0].DataBoundItem;

            if (UIHelper.ConfirmDelete($"Смена на участке: {schedule.SectionAddress}"))
            {
                UIHelper.SafeExecute(() => {
                    scheduleService.RemoveSchedule(schedule.ShiftId);
                    RefreshData();
                }, "Ошибка удаления");
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
    }
}
