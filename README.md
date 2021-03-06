# Introduction 
KnowledgeSpace is open source project for everyone. Every member can create new knowledge base record (KB) and share community.
For each KB, user can vote it and comment to below KB.

# Migration
- Add-Migration Initial -OutputDir Data/Migrations
- Update-Database

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
    Add-Migration InitialMain -Context MainDbContext -o Data/Migrations/Main
2. Update database
	Update-Database
    Update-Database -Context MainDbContext

# Git references
- [Tedu] (https://dev.azure.com/tedu-official/KnowledgeSpace/_git/KnowledgeSpace)
- [For Me] (https://dev.azure.com/vietdungst0043/Learning_Angular_NetCore/_git/Learning_Angular_NetCore)

# Link Notes
https://medium.com/eway/nguy%C3%AAn-t%E1%BA%AFc-thi%E1%BA%BFt-k%E1%BA%BF-rest-api-23add16968d7

#Angular Statement
- Gen Module: ng g m protected-zone/systems
- Gen Component: ng g c protected-zone/systems/functions

# Cache table SQL
```sql
    CREATE TABLE [dbo].[CacheTable](
        [Id] [nvarchar](449) NOT NULL,
        [Value] [varbinary](max) NOT NULL,
        [ExpiresAtTime] [datetimeoffset](7) NOT NULL,
        [SlidingExpirationInSeconds] [bigint] NULL,
        [AbsoluteExpiration] [datetimeoffset](7) NULL,
     CONSTRAINT [pk_Id] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
           IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
           ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
    CREATE NONCLUSTERED INDEX [Index_ExpiresAtTime] ON [dbo].[CacheTable]
    (
        [ExpiresAtTime] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
           SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, 
           ONLINE = OFF, ALLOW_ROW_LOCKS = ON, 
           ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
```