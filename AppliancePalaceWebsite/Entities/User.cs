using System.Text.Json.Serialization;

namespace AppliancePalaceWebsite;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonIgnore]
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public RoleEmun Role { get; set; } = RoleEmun.User;
    [JsonIgnore]
    public ICollection<Order>? Orders { get; set; }
}
