namespace webapi.Models;

public class UżytkownikDTO
{
    public long IdUżytkownika { get; set; }

    public string Imię { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Hasło { get; set; } = null!;

    public string? Grupa { get; set; }

    public string? NumerTel { get; set; }

    public string? Budynek { get; set; }

    public virtual Administrator? Administrator { get; set; }

    public virtual Nauczyciel? Nauczyciel { get; set; }

    public virtual Student? Student { get; set; }

    public virtual ICollection<Zamówienie> Zamówienia { get; set; } = new List<Zamówienie>();

    public virtual Profil Profil { get; set; } = null!;
}