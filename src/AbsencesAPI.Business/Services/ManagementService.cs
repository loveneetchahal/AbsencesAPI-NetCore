using AbsencesAPI.Business.Exceptions;
using AbsencesAPI.Business.Validation.Management;
using AbsencesAPI.Common.DTOS.Management;
using AbsencesAPI.Common.DTOS.Stats;
using AbsencesAPI.Common.Interfaces;
using AbsencesAPI.Common.Model;
using AutoMapper;
using FluentValidation;

namespace AbsencesAPI.Business.Services;

public class ManagementService : IManangementService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Management> ManagementRepository { get; }
    private ManagementCreateValidator CreateValidator { get; }
    private ManagementUpdateValidator UpdateValidator { get; }

    public ManagementService(IMapper mapper,
                            IGenericRepository<Management> managementRepository,
                            ManagementCreateValidator createValidator,
                            ManagementUpdateValidator updateValidator)
    {
        Mapper = mapper;
        ManagementRepository = managementRepository;
        CreateValidator = createValidator;
        UpdateValidator = updateValidator;
    }

    public async Task<int> CreateManagementAsync(ManagementCreate managementCreate)
    {
        await CreateValidator.ValidateAndThrowAsync(managementCreate);

        var entity = Mapper.Map<Management>(managementCreate);
        await ManagementRepository.InsertAsync(entity);
        await ManagementRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteManagementAsync(ManagementDelete managementDelete)
    {
        var entity = await ManagementRepository.GetByIdAsync(managementDelete.Id);

        if (entity == null)
            throw new NotFoundException(managementDelete.Id, "Management");

        if (entity.Employees.Count > 0)
            throw new DependentEntitiesException(entity.Employees.Select(a => a.Id).ToList(), "Employees");

        ManagementRepository.Delete(entity);
        await ManagementRepository.SaveChangesAsync();
    }

    public async Task<List<ManagementGet>> GetManagementAsync()
    {
        var entities = await ManagementRepository.GetAsync(null, null);
        return Mapper.Map<List<ManagementGet>>(entities);
    }

    public async Task<ManagementGet> GetManagementByIdAsync(int id)
    {
        var entity = await ManagementRepository.GetByIdAsync(id);

        if (entity == null)
            throw new NotFoundException(id, "Management");

        return Mapper.Map<ManagementGet>(entity);
    }

    public async Task UpdateManagementAsync(ManagementUpdate managementUpdate)
    {
        await UpdateValidator.ValidateAndThrowAsync(managementUpdate);

        var existingEntity = await ManagementRepository.GetByIdAsync(managementUpdate.Id);

        if (existingEntity == null)
            throw new NotFoundException(managementUpdate.Id, "Management");

        var entity = Mapper.Map<Management>(managementUpdate);
        ManagementRepository.Update(entity);
        await ManagementRepository.SaveChangesAsync();
    }
}
