namespace webapi.Models
{
    public class StołówkaDTO
    {
        public long IdStołówki { get; set; }

        public long IdBudynku { get; set; }

        public long IdZamówienia { get; set; }

        public long IdProduktu { get; set; }

        public string? InformacjeDodatkowe { get; set; }

        public virtual Budynek Budynek { get; set; } = null!;

        public virtual ICollection<Produkt> Produkty { get; set; } = new List<Produkt>();

        public virtual ICollection<Zamówienie> Zamówienia { get; set; } = new List<Zamówienie>();
    }
}
