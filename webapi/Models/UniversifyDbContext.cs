using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

public partial class UniversifyDbContext : DbContext
{
    public UniversifyDbContext()
    {
    }

    public UniversifyDbContext(DbContextOptions<UniversifyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administratorzy { get; set; }

    public virtual DbSet<Alergen> Alergeny { get; set; }

    public virtual DbSet<Budynek> Budynki { get; set; }

    public virtual DbSet<Danie> Dania { get; set; }

    public virtual DbSet<Dieta> Diety { get; set; }

    public virtual DbSet<Grupa> Grupy { get; set; }

    public virtual DbSet<GrupaStudencka> GrupyStudenckie { get; set; }

    public virtual DbSet<Kategoria> Kategorie { get; set; }

    public virtual DbSet<Miejsce> Miejsca { get; set; }

    public virtual DbSet<Nauczyciel> Nauczyciele { get; set; }

    public virtual DbSet<Parking> Parkingi { get; set; }

    public virtual DbSet<Produkt> Produkty { get; set; }

    public virtual DbSet<Przedmiot> Przedmioty { get; set; }

    public virtual DbSet<Rola> Role { get; set; }

    public virtual DbSet<Składnik> Składniki { get; set; }

    public virtual DbSet<Specjalizacja> Specjalizacje { get; set; }

    public virtual DbSet<Stołówka> Stołówki { get; set; }

    public virtual DbSet<Student> Studenci { get; set; }

    public virtual DbSet<TypMiejsca> TypyMiejsc { get; set; }

    public virtual DbSet<Uprawnienie> Uprawnienia { get; set; }

    public virtual DbSet<Użytkownik> Użytkownicy { get; set; }

    public virtual DbSet<Wydział> Wydziały { get; set; }

    public virtual DbSet<Zamówienie> Zamówienia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Name=ConnectionStrings:UniversifyDB");

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
                .HasColumnName("ID_UŻYTKOWNIKA");

            entity.HasOne(d => d.IdUżytkownikaNavigation).WithMany(p => p.Administratorzy)
                .HasForeignKey(d => d.IdUżytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Alergen>(entity =>
        {
            entity.HasKey(e => e.IdAlergenu);

            entity.ToTable("Alergeny");

            entity.Property(e => e.IdAlergenu).HasColumnName("ID_ALERGENU");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(50)")
                .HasColumnName("NAZWA");
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

            entity.Property(e => e.IdDania).HasColumnName("ID_DANIA");
            entity.Property(e => e.IdSkładnika).HasColumnName("ID_SKŁADNIKA");
            entity.Property(e => e.IlośćKalorii).HasColumnName("ILOŚĆ_KALORII");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(50)")
                .HasColumnName("NAZWA");

            entity.HasOne(d => d.IdSkładnikaNavigation).WithMany(p => p.Dania)
                .HasForeignKey(d => d.IdSkładnika)
                .OnDelete(DeleteBehavior.ClientSetNull);
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

            entity.HasOne(d => d.IdAlergenuNavigation).WithMany(p => p.Dieties)
                .HasForeignKey(d => d.IdAlergenu)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdDaniaNavigation).WithMany(p => p.Dieties)
                .HasForeignKey(d => d.IdDania)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdKategoriiNavigation).WithMany(p => p.Diety)
                .HasForeignKey(d => d.IdKategorii)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Grupa>(entity =>
        {
            entity.HasKey(e => e.IdGrupy);

            entity.ToTable("Grupy");

            entity.Property(e => e.IdGrupy).HasColumnName("ID_GRUPY");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(50)")
                .HasColumnName("NAZWA");
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

        modelBuilder.Entity<Miejsce>(entity =>
        {
            entity.HasKey(e => e.IdMiejsca);

            entity.ToTable("Miejsca");

            entity.Property(e => e.IdMiejsca).HasColumnName("ID_MIEJSCA");
            entity.Property(e => e.Dostępność)
                .HasColumnType("BOOLEAN")
                .HasColumnName("DOSTĘPNOŚĆ");
            entity.Property(e => e.IdTypu)
                .HasColumnType("INT")
                .HasColumnName("ID_TYPU");

            entity.HasOne(d => d.IdTypuNavigation).WithMany(p => p.Miejsca)
                .HasForeignKey(d => d.IdTypu)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Nauczyciel>(entity =>
        {
            entity.HasKey(e => e.IdNauczyciela);

            entity.ToTable("Nauczyciele");

            entity.Property(e => e.IdNauczyciela).HasColumnName("ID_NAUCZYCIELA");
            entity.Property(e => e.IdPrzedmiotu).HasColumnName("ID_PRZEDMIOTU");
            entity.Property(e => e.IdSpecjalizacji).HasColumnName("ID_SPECJALIZACJI");
            entity.Property(e => e.IdUżytkownika).HasColumnName("ID_UŻYTKOWNIKA");
            entity.Property(e => e.IdWydziału).HasColumnName("ID_WYDZIAŁU");

            entity.HasOne(d => d.IdPrzedmiotuNavigation).WithMany(p => p.Nauczyciele)
                .HasForeignKey(d => d.IdPrzedmiotu)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdSpecjalizacjiNavigation).WithMany(p => p.Nauczyciele)
                .HasForeignKey(d => d.IdSpecjalizacji)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdUżytkownikaNavigation).WithMany(p => p.Nauczyciele)
                .HasForeignKey(d => d.IdUżytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdWydziałuNavigation).WithMany(p => p.Nauczyciele)
                .HasForeignKey(d => d.IdWydziału)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Parking>(entity =>
        {
            entity.HasKey(e => e.IdParkingu);

            entity.ToTable("Parkingi");

            entity.Property(e => e.IdParkingu).HasColumnName("ID_PARKINGU");
            entity.Property(e => e.Adres)
                .HasColumnType("NCHAR(60)")
                .HasColumnName("ADRES");
            entity.Property(e => e.IdMiejsca)
                .HasColumnType("INT")
                .HasColumnName("ID_MIEJSCA");

            entity.HasOne(d => d.IdMiejscaNavigation).WithMany(p => p.Parkingis)
                .HasForeignKey(d => d.IdMiejsca)
                .OnDelete(DeleteBehavior.ClientSetNull);
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
        });

        modelBuilder.Entity<Przedmiot>(entity =>
        {
            entity.HasKey(e => e.IdPrzedmiotu);

            entity.ToTable("Przedmioty");

            entity.Property(e => e.IdPrzedmiotu).HasColumnName("ID_PRZEDMIOTU");
            entity.Property(e => e.IdNauczyciela)
                .HasColumnType("INT")
                .HasColumnName("ID_NAUCZYCIELA");
            entity.Property(e => e.IlośćSemestrów)
                .HasColumnType("INT")
                .HasColumnName("ILOŚĆ_SEMESTRÓW");
            entity.Property(e => e.Kategoria)
                .HasColumnType("NCHAR(60)")
                .HasColumnName("KATEGORIA");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(120)")
                .HasColumnName("NAZWA");
            entity.Property(e => e.SemestrRozpoczęcia)
                .HasColumnType("INT")
                .HasColumnName("SEMESTR_ROZPOCZĘCIA");

            entity.HasOne(d => d.IdNauczycielaNavigation).WithMany(p => p.Przedmioty)
                .HasForeignKey(d => d.IdNauczyciela)
                .OnDelete(DeleteBehavior.ClientSetNull);
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

            entity.HasOne(d => d.IdUprawnieniaNavigation).WithMany(p => p.Role)
                .HasForeignKey(d => d.IdUprawnienia)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Składnik>(entity =>
        {
            entity.HasKey(e => e.IdSkładnika);

            entity.ToTable("Składniki");

            entity.Property(e => e.IdSkładnika).HasColumnName("ID_SKŁADNIKA");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(50)")
                .HasColumnName("NAZWA");
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

            entity.ToTable("Stołówki");

            entity.Property(e => e.IdStołówki)
                .ValueGeneratedNever()
                .HasColumnName("ID_STOŁÓWKI");
            entity.Property(e => e.IdBudynku).HasColumnName("ID_BUDYNKU");
            entity.Property(e => e.IdProduktu).HasColumnName("ID_PRODUKTU");
            entity.Property(e => e.IdZamówienia).HasColumnName("ID_ZAMÓWIENIA");
            entity.Property(e => e.InformacjeDodatkowe)
                .HasColumnType("NCHAR(1000)")
                .HasColumnName("INFORMACJE_DODATKOWE");

            entity.HasOne(d => d.IdBudynkuNavigation).WithMany(p => p.Stołówkis)
                .HasForeignKey(d => d.IdBudynku)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdProduktuNavigation).WithMany(p => p.Stołówkis)
                .HasForeignKey(d => d.IdProduktu)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdZamówieniaNavigation).WithMany(p => p.Stołówki)
                .HasForeignKey(d => d.IdZamówienia)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.IdStudenta);

            entity.ToTable("Studenci");

            entity.Property(e => e.IdStudenta).HasColumnName("ID_STUDENTA");
            entity.Property(e => e.IdGrupyStudenckiej).HasColumnName("ID_GRUPY_STUDENCKIEJ");
            entity.Property(e => e.IdPrzedmiotu).HasColumnName("ID_PRZEDMIOTU");
            entity.Property(e => e.IdUżytkownika).HasColumnName("ID_UŻYTKOWNIKA");
            entity.Property(e => e.RokStudiów).HasColumnName("ROK_STUDIÓW");

            entity.HasOne(d => d.IdGrupyStudenckiejNavigation).WithMany(p => p.Studencis)
                .HasForeignKey(d => d.IdGrupyStudenckiej)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdPrzedmiotuNavigation).WithMany(p => p.Studenci)
                .HasForeignKey(d => d.IdPrzedmiotu)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdUżytkownikaNavigation).WithMany(p => p.Studenci)
                .HasForeignKey(d => d.IdUżytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull);
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
        });

        modelBuilder.Entity<Użytkownik>(entity =>
        {
            entity.HasKey(e => e.IdUżytkownika);

            entity.ToTable("Użytkownicy");

            entity.Property(e => e.IdUżytkownika).HasColumnName("ID_UŻYTKOWNIKA");
            entity.Property(e => e.Budynek)
                .HasColumnType("NCHAR(40)")
                .HasColumnName("BUDYNEK");
            entity.Property(e => e.Grupa)
                .HasColumnType("NCHAR(40)")
                .HasColumnName("GRUPA");
            entity.Property(e => e.Hasło)
                .HasColumnType("NCHAR(40)")
                .HasColumnName("HASŁO");
            entity.Property(e => e.Imię)
                .HasColumnType("NCHAR(30)")
                .HasColumnName("IMIĘ");
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

            entity.ToTable("Wydziały");

            entity.Property(e => e.IdWydziału).HasColumnName("ID_WYDZIAŁU");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(80)")
                .HasColumnName("NAZWA");
        });

        modelBuilder.Entity<Zamówienie>(entity =>
        {
            entity.HasKey(e => e.IdZamówienia);

            entity.Property(e => e.IdZamówienia).HasColumnName("ID_ZAMÓWIENIA");
            entity.Property(e => e.DzieńZamówienia)
                .HasColumnType("DATETIME")
                .HasColumnName("DZIEŃ_ZAMÓWIENIA");
            entity.Property(e => e.IdDania).HasColumnName("ID_DANIA");
            entity.Property(e => e.IdDiety).HasColumnName("ID_DIETY");
            entity.Property(e => e.IdUżytkownika).HasColumnName("ID_UŻYTKOWNIKA");
            entity.Property(e => e.Nazwa)
                .HasColumnType("NCHAR(70)")
                .HasColumnName("NAZWA");

            entity.HasOne(d => d.IdDaniaNavigation).WithMany(p => p.Zamówienia)
                .HasForeignKey(d => d.IdDania)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdDietyNavigation).WithMany(p => p.Zamówienia)
                .HasForeignKey(d => d.IdDiety)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdUżytkownikaNavigation).WithMany(p => p.Zamówienia)
                .HasForeignKey(d => d.IdUżytkownika)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
