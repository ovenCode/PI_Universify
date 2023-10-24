namespace webapi.Models;


public class ProfilDTO
{
    public long IdProfilu { get; set; }

    public long IdUżytkownika { get; set; }

    public string ObrazProfilu { get; set; } = null!;

    // INFORMACJE

    // PASEK PROFILU

    // GŁÓWNA ZAWARTOŚĆ

    public string PasekProfilu { get; set; } = null!;

    public string GłównaZawartość { get; set; } = null!;
    public virtual UżytkownikDTO Użytkownik { get; set; } = null!;
}