using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Database.Access;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories.Access;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using Xunit;

// Отключаем параллельный запуск тестов для проекта, 
// чтобы интеграционные тесты не мешали друг другу при работе с одним общим файлом БД.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace SchedulerDBManager.Tests;

public class DatabaseIntegrationTests : IDisposable
{
    private readonly AccessDatabase _database;

    // Репозитории для проверок состояния БД
    private readonly AccessUserRepository _userRepository;
    private readonly AccessDepartmentRepository _departmentRepository;
    private readonly AccessSectionRepository _sectionRepository;
    private readonly AccessScheduleRepository _scheduleRepository;

    // Сервисы для выполнения бизнес-операций и каскадного удаления
    private readonly UserService _userService;
    private readonly DepartmentService _departmentService;
    private readonly SectionService _sectionService;
    private readonly ScheduleService _scheduleService;

    // Уникальные маркеры для тестовых данных
    private readonly string _testLogin = "int_test_user_temp";
    private readonly string _testDeptName = "int_test_dept_temp";
    private readonly string _testSectionAddress = "int_test_section_temp";
    private readonly string _testSupervisor = "int_test_supervisor_temp";

    public DatabaseIntegrationTests()
    {
        // Инициализируем подключение к файлу шаблона
        _database = new AccessDatabase("db_template.accdb");

        // Инициализируем репозитории
        _userRepository = new AccessUserRepository(_database);
        _departmentRepository = new AccessDepartmentRepository(_database);
        _sectionRepository = new AccessSectionRepository(_database);
        _scheduleRepository = new AccessScheduleRepository(_database);

        // Внедряем репозитории в сервисы
        _userService = new UserService(_userRepository);
        _departmentService = new DepartmentService(_departmentRepository, _sectionRepository, _scheduleRepository);
        _sectionService = new SectionService(_sectionRepository, _scheduleRepository);
        _scheduleService = new ScheduleService(_scheduleRepository);

        // Перед началом каждого теста принудительно очищаем базу данных
        CleanUpTestData();
    }

    #region AccessDatabase Tests

    [Fact]
    public void AccessDatabase_CheckConnection_SuccessfullyOpensAndClosesConnection()
    {
        // Act & Assert
        var exception = Record.Exception(() => _database.CheckConnection());
        Assert.Null(exception);
    }

    #endregion

    #region UserService Integration Tests

    [Fact]
    public void UserService_Create_Authenticate_And_Remove_CycleWorksCorrectly()
    {
        // Arrange
        var testUser = new User
        {
            Login = _testLogin,
            Password = "SecurePassword123",
            Role = 2 // Редактор
        };

        // Act (Создание через сервис)
        _userService.CreateUser(testUser);

        // Assert (Авторизация и проверка чтения)
        var authenticatedUser = _userService.Authenticate(_testLogin, "SecurePassword123");
        Assert.NotNull(authenticatedUser);
        Assert.Equal(_testLogin, authenticatedUser!.Login);
        Assert.True(authenticatedUser.UserId > 0);

        // Act (Удаление через сервис)
        _userService.RemoveUser(authenticatedUser.UserId);

        // Assert (Проверка стирания)
        var deletedUser = _userRepository.GetByLogin(_testLogin);
        Assert.Null(deletedUser);
    }

    #endregion

    #region DepartmentService Integration Tests

    [Fact]
    public void DepartmentService_Create_Update_And_Remove_CycleWorksCorrectly()
    {
        // Arrange
        var dept = new Department
        {
            DepartmentName = _testDeptName,
            HeadFullName = "Иванов Иван Иванович"
        };

        // Act (Создание)
        _departmentService.CreateDepartment(dept);

        // Assert (Чтение)
        var fetchedDept = _departmentRepository.SearchByName(_testDeptName).First();
        Assert.Equal(_testDeptName, fetchedDept.DepartmentName);
        Assert.Equal("Иванов Иван Иванович", fetchedDept.HeadFullName);

        // Act (Обновление руководителя)
        fetchedDept.HeadFullName = "Петров Петр Петрович";
        _departmentService.UpdateDepartment(fetchedDept);

        // Assert (Проверка обновления)
        var updatedDept = _departmentRepository.SearchByName(_testDeptName).First();
        Assert.Equal("Петров Петр Петрович", updatedDept.HeadFullName);

        // Act (Удаление через сервис)
        _departmentService.RemoveDepartment(fetchedDept.DepartmentId);

        // Assert (Проверка)
        Assert.Empty(_departmentRepository.SearchByName(_testDeptName));
    }

    #endregion

    #region SectionService & Cascade Deletion Integration Tests

    [Fact]
    public void SectionService_Create_And_Remove_CascadesToSchedules()
    {
        // Arrange - Создаем отдел
        var dept = new Department { DepartmentName = _testDeptName, HeadFullName = "Начальник ОТК" };
        _departmentService.CreateDepartment(dept);
        var createdDept = _departmentRepository.SearchByName(_testDeptName).First();

        // Создаем участок через SectionService
        var section = new Section
        {
            Address = _testSectionAddress,
            DepartmentId = createdDept.DepartmentId,
            Phone = "+79998887766"
        };
        _sectionService.CreateSection(section);
        var createdSection = _sectionRepository.SearchByAddress(_testSectionAddress).First();

        // Act (Удаление участка через сервис)
        _sectionService.RemoveSection(createdSection.SectionId);

        // Очищаем оставшийся тестовый отдел
        _departmentService.RemoveDepartment(createdDept.DepartmentId);

        // Assert (Проверка удаления)
        Assert.Empty(_sectionRepository.SearchByAddress(_testSectionAddress));
    }

    #endregion

    #region Full Cascade Deletion Integration Test

    [Fact]
    public void DepartmentService_RemoveDepartment_CascadesAllTheWay_To_SchedulesAndSections()
    {
        // 1. Arrange - Готовим связную структуру на реальном файле БД

        // Создаем отдел
        var dept = new Department { DepartmentName = _testDeptName, HeadFullName = "Зав. Производством" };
        _departmentService.CreateDepartment(dept);
        var createdDept = _departmentRepository.SearchByName(_testDeptName).First();

        // Создаем участок
        var section = new Section { Address = _testSectionAddress, DepartmentId = createdDept.DepartmentId, Phone = "123" };
        _sectionService.CreateSection(section);
        var createdSection = _sectionRepository.SearchByAddress(_testSectionAddress).First();

        // Создаем смену
        var startTime = DateTime.Now.Date.AddHours(8);
        var endTime = DateTime.Now.Date.AddHours(16);
        var schedule = new Schedule
        {
            StartTime = startTime,
            EndTime = endTime,
            WorkerCount = 10,
            SupervisorFullname = _testSupervisor,
            SectionId = createdSection.SectionId
        };
        _scheduleService.CreateSchedule(schedule);

        // Убеждаемся, что все объекты записались в БД перед удалением
        Assert.Single(_scheduleRepository.SearchBySupervisor(_testSupervisor));
        Assert.Single(_sectionRepository.SearchByAddress(_testSectionAddress));
        Assert.Single(_departmentRepository.SearchByName(_testDeptName));

        // 2. Act - Вызываем каскадное удаление ОДНОГО лишь отдела на уровне сервиса
        _departmentService.RemoveDepartment(createdDept.DepartmentId);

        // 3. Assert - Проверяем, что каскадный цикл в сервисе успешно очистил всю цепочку в СУБД
        Assert.Empty(_scheduleRepository.SearchBySupervisor(_testSupervisor)); // Смена удалена
        Assert.Empty(_sectionRepository.SearchByAddress(_testSectionAddress)); // Участок удален
        Assert.Empty(_departmentRepository.SearchByName(_testDeptName));       // Отдел удален
    }

    #endregion

    private void CleanUpTestData()
    {
            // Каскадно удаляем все тестовые отделы (это автоматически сотрет привязанные к ним тест-участки и тест-смены)
            var depts = _departmentRepository.SearchByName(_testDeptName).ToList();
            foreach (var d in depts)
            {
                _departmentService.RemoveDepartment(d.DepartmentId);
            }

            // На случай, если тест упал посреди создания участка (без привязанного отдела), чистим их отдельно через сервис
            var sections = _sectionRepository.SearchByAddress(_testSectionAddress).ToList();
            foreach (var sec in sections)
            {
                _sectionService.RemoveSection(sec.SectionId);
            }

            // На случай, если осталась изолированная смена
            var schedules = _scheduleRepository.SearchBySupervisor(_testSupervisor).ToList();
            foreach (var s in schedules)
            {
                _scheduleService.RemoveSchedule(s.ShiftId);
            }

            // Очищаем тестового пользователя
            var existingUser = _userRepository.GetByLogin(_testLogin);
            if (existingUser != null)
            {
                _userService.RemoveUser(existingUser.UserId);
            }
    }

    public void Dispose()
    {
        // Окончательно очищаем тестовые сущности
        CleanUpTestData();

        OleDbConnection.ReleaseObjectPool();

        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}