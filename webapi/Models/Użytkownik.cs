using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Użytkownik
{
    public long IdUżytkownika { get; set; }

    public string Imię { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Hasło { get; set; } = null!;

    public string? Grupa { get; set; }

    public string? NumerTel { get; set; }

    public string? Budynek { get; set; }

    public virtual ICollection<Administrator> Administratorzy { get; set; } = new List<Administrator>();

    public virtual ICollection<Nauczyciel> Nauczyciele { get; set; } = new List<Nauczyciel>();

    public virtual ICollection<Student> Studenci { get; set; } = new List<Student>();

    public virtual ICollection<Zamówienie> Zamówienia { get; set; } = new List<Zamówienie>();
}
