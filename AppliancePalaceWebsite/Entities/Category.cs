﻿using System.Text.Json.Serialization;

namespace AppliancePalaceWebsite;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Product>? Products { get; set; }
}
