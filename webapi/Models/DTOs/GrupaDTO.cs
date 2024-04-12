namespace webapi.Models.DTOs
{
    public class GrupaDTO
    {
        public String? Nazwa { get; private set; }
        
        public GrupaDTO(Grupa? grupa) 
        {
            Nazwa = grupa?.Nazwa ?? "";
        }
    }
}