namespace webapi.Models;

public class NauczycielPrzedmiot
{
    public long PrzedmiotyIdPrzedmiotu { get; set; }
    public long NauczycieleIdUżytkownika { get; set; }

    public Nauczyciel Nauczyciel { get; set; } = null!;
    public Przedmiot Przedmiot { get; set; } = null!;
}
