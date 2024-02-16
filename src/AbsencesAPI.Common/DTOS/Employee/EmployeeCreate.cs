namespace AbsencesAPI.Common.DTOS.Employee;

public record EmployeeCreate(string Name, int? EmployeeNumber, string Department, int ManagerId);