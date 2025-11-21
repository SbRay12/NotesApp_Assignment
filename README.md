# NotesApp - ASP.NET Core MVC

## Quick start

1. Ensure .NET 8 SDK is installed.
2. Update `appsettings.json` connection string if necessary.
3. Restore and add EF migrations:
   ```
   dotnet restore
   dotnet tool install --global dotnet-ef
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
4. Run the app:
   ```
   dotnet run
   ```
5. Open `https://localhost:5001` (or the URL shown in console). Default route points to /Notes.

## What is included
- EF Core Code-First `Note` model
- `ApplicationDbContext`
- `NotesController` with Create/Edit/Delete

