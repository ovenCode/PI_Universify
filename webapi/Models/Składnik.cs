using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Składnik
{
    public long IdSkładnika { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Danie> Dania { get; set; } = new List<Danie>();
}
