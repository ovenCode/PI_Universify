using System;
using System.Collections.Generic;

namespace webapi.Models;

public class Nauczyciel : Użytkownik
{
    // public Nauczyciel(Nauczyciel? teacher, Specjalizacja? specjalizacja)
    // {
    //     IdNauczyciela = teacher?.IdNauczyciela ?? Convert.ToInt64(0);
    //     IdUżytkownika = teacher?.IdUżytkownika ?? Convert.ToInt64(0);
    //     IdWydziału = teacher?.IdWydziału ?? Convert.ToInt64(0);
    //     IdSpecjalizacji = teacher?.IdSpecjalizacji ?? Convert.ToInt64(0);
    //     Specjalizacja = specjalizacja ?? new Specjalizacja();
    // }
    //public long IdNauczyciela { get; set; }

    //public long IdUżytkownika { get; set; }

    //public long IdWydziału { get; set; }

    //public long IdSpecjalizacji { get; set; }

    //public long IdPrzedmiotu { get; set; }

    //public virtual ICollection<NauczycielPrzedmiot> NauczycielPrzedmioty { get; set; } = new List<NauczycielPrzedmiot>();
    public virtual ICollection<NauczycielPrzedmiot> Przedmioty { get; set; } = new List<NauczycielPrzedmiot>();

    public virtual ICollection<GrupaNauczyciel> Grupy { get; set; } = new List<GrupaNauczyciel>();

    public virtual Specjalizacja Specjalizacja { get; set; } = null!;

    //public virtual Użytkownik Użytkownik { get; set; } = null!;

    public virtual Wydział Wydział { get; set; } = null!;
}
