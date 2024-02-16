using AbsencesAPI.Common.DTOS.Stats;
using AbsencesAPI.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AbsencesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StatsController : Controller
{
    private IStatsService StatsService { get; }

	public StatsController(IStatsService statsService)
	{
        StatsService = statsService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateStat(StatsCreate statsCreate)
    {
        var id = await StatsService.CreateStatAsync(statsCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateStat(StatsUpdate statsUpdate)
    {
        await StatsService.UpdateStatAsync(statsUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteStat(StatsDelete statsDelete)
    {
        await StatsService.DeleteStatAsync(statsDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetStats()
    {
        var stats = await StatsService.GetStatsAsync();
        return Ok(stats);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetStatById(int id)
    {
        var stat = await StatsService.GetStatByIdAsync(id);
        return Ok(stat);
    }
}
