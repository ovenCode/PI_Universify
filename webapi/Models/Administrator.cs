using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Administrator
{
    public long IdAdministratora { get; set; }

    public long IdUżytkownika { get; set; }

    public long? IdRoli { get; set; }

    public virtual Użytkownik IdUżytkownikaNavigation { get; set; } = null!;
}
