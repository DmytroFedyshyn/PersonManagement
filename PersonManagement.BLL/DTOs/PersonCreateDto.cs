namespace PersonManagement.BLL.DTOs;

public class PersonCreateDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? City { get; set; }
    public string? AddressLine { get; set; }
}
