# Introduction 
KnowledgeSpace is open source project for everyone. Every member can create new knowledge base record (KB) and share community.
For each KB, user can vote it and comment to below KB.

# Technology stack
1.  Asp.Net Core 3.1
2.	Angular 8
3.  Identity Server 4
4.  SQL Server 2019

# Getting Started
1.	Clone this source code from Repository
2.  Build Solution to restore all Nuget Packages
3.	Set startup project is KnowledgeSpace.BackendServer
4.	Run update Database to generate database
5.	Set startup project to multiple projects include: KnowledgeSpace.BackendServer and KnowledgeSpace.WebPortal

# References
- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
- [Visual Studio](https://visualstudio.microsoft.com/)
- [Identity Server4](https://identityserver.io/)

# Commands
1.  Migration
	Add-Migration Initial -OutputDir Data/Migrations
2. Update database
	Update-Database
