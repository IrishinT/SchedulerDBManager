using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.Presentation.Helpers;
using System;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class AuthForm : Form
    {
        private readonly UserService userService;

        // Свойство для получения авторизованного пользователя извне
        public User AuthenticatedUser { get; private set; }

        public AuthForm(UserService userService)
        {
            InitializeComponent();
            this.userService = userService;

            // Настройки формы
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btnAuth; // Вход по нажатию Enter

            btnAuth.Click += BtnAuth_Click;
        }

        private void BtnAuth_Click(object sender, EventArgs e)
        {
            // Используем Helper для проверки полей
            if (!UIHelper.ValidateRequired(loginField, "Логин")) return;
            if (!UIHelper.ValidateRequired(passField, "Пароль")) return;

            UIHelper.SafeExecute(() =>
            {
                string login = loginField.Text.Trim();
                string password = passField.Text;

                var user = userService.Authenticate(login, password);

                if (user != null)
                {
                    AuthenticatedUser = user;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }, "Ошибка авторизации");
        }
    }
}