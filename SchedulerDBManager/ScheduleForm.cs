using SchedulerDBManager.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation
{
    public partial class ScheduleForm : Form
    {
        private readonly ScheduleService scheduleService;

        public ScheduleForm(ScheduleService service)
        {
            InitializeComponent();
            this.scheduleService = service;
            this.Load += Schedule_Load;
        }

        private void Schedule_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                var schedules = scheduleService.GetAllSchedules().ToList();
                dvgSchedules.DataSource = null;
                dvgSchedules.DataSource = schedules;

                // Скрываем колонку Id если нужно
                if (dvgSchedules.Columns.Contains("Id"))
                    dvgSchedules.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
