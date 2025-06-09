using Microsoft.AspNetCore.Mvc;
using PersonManagement.BLL.Interfaces;
using PersonManagement.BLL.Requests;
using PersonManagement.BLL.Responses;

namespace PersonManagement.PL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PersonCreateResponse dto)
    {
        await _personService.AddPersonAsync(dto);
        return Ok(new { message = "Person added successfully" });
    }

    [HttpGet]
    public async Task<IActionResult> GetByFilter([FromQuery] GetAllRequest filter)
    {
        var result = await _personService.GetFilteredPersonsAsync(filter);
        return Ok(result);
    }
}
