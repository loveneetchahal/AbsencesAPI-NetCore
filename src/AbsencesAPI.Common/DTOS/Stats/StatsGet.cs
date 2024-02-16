using AbsencesAPI.Common.DTOS.Absence;

namespace AbsencesAPI.Common.DTOS.Stats;

public record StatsGet(int Id, string Description, int? Value, List<AbsenceGet> Absences);
