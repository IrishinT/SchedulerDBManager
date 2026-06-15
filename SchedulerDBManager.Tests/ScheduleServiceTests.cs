using Moq;
using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;
using Xunit;

namespace SchedulerDBManager.Tests;

public class ScheduleServiceTests
{
    private readonly Mock<IScheduleRepository> _scheduleRepoMock;
    private readonly ScheduleService _scheduleService;

    public ScheduleServiceTests()
    {
        _scheduleRepoMock = new Mock<IScheduleRepository>();
        _scheduleService = new ScheduleService(_scheduleRepoMock.Object);
    }

    #region GetAllSchedules Tests

    [Fact]
    public void GetAllSchedules_ReturnsSchedulesFromRepository()
    {
        // Arrange
        var expectedSchedules = new List<Schedule>
        {
            new() { ShiftId = 1, SupervisorFullname = "Иванов И.И." },
            new() { ShiftId = 2, SupervisorFullname = "Петров П.П." }
        };
        _scheduleRepoMock.Setup(r => r.GetAll()).Returns(expectedSchedules);

        // Act
        var result = _scheduleService.GetAllSchedules().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        _scheduleRepoMock.Verify(r => r.GetAll(), Times.Once);
    }

    #endregion

    #region CreateSchedule Tests

    [Fact]
    public void CreateSchedule_ValidSchedule_CalculatesFieldsAndSaves()
    {
        // Arrange
        var startTime = new DateTime(2026, 6, 15, 8, 0, 0);
        var endTime = new DateTime(2026, 6, 15, 16, 0, 0);
        var schedule = new Schedule
        {
            StartTime = startTime,
            EndTime = endTime,
            WorkerCount = 10,
            SupervisorFullname = "  Сидоров С.С.  "
        };

        // Act
        _scheduleService.CreateSchedule(schedule);

        // Assert
        Assert.Equal(8, schedule.Duration); // Авторасчет: 16:00 - 08:00 = 8 часов
        Assert.Equal(startTime.Date, schedule.ShiftDate); // Дата смены совпадает с датой начала
        Assert.Equal("Сидоров С.С.", schedule.SupervisorFullname); // Строка очищена от пробелов
        _scheduleRepoMock.Verify(r => r.Add(schedule), Times.Once);
    }

    [Fact]
    public void CreateSchedule_NullSchedule_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _scheduleService.CreateSchedule(null!));
    }

    #endregion

    #region UpdateSchedule Tests

    [Fact]
    public void UpdateSchedule_ValidSchedule_CalculatesFieldsAndUpdates()
    {
        // Arrange
        var startTime = new DateTime(2026, 6, 15, 12, 0, 0);
        var endTime = new DateTime(2026, 6, 15, 22, 0, 0);
        var schedule = new Schedule
        {
            ShiftId = 1,
            StartTime = startTime,
            EndTime = endTime,
            WorkerCount = 5,
            SupervisorFullname = "Иванов И.И."
        };

        // Act
        _scheduleService.UpdateSchedule(schedule);

        // Assert
        Assert.Equal(10, schedule.Duration); // Авторасчет: 22:00 - 12:00 = 10 часов
        Assert.Equal(startTime.Date, schedule.ShiftDate);
        _scheduleRepoMock.Verify(r => r.Update(schedule), Times.Once);
    }

    #endregion

    #region RemoveSchedule Tests

    [Fact]
    public void RemoveSchedule_CallsRepositoryDelete()
    {
        // Arrange
        int shiftId = 99;

        // Act
        _scheduleService.RemoveSchedule(shiftId);

        // Assert
        _scheduleRepoMock.Verify(r => r.Delete(shiftId), Times.Once);
    }

    #endregion

    #region Validation Tests

    [Fact]
    public void Validate_StartTimeAfterOrEqualEndTime_ThrowsArgumentException()
    {
        // Arrange
        var schedule = new Schedule
        {
            StartTime = new DateTime(2026, 6, 15, 17, 0, 0),
            EndTime = new DateTime(2026, 6, 15, 16, 0, 0), // Окончание раньше начала
            WorkerCount = 5,
            SupervisorFullname = "Иванов И.И."
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _scheduleService.CreateSchedule(schedule));
        Assert.Equal("Время начала смены должно быть раньше времени окончания.", ex.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Validate_WorkerCountLessThanOrEqualToZero_ThrowsArgumentException(int workerCount)
    {
        // Arrange
        var schedule = new Schedule
        {
            StartTime = new DateTime(2026, 6, 15, 8, 0, 0),
            EndTime = new DateTime(2026, 6, 15, 16, 0, 0),
            WorkerCount = workerCount,
            SupervisorFullname = "Иванов"
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _scheduleService.CreateSchedule(schedule));
        Assert.Equal("Количество рабочих должно быть больше нуля.", ex.Message);
    }

    [Fact]
    public void Validate_WorkerCountGreaterThan100_ThrowsArgumentException()
    {
        // Arrange
        var schedule = new Schedule
        {
            StartTime = new DateTime(2026, 6, 15, 8, 0, 0),
            EndTime = new DateTime(2026, 6, 15, 16, 0, 0),
            WorkerCount = 101, // превышает лимит в 100 человек
            SupervisorFullname = "Иванов"
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _scheduleService.CreateSchedule(schedule));
        Assert.Equal("Количество рабочих должно быть больше 100.", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_InvalidSupervisorName_ThrowsArgumentException(string supervisor)
    {
        // Arrange
        var schedule = new Schedule
        {
            StartTime = new DateTime(2026, 6, 15, 8, 0, 0),
            EndTime = new DateTime(2026, 6, 15, 16, 0, 0),
            WorkerCount = 10,
            SupervisorFullname = supervisor
        };

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _scheduleService.CreateSchedule(schedule));
        Assert.Equal("Укажите ФИО начальника смены.", ex.Message);
    }

    #endregion
}