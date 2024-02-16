using AbsencesAPI.Common.DTOS.Management;
using AbsencesAPI.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AbsencesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagementController : Controller
{
    private IManangementService ManangementService { get; }

	public ManagementController(IManangementService manangementService)
	{
        ManangementService = manangementService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateManangement(ManagementCreate managementCreate)
    {
        var id = await ManangementService.CreateManagementAsync(managementCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateManangement(ManagementUpdate managementUpdate)
    {
        await ManangementService.UpdateManagementAsync(managementUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteManangement(ManagementDelete managementDelete)
    {
        await ManangementService.DeleteManagementAsync(managementDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetManangementById(int id)
    {
        var managementGet = await ManangementService.GetManagementByIdAsync(id);
        return Ok(managementGet);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetManangement()
    {
        var managements = await ManangementService.GetManagementAsync();
        return Ok(managements);
    }
}
