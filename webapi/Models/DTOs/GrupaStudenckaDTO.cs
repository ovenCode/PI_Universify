using webapi.Controllers;

namespace webapi.Models.DTOs
{
    public class GrupaStudenckaDTO
    {
        public string Nazwa { get; set; } = null!;
        public ICollection<StudentDTO>? Studenci { get; set; }

        public GrupaStudenckaDTO(GrupaStudencka? grupa)
        {
            this.Nazwa = grupa?.Nazwa ?? "";
        }
    }
}