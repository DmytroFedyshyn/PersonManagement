using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonManagement.BLL.Interfaces;
using PersonManagement.BLL.Requests;
using PersonManagement.BLL.Responses;
using PersonManagement.DAL.Entities;
using PersonManagement.DAL.Interfaces;

namespace PersonManagement.BLL.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IPersonRepository repository, IMapper mapper, ILogger<PersonService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task AddPersonAsync(PersonCreateResponse dto)
    {
        _logger.LogInformation("Adding person: {FirstName} {LastName}", dto.FirstName, dto.LastName);

        var person = _mapper.Map<Person>(dto);

        await _repository.AddAsync(person);
        await _repository.SaveChangesAsync();

        _logger.LogInformation("Person added successfully.");
    }

    public async Task<IEnumerable<Person>> GetFilteredPersonsAsync(GetAllRequest filter)
    {
        _logger.LogInformation("Filtering persons: FirstName={FirstName}, LastName={LastName}, City={City}",
            filter.FirstName, filter.LastName, filter.City);

        var result = await _repository.GetAllAsync(
            p =>
                (string.IsNullOrEmpty(filter.FirstName) || p.FirstName.Contains(filter.FirstName)) &&
                (string.IsNullOrEmpty(filter.LastName) || p.LastName.Contains(filter.LastName)) &&
                (string.IsNullOrEmpty(filter.City) || (p.Address != null && p.Address.City.Contains(filter.City))),
            p => p.Address!
        );

        _logger.LogInformation("Filtered result count: {Count}", result.Count());

        return result;
    }
}
