using AbsencesAPI.Common.DTOS.Absence;

namespace AbsencesAPI.Common.Interfaces;

public interface IAbsenceService
{
    Task<int> CreateAbsenceAsync(AbsenceCreate absenceCreate);
    Task UpdateAbsenceAsync(AbsenceUpdate absenceUpdate);
    Task DeleteAbsenceAsync(AbsenceDelete absenceDelete);
    Task<AbsenceGet> GetAbsenceByIdAsync(int id);
    Task<List<AbsenceGet>> GetAbsencesAsync();
}
