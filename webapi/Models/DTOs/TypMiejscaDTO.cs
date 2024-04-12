namespace webapi.Models.DTOs
{
    public class TypMiejscaDTO
    {
        public string? Typ {  get; set; }
        //public ICollection<MiejsceDTO>? Miejsca { get; set; }

        public TypMiejscaDTO(TypMiejsca? typ)
        {
            Typ = typ?.Typ;
            //Miejsca = typ?.Miejsca.Select(m => new MiejsceDTO(m)).ToList(); CRASH
        }
    }
}