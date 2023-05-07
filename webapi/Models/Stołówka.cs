using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Stołówka
{
    public long IdStołówki { get; set; }

    public long IdBudynku { get; set; }

    public long IdZamówienia { get; set; }

    public long IdProduktu { get; set; }

    public string? InformacjeDodatkowe { get; set; }

    public virtual Budynek IdBudynkuNavigation { get; set; } = null!;

    public virtual Produkt IdProduktuNavigation { get; set; } = null!;

    public virtual Zamówienie IdZamówieniaNavigation { get; set; } = null!;
}
