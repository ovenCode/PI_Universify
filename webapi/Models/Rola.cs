using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Rola
{
    public long IdRoli { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Uprawnienie> Uprawnienia { get; set; } = new List<Uprawnienie>();
    // public virtual ICollection<RolaUprawnienie> RoleUprawnienia { get; set; } = new List<RolaUprawnienie>();
    public virtual Użytkownik? Użytkownik { get; set; }
}
