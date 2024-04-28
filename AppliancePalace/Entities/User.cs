using NUlid;
using System.Text.Json.Serialization;

namespace AppliancePalace;

public class User
{
    public Ulid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    [JsonIgnore]
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public RoleEmun Role { get; set; }
    public ICollection<Orders>? Orders { get; set; }
}
