USE TechNews;
GO

MERGE INTO Authors AS target
USING (
    VALUES
        ('Danilo Vasconcellos', 'Danilo Vasconcellos@exemplo.com'),
        ('Everton', 'Everton@email.com'),
        ('Igor', 'Igor@email.com'),
        ('Igor', 'Igor@email.com'),
        ('Igor', 'Igor@email.com')
) AS source (Name, Email)
ON target.Name = source.Name AND target.Email = source.Email
WHEN NOT MATCHED THEN
    INSERT (Id, Name, Email, CreatedAt, IsDeleted, ImageSource)
    VALUES (NEWID(), source.Name, source.Email, GETUTCDATE(), 0, '');

COMMIT;
GO