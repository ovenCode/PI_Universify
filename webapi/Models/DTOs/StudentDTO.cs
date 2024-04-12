namespace webapi.Models.DTOs
{
    public class StudentDTO
    {
        public GrupaStudenckaDTO GrupaStudencka { get; set; }
        public KierunekStudiówDTO KierunekStudiów { get; set; }
        public long? RokStudiów { get; set; }
        public ICollection<GrupaDTO>? Grupy { get; set; } = new List<GrupaDTO>();
        public ICollection<PrzedmiotDTO>? Przedmioty { get; set; } = new List<PrzedmiotDTO>();

        public StudentDTO(Student? student)
        {
            RokStudiów = student?.RokStudiów;
            GrupaStudencka = new GrupaStudenckaDTO(student?.GrupaStudencka.Nazwa);
            KierunekStudiów = new KierunekStudiówDTO(student?.KierunekStudiów);
            Grupy = student?.Grupy.Select(g => new GrupaDTO(g)).ToList();
            Przedmioty = student?.Przedmioty.Select(p => new PrzedmiotDTO(p)).ToList();
        }
    }
}
