using AbsencesAPI.Common.DTOS.Absence;
using AbsencesAPI.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AbsencesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AbsenceController : Controller
{
    private IAbsenceService AbsenceService { get; }

	public AbsenceController(IAbsenceService absenceService)
	{
        AbsenceService = absenceService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateAbsence(AbsenceCreate absenceCreate)
    {
        var id = await AbsenceService.CreateAbsenceAsync(absenceCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateAbsence(AbsenceUpdate absenceUpdate)
    {
        await AbsenceService.UpdateAbsenceAsync(absenceUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteAbsence(AbsenceDelete absenceDelete)
    {
        await AbsenceService.DeleteAbsenceAsync(absenceDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetAbsenceById(int id)
    {
        var absence = await AbsenceService.GetAbsenceByIdAsync(id);
        return Ok(absence);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetAbsences()
    {
        var absences = await AbsenceService.GetAbsencesAsync();
        return Ok(absences);
    }
}
