namespace SchedulerDBManager.DataAccess.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Role { get; set; } // 1 - Читатель, 2 - Редактор, 3 - Администратор

        public bool CanEditData => Role >= 2;

        public bool IsAdmin => Role == 3;

        // Вспомогательное свойство для отображения названия роли в интерфейсе
        public string RoleName => Role switch
        {
            1 => "Читатель",
            2 => "Редактор",
            3 => "Администратор",
            _ => "Неизвестно"
        };
    }
}