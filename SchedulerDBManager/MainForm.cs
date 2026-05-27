using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Database;
using SchedulerDBManager.DataAccess.Database.Access;
using SchedulerDBManager.DataAccess.Repositories;
using SchedulerDBManager.DataAccess.Repositories.Access;


namespace SchedulerDBManager.Presentaton
{
    public partial class MainForm : Form
    {

        private readonly IDatabase database;
        private readonly ScheduleService scheduleService;

        public MainForm()
        {
            InitializeComponent();
            database = new AccessDatabase();
            IScheduleRepository serviceRepo = new AccessScheduleRepository(database);
            scheduleService = new ScheduleService(serviceRepo);

            this.Load += new EventHandler(MainForm_Load);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            // Биндим список типизированных объектов к DataGridView
            dgvSchedule.DataSource = scheduleService.GetAllSchedules();
        }

    }
}
