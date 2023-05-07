using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Student
{
    public long IdStudenta { get; set; }

    public long IdUżytkownika { get; set; }

    public long IdPrzedmiotu { get; set; }

    public long IdGrupyStudenckiej { get; set; }

    public long RokStudiów { get; set; }

    public virtual Grupa IdGrupyStudenckiejNavigation { get; set; } = null!;

    public virtual Przedmiot IdPrzedmiotuNavigation { get; set; } = null!;

    public virtual Użytkownik IdUżytkownikaNavigation { get; set; } = null!;
}
