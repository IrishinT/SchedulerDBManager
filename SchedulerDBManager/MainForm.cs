using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Database;
using SchedulerDBManager.DataAccess.Database.Access;
using SchedulerDBManager.DataAccess.Repositories;
using SchedulerDBManager.DataAccess.Repositories.Access;
using SchedulerDBManager.Presentation;


namespace SchedulerDBManager.Presentaton
{
    public partial class MainForm : Form
    {

        public MainForm()
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
                // Создаем подключение к базе данных
                IDatabase database = new AccessDatabase(filePathField.Text);
                
                // Проверяем подключение
                if (database is AccessDatabase accessDb)
                {
                    try { 
                        accessDb.CheckConnection();
                    } catch (Exception ex) {
                        MessageBox.Show("Не удалось подключиться к базе данных",
                        $"Ошибка {ex.Message}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Создаем репозиторий и сервис
                IScheduleRepository repository = new AccessScheduleRepository(database);
                ScheduleService scheduleService = new ScheduleService(repository);

                // Открываем форму Schedule и передаем сервис
                ScheduleForm scheduleForm = new ScheduleForm(scheduleService);
                scheduleForm.Show();
                
                // Скрываем главную форму или оставляем открытой
                this.Hide(); // или this.Show() если нужно показать обе
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
