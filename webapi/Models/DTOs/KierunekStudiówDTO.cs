namespace webapi.Models.DTOs
{
    public class KierunekStudiówDTO
    {
        public String NazwaKierunku { get; set; } = null!;
        public ICollection<StudentDTO>? Studenci { get; set; } 

        public KierunekStudiówDTO(KierunekStudiów? kierunek)
        {
            NazwaKierunku = kierunek?.NazwaKierunku ?? "";
            Studenci = kierunek?.Studenci.Select(s => new StudentDTO(s)).ToList();
        }
    }
}