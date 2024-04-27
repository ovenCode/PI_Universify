using System;
using System.Collections.Generic;

namespace webapi.Models;

public abstract class Użytkownik
{
    public long IdUżytkownika { get; set; }

    public string Imię { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Hasło { get; set; } = null!;

    public string? Grupa { get; set; }

    public string? NumerTel { get; set; }

    public long? IdBudynku { get; set; }

    //public string TypUżytkownika { get; set; } = null!;

    // public virtual Administrator? Administrator { get; set; }

    // public virtual Nauczyciel? Nauczyciel { get; set; }

    // public virtual Student? Student { get; set; }

    public long? IdAdministratora { get; set; }
    public long? IdRoli { get; set; }

    public long? IdNauczyciela { get; set; }
    public long? IdWydziału { get; set; }
    public long? IdSpecjalizacji { get; set; }

    public long? IdStudenta { get; set; }
    public long? IdGrupyStudenckiej { get; set; }
    public long? IdKierunkuStudiów { get; set; }
    public long? RokStudiów { get; set; }

    public virtual ICollection<Zamówienie> Zamówienia { get; set; } = new List<Zamówienie>();

    public virtual Profil? Profil { get; set; }

    public virtual Rola Rola { get; set; } = null!;
}
