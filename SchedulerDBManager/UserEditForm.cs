using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class UserEditForm : Form
    {
        public User CurrentUser { get; private set; }

        public UserEditForm(User user = null)
        {
            InitializeComponent();
            SetupFormBehavior();
            BindRolesCombo();
            InitializeFormData(user);
        }

        private void SetupFormBehavior()
        {
            btnSave.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            btnSave.Click += BtnSave_Click;
        }

        private void BindRolesCombo()
        {
            var roles = new[]
            {
                new { Id = 1, Name = "Читатель" },
                new { Id = 2, Name = "Редактор" },
                new { Id = 3, Name = "Администратор" }
            };
            roleField.DataSource = roles.ToList();
            roleField.DisplayMember = "Name";
            roleField.ValueMember = "Id";
        }

        private void InitializeFormData(User user)
        {
            if (user == null)
            {
                this.Text = "Новый пользователь";
                CurrentUser = new User();
                roleField.SelectedValue = 1; // По умолчанию Читатель
            }
            else
            {
                this.Text = "Редактирование пользователя";
                CurrentUser = new User { UserId = user.UserId };

                loginField.Text = user.Login;
                roleField.SelectedValue = user.Role;

                // Пароль оставляем пустым (согласно требованию)
                passField.Text = string.Empty;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Валидация логина и роли через Helper
            if (!UIHelper.ValidateRequired(loginField, "Логин")) { this.DialogResult = DialogResult.None; return; }
            if (!UIHelper.ValidateRequired(passField, "Пароль")) { this.DialogResult = DialogResult.None; return; }
            if (!UIHelper.ValidateSelection(roleField, "Роль")) { this.DialogResult = DialogResult.None; return; }
            if (!UIHelper.ValidateMinLength(passField, "Пароль", 6)){ this.DialogResult = DialogResult.None; return; }

            // Если это новый пользователь, пароль обязателен
            if (CurrentUser.UserId == 0 && !UIHelper.ValidateRequired(passField, "Пароль"))
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            CurrentUser.Login = loginField.Text.Trim();
            CurrentUser.Role = (int)roleField.SelectedValue;
            CurrentUser.Password = passField.Text; // Может быть пустым при редактировании
        }
    }
}