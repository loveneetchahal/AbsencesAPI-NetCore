using AbsencesAPI.Common.DTOS.Employee;

namespace AbsencesAPI.Common.Interfaces;

public interface IEmployeeService
{
    Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate);
    Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate);
    Task DeleteEmployeeAsync(EmployeeDelete employeeDelete);
    Task<EmployeeDetails> GetEmployeeByIdAsync(int id);
    Task<List<EmployeeList>> GetEmployeesAsync(EmployeeFilter employeeFilter);    
}
