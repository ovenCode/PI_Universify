using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Alergen
{
    public long IdAlergenu { get; set; }

    public long IdDiety { get; set; }

    public string Nazwa { get; set; } = null!;

    public virtual Dieta Dieta { get; set; } = null!;
}
