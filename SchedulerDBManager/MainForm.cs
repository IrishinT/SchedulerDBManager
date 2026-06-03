using SchedulerDBManager.BusinessLogic.Services;
using System;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class MainForm : Form
    {
        private readonly ScheduleService _scheduleService;
        private readonly SectionService _sectionService;
        private readonly DepartmentService _departmentService;

        // Принимаем сервисы из LoadForm
        public MainForm(ScheduleService scheduleService, SectionService sectionService, DepartmentService departmentService)
        {
            InitializeComponent();
            _scheduleService = scheduleService;
            _sectionService = sectionService;
            _departmentService = departmentService;

            // Привязываем события кнопкам
            btnSchedule.Click += btnSchedule_Click;
            btnSections.Click += btnSections_Click;
            btnDepartments.Click += btnDepartments_Click;

            // Чтобы при закрытии MainForm закрывалось все приложение
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            // Открываем форму смен и передаем ей нужные сервисы
            ScheduleForm scheduleForm = new ScheduleForm(_scheduleService, _sectionService);
            scheduleForm.ShowDialog();
        }

        private void btnSections_Click(object sender, EventArgs e)
        {
            SectionForm sectionForm = new SectionForm(_sectionService, _departmentService);
            sectionForm.ShowDialog();
        }

        private void btnDepartments_Click(object sender, EventArgs e)
        {
            DepartmentForm deptForm = new DepartmentForm(_departmentService);
            deptForm.ShowDialog();
        }
    }
}