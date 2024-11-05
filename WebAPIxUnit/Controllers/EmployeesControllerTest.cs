using WebAPITest1.Controllers;
using WebAPITest1.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPITest1.Data.Interfaces;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace WebAPIxUnit.Controllers
{
    public class EmployeesControllerTest
    {

        private Mock<IEmployeeRepository> mockRepository;
        private readonly Fixture fixture;
        private EmployeesController? controller;

        public EmployeesControllerTest()
        {
            this.mockRepository = new Mock<IEmployeeRepository>();
            this.fixture = new Fixture();
        }

        [Fact]
        public void EmployeeController_GetAllEmployees_ReturnsOK()
        {
            // Arrange
            var employees = fixture.CreateMany<Employee>(3).ToList();
            mockRepository.Setup(x => x.GetAll()).Returns(employees);
            controller = new EmployeesController(mockRepository.Object);

            // Act
            var result = controller.GetAllEmployees();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void EmployeeController_GetAllEmployees_ReturnsInternalServerError()
        {
            // Arrange
            mockRepository.Setup(x => x.GetAll()).Throws<Exception>();
            controller = new EmployeesController(mockRepository.Object);

            // Act
            var result = controller.GetAllEmployees();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public void EmployeeController_GetEmployeeById_ReturnsOK()
        {
            // Arrange
            var employee = fixture.Create<Employee>();
            mockRepository.Setup(x => x.GetOne(employee.Id)).Returns(employee);
            controller = new EmployeesController(mockRepository.Object);

            // Act 
            var result = controller.GetEmployeeById(employee.Id);

            // Assert  
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}