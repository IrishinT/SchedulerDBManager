using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class SectionsForm : Form
    {
        private readonly SectionService sectionService;
        private readonly DepartmentService departmentService;

        public SectionsForm(SectionService sectionService, DepartmentService departmentService)
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSections.SelectedRows.Count == 0) return;

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSections.SelectedRows.Count == 0) return;

            var section = (Section)dgvSections.SelectedRows[0].DataBoundItem;
            if (MessageBox.Show($"Удалить участок '{section.Address}'?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sectionService.RemoveSection(section.SectionId);
                RefreshGrid();
            }
        }
    }
}