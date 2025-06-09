using PersonManagement.DAL.Entities;
using PersonManagement.BLL.Requests;
using PersonManagement.BLL.Responses;

namespace PersonManagement.BLL.Interfaces;

public interface IPersonService
{
    Task AddPersonAsync(PersonCreateResponse dto);
    Task<IEnumerable<Person>> GetFilteredPersonsAsync(GetAllRequest filter);
}
