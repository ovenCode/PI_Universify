using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Produkt
{
    public long IdProduktu { get; set; }

    public string Nazwa { get; set; } = null!;

    public double IlośćProduktu { get; set; }

    public string Jednostka { get; set; } = null!;

    public long IdStołówki { get; set; }

    public virtual Stołówka Stołówka { get; set; } = null!;
}
