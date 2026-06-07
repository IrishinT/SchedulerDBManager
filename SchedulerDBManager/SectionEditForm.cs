using SchedulerDBManager.DataAccess.Models;
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

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            btnSave.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;

            btnSave.Click += BtnSave_Click;

            // Настраиваем ComboBox
            sectionAddress.DataSource = departments.ToList();
            sectionAddress.DisplayMember = "DepartmentName"; // То, что видит пользователь
            sectionAddress.ValueMember = "DepartmentId";     // ID, который пойдет в базу

            if (section == null)
            {
                this.Text = "Добавление участка";
                CurrentSection = new Section();
            }
            else
            {
                this.Text = "Редактирование участка";
                CurrentSection = new Section
                {
                    SectionId = section.SectionId,
                    Address = section.Address,
                    DepartmentId = section.DepartmentId,
                    Phone = section.Phone
                };

                addressField.Text = CurrentSection.Address;
                phoneField.Text = CurrentSection.Phone;
                sectionAddress.SelectedValue = CurrentSection.DepartmentId; // Устанавливаем выбранное подразделение
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            CurrentSection.Address = addressField.Text.Trim();
            CurrentSection.Phone = phoneField.Text.Trim();

            if (sectionAddress.SelectedValue != null)
            {
                CurrentSection.DepartmentId = (int)sectionAddress.SelectedValue;
            }
        }

        private void cmbSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}