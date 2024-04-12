using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Student
{
    // public Student(Student? student, GrupaStudencka? grupaStudencka, KierunekStudiów? kierunekStudiów)
    // {
    //     IdStudenta = student?.IdStudenta ?? Convert.ToInt64(0);
    //     IdUżytkownika = student?.IdUżytkownika ?? Convert.ToInt64(0);
    //     IdPrzedmiotu = student?.IdPrzedmiotu ?? Convert.ToInt64(0);
    //     IdGrupyStudenckiej = student?.IdGrupyStudenckiej ?? Convert.ToInt64(0);
    //     IdKierunkuStudiów = student?.IdKierunkuStudiów ?? Convert.ToInt64(0);
    //     RokStudiów = student?.RokStudiów ?? Convert.ToInt64(0);
    //     GrupaStudencka = grupaStudencka ?? new GrupaStudencka();
    //     KierunekStudiów = kierunekStudiów ?? new KierunekStudiów();
    // }
    public long IdStudenta { get; set; }

    public long IdUżytkownika { get; set; }

    public long IdGrupyStudenckiej { get; set; }

    public long IdKierunkuStudiów { get; set; }

    public long RokStudiów { get; set; }

    public virtual GrupaStudencka GrupaStudencka { get; set; } = null!;

    public virtual KierunekStudiów KierunekStudiów { get; set; } = null!;

    public virtual ICollection<Grupa> Grupy { get; set; } = new List<Grupa>();

    public virtual ICollection<Przedmiot> Przedmioty { get; set; } = new List<Przedmiot>();

    public virtual Użytkownik Użytkownik { get; set; } = null!;
}
