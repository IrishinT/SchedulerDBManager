using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.Presentation.Helpers;
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
            if (!UIHelper.ValidateRequired(nameField, "Название подразделения")) { this.DialogResult = DialogResult.None; return; }
            if (!UIHelper.ValidateRequired(headField, "ФИО руководителя")) { this.DialogResult = DialogResult.None; return; }

            CurrentDepartment.DepartmentName = nameField.Text.Trim();
            CurrentDepartment.HeadFullName = headField.Text.Trim();
        }
    }
}