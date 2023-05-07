using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Nauczyciel
{
    public long IdNauczyciela { get; set; }

    public long IdUżytkownika { get; set; }

    public long IdPrzedmiotu { get; set; }

    public long IdWydziału { get; set; }

    public long IdSpecjalizacji { get; set; }

    public virtual Przedmiot IdPrzedmiotuNavigation { get; set; } = null!;

    public virtual Specjalizacja IdSpecjalizacjiNavigation { get; set; } = null!;

    public virtual Użytkownik IdUżytkownikaNavigation { get; set; } = null!;

    public virtual Wydział IdWydziałuNavigation { get; set; } = null!;

    public virtual ICollection<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();
}
