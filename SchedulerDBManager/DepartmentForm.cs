using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class DepartmentForm : Form
    {
        private readonly DepartmentService departmentService;
        private List<Department> allDepartments = new List<Department>();

        public DepartmentForm(DepartmentService departmentService)
        {
            InitializeComponent();
            this.departmentService = departmentService;
            this.Load += DepartmentForm_Load;

            // Настройка кнопок (привязка событий)
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnHelp.Click += btnHelp_Click;

            // Настройка DataGridView
            dgvDepartments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDepartments.MultiSelect = false;
            dgvDepartments.ReadOnly = true;
            dgvDepartments.AllowUserToAddRows = false;
            searchField.TextChanged += (s, e) => ApplyFilters();
            cmbSortBy.SelectedIndexChanged += (s, e) => ApplyFilters();
        }

        private void DepartmentForm_Load(object sender, EventArgs e)
        {
            if (cmbSortBy.Items.Count > 0) cmbSortBy.SelectedIndex = 0;
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                // Загружаем данные из сервиса
                allDepartments = departmentService.GetAllDepartments().ToList();
                ApplyFilters();
                SetupGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }

        private void ApplyFilters()
        {
            if (allDepartments == null) return;

            IEnumerable<Department> filteredData = allDepartments;

            // 1. Поиск по названию
            string searchText = searchField.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredData = filteredData.Where(d =>
                    d.DepartmentName != null && d.DepartmentName.ToLower().Contains(searchText));
            }

            // 2. Сортировка (по названию или руководителю)
            string sortOption = cmbSortBy.SelectedItem?.ToString();
            switch (sortOption)
            {
                case "По названию":
                    filteredData = filteredData.OrderBy(d => d.DepartmentName);
                    break;
                case "По руководителю":
                    filteredData = filteredData.OrderBy(d => d.HeadFullName);
                    break;
            }

            dgvDepartments.DataSource = filteredData.ToList();
        }

        private void SetupGridColumns()
        {
            if (dgvDepartments.Columns.Count == 0) return;

            if (dgvDepartments.Columns.Contains("DepartmentId"))
                dgvDepartments.Columns["DepartmentId"].Visible = false;

            if (dgvDepartments.Columns.Contains("DepartmentName"))
                dgvDepartments.Columns["DepartmentName"].HeaderText = "Название подразделения";

            if (dgvDepartments.Columns.Contains("HeadFullName"))
                dgvDepartments.Columns["HeadFullName"].HeaderText = "ФИО Руководителя";

            // Растягиваем колонку
            if (dgvDepartments.Columns.Contains("DepartmentName"))
                dgvDepartments.Columns["DepartmentName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new DepartmentEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        departmentService.CreateDepartment(editForm.CurrentDepartment);
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
            if (dgvDepartments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите подразделение для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedDept = (Department)dgvDepartments.SelectedRows[0].DataBoundItem;

            using (var editForm = new DepartmentEditForm(selectedDept))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        departmentService.UpdateDepartment(editForm.CurrentDepartment);
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
            if (dgvDepartments.SelectedRows.Count == 0) return;

            var selectedDept = (Department)dgvDepartments.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show($"Вы уверены, что хотите удалить подразделение '{selectedDept.DepartmentName}'?\n\nВнимание: это может привести к ошибкам, если к отделу привязаны участки.",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    departmentService.RemoveDepartment(selectedDept.DepartmentId);
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось удалить подразделение. Возможно, оно используется в других таблицах.\n\nОшибка: {ex.Message}",
                        "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string helpText =
                "Справка по разделу 'Подразделения':\n\n" +
                "1. Управление списком:\n" +
                "- Здесь ведется учет основных отделов предприятия (Бухгалтерия, Тех. отдел и т.д.).\n\n" +
                "2. Создание подразделения:\n" +
                "- Нажмите 'Создать подразделение'.\n" +
                "- Укажите полное название и ФИО руководителя.\n\n" +
                "3. Редактирование:\n" +
                "- Выберите запись в таблице и нажмите 'Редактировать'.\n" +
                "- Изменения названия подразделения мгновенно отразятся в справочнике участков.\n\n" +
                "4. Ограничения удаления:\n" +
                "- База данных ACE OLEDB не позволит удалить подразделение, если за ним закреплен хотя бы один участок в системе.";

            MessageBox.Show(helpText, "Руководство пользователя", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}