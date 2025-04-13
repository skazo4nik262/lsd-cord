using System;
using System.Collections.Generic;

namespace LSD_CORP;

public partial class Furniture
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int MaterialId { get; set; }

    public double MaterialCount { get; set; }

    public double MakeCost { get; set; }

    public double SelCost { get; set; }

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Material Material { get; set; } = null!;

    public void SetMatCli()
    {
        ClientId = Client.Id;
        MaterialId = Material.Id;
    }
}
