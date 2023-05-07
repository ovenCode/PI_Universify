using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Miejsce
{
    public long IdMiejsca { get; set; }

    public byte[] Dostępność { get; set; } = null!;

    public long IdTypu { get; set; }

    public virtual TypMiejsca IdTypuNavigation { get; set; } = null!;

    public virtual ICollection<Parking> Parkingis { get; set; } = new List<Parking>();
}
