namespace webapi.Models;

public class PrzedmiotStudent
{
    public long PrzedmiotyIdPrzedmiotu { get; set; }
    public long StudenciIdUzytkownika { get; set; }

    public Przedmiot Przedmiot { get; set; } = null!;
    public Student Student { get; set; } = null!;
}
