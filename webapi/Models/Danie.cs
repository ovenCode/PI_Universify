using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Danie
{
    public long IdDania { get; set; }

    public string Nazwa { get; set; } = null!;

    public long IlośćKalorii { get; set; }

    public long IdSkładnika { get; set; }

    public virtual ICollection<Dieta> Dieties { get; set; } = new List<Dieta>();

    public virtual Składnik IdSkładnikaNavigation { get; set; } = null!;

    public virtual ICollection<Zamówienie> Zamówienia { get; set; } = new List<Zamówienie>();
}
