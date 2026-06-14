using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.Presentation.Helpers;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class DepartmentForm : BaseTableForm
    {
        private readonly DepartmentService departmentService;
        private List<Department> allDepartments = new List<Department>();

        private ToolTip toolTip;
        private TextBox searchField;
        private ComboBox cmbSortBy;

        public DepartmentForm(DepartmentService departmentService, User user)
        {
            InitializeComponent();
            this.departmentService = departmentService;

            SetupTableForm();
            SetupEventHandlers();
            InitializeToolTips();

            UIHelper.ApplySecurity(user, btnAdd, btnEdit, btnDelete);
        }

        private void SetupTableForm()
        {
            Text = "Подразделения";
            btnAdd.Text = "Создать подразделение";
            btnEdit.Text = "Редактировать подразделение";
            btnDelete.Text = "Удалить подразделение";

            searchField = new TextBox { PlaceholderText = "Введите название..." };
            cmbSortBy = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cmbSortBy.Items.AddRange(new object[] { "Без сортировки", "По названию", "По руководителю" });

            SetupSearchPanel(
                ("Поиск:", searchField),
                ("Сортировка:", cmbSortBy)
            );
        }

        private void SetupEventHandlers()
        {
            Load += (s, e) => { if (cmbSortBy.Items.Count > 0) cmbSortBy.SelectedIndex = 0; RefreshData(); };
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnHelp.Click += btnHelp_Click;

            searchField.TextChanged += (s, e) => ApplyFilters();
            cmbSortBy.SelectedIndexChanged += (s, e) => ApplyFilters();
        }

        private void InitializeToolTips()
        {
            toolTip = new ToolTip { ShowAlways = true };

            toolTip.SetToolTip(searchField, "Введите часть названия подразделения для мгновенного поиска");
            toolTip.SetToolTip(cmbSortBy, "Выберите критерий для сортировки записей");
            toolTip.SetToolTip(btnAdd, "Создать и добавить новое подразделение");
            toolTip.SetToolTip(btnEdit, "Изменить данные выбранного подразделения");
            toolTip.SetToolTip(btnDelete, "Безвозвратно удалить выбранное подразделение");
            toolTip.SetToolTip(dgvTable, "Выберите строку для редактирования или удаления");
        }

        private void RefreshData()
        {
            UIHelper.SafeExecute(() =>
            {
                allDepartments = departmentService.GetAllDepartments().ToList();
                ApplyFilters();

                UIHelper.ConfigureGrid(
                    dgvTable,
                    hideColumns: ["DepartmentId"],
                    renameColumns: new Dictionary<string, string> {
                        { "DepartmentName", "Название подразделения" },
                        { "HeadFullName", "ФИО Руководителя" }
                    },
                    fillColumn: ["DepartmentName", "HeadFullName"]
                );
            }, "Ошибка загрузки данных");
        }

        private void ApplyFilters()
        {
            if (allDepartments == null) return;

            var filtered = allDepartments.AsEnumerable();

            // Поиск
            string searchText = searchField.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(d => d.DepartmentName?.ToLower().Contains(searchText) == true);
            }

            // Сортировка
            filtered = cmbSortBy.SelectedItem?.ToString() switch
            {
                "По руководителю" => filtered.OrderBy(d => d.HeadFullName),
                _ => filtered.OrderBy(d => d.DepartmentName) // "По названию"
            };

            dgvTable.DataSource = filtered.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var editForm = new DepartmentEditForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                UIHelper.SafeExecute(() =>
                {
                    departmentService.CreateDepartment(editForm.CurrentDepartment);
                    RefreshData();
                }, "Ошибка создания");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0) return;

            var selectedDept = (Department)dgvTable.SelectedRows[0].DataBoundItem;
            using var editForm = new DepartmentEditForm(selectedDept);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                UIHelper.SafeExecute(() =>
                {
                    departmentService.UpdateDepartment(editForm.CurrentDepartment);
                    RefreshData();
                }, "Ошибка обновления");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0) return;
            var selectedDept = (Department)dgvTable.SelectedRows[0].DataBoundItem;

            if (UIHelper.ConfirmDelete($"Подразделение: {selectedDept.DepartmentName}"))
            {
                UIHelper.SafeExecute(() =>
                {
                    departmentService.RemoveDepartment(selectedDept.DepartmentId);
                    RefreshData();
                }, "Ошибка удаления");
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