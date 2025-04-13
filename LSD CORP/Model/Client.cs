using System;
using System.Collections.Generic;

namespace LSD_CORP;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Furniture> Furnitures { get; set; } = new List<Furniture>();
}
