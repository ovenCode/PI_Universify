using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Grupa
{
    public long IdGrupy { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Student> Studencis { get; set; } = new List<Student>();
}
