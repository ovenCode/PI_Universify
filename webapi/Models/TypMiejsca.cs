using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class TypMiejsca
{
    public long IdTypu { get; set; }

    public string Typ { get; set; } = null!;

    public virtual ICollection<Miejsce> Miejsca { get; set; } = new List<Miejsce>();
}
