using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Database;
using SchedulerDBManager.DataAccess.Database.Access;
using SchedulerDBManager.DataAccess.Repositories;
using SchedulerDBManager.DataAccess.Repositories.Access;
using SchedulerDBManager.Presentation;


namespace SchedulerDBManager.Presentaton
{
    public partial class LoadForm : Form
    {

        public LoadForm()
        {
            InitializeComponent();

            explorerOpenBtn.Click += ExplorerOpenBtn_Click;
            connectBtn.Click += ConnectBtn_Click;
        }

        private void ExplorerOpenBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Access Database (*.accdb)|*.accdb|All files (*.*)|*.*";
                openFileDialog.Title = "Выберите файл базы данных";
                
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePathField.Text = openFileDialog.FileName;
                }
            }
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filePathField.Text))
            {
                MessageBox.Show("Выберите файл базы данных", 
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(filePathField.Text))
            {
                MessageBox.Show("Файл базы данных не найден", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1. Создаем подключение
                IDatabase database = new AccessDatabase(filePathField.Text);

                // 2. Проверяем связь
                if (database is AccessDatabase accessDb)
                {
                    accessDb.CheckConnection();
                }

                // 3. Создаем все репозитории
                IScheduleRepository scheduleRepo = new AccessScheduleRepository(database);
                ISectionRepository sectionRepo = new AccessSectionRepository(database);
                IDepartmentRepository departmentRepo = new AccessDepartmentRepository(database);
                IUserRepository userRepository = new AccessUserRepository(database);

                // 4. Создаем все сервисы
                ScheduleService scheduleService = new ScheduleService(scheduleRepo);
                SectionService sectionService = new SectionService(sectionRepo, scheduleRepo);
                DepartmentService departmentService = new DepartmentService(departmentRepo, sectionRepo, scheduleRepo);
                UserService userService = new UserService(userRepository);

                // 5. Открываем ГЛАВНОЕ МЕНЮ и передаем туда сервисы
                MainForm mainForm = new MainForm(scheduleService, sectionService, departmentService, userService);
                mainForm.Show();

                // Скрываем форму загрузки
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
