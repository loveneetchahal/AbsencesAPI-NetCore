using AbsencesAPI.Business.Exceptions;
using AbsencesAPI.Business.Validation.Employee;
using AbsencesAPI.Common.DTOS.Absence;
using AbsencesAPI.Common.DTOS.Employee;
using AbsencesAPI.Common.DTOS.Stats;
using AbsencesAPI.Common.Interfaces;
using AbsencesAPI.Common.Model;
using AutoMapper;
using FluentValidation;
using System.Linq.Expressions;

namespace AbsencesAPI.Business.Services;
public class EmployeeService : IEmployeeService
{
    private IGenericRepository<Employee> EmployeeRepository { get; }
    private IGenericRepository<Management> ManagementRepository { get; }
    private IMapper Mapper { get; }
    private EmployeeCreateValidator CreateValidator { get; }
    private EmployeeUpdateValidator UpdateValidator { get; }

    public EmployeeService(IGenericRepository<Employee> employeeRepository,
                            IGenericRepository<Management> managementRepository,
                            IMapper mapper,
                            EmployeeCreateValidator createValidator,
                            EmployeeUpdateValidator updateValidator)
    {
        EmployeeRepository = employeeRepository;
        ManagementRepository = managementRepository;
        Mapper = mapper;
        CreateValidator = createValidator;
        UpdateValidator = updateValidator;
    }

    public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
    {
        await CreateValidator.ValidateAndThrowAsync(employeeCreate);

        var manager = await ManagementRepository.GetByIdAsync(employeeCreate.ManagerId);
        if (manager == null)
            throw new NotFoundException(employeeCreate.ManagerId, "Management");

        var entity = Mapper.Map<Employee>(employeeCreate);
        entity.Manager = manager;

        await EmployeeRepository.InsertAsync(entity);
        await EmployeeRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteEmployeeAsync(EmployeeDelete employeeDelete)
    {
        var entity = await EmployeeRepository.GetByIdAsync(employeeDelete.Id);
        if (entity == null)
            throw new NotFoundException(employeeDelete.Id, "Employee");

        EmployeeRepository.Delete(entity);
        await EmployeeRepository.SaveChangesAsync();
    }

    public async Task<EmployeeDetails> GetEmployeeByIdAsync(int id)
    {
                                                               //includes
        var entity = await EmployeeRepository.GetByIdAsync(id, (employee) => employee.Manager, (employee) => employee.Absences);
        if (entity == null)
            throw new NotFoundException(id, "Employee");

        return Mapper.Map<EmployeeDetails>(entity);
    }

    public async Task<List<EmployeeList>> GetEmployeesAsync(EmployeeFilter employeeFilter)
    {
        Expression<Func<Employee, bool>> nameFilter = (employee) => employeeFilter.Name == null ? true :
                                                                            employee.Name.Equals(employeeFilter.Name);

        Expression<Func<Employee, bool>> departmentFilter = (employee) => employeeFilter.Department == null ? true :
                                                                            employee.Department.Equals(employeeFilter.Department);

        Expression<Func<Employee, bool>> numberFilter = (employee) => employeeFilter.EmployeeNumber == null ? true :
                                                                            employee.EmployeeNumber.Equals(employeeFilter.EmployeeNumber);

        Expression<Func<Employee, bool>> managerFilter = (employee) => employeeFilter.Manager == null ? true :
                                                                            employee.Manager.Manager.Equals(employeeFilter.Manager);

        Expression<Func<Employee, bool>> absencesFilter = (employee) => employeeFilter.Absence == null ? true :
                                    employee.Absences.Any(absence => absence.Date.ToShortDateString().Equals(employeeFilter.Absence));

        var entities = await EmployeeRepository.GetFilteredAsync(new Expression<Func<Employee, bool>>[]
        {
            nameFilter, departmentFilter, numberFilter, managerFilter, absencesFilter
        }, employeeFilter.Skip, employeeFilter.Take, (employee) => employee.Manager, (employee) => employee.Absences);

        return Mapper.Map<List<EmployeeList>>(entities);
    }

    public async Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
    {
        await UpdateValidator.ValidateAndThrowAsync(employeeUpdate);

        var manager = await ManagementRepository.GetByIdAsync(employeeUpdate.ManagerId);
        if (manager == null)
            throw new NotFoundException(employeeUpdate.ManagerId, "Management");

        var existingEntity = await EmployeeRepository.GetByIdAsync(employeeUpdate.Id);
        if (existingEntity == null)
            throw new NotFoundException(employeeUpdate.Id, "Employee");

        var entity = Mapper.Map<Employee>(employeeUpdate);
        entity.Manager = manager;

        EmployeeRepository.Update(entity);
        await EmployeeRepository.SaveChangesAsync();
    }
}
