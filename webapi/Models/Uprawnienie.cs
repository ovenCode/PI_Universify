using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Uprawnienie
{
    public long IdUprawnienia { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual ICollection<Rola> Role { get; set; } = new List<Rola>();
    // public virtual ICollection<RolaUprawnienie> RoleUprawnienia { get; set; } = new List<RolaUprawnienie>();
}
