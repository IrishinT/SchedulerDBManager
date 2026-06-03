using SchedulerDBManager.BusinessLogic.Services;
using System;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class MainForm : Form
    {
        private readonly ScheduleService _scheduleService;
        private readonly SectionService _sectionService;

        // Принимаем сервисы из LoadForm
        public MainForm(ScheduleService scheduleService, SectionService sectionService)
        {
            InitializeComponent();
            _scheduleService = scheduleService;
            _sectionService = sectionService;

            // Привязываем события кнопкам
            btnSchedule.Click += BtnSchedule_Click;
            button1.Click += BtnSections_Click;

            // Чтобы при закрытии MainForm закрывалось все приложение
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void BtnSchedule_Click(object sender, EventArgs e)
        {
            // Открываем форму смен и передаем ей нужные сервисы
            ScheduleForm scheduleForm = new ScheduleForm(_scheduleService, _sectionService);
            scheduleForm.ShowDialog();
        }

        private void BtnSections_Click(object sender, EventArgs e)
        {
            // TODO: реализовать форму для управления участками
            MessageBox.Show("Функционал управления участками в разработке.");
        }
    }
}