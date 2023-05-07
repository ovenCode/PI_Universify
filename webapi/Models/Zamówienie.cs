using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Zamówienie
{
    public long IdZamówienia { get; set; }

    public string? Nazwa { get; set; }

    public long IdUżytkownika { get; set; }

    public long IdDiety { get; set; }

    public long IdDania { get; set; }

    public byte[] DzieńZamówienia { get; set; } = null!;

    public virtual Danie IdDaniaNavigation { get; set; } = null!;

    public virtual Dieta IdDietyNavigation { get; set; } = null!;

    public virtual Użytkownik IdUżytkownikaNavigation { get; set; } = null!;

    public virtual ICollection<Stołówka> Stołówki { get; set; } = new List<Stołówka>();
}
