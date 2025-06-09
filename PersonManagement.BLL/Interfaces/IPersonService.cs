using PersonManagement.DAL.Entities;
using PersonManagement.BLL.DTOs;

namespace PersonManagement.BLL.Interfaces;

public interface IPersonService
{
    Task AddPersonAsync(PersonCreateDto dto);
    Task<IEnumerable<Person>> GetFilteredPersonsAsync(GetAllRequest filter);
}
