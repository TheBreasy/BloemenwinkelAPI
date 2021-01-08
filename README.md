# Flower store .NET Core 3.1 REST API

## Installation requirements

You will need the following software:
- [.NET Core (At least version 3.1).](https://dotnet.microsoft.com/download)
- An editor/IDE of your choice. We recommend using Visual Studio because of its support capabilities for C#.
- A local MySQL server.
- A visual tool for your MySQL database (e.g. phpMyAdmin).

## Setup the API
1.	Clone the repository in the desired file location.
2.	Open your IDE and navigate to the project with the command tool (e.g. Developer Powershell within Visual Studio).
3.	Open Startup.cs and change the string to your connection on line 35 as well as the server and version type on line 38.
4.	Run your MySQL server.
5.	Use ‘dotnet ef database update’ in your command tool to create your database in MySQL.
6.	Use ‘dotnet run’ to start the API.
7.	Open up your browser and navigate to to http://localhost:5000/swagger/index.html. This will give you an overview of all the API endpoints and a quick method to execute them.

## What we couldn't finish
- Endpoints aren't fully completed.
- MongoDB is partially working.
- Tried to insert the external API 'BasisRegisters.Vlaanderen' but the use reference link didn't work.
