using SchedulerDBManager.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchedulerDBManager.Presentation.Helpers
{
    public static class UIHelper
    {

        public static void ApplySecurity(User user, Button btnAdd, Button btnEdit, Button btnDelete)
        {
            bool hasAccess = user.CanEditData;

            if (btnAdd != null) btnAdd.Enabled = hasAccess;
            if (btnEdit != null) btnEdit.Enabled = hasAccess;
            if (btnDelete != null) btnDelete.Enabled = hasAccess;
        }

        // Проверка текстового поля на пустоту
        public static bool ValidateRequired(Control control, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                MessageBox.Show($"Поле '{fieldName}' обязательно для заполнения.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }
            return true;
        }

        // Проверка, что в ComboBox выбрано значение (не пустое и не -1)
        public static bool ValidateSelection(ComboBox combo, string fieldName)
        {
            if (combo.SelectedValue == null || combo.SelectedIndex == -1)
            {
                MessageBox.Show($"Необходимо выбрать значение в поле '{fieldName}'.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                combo.Focus();
                return false;
            }
            return true;
        }

        // Проверка числового диапазона (например, для NumericUpDown)
        public static bool ValidateRange(NumericUpDown control, string fieldName, decimal min, decimal max)
        {
            if (control.Value < min || control.Value > max)
            {
                MessageBox.Show($"Значение в поле '{fieldName}' должно быть в диапазоне от {min} до {max}.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }
            return true;
        }

        // Выполнение действий с автоматическим перехватом ошибок (избавляет от try-catch в кнопках)
        public static void SafeExecute(Action action, string errorTitle = "Ошибка")
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Стандартный диалог подтверждения удаления
        public static bool ConfirmDelete(string itemName)
        {
            var result = MessageBox.Show(
                $"Вы уверены, что хотите безвозвратно удалить:\n{itemName}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return result == DialogResult.Yes;
        }

        // Универсальная настройка внешнего вида любой таблицы (DataGridView)
        public static void ConfigureGrid(DataGridView grid, string[] hideColumns, Dictionary<string, string> renameColumns, string[] fillColumn = null)
        {
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.BackgroundColor = SystemColors.Control;

            if (grid.Columns.Count == 0) return;

            // Скрываем технические колонки
            foreach (var col in hideColumns)
                if (grid.Columns.Contains(col)) grid.Columns[col].Visible = false;

            // Переименовываем заголовки
            foreach (var col in renameColumns)
                if (grid.Columns.Contains(col.Key)) grid.Columns[col.Key].HeaderText = col.Value;

            // Растягиваем главную колонку

            if(fillColumn != null)
            {
                foreach (var col in fillColumn)
                    if (grid.Columns.Contains(col)) grid.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}