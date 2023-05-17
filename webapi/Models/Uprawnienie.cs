using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Uprawnienie
{
    public long IdUprawnienia { get; set; }

    public long IdRoli { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual Rola Rola { get; set; } = null!;
}
