using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.Presentation.Helpers;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class SectionForm : Form
    {
        private readonly SectionService sectionService;
        private readonly DepartmentService departmentService;

        private List<Section> allSections = new List<Section>();
        private List<Department> allDepartments = new List<Department>();
        private ToolTip toolTip;

        public SectionForm(SectionService sectionService, DepartmentService departmentService, User user)
        {
            InitializeComponent();
            this.sectionService = sectionService;
            this.departmentService = departmentService;

            SetupEventHandlers();
            InitializeToolTips();

            UIHelper.ApplySecurity(user, btnAdd, btnEdit, btnDelete);
        }

        private void SetupEventHandlers()
        {
            this.Load += (s, e) => { cmbSortBy.SelectedIndex = 0; RefreshData(); };
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnHelp.Click += btnHelp_Click;

            searchField.TextChanged += (s, e) => ApplyFilters();
            cmbFilterDepartment.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbSortBy.SelectedIndexChanged += (s, e) => ApplyFilters();
        }

        private void InitializeToolTips()
        {
            toolTip = new ToolTip { ShowAlways = true };
            toolTip.SetToolTip(searchField, "Введите часть адреса для быстрого поиска");
            toolTip.SetToolTip(cmbFilterDepartment, "Фильтр по подразделению");
            toolTip.SetToolTip(cmbSortBy, "Сортировка списка");
            toolTip.SetToolTip(btnAdd, "Добавить новый производственный участок");
            toolTip.SetToolTip(btnEdit, "Изменить свойства выбранного участка");
            toolTip.SetToolTip(btnDelete, "Удалить выбранный участок");
        }

        private void RefreshData()
        {
            UIHelper.SafeExecute(() =>
            {
                // Загружаем данные
                allSections = sectionService.GetAllSections().ToList();
                allDepartments = departmentService.GetAllDepartments().ToList();

                // Обновляем комбобокс фильтра
                UpdateDepartmentFilter();

                // Применяем фильтры и настраиваем таблицу
                ApplyFilters();

                UIHelper.ConfigureGrid(
                    dgvSections,
                    hideColumns: ["SectionId", "DepartmentId"],
                    renameColumns: new Dictionary<string, string> {
                        { "Address", "Адрес участка" },
                        { "DepartmentName", "Подразделение" },
                        { "Phone", "Телефон" }
                    },
                    fillColumn: ["Address"]
                );
            }, "Ошибка загрузки данных");
        }

        private void UpdateDepartmentFilter()
        {
            string currentSelection = cmbFilterDepartment.SelectedItem?.ToString() ?? "Все подразделения";

            cmbFilterDepartment.Items.Clear();
            cmbFilterDepartment.Items.Add("Все подразделения");
            foreach (var dept in allDepartments)
                cmbFilterDepartment.Items.Add(dept.DepartmentName);

            cmbFilterDepartment.SelectedItem = cmbFilterDepartment.Items.Contains(currentSelection)
                ? currentSelection
                : "Все подразделения";
        }

        private void ApplyFilters()
        {
            if (allSections == null) return;

            var filtered = allSections.AsEnumerable();

            // 1. Поиск по адресу
            string search = searchField.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(search))
                filtered = filtered.Where(s => s.Address != null && s.Address.ToLower().Contains(search));

            // 2. Фильтр по отделу
            if (cmbFilterDepartment.SelectedIndex > 0)
            {
                string selectedDept = cmbFilterDepartment.SelectedItem.ToString();
                filtered = filtered.Where(s => s.DepartmentName == selectedDept);
            }

            // 3. Сортировка
            filtered = cmbSortBy.SelectedItem?.ToString() switch
            {
                "По телефону" => filtered.OrderBy(s => s.Phone),
                _ => filtered.OrderBy(s => s.Address) // "По адресу"
            };

            dgvSections.DataSource = filtered.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UIHelper.SafeExecute(() =>
            {
                using var form = new SectionEditForm(allDepartments);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    sectionService.CreateSection(form.CurrentSection);
                    RefreshData();
                }
            }, "Ошибка добавления");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSections.SelectedRows.Count == 0) return;
            var selected = (Section)dgvSections.SelectedRows[0].DataBoundItem;

            UIHelper.SafeExecute(() =>
            {
                using var form = new SectionEditForm(allDepartments, selected);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    sectionService.UpdateSection(form.CurrentSection);
                    RefreshData();
                }
            }, "Ошибка обновления");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSections.SelectedRows.Count == 0) return;
            var selected = (Section)dgvSections.SelectedRows[0].DataBoundItem;

            if (UIHelper.ConfirmDelete($"Участок: {selected.Address}"))
            {
                UIHelper.SafeExecute(() =>
                {
                    sectionService.RemoveSection(selected.SectionId);
                    RefreshData();
                }, "Ошибка удаления");
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string helpText =
                "Руководство по работе с реестром участков:\n\n" +
                "1. Просмотр данных:\n" +
                "- В таблице представлен список всех производственных площадок предприятия.\n" +
                "- Для каждой площадки указан её адрес, принадлежность к отделу и телефон.\n\n" +
                "2. Добавление участка:\n" +
                "- Нажмите кнопку 'Добавить'.\n" +
                "- Введите полный адрес площадки.\n" +
                "- Введите номер телефона для связи.\n" +
                "- Выберите из списка подразделение, за которым закреплен данный участок.\n\n" +
                "3. Редактирование:\n" +
                "- Выберите участок в списке и нажмите 'Редактировать'.\n" +
                "- Изменение адреса участка автоматически обновит его отображение во всех связанных сменах.\n\n" +
                "4. Удаление:\n" +
                "- Удаление участка возможно только в том случае, если на него не назначено ни одной смены в расписании.\n\n" +
                "Совет: Если адрес содержит кавычки, программа автоматически очистит их для корректного отображения.";

            MessageBox.Show(helpText, "Справка: Управление участками", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}