namespace webapi.Models
{
    public partial class KierunekStudiów
    {
        public long IdKierunkuStudiów { get; set; }

        public String NazwaKierunku { get; set; } = null!;

        public ICollection<Student> Studenci { get; set; } = new List<Student>();
    }
}
