namespace webapi.Models
{
    public class AdministratorDTO
    {
        public long IdAdministratora { get; set; }

        public long IdUżytkownika { get; set; }

        public long? IdRoli { get; set; }

        public virtual Użytkownik IdUżytkownikaNavigation { get; set; } = null!;
    }
}
