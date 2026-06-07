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

            // Настройка кнопок диалога
            btnSave.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;

            btnSave.Click += btnSave_Click;

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
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName,
                    HeadFullName = department.HeadFullName
                };

                // Заполнение полей (используем твои имена из дизайнера)
                nameField.Text = CurrentDepartment.DepartmentName;
                headField.Text = CurrentDepartment.HeadFullName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Считываем данные из текстовых полей обратно в объект
            CurrentDepartment.DepartmentName = nameField.Text.Trim();
            CurrentDepartment.HeadFullName = headField.Text.Trim();

            // Базовая валидация перед закрытием
            if (string.IsNullOrWhiteSpace(CurrentDepartment.DepartmentName))
            {
                MessageBox.Show("Название подразделения обязательно для заполнения.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None; // Не закрываем форму
            }
        }
    }
}