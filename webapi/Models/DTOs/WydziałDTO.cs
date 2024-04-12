namespace webapi.Models.DTOs
{
    public class WydziałDTO
    {
        public WydziałDTO(Wydział? wydział)
        {
            Wydział = wydział;
        }

        public Wydział? Wydział { get; }
    }
}