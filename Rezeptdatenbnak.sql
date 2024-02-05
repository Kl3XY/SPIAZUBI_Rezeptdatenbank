USE master;
DECLARE @kill varchar(8000) = '';  
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
FROM sys.dm_exec_sessions
WHERE database_id  = db_id('recipes')
EXEC(@kill);
GO

drop database recipes

go

create database recipes

go

use recipes;

create table dish (
	Dish_ID int identity(1, 1) primary key,
	dish_name varchar(64),
	dish_description varchar(8000)
)
insert into dish(dish_name, dish_description) values
('Spaghetti Bolognese', 'Spaghetti umgeben von eine Tomaten-Hackfleisch Soﬂe'),
('Spaghetti Bolognese', 'Spaghetti umgeben von eine Tomaten-Hackfleisch Soﬂe. Vegan'),
('Spaghetti Bolognese', 'Spaghetti umgeben von eine Tomaten-Hackfleisch Soﬂe. Scharf');

create table ingredient (
	Ingredient_ID int identity(1, 1) primary key,
	ingredient_name varchar(64)
)
insert into ingredient(ingredient_name) values
('Spaghetti'),
('Tomaten'),
('Salz'),
('Hackfleisch'),
('Zwiebeln'),
('Karotten'),
('Schnittlauch'),
('bommes');


create table step (
	Dish_ID int foreign key references dish(Dish_ID),
	step_number int,
	step_description varchar(3000),
)
insert into step(Dish_ID, step_number, step_description) values
(1, 1, 'Nudeln Kochen'),
(1, 2, 'Nudeln Kochen'),
(1, 3, 'Nudeln Kochen'),
(1, 4, 'Nudeln Kochen'),
(1, 5, 'Nudeln Kochen');

create table unit (
	Unit_ID int identity(1, 1) primary key,
	unit_name varchar(20)
)
insert into unit(unit_name) values
('ml'),
('l'),
('mg'),
('g'),
('kg'),
('t'),
(' St¸ck');

create table recipe (
	Dish_ID int foreign key references dish(Dish_ID),
	Ingredient_ID int foreign key references ingredient(Ingredient_ID),
	ingredient_amount int,
	Unit_ID int foreign key references unit(Unit_ID),
	primary key (Dish_ID, Ingredient_ID)
)

insert into recipe values
(1,		1,		500,	4),
(1,		2,		20,		7),
(1,		3,		5,		3),
(1,		4,		500,	4),
(1,		5,		2,		7),
(1,		6,		2,		7),
(1,		7,		1,		7);
GO
--______________________________________________________________________________________________________________________________________________________________________________________________________________________
--PROCEDURES
--____________________________________________________________________________________________________________________________________________________________________________________________________________________________________

create procedure show_units @id int = -1
as
	IF @id = -1
		select * from unit;
	else
		select * from unit where @id = unit_id;
go

create procedure add_units @name varchar(20)
as
	insert into unit(unit_name) values
	(@name);
go

create procedure edit_units @oldname varchar(20), @newname varchar(20)
as
	update unit
	set unit_name = @newname
	where unit_name = @oldname
go

create procedure delete_units @name varchar(20)
as
	delete from unit where unit_name = @name
go

--


create procedure show_ingredient @id int = -1
as
	IF @id = -1
		select * from ingredient;
	else
		select * from ingredient where @id = Ingredient_ID;
go

create procedure add_ingredient @name varchar(20)
as
	insert into ingredient(ingredient_name) values
	(@name);
go

create procedure edit_ingredient @oldname varchar(20), @newname varchar(20)
as
	update ingredient
	set ingredient_name = @newname
	where ingredient_name = @oldname
go

create procedure delete_ingredient @name varchar(20)
as
	delete from ingredient where ingredient_name = @name
go

--

create procedure show_dish @id int
as
	select * from dish where @id = Dish_ID;
	select displayDish.dish_name, displayIngredient.ingredient_name, CAST(ingredient_amount as varchar) + displayUnit.unit_name
	from recipe
		inner join ingredient as displayIngredient on recipe.Ingredient_ID = displayIngredient.Ingredient_ID
		inner join unit as displayUnit on recipe.Unit_ID = displayUnit.Unit_ID
		inner join dish as displayDish on recipe.Dish_ID = displayDish.Dish_ID
	where @id = recipe.Dish_ID

	select step_number as 'Schritt', step_description from step where @id = Dish_ID;
go

create procedure add_dish @name varchar(30), @description varchar(8000)
as
	insert into dish(dish_name, dish_description) values
	(@name, @description);
go

create procedure edit_dish @id int, @name varchar(30), @description varchar(8000)
as
	update dish
	set dish_name = @name, dish_description = @description
	where Dish_ID = @id
go

create procedure delete_dish @id int
as
	delete from dish where Dish_ID = @id
go

create procedure add_step @dish_id int, @description varchar(8000)
as
	IF EXISTS(select Dish_ID from step where Dish_ID = @Dish_ID)
		begin
			declare @maxNum int;
			select @maxNum = max(step_number) from step where Dish_ID = @Dish_ID

			insert into step(Dish_ID, step_number, step_description) values
			(@Dish_ID, @maxNum + 1, @description);
		end
	else
		begin
			insert into step(Dish_ID, step_number, step_description) values
			(@Dish_ID, 1, @description);
		end
go

create procedure clear_step @Dish_ID int
as
	delete from step where Dish_ID = @Dish_ID;
go

create procedure add_ingredient_to_recipe @Dish_ID int, @Ingredient_ID int, @amount int, @unitID int
as
	IF EXISTS(select Ingredient_ID from recipe where Dish_ID = @Dish_ID and @Ingredient_ID = Ingredient_ID)
		begin
			update recipe
			set ingredient_amount = ingredient_amount + @amount
			where Dish_ID = @Dish_ID and @Ingredient_ID = Ingredient_ID
		end
	else
		begin
			print 'bingo'
			insert recipe values
			(@Dish_ID, @Ingredient_ID, @amount, @unitID);
		end
go

create procedure clear_ingredientList @Dish_ID int
as
	delete from recipe where Dish_ID = @Dish_ID;
go

create procedure search_recipe @search_term varchar(50)
as
	declare @fixedVar varchar(50)
	SELECT @fixedVar = REPLACE(@search_term, ';', '%')
	select * from dish 
	where dish_name LIKE '%' + @fixedVar + '%' or dish_description LIKE '%'+ @fixedVar +'%'
go


GO
--_______________________________________________________________________________________________________________________________________________________________________________________________________________
--EXEC's
--____________________________________________________________________________________________________________________________________________________________________________________________________________________________________

exec search_recipe @search_term = 'scharf'