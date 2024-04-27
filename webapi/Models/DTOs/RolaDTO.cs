using webapi.Controllers;

namespace webapi.Models.DTOs
{
    public class RolaDTO
    {
        public long IdRoli { get; }
        public string Nazwa { get; set; }
        public ICollection<UprawnienieDTO>? Uprawnienia { get; set; } = new List<UprawnienieDTO>();

        public RolaDTO(Rola? rola)
        {
            IdRoli = rola?.IdRoli ?? -1;
            Nazwa = rola?.Nazwa ?? "";
            Uprawnienia = rola?.Uprawnienia.Select(u => new UprawnienieDTO(u)).ToList();
        }

    }
}