namespace PersonManagement.BLL.Responses;

public class PersonCreateResponse
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? City { get; set; }
    public string? AddressLine { get; set; }
}
