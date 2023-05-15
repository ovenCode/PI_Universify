using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Danie
{
    public long IdDania { get; set; }

    public string Nazwa { get; set; } = null!;

    public long IlośćKalorii { get; set; }

    public long IdSkładnika { get; set; }

    public long IdDiety { get; set; }

    public virtual Dieta Dieta { get; set; } = null!;

    public virtual ICollection<Składnik> Składniki { get; set; } = new List<Składnik>();

    public virtual Zamówienie? Zamówienie { get; set; }
}
