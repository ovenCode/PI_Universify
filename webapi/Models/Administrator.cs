using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Administrator
{
    public long IdAdministratora { get; set; }

    public long IdUżytkownika { get; set; }

    public long IdRoli { get; set; }

    public virtual Użytkownik Użytkownik { get; set; } = null!;
    public virtual Rola Rola { get; set; } = null!;
}
