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
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                dgvSections.DataSource = null;
                dgvSections.DataSource = sectionService.GetAllSections().ToList();

                // Скрываем технические ID
                if (dgvSections.Columns.Contains("SectionId")) dgvSections.Columns["SectionId"].Visible = false;
                if (dgvSections.Columns.Contains("DepartmentId")) dgvSections.Columns["DepartmentId"].Visible = false;

                // Переводим названия
                if (dgvSections.Columns.Contains("Address")) dgvSections.Columns["Address"].HeaderText = "Адрес";
                if (dgvSections.Columns.Contains("DepartmentName")) dgvSections.Columns["DepartmentName"].HeaderText = "Подразделение";
                if (dgvSections.Columns.Contains("Phone")) dgvSections.Columns["Phone"].HeaderText = "Телефон";

                // Растягиваем адрес
                if (dgvSections.Columns.Contains("Address")) dgvSections.Columns["Address"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
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