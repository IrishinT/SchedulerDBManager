using System.Data;
using System.Data.OleDb;
using Moq;
using SchedulerDBManager.DataAccess.Database;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories.Access;
using Xunit;

namespace SchedulerDBManager.Tests;

public class AccessScheduleRepositoryTests
{
    private readonly Mock<IDatabase> _dbMock;
    private readonly AccessScheduleRepository _repository;

    public AccessScheduleRepositoryTests()
    {
        _dbMock = new Mock<IDatabase>();
        _repository = new AccessScheduleRepository(_dbMock.Object);
    }

    [Fact]
    public void GetAll_ReturnsMappedSchedules()
    {
        // Arrange
        var dt = new DataTable();
        dt.Columns.Add("shift_id", typeof(int));
        dt.Columns.Add("start_time", typeof(DateTime));
        dt.Columns.Add("end_time", typeof(DateTime));
        dt.Columns.Add("duration", typeof(int));
        dt.Columns.Add("worker_count", typeof(int));
        dt.Columns.Add("supervisor_fullname", typeof(string));
        dt.Columns.Add("section_id", typeof(int));
        dt.Columns.Add("address", typeof(string));
        dt.Columns.Add("shift_date", typeof(DateTime));

        var startTime = new DateTime(2026, 6, 15, 8, 0, 0);
        var endTime = new DateTime(2026, 6, 15, 16, 0, 0);
        dt.Rows.Add(1, startTime, endTime, 8, 10, "Иванов И.И.", 101, "\"ул. Ленина, 5\"", startTime.Date);

        _dbMock.Setup(d => d.ExecuteSelect(It.Is<string>(q => q.Contains("sections sec")), It.IsAny<OleDbParameter[]>())).Returns(dt);

        // Act
        var result = _repository.GetAll().ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result[0].ShiftId);
        Assert.Equal(startTime, result[0].StartTime);
        Assert.Equal(endTime, result[0].EndTime);
        Assert.Equal(8, result[0].Duration);
        Assert.Equal(10, result[0].WorkerCount);
        Assert.Equal("Иванов И.И.", result[0].SupervisorFullname);
        Assert.Equal(101, result[0].SectionId);
        Assert.Equal("ул. Ленина, 5", result[0].SectionAddress); // проверяем кавычки
        Assert.Equal(startTime.Date, result[0].ShiftDate);
    }

    [Fact]
    public void SearchBySupervisor_ReturnsMatchingSchedules()
    {
        // Arrange
        var dt = new DataTable();
        dt.Columns.Add("shift_id", typeof(int));
        dt.Columns.Add("start_time", typeof(DateTime));
        dt.Columns.Add("end_time", typeof(DateTime));
        dt.Columns.Add("duration", typeof(int));
        dt.Columns.Add("worker_count", typeof(int));
        dt.Columns.Add("supervisor_fullname", typeof(string));
        dt.Columns.Add("section_id", typeof(int));
        dt.Columns.Add("address", typeof(string));
        dt.Columns.Add("shift_date", typeof(DateTime));

        var startTime = new DateTime(2026, 6, 15, 8, 0, 0);
        dt.Rows.Add(1, startTime, startTime.AddHours(8), 8, 5, "Петров П.П.", 101, "Адрес", startTime.Date);

        OleDbParameter[] capturedParams = null!;
        _dbMock.Setup(d => d.ExecuteSelect(It.Is<string>(q => q.Contains("supervisor_fullname")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p)
               .Returns(dt);

        // Act
        var result = _repository.SearchBySupervisor("Петр").ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Петров П.П.", result[0].SupervisorFullname);
        Assert.NotNull(capturedParams);
        Assert.Single(capturedParams);
        Assert.Equal("%Петр%", capturedParams[0].Value);
    }

    [Fact]
    public void Add_ExecutesInsertCommandWithParameters()
    {
        // Arrange
        var s = new Schedule
        {
            StartTime = new DateTime(2026, 6, 15, 8, 0, 0),
            EndTime = new DateTime(2026, 6, 15, 16, 0, 0),
            Duration = 8,
            WorkerCount = 12,
            SupervisorFullname = "Сидоров С.С.",
            SectionId = 101,
            ShiftDate = new DateTime(2026, 6, 15)
        };
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("INSERT INTO schedule")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Add(s);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Equal(7, capturedParams.Length);
        Assert.Equal(s.StartTime, capturedParams[0].Value);
        Assert.Equal(s.EndTime, capturedParams[1].Value);
        Assert.Equal(s.Duration, capturedParams[2].Value);
        Assert.Equal(s.WorkerCount, capturedParams[3].Value);
        Assert.Equal(s.SupervisorFullname, capturedParams[4].Value);
        Assert.Equal(s.SectionId, capturedParams[5].Value);
        Assert.Equal(s.ShiftDate, capturedParams[6].Value);
    }

    [Fact]
    public void Update_ExecutesUpdateCommandWithParameters()
    {
        // Arrange
        var s = new Schedule
        {
            ShiftId = 99,
            StartTime = new DateTime(2026, 6, 15, 8, 0, 0),
            EndTime = new DateTime(2026, 6, 15, 16, 0, 0),
            Duration = 8,
            WorkerCount = 12,
            SupervisorFullname = "Сидоров С.С.",
            SectionId = 101,
            ShiftDate = new DateTime(2026, 6, 15)
        };
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("UPDATE schedule")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Update(s);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Equal(8, capturedParams.Length);
        Assert.Equal(s.StartTime, capturedParams[0].Value);
        Assert.Equal(s.EndTime, capturedParams[1].Value);
        Assert.Equal(s.Duration, capturedParams[2].Value);
        Assert.Equal(s.WorkerCount, capturedParams[3].Value);
        Assert.Equal(s.SupervisorFullname, capturedParams[4].Value);
        Assert.Equal(s.SectionId, capturedParams[5].Value);
        Assert.Equal(s.ShiftDate, capturedParams[6].Value);
        Assert.Equal(s.ShiftId, capturedParams[7].Value);
    }

    [Fact]
    public void Delete_ExecutesDeleteCommandWithId()
    {
        // Arrange
        int shiftId = 77;
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("DELETE FROM schedule")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Delete(shiftId);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Single(capturedParams);
        Assert.Equal(shiftId, capturedParams[0].Value);
    }
}