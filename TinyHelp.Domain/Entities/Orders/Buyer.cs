namespace TinyHelp.Domain.Entities.Orders;

public record Buyer
{
    public string FirstName { get; init; }
    public string? LastName { get; init; } 
    public string Email { get; init; } 
    public string PhoneNumber { get; init; }

    public Buyer(string firstName, string lastName, string email, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}