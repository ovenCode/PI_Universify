namespace webapi.Models.DTOs
{
    public class SpecjalizacjaDTO
    {
        public string? Nazwa { get; set; }

        public SpecjalizacjaDTO(Specjalizacja? specjalizacja)
        {
            Nazwa = specjalizacja?.Nazwa ?? "";
        }
    }
}