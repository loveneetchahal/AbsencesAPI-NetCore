using AbsencesAPI.Common.DTOS.Employee;

namespace AbsencesAPI.Common.DTOS.Absence;

public record AbsenceUpdate(int Id, DateTime Date, string Type, List<int> Employees, int StatsId);