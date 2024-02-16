using AbsencesAPI.Common.DTOS.Management;

namespace AbsencesAPI.Common.Interfaces;

public interface IManangementService
{
    Task<int> CreateManagementAsync(ManagementCreate managementCreate);
    Task UpdateManagementAsync(ManagementUpdate managementUpdate);
    Task DeleteManagementAsync(ManagementDelete managementDelete);
    Task<ManagementGet> GetManagementByIdAsync(int id);
    Task<List<ManagementGet>> GetManagementAsync();
}
