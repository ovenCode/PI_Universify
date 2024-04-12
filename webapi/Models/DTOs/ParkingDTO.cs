namespace webapi.Models.DTOs
{
    public class ParkingDTO
    {
        public string? Adres { get; set; }
        public long? LiczbaRzedow { get; set; }
        public ICollection<MiejsceDTO>? Miejsca { get; set; }
        public ICollection<RozkładParkinguDTO>? Rozkład { get; set; }
        public ICollection<KodParkinguDTO>? KodyParkingu { get; set; }

        public ParkingDTO(Parking? parking) 
        {
            Adres = parking?.Adres;
            Miejsca = parking?.Miejsca.Select(m => new MiejsceDTO(m)).ToList() ?? new List<MiejsceDTO>();
            Rozkład = parking?.RozkładParkingu?.OrderBy(r => r.Id).Select(r => new RozkładParkinguDTO(r)).ToList() ?? new List<RozkładParkinguDTO>();
            KodyParkingu = parking?.KodyParkingu.OrderBy(k => k.IdKodu).Select(k => new KodParkinguDTO(k)).ToList();
            LiczbaRzedow = parking?.LiczbaRzedow;
        }
    }
}
