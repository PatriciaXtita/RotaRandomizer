# RotaRandomizer
This project consists in a basic rota creator for a sales department.

## Hierarchy
The main project is divided in three child projects that consist in:
	-> RotaRandomizer: Backend API developed in .Net Core 3.1
	-> RotaRandomizerTest: Test project that uses xunit to validate API methods
	-> salesdepartment: Front end project developed in VueJS to use API.

## Dependencies
->Mysql for the database
->nodejs

##Setting up the project
->Create a database and update appsettings.json in RotaRandomizer for the connection string intended
->Open the solution in visual studio and run the project to start the API
->From command prompt within the VueJS project run 
	>npm run build
	>npm run dev
Access the url displayed.
Warning: if the API runs in a different port than what's expected update to the url requests in the VueJS project are required.

