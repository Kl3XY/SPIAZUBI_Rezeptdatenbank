using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datenbank
{
    internal static class initialQuery
    {
        public static string query = @"
		create database recipes
		";

		public static string query1 = @"
		use recipes;
		";
		
		public static string query3 = @"
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
		create procedure edit_units @oldname varchar(20), @newname varchar(20), @version int
		as
			if (select ver from unit where ver = @version) IS NOT NULL
				begin
					if (select ingredient_name from ingredient where ingredient_name = @newname) IS NULL 
					BEGIN
						update unit
						set unit_name = @newname, ver += 1
						where unit_name = @oldname
					END
				end
			else
				begin
					RAISERROR(51000, -1, -1);
				end
		go
		create procedure delete_units @name varchar(20)
		as
			delete from unit where unit_name = @name
		go


		create procedure show_ingredient @id int = -1
		as
			IF @id = -1
				select * from ingredient;
			else
				select * from ingredient where @id = Ingredient_ID;
		go
		create procedure add_ingredient @name varchar(6420)
		as
			insert into ingredient(ingredient_name) values
			(@name);
		go
		create procedure edit_ingredient @id int, @newname varchar(64), @version int
		as
			if (select ver from ingredient where ver = @version) IS NOT NULL
				begin
					if (select ingredient_name from ingredient where ingredient_name = @newname) IS NULL 
					BEGIN
						update ingredient
						set ingredient_name = @newname, ver += 1
						where Ingredient_ID = @id
					END
				end
			else
				begin
					RAISERROR(51000, -1, -1);
				end
		go
		create procedure search_ingredient @name int
		as
			select * from ingredient where Ingredient_ID = @name
		go
		create procedure delete_ingredient @id int
		as
			delete from ingredient where Ingredient_ID = @id;
		go


		create procedure show_recipe @id int
		as
			select displayDish.dish_name, displayIngredient.ingredient_name, CAST(ingredient_amount as varchar) + displayUnit.unit_name
			from recipe
				inner join ingredient as displayIngredient on recipe.Ingredient_ID = displayIngredient.Ingredient_ID
				inner join unit as displayUnit on recipe.Unit_ID = displayUnit.Unit_ID
				inner join dish as displayDish on recipe.Dish_ID = displayDish.Dish_ID
			where @id = recipe.Dish_ID

		go
		create procedure add_ingredient_to_recipe @Dish_ID int, @Ingredient_ID varchar(64), @amount int, @unitID int
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
					insert recipe(Dish_ID, Ingredient_ID, ingredient_amount, Unit_ID) values
					(@Dish_ID, @Ingredient_ID, @amount, @unitID);
				end
		go
		create procedure search_recipe @search_term varchar(50)
		as
			declare @fixedVar varchar(50)
			SELECT @fixedVar = REPLACE(@search_term, ';', '%')
			select * from dish 
			where dish_name LIKE '%' + @fixedVar + '%' or dish_description LIKE '%'+ @fixedVar +'%'
		go



		create procedure show_dish @id int = -1
		as
			IF @id = -1
				select * from dish;
			else
				select * from dish where @id = Dish_ID;
		go
		create procedure show_dish_ingredients @id int
		as
			select displayIngredient.Ingredient_ID, displayIngredient.ingredient_name, CAST(ingredient_amount as varchar) + displayUnit.unit_name as amount, recipe.ver
			from recipe
				inner join ingredient as displayIngredient on recipe.Ingredient_ID = displayIngredient.Ingredient_ID
				inner join unit as displayUnit on recipe.Unit_ID = displayUnit.Unit_ID
				inner join dish as displayDish on recipe.Dish_ID = displayDish.Dish_ID
			where @id = recipe.Dish_ID

		go

		create procedure show_entire_dish_info @id int
		as
			declare @thisID int = @id;
			select * from dish where @id = Dish_ID;
			exec show_dish_ingredients @id = @thisID;
			select step_number as 'Schritt', step_description from step where @id = Dish_ID;
		go
		create procedure add_dish @name varchar(30), @description varchar(8000)
		as
			insert into dish(dish_name, dish_description) values
			(@name, @description);
		go
		create procedure edit_dish @id int, @name varchar(30), @description varchar(8000), @version int
		as
			if (select ver from dish where ver = @version) IS NOT NULL
				begin
					update dish
					set dish_name = @name, dish_description = @description , ver += 1
					where Dish_ID = @id 
				end
			else
				begin
					RAISERROR(51000, -1, -1);
				end
	
		go
		create procedure delete_dish @id int
		as
			exec clear_dish_ingredients @Dish_ID = @id;

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
		create procedure show_step @Dish_ID int
		as
			select step_number as 'Step', step_description from step where @Dish_ID = Dish_ID;
		go
		create procedure edit_step @Dish_ID int, @stepNumber int, @description varchar(8000), @version int
		as
			if (select ver from step where ver = @version) IS NOT NULL
				begin
					update step 
					set step_description = @description, ver = ver + 1
					where @Dish_ID = Dish_ID and @stepNumber = step_number;
				end
			else
				begin
					RAISERROR(51000, -1, -1);
				end
		go


		create procedure clear_step @Dish_ID int
		as
			delete from step where Dish_ID = @Dish_ID;
		go
		create procedure delete_step @Dish_ID int, @Stepnmb int
		as
	 
			delete from step where Dish_ID = @Dish_ID and @Stepnmb = step_number;
			update step set step_number -= 1 where step_number > @Stepnmb and @Dish_ID = Dish_ID;
		GO

		create procedure clear_dish_ingredients @Dish_ID int
		as
			delete from recipe where Dish_ID = @Dish_ID;
		go
		create procedure delete_dish_ingredient @Dish_ID int, @Ingredient_ID int
		as
			delete from recipe where Dish_ID = @Dish_ID and Ingredient_ID = @Dish_ID;
		go
		create procedure edit_dish_ingredients @ownDish_ID int, @ownIngredient_ID int, @newIngID int, @newMeasurement int, @newUnit int, @version int
		as
			IF (select Ingredient_ID from recipe where Ingredient_ID = @newIngID and Dish_ID = @ownDish_ID) IS NULL
				begin
					if (select ver from recipe where Ingredient_ID = @ownIngredient_ID and Dish_ID = @ownDish_ID and ver = @version) IS NOT NULL
						begin
							update recipe 
							set
							Ingredient_ID = @newIngID,
							ingredient_amount = @newMeasurement,
							Unit_ID = @newUnit, ver += 1
							where Dish_ID = @ownDish_ID and Ingredient_ID = @ownIngredient_ID;
						end
					else
						begin
							RAISERROR(51000, -1, -1);
						end
				end
			else
				begin
					exec add_ingredient_to_recipe @Dish_ID = @ownDish_ID, @Ingredient_ID = @newIngID, @amount = @newMeasurement, @unitID = @newUnit;
				end
		go
		";

        public static string query2 = @"
        
		create table dish (
			Dish_ID int identity(1, 1) primary key,
			dish_name varchar(64),
			dish_description varchar(8000)
		)
		insert into dish(dish_name, dish_description) values
		('Spaghetti Bolognese', 'Spaghetti umgeben von eine Tomaten-Hackfleisch Soße'),
		('Spaghetti Bolognese', 'Spaghetti umgeben von eine Tomaten-Hackfleisch Soße. Vegan'),
		('Spaghetti Bolognese', 'Spaghetti umgeben von eine Tomaten-Hackfleisch Soße. Scharf');

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
		(' Stück');

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

		alter table recipe
		add
			ver int not null default 1;

		alter table unit
		add
			ver int not null default 1;

		alter table step
		add
			ver int not null default 1;

		alter table ingredient
		add
			ver int not null default 1;

		alter table dish
		add
			ver int not null default 1;
		";
    }
}
