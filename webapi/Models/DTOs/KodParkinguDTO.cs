namespace webapi.Models.DTOs
{
    public class KodParkinguDTO
    {
        public long IdParkingu { get; set; }
        public Guid Kod { get; set; }
        public ParkingDTO Parking { get; set; }
        public RozkładParkinguDTO RozkładParkingu { get; set; }

        public KodParkinguDTO(KodParkingu kod) 
        {
            IdParkingu = kod.IdParkingu;
            Kod = kod.Kod;
            Parking = new ParkingDTO(kod.Parking);
            RozkładParkingu = new RozkładParkinguDTO(kod.RozkładParkingu);
        }
    }
}
