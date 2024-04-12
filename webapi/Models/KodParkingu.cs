namespace webapi.Models
{
    public class KodParkingu
    {
        public long IdKodu { get; set; }
        public long IdParkingu { get; set; }
        public long IdMiejsca { get; set; }
        public Guid Kod { get; set; }
        public Parking Parking { get; set; } = null!;
        public RozkładParkingu RozkładParkingu { get; set; } = null!;
    }
}
