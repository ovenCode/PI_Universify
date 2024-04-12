using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Miejsce
{
    public long IdMiejsca { get; set; }

    public long IdParkingu { get; set; }

    public bool? Dostępność { get; set; }

    public long IdTypu { get; set; }

    public virtual TypMiejsca Typ { get; set; } = null!;

    public virtual Parking Parking { get; set; } = null!;
}
