namespace PersonManagement.DAL.Entities
{
    public class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public long? AddressId { get; set; }
        public virtual Address? Address { get; set; }
    }
}
