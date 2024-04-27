namespace webapi.Models.DTOs
{
    public class WydziałDTO
    {
        public WydziałDTO(Wydział? wydział)
        {
            IdWydziału = wydział?.IdWydziału ?? -1;
            Nazwa = wydział?.Nazwa ?? "";
        }

        public long IdWydziału { get; }
        public string Nazwa { get; set; } = null!;
    }
}