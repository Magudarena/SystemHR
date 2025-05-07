# SystemHR


Magdalena Zawada
15142
lab3/2/AD


Projekt
 Systemu HR
System kadrowy do ewidencji i rozliczania urlopów, delegacji, nieobecności



 



Cel aplikacji
Stworzenie nowoczesnego i bezpiecznego systemu HR, który będzie służył do zarządzania danymi kadrowymi, ewidencjonowania urlopów, delegacji, nieobecności (w tym chorobowych) oraz umożliwi pracownikom dostęp do systemu przez portal WWW. System zapewni wysoki poziom bezpieczeństwa danych i funkcjonalności, szczególnie w zakresie ochrony danych osobowych zgodnie z przepisami RODO, a także będzie chronił przed zagrożeniami wynikającymi z cyberataków.
Dane
Platforma: ASP.NET Core
Środowisko: Visual Studio 2022
Wersja .NET: .NET 8.0 lub nowsza
Baza danych: SQL Server
Wymagania
Aby poprawnie uruchomić i korzystać z systemu, wymagane są następujące komponenty:
•	Visual Studio z zainstalowanymi dodatkami do programowania w ASP.NET oraz odpowiednie pakiety Entity Framework – Core i SQL server, wersja projektu: .NET 8
•	SQL Server lub kompatybilna baza danych, na której postawimy bazę z README.md
•	Repozytorium pobrane z https://github.com/Magudarena/SystemHR.git
•	Komputer z systemem operacyjnym Windows 
•	RAM i odrobinę dostępnego miejsca na dysku.
Instrukcja instalacji – może Pan pominąć
Instalacja nie będzie konieczna, bo umieściłam wszystkie widoki w dokumentacji, ale jakby Pan chciał to poniżej przedstawiam instalację:
1.	Najpierw należy pobrać repozytorium do swojego Visual Studio z mojego konta na githubie https://github.com/Magudarena/SystemHR.git
2.	Po tym należy włączyć Microsoft SQL Server Management Studio i wkleić w niego podany poniżej kod wprowadzając kolejne fragmenty kodu:
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
3.	Teraz można uruchomić program. Jeżeli nie łączy się z bazą danych, to należy jedynie zmienić nazwę bazy danych w pliku appsettings,json na nazwę serwera na którym znajduje się lokalna baza.
Schemat działania
System kadrowy SystemHR realizuje następujące funkcjonalności:
1.	Zarządzanie użytkownikami
•	Rejestracja nowych użytkowników
•	Logowanie użytkowników
•	Edytowanie profili użytkowników (np. zmiana danych osobowych lub ról)
2.	Zarządzanie pracownikami
•	Dodawanie nowych pracowników do bazy danych
•	Edytowanie danych pracowników (np. zmiana działu, stanowiska)
•	Usuwanie pracowników z systemu
3.	Zarządzanie urlopami
•	Składanie wniosków urlopowych przez pracowników
•	Przeglądanie i akceptowanie wniosków przez HR
•	Edytowanie i anulowanie wniosków urlopowych
•	Zarządzanie historią urlopów i nieobecności
4.	Zarządzanie delegacjami
•	Składanie wniosków delegacyjnych przez pracowników
•	Zatwierdzanie wyjazdów przez dział HR
•	Rozliczanie kosztów delegacji i generowanie raportów
5.	Raportowanie
•	Lista pracowników wraz z aktywnymi urlopami
•	Historia urlopów i nieobecności dla każdego pracownika
•	Generowanie raportów o delegacjach i wykorzystaniu dni wolnych
•	Eksport danych do formatu PDF i Excel
Opis funkcjonalności
1.	Zarządzanie użytkownikami
•	Rejestracja – Użytkownicy tworzą konto podając podstawowe dane, jak imię, nazwisko.
•	Logowanie – Użytkownicy logują się, a system zarządza sesjami i ich poziomem dostępu.
•	Edycja profilu – HR i administratorzy mogą edytować dane użytkowników oraz ich role.
•	Usuwanie – Możliwość usunięcia użytkowników z systemu przez administratora.
2.	Zarządzanie pracownikami
•	Dodawanie – Dział HR może dodawać nowych pracowników do bazy.
•	Edycja – HR może aktualizować dane pracowników (np. dział, stanowisko, uprawnienia).
•	Usuwanie – Możliwość usunięcia pracowników, np. po zakończeniu współpracy.
3.	Zarządzanie urlopami
•	Składanie wniosków urlopowych – Pracownicy mogą składać wnioski o urlop z datami.
•	Zarządzanie wnioskami – HR akceptuje lub odrzuca wnioski.
•	Anulowanie – Możliwość anulowania wniosku przez pracownika lub przez HR.
•	Przegląd historii – Możliwość przeglądania historii wniosków urlopowych pracownika.
4.	Zarządzanie delegacjami
•	Składanie wniosków – Pracownik wypełnia wniosek delegacyjny z danymi wyjazdu.
•	Zatwierdzanie – Dział HR zatwierdza delegację, a system rejestruje szczegóły wyjazdu.
•	Rozliczenie – Po zakończeniu delegacji system generuje raport kosztów.
5.	Raportowanie
•	Lista pracowników – Wyświetlanie listy pracowników oraz ich aktywnych urlopów.
•	Historia urlopów – Przegląd historii wniosków urlopowych i dni wolnych pracownika.
•	Raport o delegacjach – Generowanie raportów z zakończonych delegacji.
•	Eksport danych – Możliwość eksportu raportów w formatach PDF i Excel.
Architektura repozytorium
Projekt oparty jest na wzorcu Model-View-Controller, który dzieli aplikację na trzy główne części:
Kontrolery
Kontroler to warstwa, która pośredniczy pomiędzy użytkownikiem a danymi. Odpowiada za obsługę żądań HTTP i komunikację z modelem, a także przekazywanie danych do widoków. W projekcie znajdują się kontrolery takie jak:
•	HomeController.cs – Obsługuje stronę główną aplikacji (np. Index i Privacy).
•	KategorieController.cs – Zarządza kategoriami, dodawanie, usuwanie i wyświetlanie.
•	LogowanieController.cs – Odpowiada za logowanie użytkowników.
•	PracownicyController.cs – Obsługuje operacje na pracownikach.
•	RejestracjaController.cs – Obsługuje rejestrację nowych użytkowników.
•	UrlopyController.cs – Zarządza urlopami, dodawaniem, edytowaniem i przeglądaniem.
•	UzytkownicyController.cs – Zarządza listą użytkowników oraz ich operacjami.
•	WolneController.cs – Obsługuje zarządzanie dniami wolnymi pracowników.
•	WracanieController.cs – Odpowiada za operacje związane z powrotem użytkowników.
Modele
Model w tym systemie reprezentuje dane aplikacji i jest odpowiedzialny za interakcję z bazą danych. Model danych jest powiązany z Entity Framework Core, co pozwala na mapowanie obiektów na tabele w bazie danych.
•	BranieWolnego.cs – Model do obsługi operacji związanych z braniem dni wolnych.
•	ErrorViewModel.cs – Model do obsługi błędów w aplikacji.
•	Kategoria.cs – Model kategorii.
•	ListaPracownikow.cs – Model reprezentujący listę pracowników.
•	ListaUrlopow.cs – Model do obsługi listy urlopów.
•	LogowanieModel.cs – Model logowania użytkownika.
•	LogowanieViewModel.cs – Model widoku logowania.
•	Pracownik.cs – Model reprezentujący pracownika.
•	PracownikHR.cs – Model pracownika HR.
•	RegisterViewModel.cs – Model widoku rejestracji.
•	SystemHRContext.cs – Kontekst bazy danych aplikacji.
•	Uprawnienia.cs – Model obsługujący uprawnienia użytkowników.
•	Urlop.cs – Model urlopu.
•	UrlopPerPracownik.cs – Model relacji między urlopem a pracownikiem.
•	Wolne.cs – Model dotyczący dni wolnych.
•	Wracanie.cs – Model do obsługi powrotów użytkowników.
Widoki
Widoki są odpowiedzialne za wyświetlanie danych użytkownikowi. W tym projekcie wykorzystywane są Razor Views, które pozwalają na dynamiczne generowanie HTML z osadzonymi fragmentami C#. Widoki znajdują się w folderze Views.
1.	Home
•	Index.cshtml – Główna strona aplikacji, wyświetla wprowadzenie.
•	Privacy.cshtml – Strona polityki prywatności, zawiera informacje o systemie.
2.	Kategorie
•	Dodaj.cshtml – Formularz do dodawania nowej kategorii, np. typów urlopów lub grup.
•	Kategorie.cshtml – Widok wyświetlający listę kategorii z możliwością ich zarządzania.
•	Usun.cshtml – Widok potwierdzenia usunięcia kategorii.
3.	Logowanie 
•	Logowanie.cshtml – Formularz logowania, w którym użytkownik wprowadza swoje dane.
4.	Pracownicy
•	Edytuj.cshtml – Formularz edycji danych pracownika, np. zmiana stanowiska.
•	ListaPracownikow.cshtml – Widok listy pracowników z opcjami edycji i usuwania.
•	NowyPracownik.cshtml – Formularz do dodawania nowego pracownika.
•	UrlopyPracownika.cshtml – Widok szczegółowy urlopów konkretnego pracownika.
•	Usun.cshtml – Widok potwierdzenia usunięcia danych pracownika.
5.	Rejestracja
•	Rejestracja.cshtml – Formularz rejestracji nowego użytkownika.
6.	Shared 
•	Error.cshtml – Widok błędów, w przypadku wystąpienia nieoczekiwanych problemów.
•	_Layout.cshtml – Główny szablon aplikacji, który definiuje układ interfejsu.
•	_ValidationScriptsPartial.cshtml – Widok włączający skrypty walidacji po stronie klienta.
7.	Urlopy
•	Edytuj.cshtml – Formularz edycji danych dotyczących urlopu, np. zmiana daty.
•	ListaUrlopow.cshtml – Widok wyświetlający listę urlopów z opcjami edycji i usuwania.
•	NowyUrlop.cshtml – Formularz do dodawania nowego urlopu dla pracownika.
•	Usun.cshtml – Widok potwierdzający usunięcie urlopu z systemu.
•	Wroc.cshtml – Widok powrotu z urlopu, umożliwiający oznaczenie, że pracownik zakończył swój urlop.
8.	Uzytkownicy
•	Uzytkownicy.cshtml – Widok wyświetlający listę użytkowników z opcjami zarządzania.
9.	Wolne
•	BladPracownika.cshtml – Widok, gdy wystąpi problem związany z operacjami.
•	PodsumowanieUrlopu.cshtml – wykorzystane i dostępne dni urlopu danego pracownika.
•	Wolne.cshtml – Formularz zarządzania dniami wolnymi pracowników.
•	WybierzPracownika.cshtml – Widok umożliwiający wybór pracownika do operacji.
10.	Wracanie 
•	Wracanie.cshtml – Widok obsługujący powrót pracownika do pracy po urlopie.
•	Wroc.cshtml – Formularz umożliwiający zapisanie szczegółów związanych z powrotem.
Baza danych
Baza danych SystemHR została zaprojektowana do zarządzania zasobami kadrowymi w organizacji, w tym pracownikami, urlopami, kategoriami oraz użytkownikami systemu. Baza danych zapewnia integralność danych, wykorzystując klucze obce do utrzymania spójności między tabelami np. pracownicy są przypisani do działów, urlopy są powiązane z konkretnymi pracownikami. Umożliwia przypisywanie użytkownikom różnych poziomów uprawnień, co pozwala na kontrolę dostępu do danych. Oto szczegółowa analiza struktury bazy:
Tabele w bazie danych
•	kategoria – przechowuje nazwy kategorii urlopowych (np. „Urlop wypoczynkowy”).
•	Pracownik – zawiera dane pracowników (imię, nazwisko, telefon, e-mail).
•	Urlop – przechowuje informacje o rodzajach wolnego (nr identyfikacyjny, nazwa, kategoria).
•	uprawnienia – typowe role: Administrator, HR, Pracownik.
•	pracownikhr – pracownicy odpowiedzialni za system HR z uprawnieniami do zarządzania.
•	wolne – rejestr powiązań między Urlop a Pracownik (kto i kiedy korzystał z urlopu).
Widoki w bazie danych
•	UrlopPerPracownik – prezentacja danych na temat urlopów wraz z informacją o pracowniku.
•	Listapracownikow – pokazuje pracowników oraz liczbę aktywnych wniosków urlopowych.
•	ListaUrlopow – lista wszystkich urlopów z kategorią i informacją o dostępności.
 

Procesy
System SystemHR zarządza wieloma procesami, które są realizowane w sposób sekwencyjny:
1.	Nowy użytkownik trafia na stronę główną gdzie może się zalogować, zarejestrować lub przeczytać politykę prywatności firmy.
 
2.	Rejestracja i logowanie użytkowników - Po rejestracji użytkownik może się zalogować. 
•	Rejestracja
 
•	Logowanie
 
3.	Zarządzanie urlopami i nieobecnościami - Pracownicy mogą składać wnioski urlopowe, które trafiają do działu HR do akceptacji. Po zatwierdzeniu system rejestruje nieobecność w harmonogramie. Możliwe jest również anulowanie wniosków przed ich rozpoczęciem.
 
4.	Pracownicy HR mogą modyfikować rodzaje urlopów, edytować lub usuwać.
 
5.	Lista pracowników jest dostępna dla osób pracujących w HR, a pracownik może wyświetlić jedynie swoje urlopy.
 
6.	Zarządzanie pracownikami –System przydziela automatycznie podstawową rolę „Pracownik” przy zakładaniu konta, którą może zmienić tylko administrator przyznając uprawnienia wyższe rangą takie jak „HR” czy „Administrator”.
 
•	Administrator ma dostęp do wszystkiego, włącznie z zarządzaniem dostępami.
 
•	Pracownik HR ma dostęp do zarządzania danymi pracowników i ich urlopami.
 
•	Zalogowany użytkownik ma dostęp do swoich danych oraz możliwych opcji.
 
•	Użytkownik niezalogowany ma jedynie dostęp do logowania i polityki prywatności.
 
7.	Zarządzanie HR- Pracownicy mogą zgłaszać wyjazdy służbowe, a HR zatwierdza je oraz rejestruje w systemie. HR może również dodawać rodzaje wolnego.
 
8.	HR może również zarządzać typami wolnych dni, dzięki czemu możliwe jest oddzielanie ich do poszczególnych kategorii.
