namespace AbsencesAPI.Common.DTOS.Employee;

public record EmployeeFilter(string? Name, int? EmployeeNumber, string? Department, string? Absence, string? Manager, int? Skip, int? Take);