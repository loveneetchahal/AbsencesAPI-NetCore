using AbsencesAPI.Common.DTOS.Absence;
using AbsencesAPI.Common.DTOS.Management;

namespace AbsencesAPI.Common.DTOS.Employee;

public record EmployeeDetails(int Id, string Name, int? EmployeeNumber, string Department, /*List<AbsenceGet> Absences,*/ ManagementGet Manager);