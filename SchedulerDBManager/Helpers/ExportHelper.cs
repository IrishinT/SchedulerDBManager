using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
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
                        // new UTF8Encoding(true) добавляет BOM-метку, благодаря ей Excel сразу правильно отобразит русские символы.
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, new UTF8Encoding(true)))
                        {
                            // 1. Записываем заголовки столбцов
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                if (dgv.Columns[i].Visible) // Берем только видимые колонки
                                {
                                    sw.Write(EscapeCsvValue(dgv.Columns[i].HeaderText));
                                    if (i < dgv.Columns.Count - 1)
                                        sw.Write(";"); // Точка с запятой стандартный разделитель для Excel
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

        /// <summary>
        /// Экспорт данных из DataGridView в PDF файл.
        /// </summary>
        public static void ExportToPDF(DataGridView dgv, string title = "Отчет", string defaultFileName = "Отчет")
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF Файл (*.pdf)|*.pdf|Все файлы (*.*)|*.*";
                sfd.FileName = $"{defaultFileName}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.pdf";
                sfd.Title = "Сохранить таблицу как PDF...";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        GeneratePDF(dgv, sfd.FileName, title);
                        MessageBox.Show("Данные успешно экспортированы в PDF!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении PDF файла:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Генерация PDF документа.
        /// </summary>
        private static void GeneratePDF(DataGridView dgv, string filePath, string title)
        {
            using (PdfWriter writer = new PdfWriter(filePath))
            using (PdfDocument pdf = new PdfDocument(writer))
            // Использование using для Document гарантирует вызов Dispose/Close и корректную запись файла
            using (Document document = new Document(pdf))
            {
                // 1. Регистрируем шрифт с поддержкой кириллицы (например, Arial)
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "..", "Fonts", "arial.ttf");

                // Если файла arial.ttf вдруг нет, можно использовать другой системный .ttf шрифт
                PdfFont font = PdfFontFactory.CreateFont(fontPath, iText.IO.Font.PdfEncodings.IDENTITY_H);

                // Устанавливаем этот шрифт как шрифт по умолчанию для всего документа
                document.SetFont(font);

                // Заголовок
                document.Add(new Paragraph(title)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(18)
                    .SetMarginBottom(20));

                // Дата создания
                document.Add(new Paragraph($"Дата создания: {DateTime.Now:dd.MM.yyyy HH:mm}")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(10)
                    .SetMarginBottom(15));

                // Создаем таблицу
                int visibleColumnsCount = 0;
                foreach (DataGridViewColumn col in dgv.Columns)
                    if (col.Visible) visibleColumnsCount++;

                Table table = new Table(visibleColumnsCount, false);

                // Заголовки столбцов
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    if (column.Visible)
                    {
                        table.AddCell(new Cell()
                            .Add(new Paragraph(column.HeaderText ?? ""))
                            .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetPadding(5));
                    }
                }

                // Данные
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    // Пропускаем новую пустую строку, предназначенную для ввода данных пользователем
                    if (row.IsNewRow) continue;

                    foreach (DataGridViewColumn column in dgv.Columns)
                    {
                        if (column.Visible)
                        {
                            string cellValue = row.Cells[column.Index].Value?.ToString() ?? "";
                            table.AddCell(new Cell()
                                .Add(new Paragraph(cellValue))
                                .SetPadding(5)
                                .SetFontSize(10));
                        }
                    }
                }

                document.Add(table);

                // Количество записей (исключая пустую строку)
                int rowCount = dgv.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);
                document.Add(new Paragraph($"Всего записей: {rowCount}")
                    .SetFontSize(10)
                    .SetMarginTop(15));
            } // Здесь автоматически закроются document, pdf и writer
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