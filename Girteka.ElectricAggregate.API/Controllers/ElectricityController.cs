using Girteka.ElectricAggregate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Girteka.ElectricAggregate.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ElectricityController : ControllerBase
{
    private readonly IElectricity _electricity;

    public ElectricityController(IElectricity electricity)
    {
        _electricity = electricity;
    }

    [HttpGet("get/electricity")]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _electricity.Get());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}