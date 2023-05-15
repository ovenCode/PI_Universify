using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Składnik
{
    public long IdSkładnika { get; set; }

    public long IdDania { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual Danie Danie { get; set; } = null!;
}
