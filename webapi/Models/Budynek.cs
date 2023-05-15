using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Budynek
{
    public long IdBudynku { get; set; }

    public string Nazwa { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public virtual ICollection<Stołówka> Stołówki { get; set; } = new List<Stołówka>();
}
