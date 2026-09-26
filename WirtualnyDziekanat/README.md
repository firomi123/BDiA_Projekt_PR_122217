# Wirtualny Dziekanat

University project - ASP.NET Core MVC (.NET 8) + Entity Framework Core + SQLite.

The application manages students, courses and grades. The user interface is in Polish;
the source code (names and comments) is in English.

## CQRS

The project follows the CQRS pattern (Command Query Responsibility Segregation):

- `Queries/` - queries that only read data (e.g. `GetStudentsQuery`)
- `Commands/` - commands that change data (e.g. `AddStudentCommand`, `DeleteGradeCommand`)
- `CQRS/Interfaces.cs` - the `IQueryHandler` and `ICommandHandler` interfaces

Every query and command has its own handler. Controllers never access the database directly,
they call the matching handler. Queries use `AsNoTracking()` because they do not change data.

## Running

Open `WirtualnyDziekanat.csproj` in Visual Studio 2026 and run it (F5).

Or from the command line:
```
dotnet run
```

The `dziekanat.db` database is created automatically on the first run.
