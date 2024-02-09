# Recipe Database
## made with C# and SQL Server
This is a repository for the Recipe Database that i got as an assignment for my apprenticeship. This readme includes the Entity Relationship Diagram aswell as the Installation instruction for the Program.

## Installation instructions
### 1. Prerequisites
1. SQL SERVER MANAGEMENT STUDIO (Get it from [here]([https://pages.github.com/](https://learn.microsoft.com/de-de/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)https://learn.microsoft.com/de-de/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16))
2. SQL Server Express LocalDB (Get it from [here](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16))
3. Visual Studio and all of it's requirements (Get it from [here](https://visualstudio.microsoft.com/de/downloads/))

### Clone this repo.
1. On your PC, go to where you want the repo to be cloned.
2. Open Git Bash and type the following
3. git clone https://github.com/Kl3XY/SPIAZUBI_Rezeptdatenbank

### 3. SQL Server.
Start SQL Server Management Studio (SSMS for short.) and connect to the following adress: (localDB)\MSSQLLocaldb and set Authentication to "Windows Authentication". 
Once everything is set up. Click Connect.

![grafik](https://github.com/Kl3XY/SPIAZUBI_Rezeptdatenbank/assets/147717328/f19fd612-6008-4b17-9b39-dbc2f976acea)

*If your PC can't connect to the LocalDB, check the installation of SQL Server Express LocalDB
![grafik](https://github.com/Kl3XY/SPIAZUBI_Rezeptdatenbank/assets/147717328/f88a0a80-f23e-4d10-a587-fa373163c46e)

Once the SSMS is connected, press Ctrl+O in order to open the query file in the repo. The file is in the root of the Repo and its just called "Rezeptdatenbank".
Once the query is loaded, Press F5 to execute it.
With the execution of the script, the database is complete and filled with placeholder information.

### 4. Program.
Inside the Program is the Codebase for the Database Program. You are free to change it and re-release it non-commercially.

Now open the solution via Visual Studio and press F5. If everything is correctly done, the Program should connect and you should be able to use it.
