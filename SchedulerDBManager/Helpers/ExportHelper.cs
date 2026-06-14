using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation.Helpers
{
    public static class ExportHelper
    {
        public static void ExportToCSV(DataGridView dgv, string defaultFileName = "Отчет.csv")
        {
            // Проверка, есть ли данные для экспорта
            if (dgv.Rows.Count == 0 || (dgv.Rows.Count == 1 && dgv.Rows[0].IsNewRow))
            {
                MessageBox.Show("Нет данных для экспорта.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Файл (*.csv)|*.csv|Все файлы (*.*)|*.*";
                sfd.FileName = defaultFileName;
                sfd.Title = "Сохранить таблицу как...";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // new UTF8Encoding(true) добавляет BOM-метку. 
                        // Благодаря ей русский MS Excel сразу правильно поймет кириллицу (не будет "кракозябр").
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, new UTF8Encoding(true)))
                        {
                            // 1. Записываем заголовки столбцов
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                if (dgv.Columns[i].Visible) // Берем только видимые колонки
                                {
                                    sw.Write(EscapeCsvValue(dgv.Columns[i].HeaderText));
                                    if (i < dgv.Columns.Count - 1)
                                        sw.Write(";"); // Точка с запятой - стандартный разделитель для русского Excel
                                }
                            }
                            sw.WriteLine();

                            // 2. Записываем данные из строк
                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                if (row.IsNewRow) continue; // Пропускаем пустую строку для ввода (если она есть)

                                for (int i = 0; i < dgv.Columns.Count; i++)
                                {
                                    if (dgv.Columns[i].Visible)
                                    {
                                        string cellValue = row.Cells[i].Value?.ToString() ?? "";
                                        sw.Write(EscapeCsvValue(cellValue));

                                        if (i < dgv.Columns.Count - 1)
                                            sw.Write(";");
                                    }
                                }
                                sw.WriteLine();
                            }
                        }

                        MessageBox.Show("Данные успешно экспортированы!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении файла:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Метод для корректной обработки текста (если внутри ячейки случайно окажется точка с запятой, кавычка или перенос строки)
        private static string EscapeCsvValue(string value)
        {
            if (value.Contains(";") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
            {
                value = value.Replace("\"", "\"\""); // Экранируем кавычки
                return $"\"{value}\""; // Оборачиваем всё значение в кавычки
            }
            return value;
        }
    }
}