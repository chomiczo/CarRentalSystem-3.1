Dokumentacja Techniczna - CarRentalSystem
Wprowadzenie
CarRentalSystem to aplikacja webowa umożliwiająca zarządzanie wypożyczalnią samochodów. Projekt wykorzystuje technologie ASP.NET Core 6.0, Entity Framework oraz Microsoft SQL Server.

Struktura Projektu
Aplikacja składa się z trzech głównych komponentów:
CarRentalSystem: Główny projekt ASP.NET Core.
CarRentalSystem.Data: Obsługa dostępu do danych, zawierająca definicje modeli danych, kontekst bazy danych oraz inicjalizację danych.
CarRentalSystem.Models: Modele danych używane w projekcie.

Technologie
ASP.NET Core 6.0: Framework do budowania aplikacji internetowych.
Entity Framework Core: Biblioteka ORM do obsługi bazy danych.
Microsoft SQL Server: System zarządzania bazą danych.
HTML, CSS, JavaScript: Technologie front-endowe do tworzenia interfejsu użytkownika.

Instrukcje Uruchomienia
1.	Sklonuj repozytorium z GitHub.
2.	Otwórz projekt w środowisku programistycznym, np. Visual Studio 2022.
3.	Uruchom konsolę menadżera pakietów i wykonaj migracje, aby zainicjować bazę danych oraz jej aktualizację. Poniżej pomocnicze komendy:
add-migration nazwa_migracji
Update-Database
4.	Uruchom projekt!

Struktura Bazy Danych
Baza danych zawiera trzy główne tabele:
CarModels: Informacje o modelach samochodów, takie jak marka, model, rok produkcji, itp.
CustomerModels: Dane klientów, takie jak imię, nazwisko, adres email, itp.
RentalModels: Reprezentacja wypożyczeń samochodów. Zawiera informacje, takie jak data wypożyczenia, data zwrotu, itp.

Instrukcje Użycia
Po utworzeniu konta za pomocą rejestracji:
Strona Główna: Użytkownik przegląda zdjęcia dostępnych modeli samochodów.
Model Samochodu: Dodanie modeli oraz modyfikacja samochodów jest dostępna tylko dla statycznego konta Administratora.
Wynajęte Modele: Po zalogowaniu użytkownik rezerwuje wybrany samochód, wybierając opcję "Wynajmij". Zalogowani użytkownicy zarządzają swoimi wypożyczeniami w panelu użytkownika.
Klient: Użytkownik wypełnia dane personalne w formularzu, aby wynająć samochód. 

Dodatkowe informacje
Dodano statyczne konto Administratora
Aby zalogować się na konto Administratora w celu zarządzania dostępnością samochodów na podstronie Modele samochodów należy wpisać prawidłowo dane logowania:
Email: admin@admin.com
Hasło: Admin123$
Wprowadzono zabezpieczenia następujące:
•	Normalny użytkownik nie ma możliwości do wszelkiego rodzaju modyfikacji na podstronie Model samochodu, tylko może Administrator.
•	Niezarejestrowany użytkownik nie ma wglądu do podstron Wynajęte modele oraz Klient, musi się najpierw zarejestrować
•	Bez wypełnionego formularzu nie ma możliwości by zarejestrowany użytkownik mógł wynająć samochód.
•	Podczas rejestracji użytkownik jest zmuszony skonstruować takie hasło, aby zawierało przynajmniej jedną dużą literę, liczbę oraz znak specjalny.
•	

Potencjalne Usprawnienia
Aplikacja posiada autoryzację za pomocą ASP.NET Identity.
Dodano również System.ComponentModel.DataAnnotations do definiowania reguł walidacji i metadanych w kontekście modeli danych.

Autor
Aleksander Chomicz

