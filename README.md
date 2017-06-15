# DorothyAndCockroaches2

## Team Details

Name | Student # | Github ID
--- | --- | ---
David Evans | 11885821 | divsyd
Mark Varney | 11418593 | MarkLark215
Zizhou Yao | 12207724 | NSNeil
Rifai Kelvin | 12197671 | Carnivalio
Sameer Joshi | . | 1992sam

### NEW THEME DOESN'T LOAD?!
Make sure you're not looking at 'cached' version of the site, press CTRL+F5 (works on most webbrowser)

### ATTENTION! ERROR 404
- When you run the project (with the views) you'll get error 404, that's totally normal, because we don't have default page yet so you have to navigate yourself
1. Client: localhost:PN/EnetClients
2. District: localhost:PN/EnetDistricts
3. User: localhost:PN/EnetUsers
4. Intervention: localhost:PN/EnetInterventions
5. Intervention Type: localhost:PN/EnetInterventionTypes

## Database setup

### Remove old identity database

Because we don't store our DB in git, the database must be manually deleted or we will get an error when ASP.NET Identity tries to initialise the DB.

1. Click Tools > Nuget package manager console.

2. Run the below commands

		PM> sqllocaldb.exe stop MSSQLLocalDB
		LocalDB instance "MSSQLLocalDB" stopped.

		PM> sqllocaldb.exe delete MSSQLLocalDB
		LocalDB instance "MSSQLLocalDB" deleted.

		PM> sqllocaldb.exe start MSSQLLocalDB
		LocalDB instance "MSSQLLocalDB" started.
		
3. Click View > SQL server object explorer

5. Find EnetIdentityDb, it could be under any (localdb)\Database

4. Right click database EnetIdentityDb > delete.

5. Save, close, and open Visual studio/project.

### Create ENETCare DB

1. Expand Solution explorer > ENETCareWebApp > APP_Data
2. Right click App_data > add > new item
3. Select Sql server database and give it the name of "ENETCare"
4. Double click the database (.mdf file) to open Server explorer (usually on the left side of the window)
5. Right click DB in server browser (ENETConnection) > new query
6. Paste the raw content of "EnetCare.sql" from git > execute query (use green play button on the top left corner)

### User accounts for testing

Please use the below user/pass for testing each user type. These should be added when you first run the project via UserManager.SetUpUser().

- Engineer
	Dean/123456
	Kim/123456
- Accountant
	Mark/123456

- Manager
	Sam/123456
	
### Handling unknown error from database
#### Model-First | Database-First
1. Delete the MDF file on App_Data
2. Clean project
3. Follow "Create ENETCare DB" steps (above)

#### Code-First | Class-First
1. Open "SQL Server Object Explorer"
2. Delete the current database

### Handling errors caused by old data

1. Delete/remove all the project data on your machine
2. Reload/Re-clone the project from Master
3. Re-do the database from the scratch
 
### Update NuGet Entity Framework 6.1.3
- (Or Install If you don't have one in your machine)
1. Open Visual Studio
2. Go to quick launch search box (or press CTRL+Q) and type "NuGet"
3. Click on "(Project) Manage NuGet Packages ..."
4. Click "Browse" on the newly opened window
5. Search for "EntityFramework"
6. Install the one by microsoft (Ver. 6.1.3 | Size: 30M)

### Other required packages
- Moq 4.7.10
- Owin
- Microsoft.Owin
- Microsoft.Owin.Host.SystemWeb
- Microsoft.AspNet.Identity.Core
- Microsoft.AspNet.Identity.EntityFramework
- Microsoft.AspNet.Identity.Owin

### Tricky library
- [Index(IsUnique = true)] << THIS REQUIRED EntityFramework.DLL
- Add reference to the EntityFramework.DLL manually

### Tricky create new (scaffolded) item
- If you're getting error when you're generating scaffolded items, try building your current project first
- NOTE: Scaffolded item generator generates things from your binary, not from your code

### Troubleshooting Identity/Owin packages

- If you experience a yellow screen of death related to identity or OWIN, try reload all packages from Nuget package manager console using the below and restart Visual Studio.

		Update-Package –reinstall

		
