namespace webapi.Models.DTOs
{
    public class AdministratorDTO : UżytkownikDTO
    {

        // public RolaDTO? Rola { get; set; }

        public AdministratorDTO(Administrator? administrator)
        {
            IdUżytkownika = administrator?.IdUżytkownika ?? 0;
            Imię = administrator?.Imię ?? "";
            Nazwisko = administrator?.Nazwisko ?? "";
            Grupa = administrator?.Grupa ?? "";
            IdBudynku = administrator?.IdBudynku ?? -1;
            Rola = new RolaDTO(administrator?.Rola);
        }
    }
}
