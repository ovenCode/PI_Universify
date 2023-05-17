namespace webapi.Models
{
    public class AdministratorDTO
    {
        public long IdAdministratora { get; set; }

        public long IdUżytkownika { get; set; }

        public long IdRoli { get; set; }

        public virtual Użytkownik Użytkownik { get; set; } = null!;
    }
}
