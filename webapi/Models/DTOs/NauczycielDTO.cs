using webapi.Controllers;

namespace webapi.Models.DTOs
{
    public class NauczycielDTO
    {
        public WydziałDTO Wydział { get; private set; }
        public SpecjalizacjaDTO Specjalizacja { get; private set; }
        public List<GrupaDTO>? Grupy { get; private set; }
        public List<PrzedmiotDTO>? Przedmioty { get; private set; }

        //
        public NauczycielDTO(Nauczyciel? nauczyciel) 
        {
            Wydział = new WydziałDTO(nauczyciel?.Wydział);
            Specjalizacja = new SpecjalizacjaDTO(nauczyciel?.Specjalizacja);
            Grupy = nauczyciel?.Grupy.Select(g => new GrupaDTO(g)).ToList();
            Przedmioty = nauczyciel?.Przedmioty.Select(p => new PrzedmiotDTO(p)).ToList();
        }

    }
}
