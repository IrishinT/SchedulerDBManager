using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class SectionForm : Form
    {
        private readonly SectionService sectionService;
        private readonly DepartmentService departmentService;

        // Списки для хранения данных в памяти (для быстрой фильтрации)
        private List<Section> allSections = new List<Section>();
        private List<Department> departments = new List<Department>();

        public SectionForm(SectionService sectionService, DepartmentService departmentService)
        {
            InitializeComponent();
            this.sectionService = sectionService;
            this.departmentService = departmentService;
            this.Load += SectionForm_Load;

            // Настройка таблицы
            dgvSections.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSections.MultiSelect = false;
            dgvSections.ReadOnly = true;
            dgvSections.AllowUserToAddRows = false;

            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnHelp.Click += btnHelp_Click;

            searchField.TextChanged += (s, e) => ApplyFilters();
            cmbFilterDepartment.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbSortBy.SelectedIndexChanged += (s, e) => ApplyFilters();
        }

        private void SectionForm_Load(object sender, EventArgs e)
        {
            LoadFilterData();
            RefreshGrid();

            // Устанавливаем начальное значение сортировки
            if (cmbSortBy.Items.Count > 0) cmbSortBy.SelectedIndex = 0;
        }

        private void LoadFilterData()
        {
            try
            {
                // Получаем все подразделения для фильтра
                departments = departmentService.GetAllDepartments().ToList();

                cmbFilterDepartment.Items.Clear();
                cmbFilterDepartment.Items.Add("Все подразделения"); // Элемент для сброса фильтра

                foreach (Department dept in departments)
                {
                    cmbFilterDepartment.Items.Add(dept.DepartmentName);
                }

                cmbFilterDepartment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки фильтров: {ex.Message}");
            }
        }

        private void RefreshGrid()
        {
            try
            {
                // Загружаем актуальные данные из БД в кэш
                allSections = sectionService.GetAllSections().ToList();

                // Применяем фильтры (этот метод обновит dgvSections.DataSource)
                ApplyFilters();

                // Настройка внешнего вида колонок (выполняется один раз при наличии данных)
                SetupGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
        }

        private void ApplyFilters()
        {
            if (allSections == null) return;

            IEnumerable<Section> filteredData = allSections;

            // 1. Текстовый поиск (по адресу)
            string searchText = searchField.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredData = filteredData.Where(s =>
                    s.Address != null && s.Address.ToLower().Contains(searchText)
                );
            }

            // 2. Фильтрация по подразделению
            if (cmbFilterDepartment.SelectedIndex > 0) // Если выбрано конкретное подразделение (не "Все")
            {
                string selectedDeptName = cmbFilterDepartment.SelectedItem.ToString();
                filteredData = filteredData.Where(s => s.DepartmentName == selectedDeptName);
            }

            // 3. Сортировка
            string sortOption = cmbSortBy.SelectedItem?.ToString();
            switch (sortOption)
            {
                case "По адресу":
                    filteredData = filteredData.OrderBy(s => s.Address);
                    break;
                case "По телефону":
                    filteredData = filteredData.OrderBy(s => s.Phone);
                    break;
            }

            // Обновляем источник данных таблицы
            dgvSections.DataSource = filteredData.ToList();
        }

        private void SetupGridColumns()
        {
            if (dgvSections.Columns.Count == 0) return;

            // Скрываем технические ID
            if (dgvSections.Columns.Contains("SectionId")) dgvSections.Columns["SectionId"].Visible = false;
            if (dgvSections.Columns.Contains("DepartmentId")) dgvSections.Columns["DepartmentId"].Visible = false;

            // Переводим названия заголовков
            if (dgvSections.Columns.Contains("Address")) dgvSections.Columns["Address"].HeaderText = "Адрес";
            if (dgvSections.Columns.Contains("DepartmentName")) dgvSections.Columns["DepartmentName"].HeaderText = "Подразделение";
            if (dgvSections.Columns.Contains("Phone")) dgvSections.Columns["Phone"].HeaderText = "Телефон";

            // Оформление
            if (dgvSections.Columns.Contains("Address"))
                dgvSections.Columns["Address"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var departments = departmentService.GetAllDepartments();
                using (var form = new SectionEditForm(departments))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        sectionService.CreateSection(form.CurrentSection);
                        RefreshGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSections.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите участок для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var section = (Section)dgvSections.SelectedRows[0].DataBoundItem;
                var departments = departmentService.GetAllDepartments();

                using (var form = new SectionEditForm(departments, section))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        sectionService.UpdateSection(form.CurrentSection);
                        RefreshGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSections.SelectedRows.Count == 0) return;

            var section = (Section)dgvSections.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show($"Вы уверены, что хотите удалить участок по адресу:\n{section.Address}?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    sectionService.RemoveSection(section.SectionId);
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось удалить участок. Возможно, он используется в расписании смен.\n\nДетали: {ex.Message}",
                        "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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