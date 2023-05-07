using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Rola
{
    public long IdRoli { get; set; }

    public string Nazwa { get; set; } = null!;

    public long IdUprawnienia { get; set; }

    public virtual Uprawnienie IdUprawnieniaNavigation { get; set; } = null!;
}
