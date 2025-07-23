# Instrukcja uruchomienia aplikacji ChatBotAI

Poni�ej znajdziesz kroki niezb�dne do uruchomienia pe�nej aplikacji, sk�adaj�cej si� z bazy danych, backendu (API) oraz interfejsu u�ytkownika.

---

## Informacje o architekturze i dalszym rozwoju

W dokumentacji projektu znajduje si� przyk�adowa **tablica Event Sourcing**, kt�ra obrazuje zdarzenia zachodz�ce w systemie. Mo�e ona pos�u�y� jako punkt odniesienia przy rozbudowie aplikacji oraz implementacji mechanizm�w rejestrowania i �ledzenia zmian w systemie.

Backend API zosta�o zaprojektowane zgodnie z zasadami **Clean Architecture**, z zastosowaniem wzorca **CQRS** oraz biblioteki **MediatR** jako mechanizmu mediatora. Ca�a struktura API zosta�a zaplanowana w spos�b modularny i abstrakcyjny, co u�atwia jej rozbudow� � dodawanie nowych funkcjonalno�ci nie wymaga modyfikacji istniej�cych komponent�w, lecz ich rozszerzania.

### Uwaga: elementy do zaimplementowania

Na obecnym etapie projekt nie zawiera jeszcze wielu kluczowych mechanizm�w produkcyjnych, kt�re nale�y doda� w kolejnych iteracjach:

- Obs�uga wyj�tk�w (global error handling)
- Walidacja danych wej�ciowych (np. za pomoc� FluentValidation)
- Mapowanie danych (np. z u�yciem AutoMapper)
- Testy jednostkowe (np. z u�yciem xUnit)

**ToDo**: Powy�sze funkcjonalno�ci s� przewidziane do implementacji w przysz�o�ci.

---

## 1. Baza danych

1. Utw�rz now� baz� danych (np. w SQL Server).
2. Skopiuj zawarto�� pliku: /Src/Database/Structures/structures.sql i wykonaj zapytanie SQL w utworzonej bazie aby doda� struktury.
3. Nast�pnie skopiuj zawarto�� pliku: /Src/Database/Data/data.sql i r�wnie� wykonaj zapytanie w tej samej bazie aby doda� wst�pne dane.

---

## 2. Backend (API)

1. Otw�rz projekt znajduj�cy si� w: /Src/Backend/ChatBotAI za pomoc� IDE (np. Visual Studio lub Rider).
2. W pliku `appsettings.json` zaktualizuj **Connection String** do bazy danych � podaj odpowiedni� nazw� bazy, u�ytkownika i has�o.
3. Aby korzysta� z funkcji OpenAI: - Skonfiguruj **User Secrets** projektu i dodaj tam sw�j `OpenAI API Secret Key` pod odpowiednim kluczem (np. `OpenAI:ApiKey`).
4. Uruchom projekt � API powinno by� dost�pne lokalnie (np. pod `https://localhost:5176`).

---

## 3. Frontend (Angular)

1. Otw�rz terminal/konsol� i przejd� do katalogu: /Src/Frontend/ChatBotAi
2. Wykonaj instalacj� zale�no�ci: npm ci
3. Uruchom frontendow� aplikacj�: ng serve
4. Aplikacja powinna by� dost�pna pod adresem: `http://localhost:4200`

---

## Uwagi ko�cowe

- Upewnij si�, �e API dzia�a i frontend ma poprawnie skonfigurowane po��czenie z backendem.
- W razie problem�w sprawd� komunikaty b��d�w w logach.
