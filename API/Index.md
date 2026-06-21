# Техническая спецификация проекта ScheduleDBManager (API)

## Введение
API `SchedulerDBManager` представляет собой основу для управления сменами на участках. Приложение построено на базе трех изолированных слоев:

1. **Database** — соединение с СУБД MS Access (`.accdb`) через OLE DB.
2. **Repositories** — выполнение SQL-запросов и маппинг данных в объекты.
3. **Services** — валидация данных, авторасчеты и каскадное удаление.

## Структура документации
Документация разделена на логические пакеты:
* [Модели данных (Models)](Models.md)
* [База данных (Database)](Database.md)
* [Слой доступа к данным (Repositories)](Repositories.md)
* [Слой бизнес-логики (Services)](Services.md)

---

## Примеры использования API

Ниже приведены типовые сценарии интеграции API в клиентское приложение.

### Пример 1. Инициализация всех слоев API

Сборка всех зависимостей перед использованием:

```csharp
// 1. Подключаем базу данных
IDatabase db = new AccessDatabase("ShiftSchedule.accdb");

// 2. Инициализируем репозитории
IUserRepository userRepo = new AccessUserRepository(db);
IDepartmentRepository deptRepo = new AccessDepartmentRepository(db);
ISectionRepository sectionRepo = new AccessSectionRepository(db);
IScheduleRepository scheduleRepo = new AccessScheduleRepository(db);

// 3. Создаем сервисы для работы (передаем им репозитории)
UserService userService = new UserService(userRepo);
DepartmentService deptService = new DepartmentService(deptRepo, sectionRepo, scheduleRepo);
SectionService sectionService = new SectionService(sectionRepo, scheduleRepo);
ScheduleService scheduleService = new ScheduleService(scheduleRepo);
```

### Пример 2. Авторизация пользователя и проверка его прав
Сценарий проверки логина/пароля:
```csharp
// Выполняем аутентификацию
User user = userService.Authenticate("petrov_ivan", "securePassword123");

if (user != null)
{
    Console.WriteLine($"Добро пожаловать, {user.Login}!");
    Console.WriteLine($"Роль в системе: {user.RoleName}");
}
else
{
    Console.WriteLine("Ошибка: Неверный логин или пароль.");
}
```

### Пример 3. Создание новой смены
```csharp
try
{
    Schedule newShift = new Schedule
    {
        StartTime = DateTime.Parse("2026-06-21 8:00:00"),
        EndTime = DateTime.Parse("2026-06-21 20:00:00"),
        WorkerCount = 10,
        SupervisorFullname = " Сидоров С.С. ",
        SectionId = 2
    };

    // Вызываем метод создания смены
    scheduleService.CreateSchedule(newShift);

    // После сохранения объект newShift автоматически обновится в памяти:
    Console.WriteLine($"Смена успешно создана!");
    Console.WriteLine($"Длительность: {newShift.Duration} ч."); // Выведет: 12
    Console.WriteLine($"Дата смены: {newShift.ShiftDate.ToShortDateString()}"); // Выведет: 21.06.2026
}
catch (ArgumentException ex)
{
    // Сработает, если нарушены лимиты (например, более 100 рабочих или StartTime >= EndTime)
    Console.WriteLine($"Ошибка валидации: {ex.Message}");
}
```
