using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Parking
{
    public long IdParkingu { get; set; }

    public string Adres { get; set; } = null!;

    public long LiczbaRzedow { get; set; }

    public virtual ICollection<Miejsce> Miejsca { get; set; } = new List<Miejsce>();

    public virtual ICollection<RozkładParkingu>? RozkładParkingu { get; set; }

    public virtual ICollection<KodParkingu> KodyParkingu { get; set; } = new List<KodParkingu>();
}
