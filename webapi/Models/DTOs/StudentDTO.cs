namespace webapi.Models.DTOs
{
    public class StudentDTO : UżytkownikDTO
    {
        public GrupaStudenckaDTO? GrupaStudencka { get; set; }
        public KierunekStudiówDTO? KierunekStudiów { get; set; }
        public long? RokStudiów { get; set; }
        public ICollection<GrupaDTO?>? Grupy { get; set; } = new List<GrupaDTO?>();
        public ICollection<PrzedmiotDTO?>? Przedmioty { get; set; } = new List<PrzedmiotDTO?>();

        public StudentDTO() { }
        public StudentDTO(long? id, string? name, string? lname, string? group, long? build, long? year,
        GrupaStudencka? grupaStudencka, KierunekStudiów? kierunekStudiów, ICollection<GrupaStudent> grupy, ICollection<PrzedmiotStudent> przedmioty)
        {
            IdUżytkownika = id ?? -1;
            Imię = name ?? "";
            Nazwisko = lname ?? "";
            Grupa = group ?? "";
            IdBudynku = build ?? -1;
            RokStudiów = year ?? -1;
            GrupaStudencka = new GrupaStudenckaDTO(grupaStudencka);
            KierunekStudiów = new KierunekStudiówDTO(kierunekStudiów);
            if (grupy == null)
            {
                Grupy = null;
            }
            else
            {
                Grupy = grupy.Select(g => g.Grupa == null ? null : new GrupaDTO(g.Grupa)).ToList();
            }
            if (przedmioty == null)
            {
                Przedmioty = null;
            }
            else
            {
                Przedmioty = przedmioty.Select(p => p.Przedmiot == null ? null : new PrzedmiotDTO(p.Przedmiot.Nazwa, p.Przedmiot.Kategoria, p.Przedmiot.SemestrRozpoczęcia, p.Przedmiot.IlośćSemestrów,
                przedmioty, p.Przedmiot.Nauczyciele, null, null)).ToList();
            }
        }
        public StudentDTO(Student? student)
        {
            IdUżytkownika = student?.IdUżytkownika ?? -1;
            Imię = student?.Imię ?? "";
            Nazwisko = student?.Nazwisko ?? "";
            Grupa = student?.Grupa ?? "";
            IdBudynku = student?.IdBudynku ?? -1;
            RokStudiów = student?.RokStudiów;
            GrupaStudencka = new GrupaStudenckaDTO(student?.GrupaStudencka);
            KierunekStudiów = new KierunekStudiówDTO(student?.KierunekStudiów);
            //Grupy = student?.Grupy.Select(g => new GrupaDTO(g)).ToList();
            Przedmioty = student?.Przedmioty.Select(p => p.Przedmiot == null ? null : new PrzedmiotDTO(p.Przedmiot)).ToList();
        }
    }
}
