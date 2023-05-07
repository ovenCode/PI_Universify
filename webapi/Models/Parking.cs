using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Parking
{
    public long IdParkingu { get; set; }

    public long IdMiejsca { get; set; }

    public string Adres { get; set; } = null!;

    public virtual Miejsce IdMiejscaNavigation { get; set; } = null!;
}
