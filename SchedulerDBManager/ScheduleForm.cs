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

        private List<Schedule> allSchedules = new List<Schedule>();

        // Флаг для предотвращения рекурсии при обновлении списков
        private bool isUpdatingFilters = false;

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

            cmbFilterSupervisor.SelectedIndexChanged += FilterControl_Changed;
            cmbFilterAddress.SelectedIndexChanged += FilterControl_Changed;
            cmbSortBy.SelectedIndexChanged += (s, e) => ApplyFilters();

            dvgSchedules.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvgSchedules.MultiSelect = false;
            dvgSchedules.ReadOnly = true;
            dvgSchedules.AllowUserToAddRows = false;
        }

        private void Schedule_Load(object sender, EventArgs e)
        {
            if (cmbSortBy.Items.Count > 0) cmbSortBy.SelectedIndex = 0;
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                allSchedules = scheduleService.GetAllSchedules().ToList();

                // Сначала обновляем списки в комбобоксах, чтобы там были актуальные данные
                ResetFilterSelections();
                UpdateFilterControls();

                ApplyFilters();
                SetupColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void ResetFilterSelections()
        {
            isUpdatingFilters = true;

            cmbFilterSupervisor.Items.Clear();
            cmbFilterSupervisor.Items.Add("Все");
            cmbFilterSupervisor.SelectedIndex = 0;

            cmbFilterAddress.Items.Clear();
            cmbFilterAddress.Items.Add("Все");
            cmbFilterAddress.SelectedIndex = 0;

            isUpdatingFilters = false;
        }

        private void FilterControl_Changed(object sender, EventArgs e)
        {
            if (isUpdatingFilters) return;

            isUpdatingFilters = true;

            // Запоминаем, что выбрал пользователь
            string selectedSupervisor = cmbFilterSupervisor.SelectedItem?.ToString();
            string selectedAddress = cmbFilterAddress.SelectedItem?.ToString();

            // 1. Обновляем список адресов на основе выбранного начальника
            var availableAddresses = allSchedules
                .Where(s => selectedSupervisor == "Все" || s.SupervisorFullname == selectedSupervisor)
                .Select(s => s.SectionAddress)
                .Distinct().OrderBy(x => x).ToList();

            cmbFilterAddress.Items.Clear();
            cmbFilterAddress.Items.Add("Все");
            cmbFilterAddress.Items.AddRange(availableAddresses.ToArray());

            // Возвращаем выбор адреса, если он еще доступен в новом списке
            if (cmbFilterAddress.Items.Contains(selectedAddress))
                cmbFilterAddress.SelectedItem = selectedAddress;
            else
                cmbFilterAddress.SelectedIndex = 0;

            // 2. Обновляем список начальников на основе выбранного адреса
            var availableSupervisors = allSchedules
                .Where(s => selectedAddress == "Все" || s.SectionAddress == selectedAddress)
                .Select(s => s.SupervisorFullname)
                .Distinct().OrderBy(x => x).ToList();

            cmbFilterSupervisor.Items.Clear();
            cmbFilterSupervisor.Items.Add("Все");
            cmbFilterSupervisor.Items.AddRange(availableSupervisors.ToArray());

            // Возвращаем выбор начальника, если он еще доступен
            if (cmbFilterSupervisor.Items.Contains(selectedSupervisor))
                cmbFilterSupervisor.SelectedItem = selectedSupervisor;
            else
                cmbFilterSupervisor.SelectedIndex = 0;

            isUpdatingFilters = false;

            // Применяем результат к таблице
            ApplyFilters();
        }

        // Первичное заполнение фильтров при загрузке данных
        private void UpdateFilterControls()
        {
            isUpdatingFilters = true;

            var supervisors = allSchedules.Select(s => s.SupervisorFullname).Distinct().OrderBy(x => x).ToArray();
            var addresses = allSchedules.Select(s => s.SectionAddress).Distinct().OrderBy(x => x).ToArray();

            cmbFilterSupervisor.Items.Clear();
            cmbFilterSupervisor.Items.Add("Все");
            cmbFilterSupervisor.Items.AddRange(supervisors);
            cmbFilterSupervisor.SelectedIndex = 0;

            cmbFilterAddress.Items.Clear();
            cmbFilterAddress.Items.Add("Все");
            cmbFilterAddress.Items.AddRange(addresses);
            cmbFilterAddress.SelectedIndex = 0;

            isUpdatingFilters = false;
        }

        private void ApplyFilters()
        {
            if (allSchedules == null) return;

            string selectedSupervisor = cmbFilterSupervisor.SelectedItem?.ToString();
            string selectedAddress = cmbFilterAddress.SelectedItem?.ToString();

            // Фильтруем данные
            var filtered = allSchedules.Where(s =>
                (selectedSupervisor == "Все" || s.SupervisorFullname == selectedSupervisor) &&
                (selectedAddress == "Все" || s.SectionAddress == selectedAddress)
            );

            // Сортируем данные
            string sortOption = cmbSortBy.SelectedItem?.ToString();
            switch (sortOption)
            {
                case "По дате":
                    filtered = filtered.OrderBy(s => s.StartTime);
                    break;
                case "По количеству рабочих":
                    filtered = filtered.OrderByDescending(s => s.WorkerCount);
                    break;
                case "По длительности":
                    filtered = filtered.OrderByDescending(s => s.Duration);
                    break;
            }

            dvgSchedules.DataSource = filtered.ToList();
        }

        private void SetupColumns()
        {
            if (dvgSchedules.Columns.Count == 0) return;
            // (Ваш существующий код скрытия ID и перевода заголовков)
            if (dvgSchedules.Columns.Contains("ShiftId")) dvgSchedules.Columns["ShiftId"].Visible = false;
            if (dvgSchedules.Columns.Contains("SectionId")) dvgSchedules.Columns["SectionId"].Visible = false;
            if (dvgSchedules.Columns.Contains("ShiftDate")) dvgSchedules.Columns["ShiftDate"].Visible = false;

            if (dvgSchedules.Columns.Contains("SectionAddress"))
                dvgSchedules.Columns["SectionAddress"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
