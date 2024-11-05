using WebAPITest1.Data.Interfaces;
using WebAPITest1.Models.Entities;

namespace WebAPITest1.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext context;
    public EmployeeRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public List<Employee> GetAll()
    {
        var list = context.Employees.ToList();
        return list;
    }

    public Employee? GetOne(Guid id)
    {
        var employee = context.Employees.Find(id);
        return employee;
    }

    public Employee Post(Employee employee)
    {
        context.Employees.Add(employee);
        context.SaveChanges();
        return employee;
    }

    public Employee? Put(Employee employee)
    {
        var editingEmployee = context.Employees.Find(employee.Id);
        if (editingEmployee is null)
        {
            return null;
        }

        editingEmployee.Name = employee.Name;
        editingEmployee.Email = employee.Email;
        editingEmployee.Phone = employee.Phone;
        editingEmployee.Salary = employee.Salary;

        context.SaveChanges();
        return editingEmployee;
    }

    public bool Delete(Guid id)
    {
        var employee = context.Employees.Find(id);
        if (employee is null)
        {
            return false;
        }
        context.Employees.Remove(employee);
        context.SaveChanges();
        return true;
    }

}
