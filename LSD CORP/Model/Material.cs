using System;
using System.Collections.Generic;

namespace LSD_CORP;

public partial class Material
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string BuyCost { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Furniture> Furnitures { get; set; } = new List<Furniture>();
}
