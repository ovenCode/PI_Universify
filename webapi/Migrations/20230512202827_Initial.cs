using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budynki",
                columns: table => new
                {
                    ID_BUDYNKU = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(40)", nullable: false),
                    ADRES = table.Column<string>(type: "NCHAR(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budynki", x => x.ID_BUDYNKU);
                });

            migrationBuilder.CreateTable(
                name: "Grupy",
                columns: table => new
                {
                    ID_GRUPY = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupy", x => x.ID_GRUPY);
                });

            migrationBuilder.CreateTable(
                name: "GrupyStudenckie",
                columns: table => new
                {
                    ID_GRUPY = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupyStudenckie", x => x.ID_GRUPY);
                });

            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    ID_KATEGORII = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.ID_KATEGORII);
                });

            migrationBuilder.CreateTable(
                name: "KierunkiStudiów",
                columns: table => new
                {
                    ID_KIERUNKU_STUDIÓW = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa_Kierunku = table.Column<string>(type: "NCHAR(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KierunkiStudiów", x => x.ID_KIERUNKU_STUDIÓW);
                });

            migrationBuilder.CreateTable(
                name: "Parkingi",
                columns: table => new
                {
                    ID_PARKINGU = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ADRES = table.Column<string>(type: "NCHAR(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkingi", x => x.ID_PARKINGU);
                });

            migrationBuilder.CreateTable(
                name: "Przedmioty",
                columns: table => new
                {
                    ID_PRZEDMIOTU = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(120)", nullable: false),
                    KATEGORIA = table.Column<string>(type: "NCHAR(60)", nullable: false),
                    SEMESTR_ROZPOCZĘCIA = table.Column<long>(type: "INT", nullable: false),
                    ILOŚĆ_SEMESTRÓW = table.Column<long>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przedmioty", x => x.ID_PRZEDMIOTU);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID_ROLI = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(50)", nullable: false),
                    ID_UPRAWNIENIA = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID_ROLI);
                });

            migrationBuilder.CreateTable(
                name: "Specjalizacje",
                columns: table => new
                {
                    ID_SPECJALIZACJI = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(80)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specjalizacje", x => x.ID_SPECJALIZACJI);
                });

            migrationBuilder.CreateTable(
                name: "TypyMiejsc",
                columns: table => new
                {
                    ID_TYPU = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TYP_MIEJSCA = table.Column<string>(type: "NCHAR(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypyMiejsc", x => x.ID_TYPU);
                });

            migrationBuilder.CreateTable(
                name: "Użytkownicy",
                columns: table => new
                {
                    ID_UŻYTKOWNIKA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IMIĘ = table.Column<string>(type: "NCHAR(30)", nullable: false),
                    NAZWISKO = table.Column<string>(type: "NCHAR(50)", nullable: false),
                    MAIL = table.Column<string>(type: "NCHAR(70)", nullable: false),
                    HASŁO = table.Column<string>(type: "NCHAR(40)", nullable: false),
                    GRUPA = table.Column<string>(type: "NCHAR(40)", nullable: true),
                    NUMER_TEL = table.Column<string>(type: "NCHAR(15)", nullable: true),
                    BUDYNEK = table.Column<string>(type: "NCHAR(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Użytkownicy", x => x.ID_UŻYTKOWNIKA);
                });

            migrationBuilder.CreateTable(
                name: "Wydziały",
                columns: table => new
                {
                    ID_WYDZIAŁU = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wydziały", x => x.ID_WYDZIAŁU);
                });

            migrationBuilder.CreateTable(
                name: "Stołówki",
                columns: table => new
                {
                    ID_STOŁÓWKI = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_BUDYNKU = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_ZAMÓWIENIA = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_PRODUKTU = table.Column<long>(type: "INTEGER", nullable: false),
                    INFORMACJE_DODATKOWE = table.Column<string>(type: "NCHAR(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stołówki", x => x.ID_STOŁÓWKI);
                    table.ForeignKey(
                        name: "FK_Stołówki_Budynki_ID_BUDYNKU",
                        column: x => x.ID_BUDYNKU,
                        principalTable: "Budynki",
                        principalColumn: "ID_BUDYNKU");
                });

            migrationBuilder.CreateTable(
                name: "Diety",
                columns: table => new
                {
                    ID_DIETY = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(70)", nullable: false),
                    ID_KATEGORII = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_ALERGENU = table.Column<long>(type: "INTEGER", nullable: false),
                    CENA = table.Column<double>(type: "REAL", nullable: false),
                    ID_DANIA = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diety", x => x.ID_DIETY);
                    table.ForeignKey(
                        name: "FK_Diety_Kategorie_ID_KATEGORII",
                        column: x => x.ID_KATEGORII,
                        principalTable: "Kategorie",
                        principalColumn: "ID_KATEGORII");
                });

            migrationBuilder.CreateTable(
                name: "Uprawnienia",
                columns: table => new
                {
                    ID_UPRAWNIENIA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdRoli = table.Column<long>(type: "INTEGER", nullable: false),
                    NAZWA = table.Column<string>(type: "NCHAR(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uprawnienia", x => x.ID_UPRAWNIENIA);
                    table.ForeignKey(
                        name: "FK_Uprawnienia_Role_IdRoli",
                        column: x => x.IdRoli,
                        principalTable: "Role",
                        principalColumn: "ID_ROLI");
                });

            migrationBuilder.CreateTable(
                name: "Miejsca",
                columns: table => new
                {
                    ID_MIEJSCA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdParkingu = table.Column<long>(type: "INTEGER", nullable: false),
                    DOSTĘPNOŚĆ = table.Column<byte[]>(type: "BOOLEAN", nullable: false),
                    ID_TYPU = table.Column<long>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miejsca", x => x.ID_MIEJSCA);
                    table.ForeignKey(
                        name: "FK_Miejsca_Parkingi_IdParkingu",
                        column: x => x.IdParkingu,
                        principalTable: "Parkingi",
                        principalColumn: "ID_PARKINGU");
                    table.ForeignKey(
                        name: "FK_Miejsca_TypyMiejsc_ID_TYPU",
                        column: x => x.ID_TYPU,
                        principalTable: "TypyMiejsc",
                        principalColumn: "ID_TYPU");
                });

            migrationBuilder.CreateTable(
                name: "Administratorzy",
                columns: table => new
                {
                    ID_ADMINISTRATORA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_UŻYTKOWNIKA = table.Column<long>(type: "INT", nullable: false),
                    ID_ROLI = table.Column<long>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administratorzy", x => x.ID_ADMINISTRATORA);
                    table.ForeignKey(
                        name: "FK_Administratorzy_Role_ID_ROLI",
                        column: x => x.ID_ROLI,
                        principalTable: "Role",
                        principalColumn: "ID_ROLI");
                    table.ForeignKey(
                        name: "FK_Administratorzy_Użytkownicy_ID_UŻYTKOWNIKA",
                        column: x => x.ID_UŻYTKOWNIKA,
                        principalTable: "Użytkownicy",
                        principalColumn: "ID_UŻYTKOWNIKA");
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ID_PROFILU = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_UŻYTKOWNIKA = table.Column<long>(type: "INT", nullable: false),
                    OBRAZ_PROFILU = table.Column<string>(type: "NCHAR(60)", nullable: false),
                    PASEK_PROFILU = table.Column<string>(type: "NCHAR(150)", nullable: false),
                    GŁÓWNA_ZAWARTOŚĆ = table.Column<string>(type: "NVARCHAR(1500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ID_PROFILU);
                    table.ForeignKey(
                        name: "FK_Profile_Użytkownicy_ID_UŻYTKOWNIKA",
                        column: x => x.ID_UŻYTKOWNIKA,
                        principalTable: "Użytkownicy",
                        principalColumn: "ID_UŻYTKOWNIKA");
                });

            migrationBuilder.CreateTable(
                name: "Studenci",
                columns: table => new
                {
                    ID_STUDENTA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_UŻYTKOWNIKA = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_PRZEDMIOTU = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_GRUPY_STUDENCKIEJ = table.Column<long>(type: "INTEGER", nullable: false),
                    IdKierunkuStudiów = table.Column<long>(type: "INTEGER", nullable: false),
                    ROK_STUDIÓW = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenci", x => x.ID_STUDENTA);
                    table.ForeignKey(
                        name: "FK_Studenci_GrupyStudenckie_ID_GRUPY_STUDENCKIEJ",
                        column: x => x.ID_GRUPY_STUDENCKIEJ,
                        principalTable: "GrupyStudenckie",
                        principalColumn: "ID_GRUPY");
                    table.ForeignKey(
                        name: "FK_Studenci_KierunkiStudiów_IdKierunkuStudiów",
                        column: x => x.IdKierunkuStudiów,
                        principalTable: "KierunkiStudiów",
                        principalColumn: "ID_KIERUNKU_STUDIÓW");
                    table.ForeignKey(
                        name: "FK_Studenci_Użytkownicy_ID_UŻYTKOWNIKA",
                        column: x => x.ID_UŻYTKOWNIKA,
                        principalTable: "Użytkownicy",
                        principalColumn: "ID_UŻYTKOWNIKA");
                });

            migrationBuilder.CreateTable(
                name: "Nauczyciele",
                columns: table => new
                {
                    ID_NAUCZYCIELA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_UŻYTKOWNIKA = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_WYDZIAŁU = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_SPECJALIZACJI = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nauczyciele", x => x.ID_NAUCZYCIELA);
                    table.ForeignKey(
                        name: "FK_Nauczyciele_Specjalizacje_ID_SPECJALIZACJI",
                        column: x => x.ID_SPECJALIZACJI,
                        principalTable: "Specjalizacje",
                        principalColumn: "ID_SPECJALIZACJI");
                    table.ForeignKey(
                        name: "FK_Nauczyciele_Użytkownicy_ID_UŻYTKOWNIKA",
                        column: x => x.ID_UŻYTKOWNIKA,
                        principalTable: "Użytkownicy",
                        principalColumn: "ID_UŻYTKOWNIKA");
                    table.ForeignKey(
                        name: "FK_Nauczyciele_Wydziały_ID_WYDZIAŁU",
                        column: x => x.ID_WYDZIAŁU,
                        principalTable: "Wydziały",
                        principalColumn: "ID_WYDZIAŁU");
                });

            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    ID_PRODUKTU = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(70)", nullable: false),
                    ILOŚĆ_PRODUKTU = table.Column<double>(type: "REAL", nullable: false),
                    JEDNOSTKA = table.Column<string>(type: "NCHAR(10)", nullable: false),
                    IdStołówki = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.ID_PRODUKTU);
                    table.ForeignKey(
                        name: "FK_Produkty_Stołówki_IdStołówki",
                        column: x => x.IdStołówki,
                        principalTable: "Stołówki",
                        principalColumn: "ID_STOŁÓWKI");
                });

            migrationBuilder.CreateTable(
                name: "Alergeny",
                columns: table => new
                {
                    ID_ALERGENU = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdDiety = table.Column<long>(type: "INTEGER", nullable: false),
                    NAZWA = table.Column<string>(type: "NCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alergeny", x => x.ID_ALERGENU);
                    table.ForeignKey(
                        name: "FK_Alergeny_Diety_IdDiety",
                        column: x => x.IdDiety,
                        principalTable: "Diety",
                        principalColumn: "ID_DIETY");
                });

            migrationBuilder.CreateTable(
                name: "Dania",
                columns: table => new
                {
                    ID_DANIA = table.Column<long>(type: "INTEGER", nullable: false),
                    NAZWA = table.Column<string>(type: "NCHAR(50)", nullable: false),
                    ILOŚĆ_KALORII = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_SKŁADNIKA = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dania", x => x.ID_DANIA);
                    table.ForeignKey(
                        name: "FK_Dania_Diety_ID_DANIA",
                        column: x => x.ID_DANIA,
                        principalTable: "Diety",
                        principalColumn: "ID_DIETY");
                });

            migrationBuilder.CreateTable(
                name: "GrupaStudent",
                columns: table => new
                {
                    GrupyIdGrupy = table.Column<long>(type: "INTEGER", nullable: false),
                    StudenciIdStudenta = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupaStudent", x => new { x.GrupyIdGrupy, x.StudenciIdStudenta });
                    table.ForeignKey(
                        name: "FK_GrupaStudent_Grupy_GrupyIdGrupy",
                        column: x => x.GrupyIdGrupy,
                        principalTable: "Grupy",
                        principalColumn: "ID_GRUPY",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupaStudent_Studenci_StudenciIdStudenta",
                        column: x => x.StudenciIdStudenta,
                        principalTable: "Studenci",
                        principalColumn: "ID_STUDENTA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrzedmiotStudent",
                columns: table => new
                {
                    PrzedmiotyIdPrzedmiotu = table.Column<long>(type: "INTEGER", nullable: false),
                    StudenciIdStudenta = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzedmiotStudent", x => new { x.PrzedmiotyIdPrzedmiotu, x.StudenciIdStudenta });
                    table.ForeignKey(
                        name: "FK_PrzedmiotStudent_Przedmioty_PrzedmiotyIdPrzedmiotu",
                        column: x => x.PrzedmiotyIdPrzedmiotu,
                        principalTable: "Przedmioty",
                        principalColumn: "ID_PRZEDMIOTU",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrzedmiotStudent_Studenci_StudenciIdStudenta",
                        column: x => x.StudenciIdStudenta,
                        principalTable: "Studenci",
                        principalColumn: "ID_STUDENTA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupaNauczyciel",
                columns: table => new
                {
                    GrupyIdGrupy = table.Column<long>(type: "INTEGER", nullable: false),
                    NauczycieleIdNauczyciela = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupaNauczyciel", x => new { x.GrupyIdGrupy, x.NauczycieleIdNauczyciela });
                    table.ForeignKey(
                        name: "FK_GrupaNauczyciel_Grupy_GrupyIdGrupy",
                        column: x => x.GrupyIdGrupy,
                        principalTable: "Grupy",
                        principalColumn: "ID_GRUPY",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupaNauczyciel_Nauczyciele_NauczycieleIdNauczyciela",
                        column: x => x.NauczycieleIdNauczyciela,
                        principalTable: "Nauczyciele",
                        principalColumn: "ID_NAUCZYCIELA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NauczycielPrzedmiot",
                columns: table => new
                {
                    NauczycieleIdNauczyciela = table.Column<long>(type: "INTEGER", nullable: false),
                    PrzedmiotyIdPrzedmiotu = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NauczycielPrzedmiot", x => new { x.NauczycieleIdNauczyciela, x.PrzedmiotyIdPrzedmiotu });
                    table.ForeignKey(
                        name: "FK_NauczycielPrzedmiot_Nauczyciele_NauczycieleIdNauczyciela",
                        column: x => x.NauczycieleIdNauczyciela,
                        principalTable: "Nauczyciele",
                        principalColumn: "ID_NAUCZYCIELA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NauczycielPrzedmiot_Przedmioty_PrzedmiotyIdPrzedmiotu",
                        column: x => x.PrzedmiotyIdPrzedmiotu,
                        principalTable: "Przedmioty",
                        principalColumn: "ID_PRZEDMIOTU",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Składniki",
                columns: table => new
                {
                    ID_SKŁADNIKA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdDania = table.Column<long>(type: "INTEGER", nullable: false),
                    NAZWA = table.Column<string>(type: "NCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Składniki", x => x.ID_SKŁADNIKA);
                    table.ForeignKey(
                        name: "FK_Składniki_Dania_IdDania",
                        column: x => x.IdDania,
                        principalTable: "Dania",
                        principalColumn: "ID_DANIA");
                });

            migrationBuilder.CreateTable(
                name: "Zamówienia",
                columns: table => new
                {
                    ID_ZAMÓWIENIA = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAZWA = table.Column<string>(type: "NCHAR(70)", nullable: true),
                    ID_UŻYTKOWNIKA = table.Column<long>(type: "INTEGER", nullable: false),
                    IdStołówki = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_DIETY = table.Column<long>(type: "INTEGER", nullable: false),
                    ID_DANIA = table.Column<long>(type: "INTEGER", nullable: false),
                    DZIEŃ_ZAMÓWIENIA = table.Column<byte[]>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamówienia", x => x.ID_ZAMÓWIENIA);
                    table.ForeignKey(
                        name: "FK_Zamówienia_Dania_ID_DANIA",
                        column: x => x.ID_DANIA,
                        principalTable: "Dania",
                        principalColumn: "ID_DANIA");
                    table.ForeignKey(
                        name: "FK_Zamówienia_Diety_ID_DIETY",
                        column: x => x.ID_DIETY,
                        principalTable: "Diety",
                        principalColumn: "ID_DIETY");
                    table.ForeignKey(
                        name: "FK_Zamówienia_Stołówki_IdStołówki",
                        column: x => x.IdStołówki,
                        principalTable: "Stołówki",
                        principalColumn: "ID_STOŁÓWKI");
                    table.ForeignKey(
                        name: "FK_Zamówienia_Użytkownicy_ID_UŻYTKOWNIKA",
                        column: x => x.ID_UŻYTKOWNIKA,
                        principalTable: "Użytkownicy",
                        principalColumn: "ID_UŻYTKOWNIKA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administratorzy_ID_ROLI",
                table: "Administratorzy",
                column: "ID_ROLI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administratorzy_ID_UŻYTKOWNIKA",
                table: "Administratorzy",
                column: "ID_UŻYTKOWNIKA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alergeny_IdDiety",
                table: "Alergeny",
                column: "IdDiety");

            migrationBuilder.CreateIndex(
                name: "IX_Diety_ID_KATEGORII",
                table: "Diety",
                column: "ID_KATEGORII");

            migrationBuilder.CreateIndex(
                name: "IX_GrupaNauczyciel_NauczycieleIdNauczyciela",
                table: "GrupaNauczyciel",
                column: "NauczycieleIdNauczyciela");

            migrationBuilder.CreateIndex(
                name: "IX_GrupaStudent_StudenciIdStudenta",
                table: "GrupaStudent",
                column: "StudenciIdStudenta");

            migrationBuilder.CreateIndex(
                name: "IX_Miejsca_ID_TYPU",
                table: "Miejsca",
                column: "ID_TYPU");

            migrationBuilder.CreateIndex(
                name: "IX_Miejsca_IdParkingu",
                table: "Miejsca",
                column: "IdParkingu");

            migrationBuilder.CreateIndex(
                name: "IX_Nauczyciele_ID_SPECJALIZACJI",
                table: "Nauczyciele",
                column: "ID_SPECJALIZACJI");

            migrationBuilder.CreateIndex(
                name: "IX_Nauczyciele_ID_UŻYTKOWNIKA",
                table: "Nauczyciele",
                column: "ID_UŻYTKOWNIKA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nauczyciele_ID_WYDZIAŁU",
                table: "Nauczyciele",
                column: "ID_WYDZIAŁU");

            migrationBuilder.CreateIndex(
                name: "IX_NauczycielPrzedmiot_PrzedmiotyIdPrzedmiotu",
                table: "NauczycielPrzedmiot",
                column: "PrzedmiotyIdPrzedmiotu");

            migrationBuilder.CreateIndex(
                name: "IX_Produkty_IdStołówki",
                table: "Produkty",
                column: "IdStołówki");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_ID_UŻYTKOWNIKA",
                table: "Profile",
                column: "ID_UŻYTKOWNIKA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrzedmiotStudent_StudenciIdStudenta",
                table: "PrzedmiotStudent",
                column: "StudenciIdStudenta");

            migrationBuilder.CreateIndex(
                name: "IX_Składniki_IdDania",
                table: "Składniki",
                column: "IdDania");

            migrationBuilder.CreateIndex(
                name: "IX_Stołówki_ID_BUDYNKU",
                table: "Stołówki",
                column: "ID_BUDYNKU");

            migrationBuilder.CreateIndex(
                name: "IX_Studenci_ID_GRUPY_STUDENCKIEJ",
                table: "Studenci",
                column: "ID_GRUPY_STUDENCKIEJ");

            migrationBuilder.CreateIndex(
                name: "IX_Studenci_ID_UŻYTKOWNIKA",
                table: "Studenci",
                column: "ID_UŻYTKOWNIKA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Studenci_IdKierunkuStudiów",
                table: "Studenci",
                column: "IdKierunkuStudiów");

            migrationBuilder.CreateIndex(
                name: "IX_Uprawnienia_IdRoli",
                table: "Uprawnienia",
                column: "IdRoli");

            migrationBuilder.CreateIndex(
                name: "IX_Zamówienia_ID_DANIA",
                table: "Zamówienia",
                column: "ID_DANIA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zamówienia_ID_DIETY",
                table: "Zamówienia",
                column: "ID_DIETY");

            migrationBuilder.CreateIndex(
                name: "IX_Zamówienia_ID_UŻYTKOWNIKA",
                table: "Zamówienia",
                column: "ID_UŻYTKOWNIKA");

            migrationBuilder.CreateIndex(
                name: "IX_Zamówienia_IdStołówki",
                table: "Zamówienia",
                column: "IdStołówki");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administratorzy");

            migrationBuilder.DropTable(
                name: "Alergeny");

            migrationBuilder.DropTable(
                name: "GrupaNauczyciel");

            migrationBuilder.DropTable(
                name: "GrupaStudent");

            migrationBuilder.DropTable(
                name: "Miejsca");

            migrationBuilder.DropTable(
                name: "NauczycielPrzedmiot");

            migrationBuilder.DropTable(
                name: "Produkty");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "PrzedmiotStudent");

            migrationBuilder.DropTable(
                name: "Składniki");

            migrationBuilder.DropTable(
                name: "Uprawnienia");

            migrationBuilder.DropTable(
                name: "Zamówienia");

            migrationBuilder.DropTable(
                name: "Grupy");

            migrationBuilder.DropTable(
                name: "Parkingi");

            migrationBuilder.DropTable(
                name: "TypyMiejsc");

            migrationBuilder.DropTable(
                name: "Nauczyciele");

            migrationBuilder.DropTable(
                name: "Przedmioty");

            migrationBuilder.DropTable(
                name: "Studenci");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Dania");

            migrationBuilder.DropTable(
                name: "Stołówki");

            migrationBuilder.DropTable(
                name: "Specjalizacje");

            migrationBuilder.DropTable(
                name: "Wydziały");

            migrationBuilder.DropTable(
                name: "GrupyStudenckie");

            migrationBuilder.DropTable(
                name: "KierunkiStudiów");

            migrationBuilder.DropTable(
                name: "Użytkownicy");

            migrationBuilder.DropTable(
                name: "Diety");

            migrationBuilder.DropTable(
                name: "Budynki");

            migrationBuilder.DropTable(
                name: "Kategorie");
        }
    }
}
