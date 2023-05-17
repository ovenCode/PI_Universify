using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Dieta
{
    public long IdDiety { get; set; }

    public string Nazwa { get; set; } = null!;

    public long IdKategorii { get; set; }

    public long IdAlergenu { get; set; }

    public double Cena { get; set; }

    public long IdDania { get; set; }

    public virtual ICollection<Alergen> Alergeny { get; set; } = new List<Alergen>();

    public virtual ICollection<Danie> Dania { get; set; } = null!;

    public virtual Kategoria Kategoria { get; set; } = null!;

    public virtual ICollection<Zamówienie> Zamówienia { get; set; } = new List<Zamówienie>();
}
