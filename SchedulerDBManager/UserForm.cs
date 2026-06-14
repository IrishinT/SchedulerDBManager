using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class UserForm : BaseTableForm
    {
        private readonly UserService userService;
        private List<User> allUsers = new List<User>();

        private ToolTip toolTip;
        private TextBox searchField;
        private ComboBox cmbSortBy;

        public UserForm(UserService userService)
        {
            InitializeComponent();
            this.userService = userService;

            SetupTableForm();
            SetupEventHandlers();
            InitializeToolTips();
        }

        private void SetupTableForm()
        {
            Text = "Пользователи";
            btnAdd.Text = "Создать пользователя";
            btnEdit.Text = "Редактировать пользователя";
            btnDelete.Text = "Удалить пользователя";

            searchField = new TextBox { PlaceholderText = "Введите логин..." };
            cmbSortBy = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cmbSortBy.Items.AddRange(["По логину", "По роли"]);

            SetupSearchPanel(
                ("Поиск:", searchField),
                ("Сортировка:", cmbSortBy)
            );
        }

        private void SetupEventHandlers()
        {
            this.Load += (s, e) => { cmbSortBy.SelectedIndex = 0; RefreshData(); };
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
            toolTip.SetToolTip(searchField, "Поиск пользователя по логину");
            toolTip.SetToolTip(btnAdd, "Создать новую учетную запись");
            toolTip.SetToolTip(btnEdit, "Изменить логин или роль пользователя");
            toolTip.SetToolTip(btnDelete, "Удалить пользователя из системы");
            toolTip.SetToolTip(btnExport, "Экспортировать текущую таблицу пользователей в CSV");
        }

        private void RefreshData()
        {
            UIHelper.SafeExecute(() =>
            {
                allUsers = userService.GetAllUsers().ToList();
                ApplyFilters();

                // Настройка таблицы (скрываем пароль и ID)
                UIHelper.ConfigureGrid(
                    dgvTable, // Имя из вашего дизайнера
                    hideColumns: ["UserId", "Password", "Role", "IsAdmin", "CanEditData"],
                    renameColumns: new Dictionary<string, string> {
                        { "Login", "Логин" },
                        { "RoleName", "Уровень доступа" }
                    },
                    fillColumn: ["Login", "RoleName"]
                );
            }, "Ошибка загрузки пользователей");
        }

        private void ApplyFilters()
        {
            if (allUsers == null) return;

            var filtered = allUsers.AsEnumerable();

            // Поиск по логину
            string search = searchField.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(search))
                filtered = filtered.Where(u => u.Login.ToLower().Contains(search));

            // Сортировка
            filtered = cmbSortBy.SelectedItem?.ToString() switch
            {
                "По роли" => filtered.OrderBy(u => u.Role),
                _ => filtered.OrderBy(u => u.Login) // По умолчанию "По логину"
            };

            dgvTable.DataSource = filtered.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var form = new UserEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                UIHelper.SafeExecute(() =>
                {
                    userService.CreateUser(form.CurrentUser);
                    RefreshData();
                }, "Ошибка создания пользователя");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0) return;
            var selected = (User)dgvTable.SelectedRows[0].DataBoundItem;

            using var form = new UserEditForm(selected);
            if (form.ShowDialog() == DialogResult.OK)
            {
                UIHelper.SafeExecute(() =>
                {
                    // Если пароль в форме не заполнили, оставляем старый
                    if (string.IsNullOrWhiteSpace(form.CurrentUser.Password))
                    {
                        form.CurrentUser.Password = selected.Password;
                    }

                    userService.UpdateUser(form.CurrentUser);
                    RefreshData();
                }, "Ошибка обновления");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0) return;
            var selected = (User)dgvTable.SelectedRows[0].DataBoundItem;

            if (UIHelper.ConfirmDelete($"Пользователь: {selected.Login}"))
            {
                UIHelper.SafeExecute(() =>
                {
                    userService.RemoveUser(selected.UserId);
                    RefreshData();
                }, "Ошибка удаления");
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string helpText =
                "Руководство по работе с системой управления пользователями:\n\n" +
                "1. Просмотр данных:\n" +
                "- В таблице представлен список всех сотрудников, имеющих доступ к программе.\n" +
                "- Для каждого аккаунта указан логин и текущий уровень доступа (роль).\n" +
                "- В целях безопасности пароли пользователей в таблице не отображаются.\n\n" +
                "2. Создание пользователя:\n" +
                "- Нажмите кнопку 'Создать пользователя'.\n" +
                "- Укажите уникальный логин и надежный пароль.\n" +
                "- Выберите роль, которая определит права сотрудника в системе.\n\n" +
                "3. Редактирование:\n" +
                "- Выберите запись в списке и нажмите 'Редактировать'.\n" +
                "- Вы можете изменить логин или сменить уровень доступа.\n" +
                "- Если поле пароля оставить пустым, текущий пароль пользователя не изменится.\n\n" +
                "4. Удаление:\n" +
                "- Вы можете удалить учетную запись. Это действие нельзя отменить.\n" +
                "- Будьте осторожны при удалении администраторов, чтобы не потерять доступ к управлению.\n\n" +
                "Уровни доступа (роли):\n" +
                "- Читатель: только просмотр расписания и справочников.\n" +
                "- Редактор: создание и правка смен, участков и подразделений.\n" +
                "- Администратор: полный доступ ко всем функциям, включая управление персоналом.";

            MessageBox.Show(helpText, "Справка: Управление пользователями", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}