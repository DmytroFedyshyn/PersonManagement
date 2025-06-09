namespace PersonManagement.DAL.Entities
{
    public class Address
    {
        public long Id { get; set; }
        public string City { get; set; } = null!;
        public string AddressLine { get; set; } = null!;
    }
}
