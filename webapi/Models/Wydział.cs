using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Wydział
{
    public long IdWydziału { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Nauczyciel> Nauczyciele { get; set; } = new List<Nauczyciel>();
}
