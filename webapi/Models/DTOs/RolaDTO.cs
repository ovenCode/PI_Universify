using webapi.Controllers;

namespace webapi.Models.DTOs
{
    public class RolaDTO
    {
        public string Nazwa { get; set; }
        public ICollection<UprawnienieDTO>? Uprawnienia { get; set; } = new List<UprawnienieDTO>();

        public RolaDTO(Rola? rola)
        {
            Nazwa = rola?.Nazwa ?? "";
            Uprawnienia = rola?.Uprawnienia.Select(u => new UprawnienieDTO(u)).ToList();
        }

    }
}