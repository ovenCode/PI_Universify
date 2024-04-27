namespace webapi.Models;

public class GrupaNauczyciel
{
    public long GrupyIdGrupy { get; set; }
    public long NauczycieleIdUzytkownika { get; set; }

    public Grupa Grupa { get; set; } = null!;
    public Nauczyciel Nauczyciel { get; set; } = null!;
}