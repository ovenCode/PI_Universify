namespace webapi.Models.DTOs;

public abstract class UżytkownikDTO
{
    public long IdUżytkownika { get; set; }

    public string Imię { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string? Grupa { get; set; }

    public long? IdBudynku { get; set; }

    public virtual ICollection<ZamówienieDTO> Zamówienia { get; set; } = new List<ZamówienieDTO>();

    public virtual ProfilDTO Profil { get; set; } = null!;

    public virtual RolaDTO Rola { get; set; } = null!;

    public UżytkownikDTO() { }

    public UżytkownikDTO(Użytkownik użytkownik)
    {
        IdUżytkownika = użytkownik.IdUżytkownika;
        Imię = użytkownik.Imię;
        Nazwisko = użytkownik.Nazwisko;
        Grupa = użytkownik.Grupa;
        IdBudynku = użytkownik.IdBudynku;
        /*Administrator = użytkownik.Administrator == null ? null : new AdministratorDTO(użytkownik.Administrator);
        Nauczyciel = użytkownik.Nauczyciel == null ? null : new NauczycielDTO(użytkownik.Nauczyciel);
        Student = użytkownik.Student == null ? null : new StudentDTO(użytkownik.Student);*/
    }
}