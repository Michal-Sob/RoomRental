IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [Buildings] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Address] nvarchar(200) NOT NULL,
        [NumberOfFloors] int NOT NULL,
        [Status] int NOT NULL,
        [OpeningTime] time NOT NULL,
        [ClosingTime] time NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Buildings] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [Departments] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Code] nvarchar(10) NOT NULL,
        [Manager] nvarchar(100) NOT NULL,
        [Budget] decimal(18,2) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Departments] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [Equipment] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [SerialNumber] nvarchar(50) NOT NULL,
        [Manufacturer] nvarchar(100) NOT NULL,
        [PurchaseDate] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Equipment] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [Meetings] (
        [Id] int NOT NULL IDENTITY,
        [Topic] nvarchar(200) NOT NULL,
        [NumberOfParticipants] int NOT NULL,
        [ParticipantsList] nvarchar(1000) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Meetings] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [MenuItems] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [Category] int NOT NULL,
        [Allergens] nvarchar(200) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_MenuItems] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [Reports] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Type] int NOT NULL,
        [Criteria] nvarchar(500) NOT NULL,
        [Content] nvarchar(2000) NOT NULL,
        [GeneratedAt] datetime2 NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Reports] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [Rooms] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Capacity] int NOT NULL,
        [Floor] int NOT NULL,
        [Type] int NOT NULL,
        [Status] int NOT NULL,
        [BuildingId] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Rooms] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Rooms_Buildings_BuildingId] FOREIGN KEY ([BuildingId]) REFERENCES [Buildings] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(50) NOT NULL,
        [LastName] nvarchar(50) NOT NULL,
        [Email] nvarchar(100) NOT NULL,
        [Phone] nvarchar(20) NOT NULL,
        [Role] int NOT NULL,
        [DepartmentId] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Users_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [RoomEquipment] (
        [EquipmentId] int NOT NULL,
        [RoomId] int NOT NULL,
        CONSTRAINT [PK_RoomEquipment] PRIMARY KEY ([EquipmentId], [RoomId]),
        CONSTRAINT [FK_RoomEquipment_Equipment_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipment] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_RoomEquipment_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [Notifications] (
        [Id] int NOT NULL IDENTITY,
        [Type] nvarchar(100) NOT NULL,
        [Content] nvarchar(1000) NOT NULL,
        [SentAt] datetime2 NOT NULL,
        [IsDelivered] bit NOT NULL,
        [UserId] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Notifications_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [Reservations] (
        [Id] int NOT NULL IDENTITY,
        [Date] datetime2 NOT NULL,
        [StartTime] time NOT NULL,
        [EndTime] time NOT NULL,
        [Status] int NOT NULL,
        [UserId] int NOT NULL,
        [RoomId] int NOT NULL,
        [MeetingId] int NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Reservations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Reservations_Meetings_MeetingId] FOREIGN KEY ([MeetingId]) REFERENCES [Meetings] ([Id]),
        CONSTRAINT [FK_Reservations_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Reservations_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [AdditionalServices] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [PreparationTime] time NOT NULL,
        [ReservationId] int NOT NULL,
        [ServiceType] nvarchar(21) NOT NULL,
        [MealType] nvarchar(100) NULL,
        [NumberOfPeople] int NULL,
        [DietaryRequirements] nvarchar(500) NULL,
        [DeliveryTime] time NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_AdditionalServices] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AdditionalServices_Reservations_ReservationId] FOREIGN KEY ([ReservationId]) REFERENCES [Reservations] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [Documents] (
        [Id] int NOT NULL IDENTITY,
        [Amount] decimal(18,2) NOT NULL,
        [IssueDate] datetime2 NOT NULL,
        [PaymentMethod] nvarchar(50) NOT NULL,
        [Status] int NOT NULL,
        [DocumentNumber] nvarchar(50) NOT NULL,
        [DueDate] datetime2 NOT NULL,
        [ReservationId] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NULL,
        CONSTRAINT [PK_Documents] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Documents_Reservations_ReservationId] FOREIGN KEY ([ReservationId]) REFERENCES [Reservations] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE TABLE [CateringMenuItem] (
        [CateringId] int NOT NULL,
        [MenuItemId] int NOT NULL,
        CONSTRAINT [PK_CateringMenuItem] PRIMARY KEY ([CateringId], [MenuItemId]),
        CONSTRAINT [FK_CateringMenuItem_AdditionalServices_CateringId] FOREIGN KEY ([CateringId]) REFERENCES [AdditionalServices] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CateringMenuItem_MenuItems_MenuItemId] FOREIGN KEY ([MenuItemId]) REFERENCES [MenuItems] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'ClosingTime', N'CreatedAt', N'Name', N'NumberOfFloors', N'OpeningTime', N'Status', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Buildings]'))
        SET IDENTITY_INSERT [Buildings] ON;
    EXEC(N'INSERT INTO [Buildings] ([Id], [Address], [ClosingTime], [CreatedAt], [Name], [NumberOfFloors], [OpeningTime], [Status], [UpdatedAt])
    VALUES (1, N''123 Main St, New York'', ''22:00:00'', ''2025-06-19T15:34:07.9605494Z'', N''Building A'', 5, ''07:00:00'', 0, NULL),
    (2, N''456 Business Ave, Chicago'', ''20:00:00'', ''2025-06-19T15:34:07.9605513Z'', N''Building B'', 3, ''08:00:00'', 0, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'ClosingTime', N'CreatedAt', N'Name', N'NumberOfFloors', N'OpeningTime', N'Status', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Buildings]'))
        SET IDENTITY_INSERT [Buildings] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Budget', N'Code', N'CreatedAt', N'Manager', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Departments]'))
        SET IDENTITY_INSERT [Departments] ON;
    EXEC(N'INSERT INTO [Departments] ([Id], [Budget], [Code], [CreatedAt], [Manager], [Name], [UpdatedAt])
    VALUES (1, 50000.0, N''IT'', ''2025-06-19T15:34:07.9600597Z'', N''John Smith'', N''Information Technology'', NULL),
    (2, 30000.0, N''HR'', ''2025-06-19T15:34:07.9600688Z'', N''Anna Johnson'', N''Human Resources'', NULL),
    (3, 40000.0, N''SALES'', ''2025-06-19T15:34:07.9600689Z'', N''Peter Williams'', N''Sales'', NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Budget', N'Code', N'CreatedAt', N'Manager', N'Name', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Departments]'))
        SET IDENTITY_INSERT [Departments] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Manufacturer', N'Name', N'PurchaseDate', N'SerialNumber', N'Status', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Equipment]'))
        SET IDENTITY_INSERT [Equipment] ON;
    EXEC(N'INSERT INTO [Equipment] ([Id], [CreatedAt], [Manufacturer], [Name], [PurchaseDate], [SerialNumber], [Status], [UpdatedAt])
    VALUES (1, ''2025-06-19T15:34:07.9607521Z'', N''Epson'', N''HD Projector'', ''2023-06-19T15:34:07.9607292Z'', N''PRJ001'', 0, NULL),
    (2, ''2025-06-19T15:34:07.9607529Z'', N''BenQ'', N''4K Projector'', ''2024-06-19T15:34:07.9607525Z'', N''PRJ002'', 0, NULL),
    (3, ''2025-06-19T15:34:07.9607531Z'', N''Daikin'', N''Air Conditioning'', ''2022-06-19T15:34:07.9607531Z'', N''AC001'', 0, NULL),
    (4, ''2025-06-19T15:34:07.9607549Z'', N''Bose'', N''Sound System'', ''2024-12-19T15:34:07.9607532Z'', N''SND001'', 0, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'Manufacturer', N'Name', N'PurchaseDate', N'SerialNumber', N'Status', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Equipment]'))
        SET IDENTITY_INSERT [Equipment] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Allergens', N'Category', N'CreatedAt', N'Description', N'Name', N'Price', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[MenuItems]'))
        SET IDENTITY_INSERT [MenuItems] ON;
    EXEC(N'INSERT INTO [MenuItems] ([Id], [Allergens], [Category], [CreatedAt], [Description], [Name], [Price], [UpdatedAt])
    VALUES (1, N''gluten, eggs, milk'', 0, ''2025-06-19T15:34:07.9608415Z'', N''Variety of fresh sandwiches'', N''Mixed Sandwiches'', 25.0, NULL),
    (2, N''eggs, milk, fish'', 1, ''2025-06-19T15:34:07.9608499Z'', N''Fresh salad with chicken'', N''Caesar Salad'', 18.0, NULL),
    (3, N'''', 3, ''2025-06-19T15:34:07.9608500Z'', N''Freshly brewed coffee'', N''Coffee'', 8.0, NULL),
    (4, N''gluten, eggs, milk, soy'', 2, ''2025-06-19T15:34:07.9608502Z'', N''Homemade chocolate cake'', N''Chocolate Cake'', 12.0, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Allergens', N'Category', N'CreatedAt', N'Description', N'Name', N'Price', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[MenuItems]'))
        SET IDENTITY_INSERT [MenuItems] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BuildingId', N'Capacity', N'CreatedAt', N'Floor', N'Name', N'Status', N'Type', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Rooms]'))
        SET IDENTITY_INSERT [Rooms] ON;
    EXEC(N'INSERT INTO [Rooms] ([Id], [BuildingId], [Capacity], [CreatedAt], [Floor], [Name], [Status], [Type], [UpdatedAt])
    VALUES (1, 1, 20, ''2025-06-19T15:34:07.9606690Z'', 1, N''Conference Room 101'', 0, 0, NULL),
    (2, 1, 50, ''2025-06-19T15:34:07.9606694Z'', 2, N''VIP Conference Suite'', 0, 2, NULL),
    (3, 2, 100, ''2025-06-19T15:34:07.9606696Z'', 1, N''Auditorium'', 0, 1, NULL),
    (4, 1, 15, ''2025-06-19T15:34:07.9606697Z'', 1, N''Meeting Room 102'', 0, 0, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BuildingId', N'Capacity', N'CreatedAt', N'Floor', N'Name', N'Status', N'Type', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Rooms]'))
        SET IDENTITY_INSERT [Rooms] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'DepartmentId', N'Email', N'FirstName', N'LastName', N'Phone', N'Role', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    EXEC(N'INSERT INTO [Users] ([Id], [CreatedAt], [DepartmentId], [Email], [FirstName], [LastName], [Phone], [Role], [UpdatedAt])
    VALUES (1, ''2025-06-19T15:34:07.9604575Z'', 1, N''admin@company.com'', N''Admin'', N''System'', N''123456789'', 2, NULL),
    (2, ''2025-06-19T15:34:07.9604579Z'', 2, N''m.brown@company.com'', N''Marcus'', N''Brown'', N''987654321'', 0, NULL),
    (3, ''2025-06-19T15:34:07.9604580Z'', 3, N''c.davis@company.com'', N''Catherine'', N''Davis'', N''555666777'', 1, NULL)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedAt', N'DepartmentId', N'Email', N'FirstName', N'LastName', N'Phone', N'Role', N'UpdatedAt') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AdditionalServices_ReservationId] ON [AdditionalServices] ([ReservationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_CateringMenuItem_MenuItemId] ON [CateringMenuItem] ([MenuItemId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Departments_Code] ON [Departments] ([Code]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Documents_DocumentNumber] ON [Documents] ([DocumentNumber]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Documents_ReservationId] ON [Documents] ([ReservationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Equipment_SerialNumber] ON [Equipment] ([SerialNumber]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Notifications_UserId] ON [Notifications] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Reservations_MeetingId] ON [Reservations] ([MeetingId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Reservations_RoomId] ON [Reservations] ([RoomId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Reservations_UserId] ON [Reservations] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_RoomEquipment_RoomId] ON [RoomEquipment] ([RoomId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Rooms_BuildingId] ON [Rooms] ([BuildingId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Users_DepartmentId] ON [Users] ([DepartmentId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250619153408_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250619153408_InitialCreate', N'9.0.6');
END;

COMMIT;
GO

