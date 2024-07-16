# To Do App

A simple app that allows to create notes. You can:
* Create and edit todo;
* Assign todo to any existing user;
* Change todo's status.

## Build
To build a server app:
1. Open a console in `api` directory;
2. Run `dotnet build` to build server solution;
3. Run `dotnet ef database update --project ./ToDoApp.API` to create a local database.

To build a client app:
1. Open a console in `client` directory;
2. Run `npm i`.

## Running the project
After the building steps are done, you can run the application through console.

To run server app:
1. Open a console in `api` directory;
2. Run `dotnet run --project ./ToDoApp.API --launch-profile https`.

To run client app:
1. Open a console in `client` directory;
2. Run `npm run start`.
