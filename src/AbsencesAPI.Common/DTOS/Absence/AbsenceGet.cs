using AbsencesAPI.Common.DTOS.Employee;

namespace AbsencesAPI.Common.DTOS.Absence;

public record AbsenceGet(int Id, DateTime Date, string Type, List<EmployeeList> Employees);