using AbsencesAPI.Common.DTOS.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsencesAPI.Common.Interfaces;

public interface IStatsService
{
    Task<int> CreateStatAsync(StatsCreate statsCreate);
    Task UpdateStatAsync(StatsUpdate statsUpdate);
    Task DeleteStatAsync(StatsDelete statsDelete);
    Task<StatsGet> GetStatByIdAsync(int id);
    Task<List<StatsGet>> GetStatsAsync();
}
