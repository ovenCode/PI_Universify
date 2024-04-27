using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Przedmiot
{
    public long IdPrzedmiotu { get; set; }

    public string Nazwa { get; set; } = null!;

    public string Kategoria { get; set; } = null!;

    public long SemestrRozpoczęcia { get; set; }

    public long IlośćSemestrów { get; set; }

    //public virtual ICollection<NauczycielPrzedmiot> NauczycielPrzedmiot { get; set; } = new List<NauczycielPrzedmiot>();
    public virtual ICollection<NauczycielPrzedmiot> Nauczyciele { get; set; } = new List<NauczycielPrzedmiot>();

    public virtual ICollection<PrzedmiotStudent> Studenci { get; set; } = new List<PrzedmiotStudent>();
}
