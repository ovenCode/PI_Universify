using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Zamówienie
{
    public long IdZamówienia { get; set; }

    public string? Nazwa { get; set; }

    public long IdUżytkownika { get; set; }

    public long IdStołówki { get; set; }

    public long IdDiety { get; set; }

    public long IdDania { get; set; }

    public string DzieńZamówienia { get; set; } = null!;

    public virtual Danie Danie { get; set; } = null!;

    public virtual Dieta Dieta { get; set; } = null!;

    public virtual Użytkownik Użytkownik { get; set; } = null!;

    public virtual Stołówka Stołówka { get; set; } = null!;
}
