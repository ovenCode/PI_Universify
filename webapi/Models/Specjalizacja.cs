using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Specjalizacja
{
    public long IdSpecjalizacji { get; set; }

    public string? Nazwa { get; set; }

    public virtual ICollection<Nauczyciel> Nauczyciele { get; set; } = new List<Nauczyciel>();
}
