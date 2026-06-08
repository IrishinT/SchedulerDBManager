using SchedulerDBManager.DataAccess.Models;
using System;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class DepartmentEditForm : Form
    {
        public Department CurrentDepartment { get; private set; }

        public DepartmentEditForm(Department department = null)
        {
            InitializeComponent();
            SetupFormBehavior();
            InitializeFormData(department);
        }

        private void SetupFormBehavior()
        {
            btnSave.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            btnSave.Click += BtnSave_Click;
        }

        private void InitializeFormData(Department department)
        {
            if (department == null)
            {
                this.Text = "Новое подразделение";
                CurrentDepartment = new Department();
            }
            else
            {
                this.Text = "Свойства подразделения";
                CurrentDepartment = new Department 
                { 
                    DepartmentId = department.DepartmentId 
                };

                nameField.Text = department.DepartmentName;
                headField.Text = department.HeadFullName;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Валидация перед сохранением
            if (string.IsNullOrWhiteSpace(nameField.Text))
            {
                MessageBox.Show("Название подразделения обязательно для заполнения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            CurrentDepartment.DepartmentName = nameField.Text.Trim();
            CurrentDepartment.HeadFullName = headField.Text.Trim();
        }
    }
}