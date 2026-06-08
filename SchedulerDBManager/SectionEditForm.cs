using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class SectionEditForm : Form
    {
        public Section CurrentSection { get; private set; }

        public SectionEditForm(IEnumerable<Department> departments, Section section = null)
        {
            InitializeComponent();
            SetupFormBehavior();
            BindDepartmentsCombo(departments);
            InitializeFormData(section);
        }

        private void SetupFormBehavior()
        {
            btnSave.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            btnSave.Click += BtnSave_Click;
        }

        private void BindDepartmentsCombo(IEnumerable<Department> departments)
        {
            // Используем анонимный тип для чистоты отображения, как в ScheduleEditForm
            var displayDepts = departments
                .Select(d => new { Id = d.DepartmentId, Name = d.DepartmentName.Trim() })
                .ToList();

            sectionAddress.DataSource = displayDepts;
            sectionAddress.DisplayMember = "Name";
            sectionAddress.ValueMember = "Id";
        }

        private void InitializeFormData(Section section)
        {
            if (section == null)
            {
                this.Text = "Добавление участка";
                CurrentSection = new Section();
            }
            else
            {
                this.Text = "Редактирование участка";
                // Создаем копию объекта (клонирование по ID)
                CurrentSection = new Section
                {
                    SectionId = section.SectionId,
                    DepartmentId = section.DepartmentId
                };

                addressField.Text = section.Address;
                phoneField.Text = section.Phone;
                sectionAddress.SelectedValue = section.DepartmentId;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!UIHelper.ValidateRequired(addressField, "Адрес участка")) { this.DialogResult = DialogResult.None; return; }
            if (!UIHelper.ValidateSelection(sectionAddress, "Подразделение")) { this.DialogResult = DialogResult.None; return; }

            // Маппинг данных из интерфейса в модель
            CurrentSection.Address = addressField.Text.Trim();
            CurrentSection.Phone = phoneField.Text.Trim();

            if (sectionAddress.SelectedValue != null)
                CurrentSection.DepartmentId = (int)sectionAddress.SelectedValue;
        }
    }
}