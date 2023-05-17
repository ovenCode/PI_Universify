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

    public virtual ICollection<Nauczyciel> Nauczyciele { get; set; } = new List<Nauczyciel>();

    public virtual ICollection<Student> Studenci { get; set; } = new List<Student>();
}
