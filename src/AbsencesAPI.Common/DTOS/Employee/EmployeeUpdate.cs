namespace AbsencesAPI.Common.DTOS.Employee;

public record EmployeeUpdate(int Id, string Name, int? EmployeeNumber, string Department, int ManagerId);