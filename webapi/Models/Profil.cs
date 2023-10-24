using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Profil
{
    public long IdProfilu { get; set; }

    public long IdUżytkownika { get; set; }

    public string ObrazProfilu { get; set; } = null!;

    // INFORMACJE

    // PASEK PROFILU

    // GŁÓWNA ZAWARTOŚĆ

    public string PasekProfilu { get; set; } = null!;

    public string GłównaZawartość { get; set; } = null!;
    public virtual Użytkownik Użytkownik { get; set; } = null!;
}
