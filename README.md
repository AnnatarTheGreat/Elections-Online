# Elections-Online

This project is a web application designed to facilitate online elections by connecting to a database. Registered users have the opportunity to cast their votes for one of three candidates or options. After voting, users can view real-time election results on the web page.

![Results](https://snipboard.io/E1Dmn3.jpg)

## Features of this project include:

-Registration, Authorization and Authentication

![Results](https://snipboard.io/2xzlVb.jpg)

-Voting
 
![Results](https://snipboard.io/ekqZum.jpg)

-Viewing election results in real-time with signalR (displays data retrieved from the databases)

![Results](https://snipboard.io/rZ65fG.jpg)

-Retrieving specific data from the REST API (for example, querying /api/voters/Biden returns information about users who voted for this candidate)



## Project's Stack and dependencies:

-Main programming language: C#

-Main template: ASP.NET MVC

-Platform: .NET 8

-ORM framework: Entity Framework (used to connect applications with database)

-Database: MySQL

-Testing framework: XUnit Tests (used for testing the project)

-Microsoft.AspNetCore.Identity

-Microsoft.AspNetCore.SignalR.Client