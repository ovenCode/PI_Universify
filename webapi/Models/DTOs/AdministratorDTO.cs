namespace webapi.Models.DTOs
{
    public class AdministratorDTO
    {

        public RolaDTO? Rola { get; set; }

        public AdministratorDTO(Administrator? administrator) 
        {
            Rola = new RolaDTO(administrator?.Rola);
        }
    }
}
