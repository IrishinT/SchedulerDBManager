using Moq;
using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;
using Xunit;

namespace SchedulerDBManager.Tests;

public class SectionServiceTests
{
    private readonly Mock<ISectionRepository> _sectionRepoMock;
    private readonly Mock<IScheduleRepository> _scheduleRepoMock;
    private readonly SectionService _sectionService;

    public SectionServiceTests()
    {
        _sectionRepoMock = new Mock<ISectionRepository>();
        _scheduleRepoMock = new Mock<IScheduleRepository>();
        _sectionService = new SectionService(_sectionRepoMock.Object, _scheduleRepoMock.Object);
    }

    #region GetAllSections Tests

    [Fact]
    public void GetAllSections_ReturnsAllSectionsFromRepository()
    {
        // Arrange
        var expectedSections = new List<Section>
        {
            new() { SectionId = 1, Address = "Адрес 1", DepartmentId = 10 },
            new() { SectionId = 2, Address = "Адрес 2", DepartmentId = 10 }
        };
        _sectionRepoMock.Setup(r => r.GetAll()).Returns(expectedSections);

        // Act
        var result = _sectionService.GetAllSections().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        _sectionRepoMock.Verify(r => r.GetAll(), Times.Once);
    }

    #endregion

    #region CreateSection Tests

    [Fact]
    public void CreateSection_ValidSection_TrimsAndSaves()
    {
        // Arrange
        var section = new Section
        {
            Address = "  г. Тверь, ул. Маршала Буденного, 10  ",
            DepartmentId = 5,
            Phone = "  +79998887766  "
        };

        // Act
        _sectionService.CreateSection(section);

        // Assert
        Assert.Equal("г. Тверь, ул. Маршала Буденного, 10", section.Address); // Пробелы по краям удалены
        Assert.Equal("+79998887766", section.Phone);
        _sectionRepoMock.Verify(r => r.Add(section), Times.Once);
    }

    [Fact]
    public void CreateSection_NullSection_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sectionService.CreateSection(null!));
    }

    [Theory]
    [InlineData(null, 5)]
    [InlineData("", 5)]
    [InlineData("   ", 5)]
    [InlineData("Адрес", 0)]
    [InlineData("Адрес", -5)]
    public void CreateSection_InvalidData_ThrowsArgumentException(string address, int deptId)
    {
        // Arrange
        var section = new Section { Address = address, DepartmentId = deptId };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _sectionService.CreateSection(section));
        _sectionRepoMock.Verify(r => r.Add(It.IsAny<Section>()), Times.Never);
    }

    #endregion

    #region UpdateSection Tests

    [Fact]
    public void UpdateSection_ValidSection_TrimsAndUpdates()
    {
        // Arrange
        var section = new Section
        {
            SectionId = 1,
            Address = " г. Москва ",
            DepartmentId = 1,
            Phone = null // Проверяем, что null-значение телефона корректно обрабатывается оператором ?.
        };

        // Act
        _sectionService.UpdateSection(section);

        // Assert
        Assert.Equal("г. Москва", section.Address);
        Assert.Null(section.Phone);
        _sectionRepoMock.Verify(r => r.Update(section), Times.Once);
    }

    #endregion

    #region RemoveSection Tests

    [Fact]
    public void RemoveSection_NoSchedules_DeletesOnlySection()
    {
        // Arrange
        int sectionId = 1;
        _scheduleRepoMock.Setup(r => r.GetAll()).Returns(new List<Schedule>());

        // Act
        _sectionService.RemoveSection(sectionId);

        // Assert
        _scheduleRepoMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
        _sectionRepoMock.Verify(r => r.Delete(sectionId), Times.Once);
    }

    [Fact]
    public void RemoveSection_WithSchedules_CascadesAllDeletions()
    {
        // Arrange
        int sectionId = 5;

        // Смены: две принадлежат удаляемому участку 5, одна — постороннему участку 99
        var schedules = new List<Schedule>
        {
            new() { ShiftId = 1, SectionId = 5 },
            new() { ShiftId = 2, SectionId = 5 },
            new() { ShiftId = 3, SectionId = 99 } // Не должна удаляться
        };
        _scheduleRepoMock.Setup(r => r.GetAll()).Returns(schedules);

        // Act
        _sectionService.RemoveSection(sectionId);

        // Assert
        // 1. Проверяем каскадное удаление смен
        _scheduleRepoMock.Verify(r => r.Delete(1), Times.Once);
        _scheduleRepoMock.Verify(r => r.Delete(2), Times.Once);
        _scheduleRepoMock.Verify(r => r.Delete(3), Times.Never); // Чужая смена сохранена

        // 2. Проверяем удаление самого участка
        _sectionRepoMock.Verify(r => r.Delete(sectionId), Times.Once);
    }

    #endregion
}