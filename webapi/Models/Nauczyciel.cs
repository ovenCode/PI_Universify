using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Nauczyciel
{
    public long IdNauczyciela { get; set; }

    public long IdUżytkownika { get; set; }

    public long IdWydziału { get; set; }

    public long IdSpecjalizacji { get; set; }

    public virtual ICollection<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();

    public virtual ICollection<Grupa> Grupy { get; set; } = new List<Grupa>();

    public virtual Specjalizacja Specjalizacja { get; set; } = null!;

    public virtual Użytkownik Użytkownik { get; set; } = null!;

    public virtual Wydział Wydział { get; set; } = null!;
}
