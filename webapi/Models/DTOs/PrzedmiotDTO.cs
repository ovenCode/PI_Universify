namespace webapi.Models.DTOs
{
    public class PrzedmiotDTO
    {
        public String Nazwa { get; set; } = null!;
        public string Kategoria { get; set; } = null!;
        public long? SemestrRozpoczęcia { get; set; }
        public long? IlośćSemestrów { get; set; }
        public ICollection<StudentDTO?>? Studenci { get; set; }
        public ICollection<NauczycielDTO?>? Nauczyciele { get; set; }
        public ICollection<dynamic>? Zasoby { get; set; }
        public ICollection<dynamic>? Lekcje { get; set; }

        public PrzedmiotDTO() { }
        public PrzedmiotDTO(string? name, string? category, long? semester, long? remaining,
        ICollection<PrzedmiotStudent>? studenci, ICollection<NauczycielPrzedmiot>? nauczyciele, ICollection<dynamic>? zasoby, ICollection<dynamic>? lekcje)
        {
            Nazwa = name ?? "";
            Kategoria = category ?? "";
            SemestrRozpoczęcia = semester ?? -1;
            IlośćSemestrów = remaining ?? -1;
            if (studenci == null)
            {
                Studenci = new List<StudentDTO?>();
            }
            else
            {
                Studenci = studenci.Select(s =>
                {
                    if (s.Student == null)
                    {
                        return null;
                    }
                    else
                    {
                        return new StudentDTO(s.Student.IdUżytkownika, s.Student.Imię, s.Student.Nazwisko, s.Student.Grupa, s.Student.IdBudynku, s.Student.RokStudiów,
                        s.Student.GrupaStudencka, s.Student.KierunekStudiów, s.Student.Grupy, s.Student.Przedmioty);
                    }
                }).ToList();
            }
        }
        public PrzedmiotDTO(Przedmiot? przedmiot)
        {
            Nazwa = przedmiot?.Nazwa ?? "";
            Kategoria = przedmiot?.Kategoria ?? "";
            SemestrRozpoczęcia = przedmiot?.SemestrRozpoczęcia;
            IlośćSemestrów = przedmiot?.IlośćSemestrów;
            //Studenci = przedmiot?.Studenci.Select(s => new StudentDTO(s)).ToList();
            //Nauczyciele = przedmiot?.Nauczyciele.Select(n => new NauczycielDTO(n)).ToList();
            Zasoby = new List<dynamic>
            {
                new Dictionary<String, dynamic> {
                    ["title"] = "Nagrania",
                    ["links"] = new List<String> {"Wideo 1", "Wideo 2", "Wideo 3" }
                },
                new Dictionary<String, dynamic> {
                    ["title"] = "Zajęcia",
                    ["links"] = new List<String> {"Zajęcia 1", "Zajęcia 2", "Zajęcia 3", "Zajęcia 1", "Zajęcia 2", "Zajęcia 3", "Zajęcia 1", "Zajęcia 2", "Zajęcia 3" }
                },
                new Dictionary<String, dynamic> {
                    ["title"] = "Źródła",
                    ["links"] = new List<String> {"Źródlo 1", "Źródlo 2", "Źródlo 3", "Źródlo 1", "Źródlo 2", "Źródlo 3", "Źródlo 1", "Źródlo 2", "Źródlo 3","Źródlo 1", "Źródlo 2", "Źródlo 3" }
                }
            };
            Lekcje = new List<dynamic>
            {
                new Dictionary<String, dynamic>
                {
                    ["tytul"] = "Tytul lekcji",
                    ["opis"] = "Krotki opis tematyki",
                    ["źródla"] = new List<Dictionary<String, String>> { new Dictionary<string, string> {["tytul"] = "Zrodlo 1"}, new Dictionary<string, string> {["tytul"] = "Zrodlo 2"}},
                    ["porady"] = "Te tematy powinny być znane, aby dobrze zrozumieć zajęcia: -a -b -c"
                },
                new Dictionary<String, dynamic>
                {
                    ["tytul"] = "Tytul lekcji",
                    ["opis"] = "Krotki opis tematyki",
                    ["źródla"] = new List<Dictionary<String, String>> { new Dictionary<string, string> {["tytul"] = "Zrodlo 1"}, new Dictionary<string, string> {["tytul"] = "Zrodlo 2"}},
                    ["porady"] = "Te tematy powinny być znane, aby dobrze zrozumieć zajęcia: -a -b -c"
                },
                new Dictionary<String, dynamic>
                {
                    ["tytul"] = "Tytul lekcji",
                    ["opis"] = "Krotki opis tematyki",
                    ["źródla"] = new List<Dictionary<String, String>> { new Dictionary<string, string> {["tytul"] = "Zrodlo 1", ["adres"] = ""}, new Dictionary<string, string> {["tytul"] = "Zrodlo 2", ["adres"] = "" } },
                    ["porady"] = "Te tematy powinny być znane, aby dobrze zrozumieć zajęcia: -a -b -c"
                },
                new Dictionary<String, dynamic>
                {
                    ["tytul"] = "Tytul lekcji",
                    ["opis"] = "Krotki opis tematyki",
                    ["źródla"] = new List<Dictionary<String, String>> { new Dictionary<string, string> {["tytul"] = "Zrodlo 1"}, new Dictionary<string, string> {["tytul"] = "Zrodlo 2"}},
                    ["porady"] = "Te tematy powinny być znane, aby dobrze zrozumieć zajęcia: -a -b -c"
                }
            };
            //Prowa
        }
    }
}