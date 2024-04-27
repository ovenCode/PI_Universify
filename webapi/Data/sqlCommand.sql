


INSERT INTO Uzytkownicy
    (IMIE, NAZWISKO, MAIL, HASLO, GRUPA, NUMER_TEL, ID_BUDYNKU,TYP_UZYTKOWNIKA,ID_ADMINISTRATORA,ID_ROLI,ID_NAUCZYCIELA,ID_WYDZIALU,ID_SPECJALIZACJI,ID_STUDENTA,ID_GRUPY_STUDENCKIEJ,ID_KIERUNKU_STUDIOW,ROK_STUDIOW)
VALUES
    ('Jan', 'Kowalski', 'jkowalski@gmail.com', 'htupsju', NULL, NULL, NULL,"Student", NULL, NULL, NULL, NULL, NULL, 1, 1, 1, 2),
    ('Magda', 'Karet', 'mkaret@gmail.com', 'fsdauyb', NULL, NULL, NULL, "Student", NULL, NULL, NULL, NULL, NULL, 2, 2, 1, 2),
    ('Filip', 'Mielski', 'fmielski@gmail.com', 'hdgdfnjkuy', NULL, NULL, NULL, "Nauczyciel", NULL, NULL, 1, 1, 1, NULL, NULL, NULL, NULL),
    ('Konrad', 'Wikrat', 'kwikrat@gmail.com', 'rthfgsdfgt', NULL, NULL, NULL, "Administrator", 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);


INSERT INTO Administratorzy
    (ID_UZYTKOWNIKA, ID_ROLI)
VALUES
    (4, 1);

INSERT INTO Alergeny
    (IdDiety, NAZWA)
VALUES
    (1, 'Gluten');

INSERT INTO Budynki
VALUES
    (1, 'Gmach główny uczelni', 'ul. X 12, 00-320 Warszawa');

INSERT INTO Dania
    (NAZWA, ILOSC_KALORII, ID_SKLADNIKA, ID_DIETY)
VALUES
    ('Spaghetti Bolognese', 850, 1, 1);

INSERT INTO Diety
    (NAZWA, ID_KATEGORII, ID_ALERGENU, CENA, ID_DANIA)
VALUES
    ('Standardowe menu 1', 1, 1, 800, 1);

INSERT INTO Grupy
    (NAZWA)
VALUES
    ('Samorząd Studencki');

INSERT INTO GrupyStudenckie
    (NAZWA)
VALUES
    ('I404');

INSERT INTO Kategorie
    (NAZWA)
VALUES
    ('Kuchnia włoska');

INSERT INTO KierunkiStudiów
VALUES
    (1, 'Inżynieria Matematyki Stosowanej');

INSERT INTO Miejsca
    (IdParkingu, DOSTEPNOSC, ID_TYPU)
VALUES
    (1, 1, 1);

INSERT INTO Parkingi
    (ADRES)
VALUES
    ('ul. X 10, 00-320 Warszawa');

INSERT INTO Produkty
    (NAZWA, ILOSC_PRODUKTU, JEDNOSTKA, IdStolówki)
VALUES
    ('Marchew', 20, 'kg', 1);

INSERT INTO Profile
    (ID_UZYTKOWNIKA, OBRAZ_PROFILU, PASEK_PROFILU, GLOWNA_ZAWARTOSC)
VALUES
    (1, 'https://localhost:3000/api/assets/basic_temp.jpg', 'TEST SIDEBAR', 'TEST CONTENT'),
    (2, 'https://localhost:3000/api/assets/basic_temp.jpg', 'TEST SIDEBAR', 'TEST CONTENT'),
    (3, 'https://localhost:3000/api/assets/basic_temp.jpg', 'TEST SIDEBAR', 'TEST CONTENT'),
    (4, 'https://localhost:3000/api/assets/basic_temp.jpg', 'TEST SIDEBAR', 'TEST CONTENT');

INSERT INTO Przedmioty
    (NAZWA, KATEGORIA, SEMESTR_ROZPOCZECIA, ILOSC_SEMESTROW)
VALUES
    ('Matematyka Stosowana', 'Matematyka', 1, 4);

INSERT INTO Role
    (NAZWA, ID_UPRAWNIENIA)
VALUES
    ('Administrator systemu', 1);

INSERT INTO GrupaNauczyciel
    (GrupyIdGrupy, NauczycieleIdNauczyciela)
VALUES
    (1, 1);

INSERT INTO GrupaStudent
    (GrupyIdGrupy, StudenciIdStudenta)
VALUES
    (1, 1);

INSERT INTO NauczycielPrzedmiot
    (NauczycieleIdNauczyciela, PrzedmiotyIdPrzedmiotu)
VALUES
    (1, 1);

INSERT INTO PrzedmiotStudent
    (PrzedmiotyIdPrzedmiotu, StudenciIdStudenta)
VALUES
    (1, 1);

INSERT INTO Specjalizacje
    (NAZWA)
VALUES
    ('Abstrakcje matematyczne');

INSERT INTO TypyMiejsc
    (TYP_MIEJSCA)
VALUES
    ('Standardowe');

INSERT INTO Uprawnienia
    (IdRoli, NAZWA)
VALUES
    (1, 'ZU'),
    -- Zarządzanie użytkownikami
    (1, 'ZP'),
    -- Zarządzanie przedmiotami
    (1, 'ZPK'),
    -- Zarządzanie parkingami
    (1, 'ZS');
-- Zarządzanie stołówkami

INSERT INTO Skladniki
    (IdDania, NAZWA)
VALUES
    (1, 'Marchew'),
    (1, 'Makaron'),
    (1, 'Sos pomidorowy'),
    (1, 'Mięso wołowe');

INSERT INTO Stolówki
    (ID_BUDYNKU, ID_ZAMOWIENIA, ID_PRODUKTU, INFORMACJE_DODATKOWE)
VALUES
    (1, 1, 1, 'Stołówka 1');

INSERT INTO Wydzialy
    (NAZWA)
VALUES
    ('Wydział Matematyki i Fizyki');

INSERT INTO Zamówienia
    (NAZWA, ID_UZYTKOWNIKA, IdStolówki, ID_DIETY, ID_DANIA, DZIEN_ZAMOWIENIA)
VALUES
    ('Spaghetti Bolognese', 1, 1, 1, 1, date()),
    ('Spaghetti Bolognese', 2, 1, 1, 1, date());

INSERT INTO Studenci
    (ID_UZYTKOWNIKA, ID_PRZEDMIOTU, ID_GRUPY_STUDENCKIEJ, IdKierunkuStudiów, ROK_STUDIOW)
VALUES
    (1,
        ( SELECT ID_PRZEDMIOTU
        FROM Przedmioty
        WHERE ID_PRZEDMIOTU = 1),
        ( SELECT ID_GRUPY
        FROM GrupyStudenckie
        WHERE ID_GRUPY = 1),
        ( SELECT ID_KIERUNKU_STUDIOW
        FROM KierunkiStudiów
        WHERE ID_KIERUNKU_STUDIOW = 1),
        2),
    (2,
        ( SELECT ID_PRZEDMIOTU
        FROM Przedmioty
        WHERE ID_PRZEDMIOTU = 1),
        ( SELECT ID_GRUPY
        FROM GrupyStudenckie
        WHERE ID_GRUPY = 2),
        ( SELECT ID_KIERUNKU_STUDIOW
        FROM KierunkiStudiów
        WHERE ID_KIERUNKU_STUDIOW = 1),
        2);

INSERT INTO Nauczyciele
    (ID_UZYTKOWNIKA, ID_WYDZIALU, ID_SPECJALIZACJI)
VALUES
    (
        3,
        1,
        1
    );
