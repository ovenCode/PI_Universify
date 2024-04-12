namespace webapi.Models.DTOs
{
    public class RozkładParkinguDTO
    {
        public long? IdParkingu { get; set; }
        public long? StanMiejsca { get; set; }

        public RozkładParkinguDTO(RozkładParkingu? rozkład) 
        {
            IdParkingu = rozkład?.IdParkingu;
            StanMiejsca = rozkład?.StanMiejsca;
        }
    }
}
