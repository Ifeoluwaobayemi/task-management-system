﻿Prerequisites:
 
.NET Core SDK installed on your machine.
Visual Studio (optional but recommended) or any code editor of your choice.
Git for version control.
Steps to Run the Application Locally:
Backend (ASP.NET Core):

Clone the repository:
git clone <repository-url>
cd <repository-folder>
Navigate to the Backend folder:
cd Backend

Open the solution in Visual Studio or your preferred code editor.

Set up the database:

a. Open appsettings.json and modify the connection string as needed.

b. Open the Package Manager Console and run migrations to create the database:

Update-Database
Run the API:

a. Press F5 in Visual Studio or run the following command in the terminal:

dotnet run
The API should be running at https://localhost:5001.

Frontend (ASP.NET Core MVC):
Open a new terminal and navigate to the Frontend folder:

cd Frontend
Run the application:


dotnet run
