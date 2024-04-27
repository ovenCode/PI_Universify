namespace webapi.Models.DTOs
{
    public class KierunekStudiówDTO
    {
        public long IdKierunku { get; set; }
        public String NazwaKierunku { get; set; } = null!;
        public ICollection<StudentDTO>? Studenci { get; set; }

        public KierunekStudiówDTO() { }

        public KierunekStudiówDTO(KierunekStudiów? kierunek)
        {
            IdKierunku = kierunek?.IdKierunkuStudiów ?? -1;
            NazwaKierunku = kierunek?.NazwaKierunku ?? "";
            Studenci = kierunek?.Studenci.Select(s => new StudentDTO
            {
                IdUżytkownika = s.IdUżytkownika,
                Imię = s.Imię,
                Nazwisko = s.Nazwisko,
                Grupa = s.Grupa,
                IdBudynku = s.IdBudynku,
                RokStudiów = s.RokStudiów,
                GrupaStudencka = new GrupaStudenckaDTO(s.GrupaStudencka),
                Grupy = s.Grupy == null ? null : s.Grupy.Select(g => g.Grupa == null ? null : new GrupaDTO(g.Grupa)).ToList(),
                Przedmioty = s.Przedmioty == null ? null : s.Przedmioty.Select(p => p.Przedmiot == null ? null : new PrzedmiotDTO(p.Przedmiot.Nazwa, p.Przedmiot.Kategoria, p.Przedmiot.SemestrRozpoczęcia, p.Przedmiot.IlośćSemestrów,
                s.Przedmioty, p.Przedmiot.Nauczyciele, null, null)).ToList()
            }).ToList();
        }
    }
}