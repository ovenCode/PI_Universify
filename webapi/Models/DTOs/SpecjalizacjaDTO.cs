namespace webapi.Models.DTOs
{
    public class SpecjalizacjaDTO
    {
        public long IdSpecjalizacji { get; set; }
        public string? Nazwa { get; set; }

        public SpecjalizacjaDTO(Specjalizacja? specjalizacja)
        {
            IdSpecjalizacji = specjalizacja?.IdSpecjalizacji ?? -1;
            Nazwa = specjalizacja?.Nazwa ?? "";
        }
    }
}