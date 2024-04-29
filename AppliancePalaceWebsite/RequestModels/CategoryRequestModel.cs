using System.ComponentModel.DataAnnotations;

namespace AppliancePalaceWebsite;

public class CategoryRequestModel
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Name { get; set; } = null!;
}
