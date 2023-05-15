﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.Data;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(UniversifyDbContext))]
    [Migration("20230512202827_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("GrupaNauczyciel", b =>
                {
                    b.Property<long>("GrupyIdGrupy")
                        .HasColumnType("INTEGER");

                    b.Property<long>("NauczycieleIdNauczyciela")
                        .HasColumnType("INTEGER");

                    b.HasKey("GrupyIdGrupy", "NauczycieleIdNauczyciela");

                    b.HasIndex("NauczycieleIdNauczyciela");

                    b.ToTable("GrupaNauczyciel");
                });

            modelBuilder.Entity("GrupaStudent", b =>
                {
                    b.Property<long>("GrupyIdGrupy")
                        .HasColumnType("INTEGER");

                    b.Property<long>("StudenciIdStudenta")
                        .HasColumnType("INTEGER");

                    b.HasKey("GrupyIdGrupy", "StudenciIdStudenta");

                    b.HasIndex("StudenciIdStudenta");

                    b.ToTable("GrupaStudent");
                });

            modelBuilder.Entity("NauczycielPrzedmiot", b =>
                {
                    b.Property<long>("NauczycieleIdNauczyciela")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PrzedmiotyIdPrzedmiotu")
                        .HasColumnType("INTEGER");

                    b.HasKey("NauczycieleIdNauczyciela", "PrzedmiotyIdPrzedmiotu");

                    b.HasIndex("PrzedmiotyIdPrzedmiotu");

                    b.ToTable("NauczycielPrzedmiot");
                });

            modelBuilder.Entity("PrzedmiotStudent", b =>
                {
                    b.Property<long>("PrzedmiotyIdPrzedmiotu")
                        .HasColumnType("INTEGER");

                    b.Property<long>("StudenciIdStudenta")
                        .HasColumnType("INTEGER");

                    b.HasKey("PrzedmiotyIdPrzedmiotu", "StudenciIdStudenta");

                    b.HasIndex("StudenciIdStudenta");

                    b.ToTable("PrzedmiotStudent");
                });

            modelBuilder.Entity("webapi.Models.Administrator", b =>
                {
                    b.Property<long>("IdAdministratora")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_ADMINISTRATORA");

                    b.Property<long>("IdRoli")
                        .HasColumnType("INT")
                        .HasColumnName("ID_ROLI");

                    b.Property<long>("IdUżytkownika")
                        .HasColumnType("INT")
                        .HasColumnName("ID_UŻYTKOWNIKA");

                    b.HasKey("IdAdministratora");

                    b.HasIndex("IdRoli")
                        .IsUnique();

                    b.HasIndex("IdUżytkownika")
                        .IsUnique();

                    b.ToTable("Administratorzy", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Alergen", b =>
                {
                    b.Property<long>("IdAlergenu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_ALERGENU");

                    b.Property<long>("IdDiety")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(50)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdAlergenu");

                    b.HasIndex("IdDiety");

                    b.ToTable("Alergeny", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Budynek", b =>
                {
                    b.Property<long>("IdBudynku")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_BUDYNKU");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("NCHAR(40)")
                        .HasColumnName("ADRES");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(40)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdBudynku");

                    b.ToTable("Budynki", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Danie", b =>
                {
                    b.Property<long>("IdDania")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_DANIA");

                    b.Property<long>("IdSkładnika")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_SKŁADNIKA");

                    b.Property<long>("IlośćKalorii")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ILOŚĆ_KALORII");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(50)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdDania");

                    b.ToTable("Dania");
                });

            modelBuilder.Entity("webapi.Models.Dieta", b =>
                {
                    b.Property<long>("IdDiety")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_DIETY");

                    b.Property<double>("Cena")
                        .HasColumnType("REAL")
                        .HasColumnName("CENA");

                    b.Property<long>("IdAlergenu")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_ALERGENU");

                    b.Property<long>("IdDania")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_DANIA");

                    b.Property<long>("IdKategorii")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_KATEGORII");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(70)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdDiety");

                    b.HasIndex("IdKategorii");

                    b.ToTable("Diety", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Grupa", b =>
                {
                    b.Property<long>("IdGrupy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_GRUPY");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(50)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdGrupy");

                    b.ToTable("Grupy", (string)null);
                });

            modelBuilder.Entity("webapi.Models.GrupaStudencka", b =>
                {
                    b.Property<long>("IdGrupy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_GRUPY");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(60)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdGrupy");

                    b.ToTable("GrupyStudenckie", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Kategoria", b =>
                {
                    b.Property<long>("IdKategorii")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_KATEGORII");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(60)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdKategorii");

                    b.ToTable("Kategorie", (string)null);
                });

            modelBuilder.Entity("webapi.Models.KierunekStudiów", b =>
                {
                    b.Property<long>("IdKierunkuStudiów")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_KIERUNKU_STUDIÓW");

                    b.Property<string>("NazwaKierunku")
                        .IsRequired()
                        .HasColumnType("NCHAR(80)")
                        .HasColumnName("Nazwa_Kierunku");

                    b.HasKey("IdKierunkuStudiów");

                    b.ToTable("KierunkiStudiów", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Miejsce", b =>
                {
                    b.Property<long>("IdMiejsca")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_MIEJSCA");

                    b.Property<byte[]>("Dostępność")
                        .IsRequired()
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("DOSTĘPNOŚĆ");

                    b.Property<long>("IdParkingu")
                        .HasColumnType("INTEGER");

                    b.Property<long>("IdTypu")
                        .HasColumnType("INT")
                        .HasColumnName("ID_TYPU");

                    b.HasKey("IdMiejsca");

                    b.HasIndex("IdParkingu");

                    b.HasIndex("IdTypu");

                    b.ToTable("Miejsca", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Nauczyciel", b =>
                {
                    b.Property<long>("IdNauczyciela")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_NAUCZYCIELA");

                    b.Property<long>("IdSpecjalizacji")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_SPECJALIZACJI");

                    b.Property<long>("IdUżytkownika")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_UŻYTKOWNIKA");

                    b.Property<long>("IdWydziału")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_WYDZIAŁU");

                    b.HasKey("IdNauczyciela");

                    b.HasIndex("IdSpecjalizacji");

                    b.HasIndex("IdUżytkownika")
                        .IsUnique();

                    b.HasIndex("IdWydziału");

                    b.ToTable("Nauczyciele", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Parking", b =>
                {
                    b.Property<long>("IdParkingu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_PARKINGU");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("NCHAR(60)")
                        .HasColumnName("ADRES");

                    b.HasKey("IdParkingu");

                    b.ToTable("Parkingi", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Produkt", b =>
                {
                    b.Property<long>("IdProduktu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_PRODUKTU");

                    b.Property<long>("IdStołówki")
                        .HasColumnType("INTEGER");

                    b.Property<double>("IlośćProduktu")
                        .HasColumnType("REAL")
                        .HasColumnName("ILOŚĆ_PRODUKTU");

                    b.Property<string>("Jednostka")
                        .IsRequired()
                        .HasColumnType("NCHAR(10)")
                        .HasColumnName("JEDNOSTKA");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(70)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdProduktu");

                    b.HasIndex("IdStołówki");

                    b.ToTable("Produkty", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Profil", b =>
                {
                    b.Property<long>("IdProfilu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_PROFILU");

                    b.Property<string>("GłównaZawartość")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(1500)")
                        .HasColumnName("GŁÓWNA_ZAWARTOŚĆ");

                    b.Property<long>("IdUżytkownika")
                        .HasColumnType("INT")
                        .HasColumnName("ID_UŻYTKOWNIKA");

                    b.Property<string>("ObrazProfilu")
                        .IsRequired()
                        .HasColumnType("NCHAR(60)")
                        .HasColumnName("OBRAZ_PROFILU");

                    b.Property<string>("PasekProfilu")
                        .IsRequired()
                        .HasColumnType("NCHAR(150)")
                        .HasColumnName("PASEK_PROFILU");

                    b.HasKey("IdProfilu");

                    b.HasIndex("IdUżytkownika")
                        .IsUnique();

                    b.ToTable("Profile", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Przedmiot", b =>
                {
                    b.Property<long>("IdPrzedmiotu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_PRZEDMIOTU");

                    b.Property<long>("IlośćSemestrów")
                        .HasColumnType("INT")
                        .HasColumnName("ILOŚĆ_SEMESTRÓW");

                    b.Property<string>("Kategoria")
                        .IsRequired()
                        .HasColumnType("NCHAR(60)")
                        .HasColumnName("KATEGORIA");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(120)")
                        .HasColumnName("NAZWA");

                    b.Property<long>("SemestrRozpoczęcia")
                        .HasColumnType("INT")
                        .HasColumnName("SEMESTR_ROZPOCZĘCIA");

                    b.HasKey("IdPrzedmiotu");

                    b.ToTable("Przedmioty", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Rola", b =>
                {
                    b.Property<long>("IdRoli")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_ROLI");

                    b.Property<long>("IdUprawnienia")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_UPRAWNIENIA");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(50)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdRoli");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Składnik", b =>
                {
                    b.Property<long>("IdSkładnika")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_SKŁADNIKA");

                    b.Property<long>("IdDania")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(50)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdSkładnika");

                    b.HasIndex("IdDania");

                    b.ToTable("Składniki", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Specjalizacja", b =>
                {
                    b.Property<long>("IdSpecjalizacji")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_SPECJALIZACJI");

                    b.Property<string>("Nazwa")
                        .HasColumnType("NCHAR(80)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdSpecjalizacji");

                    b.ToTable("Specjalizacje", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Stołówka", b =>
                {
                    b.Property<long>("IdStołówki")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_STOŁÓWKI");

                    b.Property<long>("IdBudynku")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_BUDYNKU");

                    b.Property<long>("IdProduktu")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_PRODUKTU");

                    b.Property<long>("IdZamówienia")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_ZAMÓWIENIA");

                    b.Property<string>("InformacjeDodatkowe")
                        .HasColumnType("NCHAR(1000)")
                        .HasColumnName("INFORMACJE_DODATKOWE");

                    b.HasKey("IdStołówki");

                    b.HasIndex("IdBudynku");

                    b.ToTable("Stołówki", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Student", b =>
                {
                    b.Property<long>("IdStudenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_STUDENTA");

                    b.Property<long>("IdGrupyStudenckiej")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_GRUPY_STUDENCKIEJ");

                    b.Property<long>("IdKierunkuStudiów")
                        .HasColumnType("INTEGER");

                    b.Property<long>("IdPrzedmiotu")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_PRZEDMIOTU");

                    b.Property<long>("IdUżytkownika")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_UŻYTKOWNIKA");

                    b.Property<long>("RokStudiów")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ROK_STUDIÓW");

                    b.HasKey("IdStudenta");

                    b.HasIndex("IdGrupyStudenckiej");

                    b.HasIndex("IdKierunkuStudiów");

                    b.HasIndex("IdUżytkownika")
                        .IsUnique();

                    b.ToTable("Studenci", (string)null);
                });

            modelBuilder.Entity("webapi.Models.TypMiejsca", b =>
                {
                    b.Property<long>("IdTypu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_TYPU");

                    b.Property<string>("Typ")
                        .IsRequired()
                        .HasColumnType("NCHAR(40)")
                        .HasColumnName("TYP_MIEJSCA");

                    b.HasKey("IdTypu");

                    b.ToTable("TypyMiejsc", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Uprawnienie", b =>
                {
                    b.Property<long>("IdUprawnienia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_UPRAWNIENIA");

                    b.Property<long>("IdRoli")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(80)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdUprawnienia");

                    b.HasIndex("IdRoli");

                    b.ToTable("Uprawnienia");
                });

            modelBuilder.Entity("webapi.Models.Użytkownik", b =>
                {
                    b.Property<long>("IdUżytkownika")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_UŻYTKOWNIKA");

                    b.Property<string>("Budynek")
                        .HasColumnType("NCHAR(40)")
                        .HasColumnName("BUDYNEK");

                    b.Property<string>("Grupa")
                        .HasColumnType("NCHAR(40)")
                        .HasColumnName("GRUPA");

                    b.Property<string>("Hasło")
                        .IsRequired()
                        .HasColumnType("NCHAR(40)")
                        .HasColumnName("HASŁO");

                    b.Property<string>("Imię")
                        .IsRequired()
                        .HasColumnType("NCHAR(30)")
                        .HasColumnName("IMIĘ");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("NCHAR(70)")
                        .HasColumnName("MAIL");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("NCHAR(50)")
                        .HasColumnName("NAZWISKO");

                    b.Property<string>("NumerTel")
                        .HasColumnType("NCHAR(15)")
                        .HasColumnName("NUMER_TEL");

                    b.HasKey("IdUżytkownika");

                    b.ToTable("Użytkownicy", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Wydział", b =>
                {
                    b.Property<long>("IdWydziału")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_WYDZIAŁU");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("NCHAR(80)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdWydziału");

                    b.ToTable("Wydziały", (string)null);
                });

            modelBuilder.Entity("webapi.Models.Zamówienie", b =>
                {
                    b.Property<long>("IdZamówienia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_ZAMÓWIENIA");

                    b.Property<byte[]>("DzieńZamówienia")
                        .IsRequired()
                        .HasColumnType("DATETIME")
                        .HasColumnName("DZIEŃ_ZAMÓWIENIA");

                    b.Property<long>("IdDania")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_DANIA");

                    b.Property<long>("IdDiety")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_DIETY");

                    b.Property<long>("IdStołówki")
                        .HasColumnType("INTEGER");

                    b.Property<long>("IdUżytkownika")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID_UŻYTKOWNIKA");

                    b.Property<string>("Nazwa")
                        .HasColumnType("NCHAR(70)")
                        .HasColumnName("NAZWA");

                    b.HasKey("IdZamówienia");

                    b.HasIndex("IdDania")
                        .IsUnique();

                    b.HasIndex("IdDiety");

                    b.HasIndex("IdStołówki");

                    b.HasIndex("IdUżytkownika");

                    b.ToTable("Zamówienia");
                });

            modelBuilder.Entity("GrupaNauczyciel", b =>
                {
                    b.HasOne("webapi.Models.Grupa", null)
                        .WithMany()
                        .HasForeignKey("GrupyIdGrupy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.Nauczyciel", null)
                        .WithMany()
                        .HasForeignKey("NauczycieleIdNauczyciela")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GrupaStudent", b =>
                {
                    b.HasOne("webapi.Models.Grupa", null)
                        .WithMany()
                        .HasForeignKey("GrupyIdGrupy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudenciIdStudenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NauczycielPrzedmiot", b =>
                {
                    b.HasOne("webapi.Models.Nauczyciel", null)
                        .WithMany()
                        .HasForeignKey("NauczycieleIdNauczyciela")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.Przedmiot", null)
                        .WithMany()
                        .HasForeignKey("PrzedmiotyIdPrzedmiotu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PrzedmiotStudent", b =>
                {
                    b.HasOne("webapi.Models.Przedmiot", null)
                        .WithMany()
                        .HasForeignKey("PrzedmiotyIdPrzedmiotu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webapi.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudenciIdStudenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("webapi.Models.Administrator", b =>
                {
                    b.HasOne("webapi.Models.Rola", "Rola")
                        .WithOne("Administrator")
                        .HasForeignKey("webapi.Models.Administrator", "IdRoli")
                        .IsRequired();

                    b.HasOne("webapi.Models.Użytkownik", "Użytkownik")
                        .WithOne("Administrator")
                        .HasForeignKey("webapi.Models.Administrator", "IdUżytkownika")
                        .IsRequired();

                    b.Navigation("Rola");

                    b.Navigation("Użytkownik");
                });

            modelBuilder.Entity("webapi.Models.Alergen", b =>
                {
                    b.HasOne("webapi.Models.Dieta", "Dieta")
                        .WithMany("Alergeny")
                        .HasForeignKey("IdDiety")
                        .IsRequired();

                    b.Navigation("Dieta");
                });

            modelBuilder.Entity("webapi.Models.Danie", b =>
                {
                    b.HasOne("webapi.Models.Dieta", "Dieta")
                        .WithMany("Dania")
                        .HasForeignKey("IdDania")
                        .IsRequired();

                    b.Navigation("Dieta");
                });

            modelBuilder.Entity("webapi.Models.Dieta", b =>
                {
                    b.HasOne("webapi.Models.Kategoria", "Kategoria")
                        .WithMany("Diety")
                        .HasForeignKey("IdKategorii")
                        .IsRequired();

                    b.Navigation("Kategoria");
                });

            modelBuilder.Entity("webapi.Models.Miejsce", b =>
                {
                    b.HasOne("webapi.Models.Parking", "Parking")
                        .WithMany("Miejsca")
                        .HasForeignKey("IdParkingu")
                        .IsRequired();

                    b.HasOne("webapi.Models.TypMiejsca", "Typ")
                        .WithMany("Miejsca")
                        .HasForeignKey("IdTypu")
                        .IsRequired();

                    b.Navigation("Parking");

                    b.Navigation("Typ");
                });

            modelBuilder.Entity("webapi.Models.Nauczyciel", b =>
                {
                    b.HasOne("webapi.Models.Specjalizacja", "Specjalizacja")
                        .WithMany("Nauczyciele")
                        .HasForeignKey("IdSpecjalizacji")
                        .IsRequired();

                    b.HasOne("webapi.Models.Użytkownik", "Użytkownik")
                        .WithOne("Nauczyciel")
                        .HasForeignKey("webapi.Models.Nauczyciel", "IdUżytkownika")
                        .IsRequired();

                    b.HasOne("webapi.Models.Wydział", "Wydział")
                        .WithMany("Nauczyciele")
                        .HasForeignKey("IdWydziału")
                        .IsRequired();

                    b.Navigation("Specjalizacja");

                    b.Navigation("Użytkownik");

                    b.Navigation("Wydział");
                });

            modelBuilder.Entity("webapi.Models.Produkt", b =>
                {
                    b.HasOne("webapi.Models.Stołówka", "Stołówka")
                        .WithMany("Produkty")
                        .HasForeignKey("IdStołówki")
                        .IsRequired();

                    b.Navigation("Stołówka");
                });

            modelBuilder.Entity("webapi.Models.Profil", b =>
                {
                    b.HasOne("webapi.Models.Użytkownik", "Użytkownik")
                        .WithOne("Profil")
                        .HasForeignKey("webapi.Models.Profil", "IdUżytkownika")
                        .IsRequired();

                    b.Navigation("Użytkownik");
                });

            modelBuilder.Entity("webapi.Models.Składnik", b =>
                {
                    b.HasOne("webapi.Models.Danie", "Danie")
                        .WithMany("Składniki")
                        .HasForeignKey("IdDania")
                        .IsRequired();

                    b.Navigation("Danie");
                });

            modelBuilder.Entity("webapi.Models.Stołówka", b =>
                {
                    b.HasOne("webapi.Models.Budynek", "Budynek")
                        .WithMany("Stołówki")
                        .HasForeignKey("IdBudynku")
                        .IsRequired();

                    b.Navigation("Budynek");
                });

            modelBuilder.Entity("webapi.Models.Student", b =>
                {
                    b.HasOne("webapi.Models.GrupaStudencka", "GrupaStudencka")
                        .WithMany("Studenci")
                        .HasForeignKey("IdGrupyStudenckiej")
                        .IsRequired();

                    b.HasOne("webapi.Models.KierunekStudiów", "KierunekStudiów")
                        .WithMany("Studenci")
                        .HasForeignKey("IdKierunkuStudiów")
                        .IsRequired();

                    b.HasOne("webapi.Models.Użytkownik", "Użytkownik")
                        .WithOne("Student")
                        .HasForeignKey("webapi.Models.Student", "IdUżytkownika")
                        .IsRequired();

                    b.Navigation("GrupaStudencka");

                    b.Navigation("KierunekStudiów");

                    b.Navigation("Użytkownik");
                });

            modelBuilder.Entity("webapi.Models.Uprawnienie", b =>
                {
                    b.HasOne("webapi.Models.Rola", "Rola")
                        .WithMany("Uprawnienia")
                        .HasForeignKey("IdRoli")
                        .IsRequired();

                    b.Navigation("Rola");
                });

            modelBuilder.Entity("webapi.Models.Zamówienie", b =>
                {
                    b.HasOne("webapi.Models.Danie", "Danie")
                        .WithOne("Zamówienie")
                        .HasForeignKey("webapi.Models.Zamówienie", "IdDania")
                        .IsRequired();

                    b.HasOne("webapi.Models.Dieta", "Dieta")
                        .WithMany("Zamówienia")
                        .HasForeignKey("IdDiety")
                        .IsRequired();

                    b.HasOne("webapi.Models.Stołówka", "Stołówka")
                        .WithMany("Zamówienia")
                        .HasForeignKey("IdStołówki")
                        .IsRequired();

                    b.HasOne("webapi.Models.Użytkownik", "Użytkownik")
                        .WithMany("Zamówienia")
                        .HasForeignKey("IdUżytkownika")
                        .IsRequired();

                    b.Navigation("Danie");

                    b.Navigation("Dieta");

                    b.Navigation("Stołówka");

                    b.Navigation("Użytkownik");
                });

            modelBuilder.Entity("webapi.Models.Budynek", b =>
                {
                    b.Navigation("Stołówki");
                });

            modelBuilder.Entity("webapi.Models.Danie", b =>
                {
                    b.Navigation("Składniki");

                    b.Navigation("Zamówienie");
                });

            modelBuilder.Entity("webapi.Models.Dieta", b =>
                {
                    b.Navigation("Alergeny");

                    b.Navigation("Dania");

                    b.Navigation("Zamówienia");
                });

            modelBuilder.Entity("webapi.Models.GrupaStudencka", b =>
                {
                    b.Navigation("Studenci");
                });

            modelBuilder.Entity("webapi.Models.Kategoria", b =>
                {
                    b.Navigation("Diety");
                });

            modelBuilder.Entity("webapi.Models.KierunekStudiów", b =>
                {
                    b.Navigation("Studenci");
                });

            modelBuilder.Entity("webapi.Models.Parking", b =>
                {
                    b.Navigation("Miejsca");
                });

            modelBuilder.Entity("webapi.Models.Rola", b =>
                {
                    b.Navigation("Administrator");

                    b.Navigation("Uprawnienia");
                });

            modelBuilder.Entity("webapi.Models.Specjalizacja", b =>
                {
                    b.Navigation("Nauczyciele");
                });

            modelBuilder.Entity("webapi.Models.Stołówka", b =>
                {
                    b.Navigation("Produkty");

                    b.Navigation("Zamówienia");
                });

            modelBuilder.Entity("webapi.Models.TypMiejsca", b =>
                {
                    b.Navigation("Miejsca");
                });

            modelBuilder.Entity("webapi.Models.Użytkownik", b =>
                {
                    b.Navigation("Administrator");

                    b.Navigation("Nauczyciel");

                    b.Navigation("Profil")
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Zamówienia");
                });

            modelBuilder.Entity("webapi.Models.Wydział", b =>
                {
                    b.Navigation("Nauczyciele");
                });
#pragma warning restore 612, 618
        }
    }
}
