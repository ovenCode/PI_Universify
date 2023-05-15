using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Parking
{
    public long IdParkingu { get; set; }

    public string Adres { get; set; } = null!;

    public virtual ICollection<Miejsce> Miejsca { get; set; } = new List<Miejsce>();
}
