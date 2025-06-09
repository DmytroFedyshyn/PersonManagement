using Microsoft.AspNetCore.Mvc;
using PersonManagement.BLL.DTOs;
using PersonManagement.BLL.Interfaces;

namespace PersonManagement.PL.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonController> _logger;

    public PersonController(IPersonService personService, ILogger<PersonController> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PersonCreateDto dto)
    {
        _logger.LogInformation("Received request to create person: {FirstName} {LastName}", dto.FirstName, dto.LastName);

        await _personService.AddPersonAsync(dto);

        _logger.LogInformation("Person created successfully.");
        return Ok(new { message = "Person added successfully" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRequest filter)
    {
        _logger.LogInformation("Received GET request with filters: FirstName={FirstName}, LastName={LastName}, City={City}",
            filter.FirstName, filter.LastName, filter.City);

        var result = await _personService.GetFilteredPersonsAsync(filter);

        _logger.LogInformation("Returned {Count} persons from filter", result.Count());
        return Ok(result);
    }
}
