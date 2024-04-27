using webapi.Controllers;

namespace webapi.Models.DTOs
{
    public class NauczycielDTO : UżytkownikDTO
    {
        public WydziałDTO? Wydział { get; set; }
        public SpecjalizacjaDTO? Specjalizacja { get; set; }
        public List<GrupaDTO?>? Grupy { get; set; }
        public List<PrzedmiotDTO?>? Przedmioty { get; set; }

        public NauczycielDTO() { }
        public NauczycielDTO(long? id, string? name, string? lname, string? group, long? build,
            Wydział wydział, Specjalizacja specjalizacja, ICollection<GrupaNauczyciel> grupy, ICollection<NauczycielPrzedmiot> przedmioty)
        {
            IdUżytkownika = id ?? -1;
            Imię = name ?? "";
            Nazwisko = lname ?? "";
            Grupa = group ?? "";
            IdBudynku = build ?? -1;
            Wydział = new WydziałDTO(wydział);
            Specjalizacja = new SpecjalizacjaDTO(specjalizacja);

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
                Przedmioty = przedmioty.Select(p => p.Przedmiot == null ? null : new PrzedmiotDTO
                {
                    Nazwa = p.Przedmiot.Nazwa,
                    Kategoria = p.Przedmiot.Kategoria,
                    SemestrRozpoczęcia = p.Przedmiot.SemestrRozpoczęcia,
                    IlośćSemestrów = p.Przedmiot.IlośćSemestrów,
                    Studenci = p.Przedmiot.Studenci.Select(s =>
                    {
                        if (s.Student == null)
                        {
                            return null;
                        }
                        else
                        {
                            return new StudentDTO
                            {
                                IdUżytkownika = s.Student.IdUżytkownika,
                                Imię = s.Student.Imię ?? "",
                                Nazwisko = s.Student.Nazwisko ?? "",
                                RokStudiów = s.Student.RokStudiów ?? -1,
                                KierunekStudiów = new KierunekStudiówDTO(s.Student.KierunekStudiów)
                            };
                        }
                    }).ToList(),
                }).ToList();
            }
        }

        public NauczycielDTO(Nauczyciel? nauczyciel)
        {
            IdUżytkownika = nauczyciel?.IdUżytkownika ?? 0;
            Imię = nauczyciel?.Imię ?? "";
            Nazwisko = nauczyciel?.Nazwisko ?? "";
            Grupa = nauczyciel?.Grupa ?? "";
            IdBudynku = nauczyciel?.IdBudynku ?? -1;
            Wydział = new WydziałDTO(nauczyciel?.Wydział);
            Specjalizacja = new SpecjalizacjaDTO(nauczyciel?.Specjalizacja);

            if (nauczyciel?.Grupy == null)
            {
                Grupy = null;
            }
            else
            {
                List<GrupaDTO?> _grupy = new List<GrupaDTO?>();

                foreach (var el in nauczyciel?.Grupy!)
                {
                    _grupy.Add(new GrupaDTO(el.Grupa));
                }

                Grupy = _grupy;
            }

            if (nauczyciel?.Przedmioty == null)
            {
                Przedmioty = null;
            }
            else
            {
                List<PrzedmiotDTO?> _przedmioty = new List<PrzedmiotDTO?>();

                foreach (var el in nauczyciel?.Przedmioty!)
                {
                    _przedmioty.Add(new PrzedmiotDTO
                    {
                        Nazwa = el.Przedmiot.Nazwa,
                        Kategoria = el.Przedmiot.Kategoria,
                        SemestrRozpoczęcia = el.Przedmiot.SemestrRozpoczęcia,
                        IlośćSemestrów = el.Przedmiot.IlośćSemestrów,
                        Studenci = el.Przedmiot.Studenci.Select(s =>
                        {
                            if (s.Student == null)
                            {
                                return null;
                            }
                            else
                            {
                                return new StudentDTO
                                {
                                    IdUżytkownika = s.Student.IdUżytkownika,
                                    Imię = s.Student.Imię ?? "",
                                    Nazwisko = s.Student.Nazwisko ?? "",
                                    RokStudiów = s.Student.RokStudiów ?? -1,
                                    KierunekStudiów = new KierunekStudiówDTO(s.Student.KierunekStudiów)
                                };
                            }
                        }).ToList(),
                    });
                }

                Przedmioty = _przedmioty;
            }
        }

    }
}
