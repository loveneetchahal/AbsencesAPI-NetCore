using AbsencesAPI.Business.Exceptions;
using AbsencesAPI.Business.Validation.Stats;
using AbsencesAPI.Common.DTOS.Stats;
using AbsencesAPI.Common.Interfaces;
using AbsencesAPI.Common.Model;
using AutoMapper;
using FluentValidation;

namespace AbsencesAPI.Business.Services;

public class StatsService : IStatsService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Stats> StatsRepository { get; }
    private StatsCreateValidator CreateValidator { get; }
    private StatsUpdateValidator UpdateValidator { get; }

    public StatsService(IMapper mapper,
                        IGenericRepository<Stats> statsRepository,
                        StatsCreateValidator createValidator,
                        StatsUpdateValidator updateValidator)
    {
        Mapper = mapper;
        StatsRepository = statsRepository;
        CreateValidator = createValidator;
        UpdateValidator = updateValidator;
    }

    public async Task<int> CreateStatAsync(StatsCreate statsCreate)
    {
        await CreateValidator.ValidateAndThrowAsync(statsCreate);

        var entity = Mapper.Map<Stats>(statsCreate);
        await StatsRepository.InsertAsync(entity);
        await StatsRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteStatAsync(StatsDelete statsDelete)
    {
        var entity = await StatsRepository.GetByIdAsync(statsDelete.Id, (stat) => stat.Absences);

        if (entity == null)
            throw new NotFoundException(statsDelete.Id, "Statistic");

        if (entity.Absences.Count > 0)
            throw new DependentEntitiesException(entity.Absences.Select(a => a.Id).ToList(), "Absences");

        StatsRepository.Delete(entity);
        await StatsRepository.SaveChangesAsync();
    }

    public async Task<StatsGet> GetStatByIdAsync(int id)
    {
        var entity = await StatsRepository.GetByIdAsync(id);

        if (entity == null)
            throw new NotFoundException(id, "Statistic");

        return Mapper.Map<StatsGet>(entity);
    }

    public async Task<List<StatsGet>> GetStatsAsync()
    {
        var entities = await StatsRepository.GetAsync(null, null, (absence) => absence.Absences);
        return Mapper.Map<List<StatsGet>>(entities);
    }

    public async Task UpdateStatAsync(StatsUpdate statsUpdate)
    {
        await UpdateValidator.ValidateAndThrowAsync(statsUpdate);

        var existingEntity = await StatsRepository.GetByIdAsync(statsUpdate.Id);

        if (existingEntity == null)
            throw new NotFoundException(statsUpdate.Id, "Statistic");

        var entity = Mapper.Map<Stats>(statsUpdate);
        StatsRepository.Update(entity);
        await StatsRepository.SaveChangesAsync();
    }
}
