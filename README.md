# SystemHR
Magdalena Zawada 15142 lab3/2/AD

Projekt Systemu HR - System kadrowy do ewidencji i rozliczania urlopów, delegacji, nieobecności

Cel aplikacji
Stworzenie nowoczesnego i bezpiecznego systemu HR, który będzie służył do zarządzania danymi kadrowymi, ewidencjonowania urlopów, delegacji, nieobecności (w tym chorobowych) oraz umożliwi pracownikom dostęp do systemu przez portal WWW.

Instrukcja instalacji:
1. Najpierw należy pobrać repozytorium do swojego Visual Studio z mojego konta na githubie https://github.com/Magudarena/SystemHR.git
2. Po tym należy włączyć Microsoft SQL Server Management Studio i wkleić w niego podany poniżej kod wprowadzając kolejne fragmenty kodu:

```sql
--Skrypt do utworzenia bazy danych
use master;
create database SystemHR;

--Utworzenie tabel w bazie
use SystemHR;

CREATE TABLE kategoria (
  id int IDENTITY(1,1) PRIMARY KEY,
  nazwa varchar(40) COLLATE Polish_CI_AS UNIQUE,);

CREATE TABLE Pracownik (
  id int IDENTITY(1,1) PRIMARY KEY,
  imie varchar(20),
  nazwisko varchar(40),
  telefon varchar(9) UNIQUE,
  email varchar(60) UNIQUE);

CREATE TABLE Urlop (
  id int IDENTITY(1,1) PRIMARY KEY,
  nr_identyfikacyjny varchar(10) UNIQUE,
  nazwa_wolnego varchar(100),
  dane_wolnego varchar(60),
  Identyfikator varchar(13),
  kategoria int FOREIGN KEY REFERENCES kategoria(id),
  dostepne bit);

CREATE TABLE uprawnienia (
  id int IDENTITY(1,1) PRIMARY KEY,
  nazwa varchar(30));

CREATE TABLE pracownikhr (
  id int IDENTITY(1,1) PRIMARY KEY,
  email varchar(60) UNIQUE,
  haslo varchar(100),
  imie varchar(20),
  nazwisko varchar(40),
  id_uprawnienia int FOREIGN KEY REFERENCES uprawnienia(id));

CREATE TABLE wolne (
  id int IDENTITY(1,1) PRIMARY KEY,
  id_Urlop int FOREIGN KEY REFERENCES Urlop(id),
  id_pracownik int FOREIGN KEY REFERENCES pracownik(id),
  poczatek_wolnego datetime,
  koniec_wolnego datetime);

--Po utworzeniu bazy należy utworzyć widoki

create view UrlopPerPracownik as
select w.id as "id_wolne", p.id as "id_pracownik", u.id as "id_Urlop", u.nr_identyfikacyjny, u.nazwa_wolnego, u.dane_wolnego, u.identyfikator, w.poczatek_wolnego, w.koniec_wolnego
from wolne w
right join pracownik p ON w.id_pracownik = p.id
left join urlop u on w.id_Urlop = u.id;

Create View Listapracownikow as
select k.id, k.imie as "imie", k.nazwisko as "nazwisko", k.telefon as "telefon", k.email as "email", (count(w.poczatek_wolnego) - count(w.koniec_wolnego)) as "wziete" from wolne w
right join pracownik k on k.id = w.id_pracownik
group by k.id, k.imie, k.nazwisko, k.telefon, k.email;

create view ListaUrlopow as
select ks.id, ks.nr_identyfikacyjny, ks.nazwa_wolnego, ks.dane_wolnego, ks.identyfikator, kt.nazwa as "kategoria", ks.dostepne from Urlop ks
join kategoria kt on ks.kategoria = kt.id;

--Należy dodać poziomy uprawnień użytkowników

INSERT INTO uprawnienia (nazwa) VALUES ('Administrator');
INSERT INTO uprawnienia (nazwa) VALUES ('HR');
INSERT INTO uprawnienia (nazwa) VALUES ('Pracownik');

--Przykładowe dane

INSERT INTO pracownikhr (email, haslo, imie, nazwisko, id_uprawnienia)
VALUES
('superadmin@example.com', 'AQAAAAIAAYagAAAAEO2rrTz9lR5MmUbI3dz1BjQmjVmk0fzyvJri2sycGyizeRqxLr+1kLj1xuvfZfPsPg==', 'Super', 'Admin', 1),
('panizhr@example.com', 'AQAAAAIAAYagAAAAEO2rrTz9lR5MmUbI3dz1BjQmjVmk0fzyvJri2sycGyizeRqxLr+1kLj1xuvfZfPsPg==', 'Anna', 'HRka', 2),
('szarykowalski@example.com', 'AQAAAAIAAYagAAAAEO2rrTz9lR5MmUbI3dz1BjQmjVmk0fzyvJri2sycGyizeRqxLr+1kLj1xuvfZfPsPg==', 'Szary', 'Kowalski', 3);

INSERT INTO kategoria (nazwa) VALUES ( 'Urlop');
INSERT INTO kategoria (nazwa) VALUES ( 'Delegacja');
INSERT INTO kategoria (nazwa) VALUES ( 'Nieobecność');

INSERT INTO Urlop (nr_identyfikacyjny, nazwa_wolnego, dane_wolnego, Identyfikator, kategoria, dostepne) VALUES
('U001', 'Urlop wypoczynkowy', 'Urlop', 'ID001', 1, 1),
('U002', 'Urlop macierzyński', 'Urlop', 'ID002', 1, 1),
('U003', 'Urlop ojcowski', 'Urlop', 'ID003', 1, 1),
('U004', 'Urlop wychowawczy', 'Urlop', 'ID004', 1, 1),
('U005', 'Urlop na żądanie', 'Urlop', 'ID005', 1, 1),
('U006', 'Urlop bezpłatny', 'Urlop', 'ID006', 1, 1),
('U007', 'L4 (chorobowe)', 'Urlop', 'ID007', 1, 1),
('U008', 'Urlop rehabilitacyjny', 'Urlop', 'ID008', 1, 1),
('U009', 'Urlop na opiekę nad dzieckiem', 'Urlop', 'ID009', 1, 1),
('U010', 'Dzień wolny z okazji ślubu', 'Urlop', 'ID010', 1, 1),
('U011', 'Dzień wolny z okazji narodzin dziecka', 'Urlop', 'ID011', 1, 1),
('U012', 'Dzień wolny z okazji pogrzebu', 'Urlop', 'ID012', 1, 1),
('U013', 'Urlop na badania lekarskie', 'Urlop', 'ID013', 1, 1),
('U014', 'Urlop na wizytę lekarską', 'Urlop', 'ID014', 1, 1),
('U015', 'Przerwa na karmienie', 'Urlop', 'ID015', 1, 1),
('U016', 'Dzień wolny z okazji rocznicy pracy', 'Urlop', 'ID016', 1, 1),
('U017', 'Urlop z okazji obrony pracy dyplomowej', 'Urlop', 'ID017', 1, 1),
('U018', 'Urlop z powodu wypadku przy pracy', 'Urlop', 'ID018', 1, 1),
('U019', 'Urlop z okazji długotrwałego leczenia', 'Urlop', 'ID019', 1, 1),
('U020', 'Urlop w związku z wojskiem', 'Urlop', 'ID020', 1, 1),
('U021', 'Urlop okolicznościowy', 'Urlop', 'ID021', 1, 1),
('U022', 'Urlop z powodu obowiązków rodzinnych', 'Urlop', 'ID022', 1, 1),
('U023', 'Urlop na udział w szkoleniu zawodowym', 'Urlop', 'ID023', 1, 1),
('U024', 'Urlop na odbycie kwarantanny', 'Urlop', 'ID024', 1, 1),
('U025', 'Urlop z powodu niepełnosprawności', 'Urlop', 'ID025', 1, 1),
('U026', 'Urlop na czas awarii sprzętu biurowego', 'Urlop', 'ID026', 1, 1),
('U027', 'Urlop z powodu świąt religijnych', 'Urlop', 'ID027', 1, 1),
('U028', 'Delegacje', 'Delegacja', 'ID028', 2, 1),
('U029', 'Delegacja krajowa', 'Delegacja', 'ID029', 2, 1),
('U030', 'Delegacja zagraniczna', 'Delegacja', 'ID030', 2, 1),
('U031', 'Delegacja szkoleniowa', 'Delegacja', 'ID031', 2, 1),
('U032', 'Delegacja konferencyjna', 'Delegacja', 'ID032', 2, 1),
('U033', 'Delegacja służbowa', 'Delegacja', 'ID033', 2, 1),
('U034', 'Delegacja audytowa', 'Delegacja', 'ID034', 2, 1),
('U035', 'Delegacja projektowa', 'Delegacja', 'ID035', 2, 1),
('U036', 'Delegacja konsultingowa', 'Delegacja', 'ID036', 2, 1),
('U037', 'Nieobecności', 'Nieobecność', 'ID037', 3, 1),
('U038', 'Nieobecność usprawiedliwiona', 'Nieobecność', 'ID038', 3, 1),
('U039', 'Nieobecność nieusprawiedliwiona', 'Nieobecność', 'ID039', 3, 0),
('U040', 'Nieobecność z przyczyn losowych', 'Nieobecność', 'ID040', 3, 1),
('U041', 'Nieobecność z powodu opieki nad członkiem rodziny', 'Nieobecność', 'ID041', 3, 1),
('U042', 'Nieobecność z powodu zdarzenia losowego', 'Nieobecność', 'ID042', 3, 1),
('U043', 'Nieobecność z powodu awarii sprzętu', 'Nieobecność', 'ID043', 3, 1),
('U044', 'Nieobecność z powodu świąt religijnych', 'Nieobecność', 'ID044', 3, 1),
('U045', 'Nieobecność z powodu pogrzebu', 'Nieobecność', 'ID045', 3, 1),
('U046', 'Nieobecność z powodu przesunięcia pracy', 'Nieobecność', 'ID046', 3, 1),
('U047', 'Nieobecność z powodu wypadku w pracy', 'Nieobecność', 'ID047', 3, 1),
('U048', 'Nieobecność z powodu urodzin dziecka', 'Nieobecność', 'ID048', 3, 1),
('U049', 'Nieobecność z powodu obrony pracy dyplomowej', 'Nieobecność', 'ID049', 3, 1);

INSERT INTO Pracownik (imie, nazwisko, telefon, email)
VALUES
('Jan', 'Nowak', '062978034', 'jan.nowak@wp.pl'),
('Jakub', 'Wiśniewski', '645371283', 'jakub.wisniewski@interia.pl'),
('Magdalena', 'Kowalska', '041852072', 'magdalena.kowalska@o2.pl'),
('Łukasz', 'Wójcik', '664500731', 'lukasz.wojcik@wp.pl'),
('Emilia', 'Kaczmarek', '053120453', 'emilia.kaczmarek@interia.pl'),
('Łukasz', 'Perez', '789540376', 'lukasz.perez@poczta.fm'),
('Zofia', 'Jankowska', '654387861', 'zofia.jankowska@interia.pl'),
('Karolina', 'Szymańska', '073452718', 'karolina.szymanska@o2.pl'),
('Jakub', 'Bąk', '873649058', 'jakub.bak@wp.pl'),
('Michał', 'Kaczmarek', '912567987', 'michal.kaczmarek@poczta.fm'),
('Jacek', 'Gonza', '100983452', 'jacek.gonza@interia.pl'),
('Sebastian', 'Clark', '554876443', 'sebastian.clark@o2.pl'),
('Grazyna', 'Walker', '689492748', 'grazyna.walker@poczta.fm'),
('Leon', 'Allen', '377154029', 'leon.allen@wp.pl'),
('Katarzyna', 'Wróblewska', '688749123', 'katarzyna.wroblewska@interia.pl'),
('Piotr', 'Zieliński', '743052647', 'piotr.zielinski@o2.pl'),
('Monika', 'Bielawska', '702471930', 'monika.bielawska@interia.pl'),
('Tomasz', 'Kozłowski', '734052876', 'tomasz.kozlowski@wp.pl'),
('Aneta', 'Jabłońska', '603527891', 'aneta.jablonska@poczta.fm'),
('Paweł', 'Ławniczak', '704123586', 'pawel.lawniczak@interia.pl'),
('Adam', 'Duda', '732684539', 'adam.duda@o2.pl');
```

3. Teraz można uruchomić program. Jeżeli nie łączy się z bazą danych, to należy jedynie zmienić nazwę bazy danych w pliku appsettings,json na nazwę serwera na którym znajduje się lokalna baza.
   Schemat działania
   System kadrowy SystemHR realizuje następujące funkcjonalności:
1. Zarządzanie użytkownikami
   • Rejestracja nowych użytkowników
   • Logowanie użytkowników
   • Edytowanie profili użytkowników (np. zmiana danych osobowych lub ról)
1. Zarządzanie pracownikami
   • Dodawanie nowych pracowników do bazy danych
   • Edytowanie danych pracowników (np. zmiana działu, stanowiska)
   • Usuwanie pracowników z systemu
1. Zarządzanie urlopami
   • Składanie wniosków urlopowych przez pracowników
   • Przeglądanie i akceptowanie wniosków przez HR
   • Edytowanie i anulowanie wniosków urlopowych
   • Zarządzanie historią urlopów i nieobecności
1. Zarządzanie delegacjami
   • Składanie wniosków delegacyjnych przez pracowników
   • Zatwierdzanie wyjazdów przez dział HR
   • Rozliczanie kosztów delegacji i generowanie raportów
1. Raportowanie
   • Lista pracowników wraz z aktywnymi urlopami
   • Historia urlopów i nieobecności dla każdego pracownika
   • Generowanie raportów o delegacjach i wykorzystaniu dni wolnych
   • Eksport danych do formatu PDF i Excel
   Opis funkcjonalności
1. Zarządzanie użytkownikami
   • Rejestracja – Użytkownicy tworzą konto podając podstawowe dane, jak imię, nazwisko.
   • Logowanie – Użytkownicy logują się, a system zarządza sesjami i ich poziomem dostępu.
   • Edycja profilu – HR i administratorzy mogą edytować dane użytkowników oraz ich role.
   • Usuwanie – Możliwość usunięcia użytkowników z systemu przez administratora.
1. Zarządzanie pracownikami
   • Dodawanie – Dział HR może dodawać nowych pracowników do bazy.
   • Edycja – HR może aktualizować dane pracowników (np. dział, stanowisko, uprawnienia).
   • Usuwanie – Możliwość usunięcia pracowników, np. po zakończeniu współpracy.
1. Zarządzanie urlopami
   • Składanie wniosków urlopowych – Pracownicy mogą składać wnioski o urlop z datami.
   • Zarządzanie wnioskami – HR akceptuje lub odrzuca wnioski.
   • Anulowanie – Możliwość anulowania wniosku przez pracownika lub przez HR.
   • Przegląd historii – Możliwość przeglądania historii wniosków urlopowych pracownika.
1. Zarządzanie delegacjami
   • Składanie wniosków – Pracownik wypełnia wniosek delegacyjny z danymi wyjazdu.
   • Zatwierdzanie – Dział HR zatwierdza delegację, a system rejestruje szczegóły wyjazdu.
   • Rozliczenie – Po zakończeniu delegacji system generuje raport kosztów.
1. Raportowanie
   • Lista pracowników – Wyświetlanie listy pracowników oraz ich aktywnych urlopów.
   • Historia urlopów – Przegląd historii wniosków urlopowych i dni wolnych pracownika.
   • Raport o delegacjach – Generowanie raportów z zakończonych delegacji.
   • Eksport danych – Możliwość eksportu raportów w formatach PDF i Excel.
