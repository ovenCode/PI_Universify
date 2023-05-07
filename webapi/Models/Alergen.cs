using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Alergen
{
    public long IdAlergenu { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Dieta> Dieties { get; set; } = new List<Dieta>();
}
