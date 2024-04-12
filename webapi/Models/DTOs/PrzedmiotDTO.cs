namespace webapi.Models.DTOs
{
    public class PrzedmiotDTO
    {
        public String Nazwa { get; set; }
        public string Kategoria { get; set; }
        public long? SemestrRozpoczęcia { get; set; }
        public long? IlośćSemestrów { get; set; }
        public ICollection<StudentDTO>? Studenci { get; set; }
        public ICollection<NauczycielDTO>? Nauczyciele { get; set; }
        public ICollection<dynamic>? Resources { get; set; }

        public PrzedmiotDTO(Przedmiot? przedmiot)
        {
            Nazwa = przedmiot?.Nazwa ?? "";
            Kategoria = przedmiot?.Kategoria ?? "";
            SemestrRozpoczęcia = przedmiot?.SemestrRozpoczęcia;
            IlośćSemestrów = przedmiot?.IlośćSemestrów;
            Studenci = przedmiot?.Studenci.Select(s => new StudentDTO(s)).ToList();
            Nauczyciele = przedmiot?.Nauczyciele.Select(n => new NauczycielDTO(n)).ToList();
            Resources = new List<dynamic>
            {
                new Dictionary<String, dynamic> {
                    ["title"] = "Nagrania",
                    ["links"] = new List<String> {"Wideo 1", "Wideo 2", "Wideo 3" }
                },
                new Dictionary<String, dynamic> {
                    ["title"] = "Zajęcia",
                    ["links"] = new List<String> {"Zajęcia 1", "Zajęcia 2", "Zajęcia 3", "Zajęcia 1", "Zajęcia 2", "Zajęcia 3", "Zajęcia 1", "Zajęcia 2", "Zajęcia 3" }
                }
            };
        }
    }
}