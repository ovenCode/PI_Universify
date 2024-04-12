namespace webapi.Models.DTOs
{
    public class MiejsceDTO
    {
        public long? Id { get; set; }
        public bool? Dostępność {  get; set; }
        public TypMiejscaDTO? Typ {  get; set; }
        //public ParkingDTO? Parking { get; set; }
        
        public MiejsceDTO(Miejsce? miejsce) 
        {
            Id = miejsce?.IdMiejsca;
            Dostępność = miejsce?.Dostępność;
            Typ = new TypMiejscaDTO(miejsce?.Typ);
            //Parking = new ParkingDTO(miejsce?.Parking ?? null); CRASH
        }
    }
}