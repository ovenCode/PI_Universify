using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Grupa
{
    public long IdGrupy { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Student> Studenci { get; set; } = new List<Student>();

    public virtual ICollection<Nauczyciel> Nauczyciele { get; set; } = new List<Nauczyciel>();
}
