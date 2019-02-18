# EF Core Foreign Key Model

In this repository I test whether EF Core will create a foreign key by convention when
declaring a model which makes use of a navigation property and letting EF know which
end of the relationship is principal and which is dependant by declaring only a single
conventional ID property.

First I scaffold a .NET Core application and install EF Core. Next I create a database
to test with, utilizing SQL Server LocalDB.

```powershell
dotnet new console
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
sqllocaldb create ef_core_fk_model -s
```

With everything set up, let's create a database context class and model classes which
model a relationship that I suspect EF will create a FK for.

The result: EF does create FKs based on convention.

## Joins

Next we demonstrate what sort of connection EF uses when accessing the dependant through
principal and vice versa:

```sql
SELECT [user].[Id], [user].[CarId], [user].[Name], [user.Car].[Id], [user.Car].[Make], [user.Car].[Model]
FROM [Users] AS [user]
INNER JOIN [Cars] AS [user.Car] ON [user].[CarId] = [user.Car].[Id]
```

```sql
SELECT [car].[Id], [car].[Make], [car].[Model], [car.User].[Id], [car.User].[CarId], [car.User].[Name]
FROM [Cars] AS [car]
LEFT JOIN [Users] AS [car.User] ON [car].[Id] = [car.User].[CarId]
```
