using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Grupa
{
    public long IdGrupy { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<GrupaStudent> Studenci { get; set; } = new List<GrupaStudent>();

    public virtual ICollection<GrupaNauczyciel> Nauczyciele { get; set; } = new List<GrupaNauczyciel>();
}
