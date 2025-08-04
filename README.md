# ChatBotAI Application Startup Guide

Below you will find the steps required to launch the complete application, consisting of the database, backend (API), and user interface.

---

## 🧱 Architecture and Future Development

The project documentation includes an example **Event Sourcing Table**, which illustrates the events occurring within the system. This can serve as a reference point for extending the application and implementing mechanisms for logging and tracking changes.

The backend API has been designed following the principles of **Clean Architecture**, using the **CQRS** pattern and the **MediatR** library as a mediator mechanism. The entire API structure is planned to be modular and abstract, making it easier to extend — adding new features does not require modifying existing components, but rather extending them.

### ⚠️ Note: Items Pending Implementation

At this stage, the project does not yet include several key production-grade mechanisms, which should be added in future iterations:

- Global exception handling
- Input data validation (e.g., using FluentValidation)
- Data mapping (e.g., with AutoMapper)
- Unit tests (e.g., using xUnit)

**ToDo**: The above features are planned for future implementation.

---

## 1️⃣ Database Setup

1. Create a new database (e.g., in SQL Server),
2. Copy the contents of the file: `/Src/Database/Structures/structures.sql` and execute the SQL script in your newly created database to create the structures,
3. Then, copy the contents of the file: `/Src/Database/Data/data.sql` and execute it in the same database to insert initial data.

---

## 2️⃣ Backend (API)

1. Open the project located at `/Src/Backend/ChatBotAI` using your IDE (e.g., Visual Studio or Rider),
2. In the `appsettings.json` file, update the **Connection String** to point to your database — specify the correct database name, username, and password,
3. To use OpenAI features:
   - Configure the project **User Secrets** and add your `OpenAI API Secret Key` under the appropriate key (e.g., `OpenAI:ApiKey`),
4. Run the project — the API should be available locally (e.g., at `https://localhost:5176`).

---

## 3️⃣ Frontend (Angular)

1. Open your terminal/console and navigate to: `/Src/Frontend/ChatBotAi`,
2. Install dependencies:
   ```bash
   npm ci
3. Start application:
   ```bash
   ng serve
