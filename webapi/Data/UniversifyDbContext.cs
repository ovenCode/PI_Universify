using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data;

public partial class UniversifyDbContext : DbContext
{
    public UniversifyDbContext()
    {
    }

    public UniversifyDbContext(DbContextOptions<UniversifyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administratorzy { get; set; } = null!;

    public virtual DbSet<Alergen> Alergeny { get; set; } = null!;

    public virtual DbSet<Budynek> Budynki { get; set; } = null!;

    public virtual DbSet<Danie> Dania { get; set; } = null!;

    public virtual DbSet<Dieta> Diety { get; set; } = null!;

    public virtual DbSet<Grupa> Grupy { get; set; } = null!;

    public virtual DbSet<GrupaStudencka> GrupyStudenckie { get; set; } = null!;

    public virtual DbSet<Kategoria> Kategorie { get; set; } = null!;

    public virtual DbSet<KierunekStudiów> KierunekiStudiów { get; set; } = null!;

    public virtual DbSet<KodParkingu> KodyParkingu { get; set; } = null!;

    public virtual DbSet<Miejsce> Miejsca { get; set; } = null!;

    public virtual DbSet<Nauczyciel> Nauczyciele { get; set; } = null!;

    public virtual DbSet<Parking> Parkingi { get; set; } = null!;

    public virtual DbSet<Produkt> Produkty { get; set; } = null!;

    public virtual DbSet<Profil> Profile { get; set; } = null!;

    public virtual DbSet<Przedmiot> Przedmioty { get; set; } = null!;

    public virtual DbSet<Rola> Role { get; set; } = null!;

    public virtual DbSet<RozkładParkingu> RozkladParkingu { get; set; } = null!;

    public virtual DbSet<Składnik> Składniki { get; set; } = null!;

    public virtual DbSet<Specjalizacja> Specjalizacje { get; set; } = null!;

    public virtual DbSet<Stołówka> Stołówki { get; set; } = null!;

    public virtual DbSet<Student> Studenci { get; set; } = null!;

    public virtual DbSet<TypMiejsca> TypyMiejsc { get; set; } = null!;

    public virtual DbSet<Uprawnienie> Uprawnienia { get; set; } = null!;

    public virtual DbSet<Użytkownik> Użytkownicy { get; set; } = null!;

    public virtual DbSet<Wydział> Wydziały { get; set; } = null!;

    public virtual DbSet<Zamówienie> Zamówienia { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(@"DataSource=.\\Data\\UniversifyDB.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.IdAdministratora);

            entity.ToTable("Administratorzy");

            entity.Property(e => e.IdAdministratora).HasColumnName("ID_ADMINISTRATORA");
            entity.Property(e => e.IdRoli)
                .HasColumnType("INT")
                .HasColumnName("ID_ROLI");
            entity.Property(e => e.IdUżytkownika)
                .HasColumnType("INT")
                .HasColumnName("ID_UZYTKOWNIKA");

            entity.HasOne(d => d.Użytkownik).WithOne(p => p.Administrator)
                .HasForeignKey<Administrator>(d => d.IdUżytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
            entity.HasOne(d => d.Rola).WithOne(p => p.Administrator)
                .HasForeignKey<Administrator>(d => d.IdRoli)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Alergen>(entity =>
        {
            entity.HasKey(e => e.IdAlergenu);

            entity.ToTable("Alergeny");

            entity.Property(e => e.IdAlergenu).HasColumnName("ID_ALERGENU");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(50)")
                .HasColumnName("NAZWA");

            entity.HasOne(e => e.Dieta).WithMany(p => p.Alergeny)
            .HasForeignKey(e => e.IdDiety)
            .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Budynek>(entity =>
        {
            entity.HasKey(e => e.IdBudynku);

            entity.ToTable("Budynki");

            entity.Property(e => e.IdBudynku).HasColumnName("ID_BUDYNKU");
            entity.Property(e => e.Adres)
                .HasColumnType("NCHAR(40)")
                .HasColumnName("ADRES");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(40)")
                .HasColumnName("NAZWA");
        });

        modelBuilder.Entity<Danie>(entity =>
        {
            entity.HasKey(e => e.IdDania);

            entity.ToTable("Dania");

            entity.Property(e => e.IdDania).HasColumnName("ID_DANIA");
            entity.Property(e => e.IdSkładnika).HasColumnName("ID_SKLADNIKA");
            entity.Property(e => e.IdDiety).HasColumnName("ID_DIETY");
            entity.Property(e => e.IlośćKalorii).HasColumnName("ILOSC_KALORII");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(50)")
                .HasColumnName("NAZWA");

            entity.HasOne(d => d.Dieta).WithMany(p => p.Dania)
            .HasForeignKey(d => d.IdDiety)
            .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Dieta>(entity =>
        {
            entity.HasKey(e => e.IdDiety);

            entity.ToTable("Diety");

            entity.Property(e => e.IdDiety).HasColumnName("ID_DIETY");
            entity.Property(e => e.Cena).HasColumnName("CENA");
            entity.Property(e => e.IdAlergenu).HasColumnName("ID_ALERGENU");
            entity.Property(e => e.IdDania).HasColumnName("ID_DANIA");
            entity.Property(e => e.IdKategorii).HasColumnName("ID_KATEGORII");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(70)")
                .HasColumnName("NAZWA");

            entity.HasOne(d => d.Kategoria).WithMany(p => p.Diety)
                .HasForeignKey(d => d.IdKategorii)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Grupa>(entity =>
        {
            entity.HasKey(e => e.IdGrupy);

            entity.ToTable("Grupy");

            entity.Property(e => e.IdGrupy).HasColumnName("ID_GRUPY");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(50)")
                .HasColumnName("NAZWA");

            entity.HasMany(d => d.Nauczyciele).WithMany(p => p.Grupy);

            entity.HasMany(d => d.Studenci).WithMany(p => p.Grupy);
        });

        modelBuilder.Entity<GrupaStudencka>(entity =>
        {
            entity.HasKey(e => e.IdGrupy);

            entity.ToTable("GrupyStudenckie");

            entity.Property(e => e.IdGrupy).HasColumnName("ID_GRUPY");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(60)")
                .HasColumnName("NAZWA");
        });

        modelBuilder.Entity<Kategoria>(entity =>
        {
            entity.HasKey(e => e.IdKategorii);

            entity.ToTable("Kategorie");

            entity.Property(e => e.IdKategorii).HasColumnName("ID_KATEGORII");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(60)")
                .HasColumnName("NAZWA");
        });

        modelBuilder.Entity<KierunekStudiów>(entity =>
        {
            entity.HasKey(e => e.IdKierunkuStudiów);

            entity.ToTable("KierunkiStudiów");

            entity.Property(e => e.IdKierunkuStudiów).HasColumnName("ID_KIERUNKU_STUDIOW");
            entity.Property(e => e.NazwaKierunku)
                .HasColumnType("NCHAR(80)")
                .HasColumnName("Nazwa_Kierunku");
        });

        modelBuilder.Entity<KodParkingu>(entity =>
        {
            entity.HasKey(e => e.IdKodu);

            entity.ToTable("KodyParkingu");

            entity.Property(e => e.IdKodu).HasColumnName("ID_KODU").IsRequired();
            entity.Property(e => e.IdParkingu).HasColumnName("ID_PARKINGU").IsRequired();
            entity.Property(e => e.IdMiejsca).HasColumnName("ID_MIEJSCA").IsRequired();
            entity.Property(e => e.Kod)
            .HasColumnType("BLOB")
            .HasColumnName("KOD")
            .IsRequired();

            entity.HasOne(e => e.RozkładParkingu).WithMany(r => r.KodyParkingu).HasForeignKey(e => e.IdMiejsca)
            .OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            entity.HasOne(e => e.Parking).WithMany(p => p.KodyParkingu).HasForeignKey(e => e.IdParkingu)
            .OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        });

        modelBuilder.Entity<Miejsce>(entity =>
        {
            entity.HasKey(e => e.IdMiejsca);

            entity.ToTable("Miejsca");

            entity.Property(e => e.IdMiejsca).HasColumnName("ID_MIEJSCA");
            entity.Property(e => e.Dostępność)
                .HasColumnType("INTEGER")
                .HasColumnName("DOSTEPNOSC");
            entity.Property(e => e.IdTypu)
                .HasColumnType("INT")
                .HasColumnName("ID_TYPU");

            entity.HasOne(d => d.Typ).WithMany(p => p.Miejsca)
                .HasForeignKey(d => d.IdTypu)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();

            entity.HasOne(d => d.Parking).WithMany(p => p.Miejsca)
                .HasForeignKey(d => d.IdParkingu)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Nauczyciel>(entity =>
        {
            entity.HasKey(e => e.IdNauczyciela);

            entity.ToTable("Nauczyciele");

            entity.Property(e => e.IdNauczyciela).HasColumnName("ID_NAUCZYCIELA");
            entity.Property(e => e.IdSpecjalizacji).HasColumnName("ID_SPECJALIZACJI");
            entity.Property(e => e.IdUżytkownika).HasColumnName("ID_UZYTKOWNIKA");
            entity.Property(e => e.IdWydziału).HasColumnName("ID_WYDZIALU");
            entity.Property(e => e.IdPrzedmiotu).HasColumnName("ID_PRZEDMIOTU");

            entity.HasMany(d => d.Przedmioty).WithMany(p => p.Nauczyciele);

            entity.HasMany(d => d.Grupy).WithMany(p => p.Nauczyciele);

            entity.HasOne(d => d.Specjalizacja).WithMany(p => p.Nauczyciele)
                .HasForeignKey(d => d.IdSpecjalizacji)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();

            entity.HasOne(d => d.Użytkownik).WithOne(p => p.Nauczyciel)
                .HasForeignKey<Nauczyciel>(d => d.IdUżytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();

            entity.HasOne(d => d.Wydział).WithMany(p => p.Nauczyciele)
                .HasForeignKey(d => d.IdWydziału)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Parking>(entity =>
        {
            entity.HasKey(e => e.IdParkingu);

            entity.ToTable("Parkingi");

            entity.Property(e => e.IdParkingu).HasColumnName("ID_PARKINGU");
            entity.Property(e => e.Adres)
                .HasColumnType("NCHAR(60)")
                .HasColumnName("ADRES");
            entity.Property(e => e.LiczbaRzedow)
                .HasColumnType("INTEGER").HasColumnName("LICZBA_RZEDOW");

            entity.HasMany(e => e.Miejsca).WithOne(m => m.Parking)
            .HasForeignKey(e => e.IdMiejsca);

            entity.HasMany(e => e.RozkładParkingu).WithOne(r => r.Parking).HasForeignKey(r => r.IdParkingu)
            .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e => e.KodyParkingu).WithOne(k => k.Parking).HasForeignKey(k => k.IdParkingu)
            .OnDelete(DeleteBehavior.Cascade);
            
        });

        modelBuilder.Entity<Produkt>(entity =>
        {
            entity.HasKey(e => e.IdProduktu);

            entity.ToTable("Produkty");

            entity.Property(e => e.IdProduktu).HasColumnName("ID_PRODUKTU");
            entity.Property(e => e.IlośćProduktu).HasColumnName("ILOŚĆ_PRODUKTU");
            entity.Property(e => e.Jednostka)
                .HasColumnType("NCHAR(10)")
                .HasColumnName("JEDNOSTKA");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(70)")
                .HasColumnName("NAZWA");

            entity.HasOne(e => e.Stołówka).WithMany(p => p.Produkty)
                .HasForeignKey(d => d.IdStołówki)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Profil>(entity =>
        {
            entity.HasKey(e => e.IdProfilu);

            entity.ToTable("Profile");

            entity.Property(e => e.IdProfilu).HasColumnName("ID_PROFILU");
            entity.Property(e => e.IdUżytkownika)
                .HasColumnType("INT")
                .HasColumnName("ID_UZYTKOWNIKA");
            entity.Property(e => e.ObrazProfilu)
                .HasColumnType("NCHAR(60)")
                .HasColumnName("OBRAZ_PROFILU");
            entity.Property(e => e.PasekProfilu)
                .HasColumnType("NCHAR(150)")
                .HasColumnName("PASEK_PROFILU");
            entity.Property(e => e.GłównaZawartość)
                .HasColumnType("NVARCHAR(1500)")
                .HasColumnName("GLOWNA_ZAWARTOSC");

            entity.HasOne(d => d.Użytkownik).WithOne(p => p.Profil)
                .HasForeignKey<Profil>(d => d.IdUżytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Przedmiot>(entity =>
        {
            entity.HasKey(e => e.IdPrzedmiotu);

            entity.ToTable("Przedmioty");

            entity.Property(e => e.IdPrzedmiotu).HasColumnName("ID_PRZEDMIOTU");
            entity.Property(e => e.IlośćSemestrów)
                .HasColumnType("INT")
                .HasColumnName("ILOSC_SEMESTROW");
            entity.Property(e => e.Kategoria)
                .HasColumnType("NCHAR(60)")
                .HasColumnName("KATEGORIA");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(120)")
                .HasColumnName("NAZWA");
            entity.Property(e => e.SemestrRozpoczęcia)
                .HasColumnType("INT")
                .HasColumnName("SEMESTR_ROZPOCZECIA");

            entity.HasMany(d => d.Nauczyciele).WithMany(p => p.Przedmioty);

            entity.HasMany(d => d.Studenci).WithMany(p => p.Przedmioty);
        });

        modelBuilder.Entity<Rola>(entity =>
        {
            entity.HasKey(e => e.IdRoli);

            entity.ToTable("Role");

            entity.Property(e => e.IdRoli).HasColumnName("ID_ROLI");
            entity.Property(e => e.IdUprawnienia).HasColumnName("ID_UPRAWNIENIA");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(50)")
                .HasColumnName("NAZWA");
        });

        modelBuilder.Entity<RozkładParkingu>(entity =>
        {
            entity.ToTable("RozkladParkingu");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdParkingu).HasColumnType("INTEGER")
            .HasColumnName("ID_PARKINGU");
            entity.Property(e => e.StanMiejsca).HasColumnType("INTEGER")
            .HasColumnName("STAN_MIEJSCA");

            entity.HasOne(e => e.Parking).WithMany(p => p.RozkładParkingu).HasForeignKey(e => e.IdParkingu);
            entity.HasMany(e => e.KodyParkingu).WithOne(k => k.RozkładParkingu)
            .HasForeignKey(e => e.IdMiejsca).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Składnik>(entity =>
        {
            entity.HasKey(e => e.IdSkładnika);

            entity.ToTable("Skladniki");

            entity.Property(e => e.IdSkładnika).HasColumnName("ID_SKLADNIKA");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(50)")
                .HasColumnName("NAZWA");

            entity.HasOne(e => e.Danie).WithMany(p => p.Składniki)
                .HasForeignKey(d => d.IdDania)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Specjalizacja>(entity =>
        {
            entity.HasKey(e => e.IdSpecjalizacji);

            entity.ToTable("Specjalizacje");

            entity.Property(e => e.IdSpecjalizacji).HasColumnName("ID_SPECJALIZACJI");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(80)")
                .HasColumnName("NAZWA");
        });

        modelBuilder.Entity<Stołówka>(entity =>
        {
            entity.HasKey(e => e.IdStołówki);

            entity.ToTable("Stolówki");

            entity.Property(e => e.IdStołówki)
                .ValueGeneratedNever()
                .HasColumnName("ID_STOLOWKI");
            entity.Property(e => e.IdBudynku).HasColumnName("ID_BUDYNKU");
            entity.Property(e => e.IdProduktu).HasColumnName("ID_PRODUKTU");
            entity.Property(e => e.IdZamówienia).HasColumnName("ID_ZAMOWIENIA");
            entity.Property(e => e.InformacjeDodatkowe)
                .HasColumnType("NCHAR(1000)")
                .HasColumnName("INFORMACJE_DODATKOWE");

            entity.HasOne(d => d.Budynek).WithMany(p => p.Stołówki)
                .HasForeignKey(d => d.IdBudynku)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.IdStudenta);

            entity.ToTable("Studenci");

            entity.Property(e => e.IdStudenta).HasColumnName("ID_STUDENTA");
            entity.Property(e => e.IdGrupyStudenckiej).HasColumnName("ID_GRUPY_STUDENCKIEJ");
            entity.Property(e => e.IdUżytkownika).HasColumnName("ID_UZYTKOWNIKA");
            entity.Property(e => e.IdKierunkuStudiów).HasColumnName("ID_KIERUNKU_STUDIOW");
            entity.Property(e => e.RokStudiów).HasColumnName("ROK_STUDIOW");

            entity.HasOne(d => d.GrupaStudencka).WithMany(p => p.Studenci)
                .HasForeignKey(d => d.IdGrupyStudenckiej)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();

            entity.HasOne(d => d.KierunekStudiów).WithMany(p => p.Studenci)
                .HasForeignKey(d => d.IdKierunkuStudiów)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();

            entity.HasMany(d => d.Grupy).WithMany(p => p.Studenci);

            entity.HasMany(d => d.Przedmioty).WithMany(p => p.Studenci);

            entity.HasOne(d => d.Użytkownik).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.IdUżytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<TypMiejsca>(entity =>
        {
            entity.HasKey(e => e.IdTypu);

            entity.ToTable("TypyMiejsc");

            entity.Property(e => e.IdTypu).HasColumnName("ID_TYPU");
            entity.Property(e => e.Typ)
                .HasColumnType("NCHAR(40)")
                .HasColumnName("TYP_MIEJSCA");
        });

        modelBuilder.Entity<Uprawnienie>(entity =>
        {
            entity.HasKey(e => e.IdUprawnienia);

            entity.Property(e => e.IdUprawnienia).HasColumnName("ID_UPRAWNIENIA");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(80)")
                .HasColumnName("NAZWA");

            entity.HasOne(d => d.Rola).WithMany(p => p.Uprawnienia)
                .HasForeignKey(d => d.IdRoli)
                .OnDelete(DeleteBehavior.ClientSetNull).IsRequired();
        });

        modelBuilder.Entity<Użytkownik>(entity =>
        {
            entity.HasKey(e => e.IdUżytkownika);

            entity.ToTable("Uzytkownicy");

            entity.Property(e => e.IdUżytkownika).HasColumnName("ID_UZYTKOWNIKA");
            entity.Property(e => e.Budynek)
                .HasColumnType("NCHAR(40)")
                .HasColumnName("BUDYNEK");
            entity.Property(e => e.Grupa)
                .HasColumnType("NCHAR(40)")
                .HasColumnName("GRUPA");
            entity.Property(e => e.Hasło)
                .HasColumnType("NCHAR(40)")
                .HasColumnName("HASLO");
            entity.Property(e => e.Imię)
                .HasColumnType("NCHAR(30)")
                .HasColumnName("IMIE");
            entity.Property(e => e.Mail)
                .HasColumnType("NCHAR(70)")
                .HasColumnName("MAIL");
            entity.Property(e => e.Nazwisko)
                .HasColumnType("NCHAR(50)")
                .HasColumnName("NAZWISKO");
            entity.Property(e => e.NumerTel)
                .HasColumnType("NCHAR(15)")
                .HasColumnName("NUMER_TEL");
        });

        modelBuilder.Entity<Wydział>(entity =>
        {
            entity.HasKey(e => e.IdWydziału);

            entity.ToTable("Wydzialy");

            entity.Property(e => e.IdWydziału).HasColumnName("ID_WYDZIALU");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(80)")
                .HasColumnName("NAZWA");
        });

        modelBuilder.Entity<Zamówienie>(entity =>
        {
            entity.HasKey(e => e.IdZamówienia);

            entity.Property(e => e.IdZamówienia).HasColumnName("ID_ZAMOWIENIA");
            entity.Property(e => e.DzieńZamówienia)
                .HasColumnType("DATETIME")
                .HasColumnName("DZIEN_ZAMOWIENIA");
            entity.Property(e => e.IdDania).HasColumnName("ID_DANIA");
            entity.Property(e => e.IdDiety).HasColumnName("ID_DIETY");
            entity.Property(e => e.IdUżytkownika).HasColumnName("ID_UZYTKOWNIKA");
            entity.Property(e => e.IdStołówki).HasColumnName("IdStolówki");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(70)")
                .HasColumnName("NAZWA");

            entity.HasOne(d => d.Danie).WithOne(p => p.Zamówienie)
                .HasForeignKey<Zamówienie>(d => d.IdDania)
                .OnDelete(DeleteBehavior.ClientCascade).IsRequired();

            entity.HasOne(d => d.Dieta).WithMany(p => p.Zamówienia)
                .HasForeignKey(d => d.IdDiety)
                .OnDelete(DeleteBehavior.ClientCascade).IsRequired();

            entity.HasOne(d => d.Użytkownik).WithMany(p => p.Zamówienia)
                .HasForeignKey(d => d.IdUżytkownika)
                .OnDelete(DeleteBehavior.ClientCascade).IsRequired();

            entity.HasOne(d => d.Stołówka).WithMany(p => p.Zamówienia)
                .HasForeignKey(d => d.IdStołówki)
                .OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
