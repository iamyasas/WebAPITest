using WebAPITest1.Models.Entities;

namespace WebAPITest1.Data.Interfaces;

public interface IEmployeeRepository
{
    List<Employee> GetAll();
    Employee? GetOne(Guid id);

    Employee Post(Employee employee);

    Employee? Put(Employee employee);

    bool Delete(Guid id);
}
