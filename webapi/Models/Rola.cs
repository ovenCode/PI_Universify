using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Rola
{
    public long IdRoli { get; set; }

    public string Nazwa { get; set; } = null!;

    public long IdUprawnienia { get; set; }

    public virtual ICollection<Uprawnienie> Uprawnienia { get; set; } = new List<Uprawnienie>();
    public virtual Administrator? Administrator { get; set; }
}
