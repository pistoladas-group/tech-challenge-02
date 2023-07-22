USE master
GO

IF DB_ID('TechNewsAuth') IS NULL
    BEGIN
        CREATE DATABASE TechNewsAuth
    END

IF NOT EXISTS (SELECT name FROM master.sys.server_principals WHERE name = 'tech-news-auth')
    BEGIN
        --select PWDENCRYPT('<senha>>')
        CREATE LOGIN [tech-news-auth] WITH
            PASSWORD = 0x02005782F37B5EF893FC53FF946641842584CEF2119B9777C0B9F55EA1017F7253F8E59F96EF6C4B6486F3E268718CF42C0F32FE653078BE5830DA5A0B2ACA74D75E4F484A1B HASHED,
            DEFAULT_DATABASE = TechNewsAuth
    END

USE TechNewsAuth
GO

IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = 'tech-news-auth')
    BEGIN
        CREATE USER [tech-news-auth] FOR LOGIN [tech-news-auth]
    END