using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Student
{
    public long IdStudenta { get; set; }

    public long IdUżytkownika { get; set; }

    public long IdPrzedmiotu { get; set; }

    public long IdGrupyStudenckiej { get; set; }

    public long IdKierunkuStudiów { get; set; }

    public long RokStudiów { get; set; }

    public virtual GrupaStudencka GrupaStudencka { get; set; } = null!;

    public virtual KierunekStudiów KierunekStudiów { get; set; } = null!;

    public virtual ICollection<Grupa> Grupy { get; set; } = new List<Grupa>();

    public virtual ICollection<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();

    public virtual Użytkownik Użytkownik { get; set; } = null!;
}
