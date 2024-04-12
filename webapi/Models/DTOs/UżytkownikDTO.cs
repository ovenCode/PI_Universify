namespace webapi.Models.DTOs;

public class UżytkownikDTO
{
    public long IdUżytkownika { get; set; }

    public string Imię { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string? Grupa { get; set; }

    public string? Budynek { get; set; }

    public virtual AdministratorDTO? Administrator { get; set; }

    public virtual NauczycielDTO? Nauczyciel { get; set; }

    public virtual StudentDTO? Student { get; set; }

    public virtual ICollection<ZamówienieDTO> Zamówienia { get; set; } = new List<ZamówienieDTO>();

    public virtual ProfilDTO Profil { get; set; } = null!;
}