namespace AppliancePalace;

public class City : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<User>? Users { get; set; }
}