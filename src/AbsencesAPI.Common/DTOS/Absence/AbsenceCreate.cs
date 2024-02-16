namespace AbsencesAPI.Common.DTOS.Absence;

public record AbsenceCreate(DateTime Date, string Type, List<int> Employees, int StatsId);