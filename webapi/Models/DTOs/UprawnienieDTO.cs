namespace webapi.Models.DTOs
{
    public class UprawnienieDTO
    {
        public String Nazwa { get; set; }

        public UprawnienieDTO(Uprawnienie? u)
        {
            Nazwa = u?.Nazwa ?? "";
        }
    }
}