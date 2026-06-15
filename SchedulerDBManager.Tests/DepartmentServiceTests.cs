using Moq;
using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;
using Xunit;

namespace SchedulerDBManager.Tests;

public class DepartmentServiceTests
{
    private readonly Mock<IDepartmentRepository> _deptRepoMock;
    private readonly Mock<ISectionRepository> _sectionRepoMock;
    private readonly Mock<IScheduleRepository> _scheduleRepoMock;
    private readonly DepartmentService _departmentService;

    public DepartmentServiceTests()
    {
        _deptRepoMock = new Mock<IDepartmentRepository>();
        _sectionRepoMock = new Mock<ISectionRepository>();
        _scheduleRepoMock = new Mock<IScheduleRepository>();

        _departmentService = new DepartmentService(
            _deptRepoMock.Object,
            _sectionRepoMock.Object,
            _scheduleRepoMock.Object
        );
    }

    #region GetAllDepartments Tests

    [Fact]
    public void GetAllDepartments_ReturnsAllDepartmentsFromRepository()
    {
        // Arrange
        var expectedDepts = new List<Department>
        {
            new() { DepartmentId = 1, DepartmentName = "Бухгалтерия", HeadFullName = "Иванова И.И." },
            new() { DepartmentId = 2, DepartmentName = "IT-отдел", HeadFullName = "Петров П.П." }
        };
        _deptRepoMock.Setup(r => r.GetAll()).Returns(expectedDepts);

        // Act
        var result = _departmentService.GetAllDepartments().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        _deptRepoMock.Verify(r => r.GetAll(), Times.Once);
    }

    #endregion

    #region CreateDepartment Tests

    [Fact]
    public void CreateDepartment_ValidDepartment_TrimsAndSaves()
    {
        // Arrange
        var dept = new Department
        {
            DepartmentName = "  Отдел контроля качества  ",
            HeadFullName = "  Сидоров С.С.  "
        };

        // Act
        _departmentService.CreateDepartment(dept);

        // Assert
        Assert.Equal("Отдел контроля качества", dept.DepartmentName); // Пробелы по краям удалены
        Assert.Equal("Сидоров С.С.", dept.HeadFullName);
        _deptRepoMock.Verify(r => r.Add(dept), Times.Once);
    }

    [Fact]
    public void CreateDepartment_NullDepartment_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _departmentService.CreateDepartment(null!));
    }

    [Theory]
    [InlineData(null, "Иванов И.И.")]
    [InlineData("", "Иванов И.И.")]
    [InlineData("   ", "Иванов И.И.")]
    [InlineData("ОТК", null)]
    [InlineData("ОТК", "")]
    [InlineData("ОТК", "   ")]
    public void CreateDepartment_InvalidData_ThrowsArgumentException(string name, string head)
    {
        // Arrange
        var dept = new Department { DepartmentName = name, HeadFullName = head };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _departmentService.CreateDepartment(dept));
        _deptRepoMock.Verify(r => r.Add(It.IsAny<Department>()), Times.Never);
    }

    #endregion

    #region UpdateDepartment Tests

    [Fact]
    public void UpdateDepartment_ValidDepartment_TrimsAndUpdates()
    {
        // Arrange
        var dept = new Department
        {
            DepartmentId = 5,
            DepartmentName = " Администрация ",
            HeadFullName = " Смирнова А.А. "
        };

        // Act
        _departmentService.UpdateDepartment(dept);

        // Assert
        Assert.Equal("Администрация", dept.DepartmentName);
        Assert.Equal("Смирнова А.А.", dept.HeadFullName);
        _deptRepoMock.Verify(r => r.Update(dept), Times.Once);
    }

    #endregion

    #region RemoveDepartment Tests

    [Fact]
    public void RemoveDepartment_NoSections_DeletesOnlyDepartment()
    {
        // Arrange
        int deptId = 1;
        // Возвращаем пустой список участков для этого отдела
        _sectionRepoMock.Setup(r => r.GetAll()).Returns(new List<Section>());

        // Act
        _departmentService.RemoveDepartment(deptId);

        // Assert
        _scheduleRepoMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
        _sectionRepoMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
        _deptRepoMock.Verify(r => r.Delete(deptId), Times.Once);
    }

    [Fact]
    public void RemoveDepartment_WithSectionsAndSchedules_CascadesAllDeletions()
    {
        // Arrange
        int deptId = 10;

        // Имитируем участки в базе данных: два относятся к отделу 10, один — к другому отделу (20)
        var allSections = new List<Section>
        {
            new() { SectionId = 101, DepartmentId = 10, Address = "Участок 1" },
            new() { SectionId = 102, DepartmentId = 10, Address = "Участок 2" },
            new() { SectionId = 103, DepartmentId = 20, Address = "Участок другого отдела" }
        };

        // Имитируем смены: две привязаны к удаляемому участку 101, одна — к постороннему участку 103
        var allSchedules = new List<Schedule>
        {
            new() { ShiftId = 501, SectionId = 101 },
            new() { ShiftId = 502, SectionId = 101 },
            new() { ShiftId = 503, SectionId = 103 } // Не должна быть удалена
        };

        _sectionRepoMock.Setup(r => r.GetAll()).Returns(allSections);
        _scheduleRepoMock.Setup(r => r.GetAll()).Returns(allSchedules);

        // Act
        _departmentService.RemoveDepartment(deptId);

        // Assert
        // 1. Проверяем каскадное удаление смен (только тех, которые привязаны к удаляемым участкам)
        _scheduleRepoMock.Verify(r => r.Delete(501), Times.Once);
        _scheduleRepoMock.Verify(r => r.Delete(502), Times.Once);
        _scheduleRepoMock.Verify(r => r.Delete(503), Times.Never); // Посторонняя смена осталась нетронутой

        // 2. Проверяем удаление самих участков
        _sectionRepoMock.Verify(r => r.Delete(101), Times.Once);
        _sectionRepoMock.Verify(r => r.Delete(102), Times.Once);
        _sectionRepoMock.Verify(r => r.Delete(103), Times.Never); // Посторонний участок остался нетронутым

        // 3. Проверяем удаление самого отдела в конце цепочки
        _deptRepoMock.Verify(r => r.Delete(deptId), Times.Once);
    }

    #endregion
}