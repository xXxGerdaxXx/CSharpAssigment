
namespace Business_Library.Models;


/// <summary>
/// Describes user entity with basic contact information
/// </summary>
public class UserBase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Mobile { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostNumber { get; set; } = null!;
    public string City { get; set; } = null!;

}

