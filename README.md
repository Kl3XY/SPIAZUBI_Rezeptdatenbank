# Recipe Database
## made with C# and SQL Server
This is a repository for the Recipe Database that i got as an assignment for my apprenticeship. This readme includes the Entity Relationship Diagram aswell as the Installation instruction for the Program.

## Installation instructions
### 1. Prerequisites
1. SQL SERVER MANAGEMENT STUDIO (Get it from [here](https://pages.github.com/](https://learn.microsoft.com/de-de/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16))
2. SQL Server Express LocalDB (Get it from [here](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16))

### 2. SQL Server.
Start SQL Server Management Studio (SSMS for short.) and connect to the following adress: (localDB)\MSSQLLocaldb and set Authentication to "Windows Authentication". 
Once everything is set up. Click Connect.

![grafik](https://github.com/Kl3XY/SPIAZUBI_Rezeptdatenbank/assets/147717328/f19fd612-6008-4b17-9b39-dbc2f976acea)

*If your PC can't connect to the LocalDB, check the installation of SQL Server Express LocalDB
![grafik](https://github.com/Kl3XY/SPIAZUBI_Rezeptdatenbank/assets/147717328/f88a0a80-f23e-4d10-a587-fa373163c46e)

Once the SSMS is connected, press Ctrl+O in order to open the query file in the repo. The file is in the root of the Repo and its just called "Rezeptdatenbank".
Once the query is loaded, Press F5 to execute it.
With the execution of the script, the database is complete and filled with placeholder information.

### 3. Download the newest Release
1. Download the newest version of the Program.
2. And execute the .exe file under .net6.0
2.5. (Optional, you can enter your own connection string. Or just press d for the most safe one)
4. If everything is set up correctly, the program should display it's main menu.

## Apprenticeship related stuff
![grafik](https://github.com/Kl3XY/SPIAZUBI_Rezeptdatenbank/assets/147717328/689150bd-bfc6-47ab-9aab-c9b621ec873c)

The difference in speed from Index Seek vs Index Scan, Index Seek is approx. 70% faster. (Tested on a database with 2 million entries)

INDECES SET:
Dish -> Dish_name, Dish_Description. For searching dishes.
Ingredients -> ID. Name. For searching Ingredients.
