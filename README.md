# Instrukcja uruchomienia aplikacji ChatBotAI

Poni¿ej znajdziesz kroki niezbêdne do uruchomienia pe³nej aplikacji, sk³adaj¹cej siê z bazy danych, backendu (API) oraz interfejsu u¿ytkownika.

---

## Informacje o architekturze i dalszym rozwoju

W dokumentacji projektu znajduje siê przyk³adowa **tablica Event Sourcing**, która obrazuje zdarzenia zachodz¹ce w systemie. Mo¿e ona pos³u¿yæ jako punkt odniesienia przy rozbudowie aplikacji oraz implementacji mechanizmów rejestrowania i œledzenia zmian w systemie.

Backend API zosta³o zaprojektowane zgodnie z zasadami **Clean Architecture**, z zastosowaniem wzorca **CQRS** oraz biblioteki **MediatR** jako mechanizmu mediatora. Ca³a struktura API zosta³a zaplanowana w sposób modularny i abstrakcyjny, co u³atwia jej rozbudowê – dodawanie nowych funkcjonalnoœci nie wymaga modyfikacji istniej¹cych komponentów, lecz ich rozszerzania.

### Uwaga: elementy do zaimplementowania

Na obecnym etapie projekt nie zawiera jeszcze wielu kluczowych mechanizmów produkcyjnych, które nale¿y dodaæ w kolejnych iteracjach:

- Obs³uga wyj¹tków (global error handling)
- Walidacja danych wejœciowych (np. za pomoc¹ FluentValidation)
- Mapowanie danych (np. z u¿yciem AutoMapper)
- Testy jednostkowe (np. z u¿yciem xUnit)

**ToDo**: Powy¿sze funkcjonalnoœci s¹ przewidziane do implementacji w przysz³oœci.

---

## 1. Baza danych

1. Utwórz now¹ bazê danych (np. w SQL Server).
2. Skopiuj zawartoœæ pliku: /Src/Database/Structures/structures.sql i wykonaj zapytanie SQL w utworzonej bazie aby dodaæ struktury.
3. Nastêpnie skopiuj zawartoœæ pliku: /Src/Database/Data/data.sql i równie¿ wykonaj zapytanie w tej samej bazie aby dodaæ wstêpne dane.

---

## 2. Backend (API)

1. Otwórz projekt znajduj¹cy siê w: /Src/Backend/ChatBotAI za pomoc¹ IDE (np. Visual Studio lub Rider).
2. W pliku `appsettings.json` zaktualizuj **Connection String** do bazy danych – podaj odpowiedni¹ nazwê bazy, u¿ytkownika i has³o.
3. Aby korzystaæ z funkcji OpenAI: - Skonfiguruj **User Secrets** projektu i dodaj tam swój `OpenAI API Secret Key` pod odpowiednim kluczem (np. `OpenAI:ApiKey`).
4. Uruchom projekt – API powinno byæ dostêpne lokalnie (np. pod `https://localhost:5176`).

---

## 3. Frontend (Angular)

1. Otwórz terminal/konsolê i przejdŸ do katalogu: /Src/Frontend/ChatBotAi
2. Wykonaj instalacjê zale¿noœci: npm ci
3. Uruchom frontendow¹ aplikacjê: ng serve
4. Aplikacja powinna byæ dostêpna pod adresem: `http://localhost:4200`

---

## Uwagi koñcowe

- Upewnij siê, ¿e API dzia³a i frontend ma poprawnie skonfigurowane po³¹czenie z backendem.
- W razie problemów sprawdŸ komunikaty b³êdów w logach.
