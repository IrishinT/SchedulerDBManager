using SchedulerDBManager.Presentation.Helpers;
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
    /// <summary>
    /// Базовая форма-шаблон для вывода табличных данных.
    /// Предоставляет структуру для форм таблиц: левое меню управления, 
    /// верхнюю панель фильтрации pnlSearch и таблицу dgvTable.
    /// </summary>
    public partial class BaseTableForm : Form
    {
        public BaseTableForm()
        {
            InitializeComponent();

            registerHandlers();
        }

        private void registerHandlers()
        {
            btnExport.Click += BtnExport_Click;
        }

        /// <summary>
        /// Обработчик клика по кнопке экспорта.
        /// Автоматически выгружает текущие видимые данные из dgvTable.
        /// </summary>
        private void BtnExport_Click(object? sender, EventArgs e)
        {
            // Очищаем заголовок формы от недопустимых для имени файла символов
            string safeTitle = string.Concat(Text.Split(Path.GetInvalidFileNameChars()));

            // Форматируем текущую дату и время
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            // Объединяем название и временную метку
            string defaultFileName = $"{safeTitle}_{timestamp}.csv";

            ExportHelper.ExportToCSV(dgvTable, defaultFileName);
        }

        /// <summary>
        /// Метод динамического построения панели поиска и фильтрации.
        /// Избавляет от необходимости вручную настраивать TableLayoutPanel в дизайнере каждого наследника.
        /// </summary>
        /// <param name="fields">Массив пар: (Текст надписи над фильтром, Сам элемент управления фильтром)</param>
        protected void SetupSearchPanel(params (string LabelText, Control InputControl)[] fields)
        {
            // Если фильтры не переданы, полностью скрываем верхнюю панель
            if (fields == null || fields.Length == 0)
            {
                pnlSearch.Visible = false;
                return;
            }

            // Задаем число колонок равным числу переданных полей
            tlpSearch.ColumnCount = fields.Length;

            // Вычисляем ширину каждой колонки в процентах
            float colPercent = 100f / fields.Length;

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];

                // Настраиваем пропорциональную ширину колонки
                tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colPercent));

                // Создаем надпись над полем ввода
                var lbl = new Label
                {
                    Text = field.LabelText,
                    Dock = DockStyle.Bottom,
                    AutoSize = true,
                    Margin = new Padding(0)
                };

                // Настраиваем переданный элемент управления (TextBox, ComboBox)
                var ctrl = field.InputControl;
                ctrl.Dock = DockStyle.Top;
                ctrl.Margin = new Padding(0, 0, 15, 0); // Задаем отступ справа, чтобы визуально отделить элементы друг от друга

                // Добавляем созданные элементы в сетку (строка 0 - надпись, строка 1 - элемент ввода)
                tlpSearch.Controls.Add(lbl, i, 0);
                tlpSearch.Controls.Add(ctrl, i, 1);
            }
        }

    }
}
