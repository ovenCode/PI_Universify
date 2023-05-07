using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Kategoria
{
    public long IdKategorii { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Dieta> Diety { get; set; } = new List<Dieta>();
}
