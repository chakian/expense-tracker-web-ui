DECLARE @CurrentMigration [nvarchar](max)

IF object_id('[dbo].[__MigrationHistory]') IS NOT NULL
    SELECT @CurrentMigration =
        (SELECT TOP (1) 
        [Project1].[MigrationId] AS [MigrationId]
        FROM ( SELECT 
        [Extent1].[MigrationId] AS [MigrationId]
        FROM [dbo].[__MigrationHistory] AS [Extent1]
        WHERE [Extent1].[ContextKey] = N'ExpenseTracker.Persistence.Context.ExpenseTrackerContext'
        )  AS [Project1]
        ORDER BY [Project1].[MigrationId] DESC)

IF @CurrentMigration IS NULL
    SET @CurrentMigration = '0'

IF @CurrentMigration < '201905202050246_InitialCreate'
BEGIN
    CREATE TABLE [dbo].[Accounts] (
        [AccountId] [int] NOT NULL IDENTITY,
        [Name] [nvarchar](max) NOT NULL,
        [StartingBalance] [decimal](18, 2) NOT NULL,
        [CurrentBalance] [decimal](18, 2) NOT NULL,
        [AccountTypeId] [int] NOT NULL,
        [BudgetId] [int] NOT NULL,
        [InsertUserId] [nvarchar](128) NOT NULL,
        [InsertTime] [datetime] NOT NULL,
        [UpdateUserId] [nvarchar](128),
        [UpdateTime] [datetime],
        [IsActive] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.Accounts] PRIMARY KEY ([AccountId])
    )
    CREATE INDEX [IX_AccountTypeId] ON [dbo].[Accounts]([AccountTypeId])
    CREATE INDEX [IX_BudgetId] ON [dbo].[Accounts]([BudgetId])
    CREATE INDEX [IX_InsertUserId] ON [dbo].[Accounts]([InsertUserId])
    CREATE INDEX [IX_UpdateUserId] ON [dbo].[Accounts]([UpdateUserId])
    CREATE TABLE [dbo].[AccountTypes] (
        [AccountTypeId] [int] NOT NULL,
        [Name] [nvarchar](max) NOT NULL,
        [IsActive] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.AccountTypes] PRIMARY KEY ([AccountTypeId])
    )
    CREATE TABLE [dbo].[Budgets] (
        [BudgetId] [int] NOT NULL IDENTITY,
        [Name] [nvarchar](max) NOT NULL,
        [CurrencyId] [int] NOT NULL,
        [InsertUserId] [nvarchar](128) NOT NULL,
        [InsertTime] [datetime] NOT NULL,
        [UpdateUserId] [nvarchar](128),
        [UpdateTime] [datetime],
        [IsActive] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.Budgets] PRIMARY KEY ([BudgetId])
    )
    CREATE INDEX [IX_CurrencyId] ON [dbo].[Budgets]([CurrencyId])
    CREATE INDEX [IX_InsertUserId] ON [dbo].[Budgets]([InsertUserId])
    CREATE INDEX [IX_UpdateUserId] ON [dbo].[Budgets]([UpdateUserId])
    CREATE TABLE [dbo].[BudgetPlans] (
        [BudgetPlanId] [int] NOT NULL IDENTITY,
        [Month] [int] NOT NULL,
        [Year] [int] NOT NULL,
        [BudgetId] [int] NOT NULL,
        [InsertUserId] [nvarchar](128) NOT NULL,
        [InsertTime] [datetime] NOT NULL,
        [UpdateUserId] [nvarchar](128),
        [UpdateTime] [datetime],
        [IsActive] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.BudgetPlans] PRIMARY KEY ([BudgetPlanId])
    )
    CREATE INDEX [IX_BudgetId] ON [dbo].[BudgetPlans]([BudgetId])
    CREATE INDEX [IX_InsertUserId] ON [dbo].[BudgetPlans]([InsertUserId])
    CREATE INDEX [IX_UpdateUserId] ON [dbo].[BudgetPlans]([UpdateUserId])
    CREATE TABLE [dbo].[BudgetPlanCategories] (
        [BudgetPlanCategoryId] [int] NOT NULL IDENTITY,
        [PlannedAmount] [decimal](18, 2) NOT NULL,
        [BudgetPlanId] [int] NOT NULL,
        [CategoryId] [int] NOT NULL,
        [InsertUserId] [nvarchar](128) NOT NULL,
        [InsertTime] [datetime] NOT NULL,
        [UpdateUserId] [nvarchar](128),
        [UpdateTime] [datetime],
        [IsActive] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.BudgetPlanCategories] PRIMARY KEY ([BudgetPlanCategoryId])
    )
    CREATE INDEX [IX_BudgetPlanId] ON [dbo].[BudgetPlanCategories]([BudgetPlanId])
    CREATE INDEX [IX_CategoryId] ON [dbo].[BudgetPlanCategories]([CategoryId])
    CREATE INDEX [IX_InsertUserId] ON [dbo].[BudgetPlanCategories]([InsertUserId])
    CREATE INDEX [IX_UpdateUserId] ON [dbo].[BudgetPlanCategories]([UpdateUserId])
    CREATE TABLE [dbo].[Categories] (
        [CategoryId] [int] NOT NULL IDENTITY,
        [Name] [nvarchar](max) NOT NULL,
        [BudgetId] [int] NOT NULL,
        [InsertUserId] [nvarchar](128) NOT NULL,
        [InsertTime] [datetime] NOT NULL,
        [UpdateUserId] [nvarchar](128),
        [UpdateTime] [datetime],
        [IsActive] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.Categories] PRIMARY KEY ([CategoryId])
    )
    CREATE INDEX [IX_BudgetId] ON [dbo].[Categories]([BudgetId])
    CREATE INDEX [IX_InsertUserId] ON [dbo].[Categories]([InsertUserId])
    CREATE INDEX [IX_UpdateUserId] ON [dbo].[Categories]([UpdateUserId])
    CREATE TABLE [dbo].[Users] (
        [Id] [nvarchar](128) NOT NULL,
        [IsActive] [bit] NOT NULL,
        [InsertUserId] [nvarchar](128),
        [InsertTime] [datetime] NOT NULL,
        [UpdateUserId] [nvarchar](128),
        [UpdateTime] [datetime],
        [ActiveBudgetId] [int],
        [Email] [nvarchar](256),
        [EmailConfirmed] [bit] NOT NULL,
        [PasswordHash] [nvarchar](max),
        [SecurityStamp] [nvarchar](max),
        [PhoneNumber] [nvarchar](max),
        [PhoneNumberConfirmed] [bit] NOT NULL,
        [TwoFactorEnabled] [bit] NOT NULL,
        [LockoutEndDateUtc] [datetime],
        [LockoutEnabled] [bit] NOT NULL,
        [AccessFailedCount] [int] NOT NULL,
        [UserName] [nvarchar](256) NOT NULL,
        CONSTRAINT [PK_dbo.Users] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_InsertUserId] ON [dbo].[Users]([InsertUserId])
    CREATE INDEX [IX_UpdateUserId] ON [dbo].[Users]([UpdateUserId])
    CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[Users]([UserName])
    CREATE TABLE [dbo].[AspNetUserClaims] (
        [Id] [int] NOT NULL IDENTITY,
        [UserId] [nvarchar](128) NOT NULL,
        [ClaimType] [nvarchar](max),
        [ClaimValue] [nvarchar](max),
        CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId])
    CREATE TABLE [dbo].[AspNetUserLogins] (
        [LoginProvider] [nvarchar](128) NOT NULL,
        [ProviderKey] [nvarchar](128) NOT NULL,
        [UserId] [nvarchar](128) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
    )
    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId])
    CREATE TABLE [dbo].[AspNetUserRoles] (
        [UserId] [nvarchar](128) NOT NULL,
        [RoleId] [nvarchar](128) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId])
    )
    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId])
    CREATE INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId])
    CREATE TABLE [dbo].[Transactions] (
        [TransactionId] [int] NOT NULL IDENTITY,
        [Date] [datetime] NOT NULL,
        [Amount] [decimal](18, 2) NOT NULL,
        [Description] [nvarchar](max),
        [CategoryId] [int] NOT NULL,
        [SourceAccountId] [int] NOT NULL,
        [TargetAccountId] [int],
        [InsertUserId] [nvarchar](128) NOT NULL,
        [InsertTime] [datetime] NOT NULL,
        [UpdateUserId] [nvarchar](128),
        [UpdateTime] [datetime],
        [IsActive] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.Transactions] PRIMARY KEY ([TransactionId])
    )
    CREATE INDEX [IX_CategoryId] ON [dbo].[Transactions]([CategoryId])
    CREATE INDEX [IX_SourceAccountId] ON [dbo].[Transactions]([SourceAccountId])
    CREATE INDEX [IX_TargetAccountId] ON [dbo].[Transactions]([TargetAccountId])
    CREATE INDEX [IX_InsertUserId] ON [dbo].[Transactions]([InsertUserId])
    CREATE INDEX [IX_UpdateUserId] ON [dbo].[Transactions]([UpdateUserId])
    CREATE TABLE [dbo].[BudgetUsers] (
        [BudgetUserId] [int] NOT NULL IDENTITY,
        [BudgetId] [int] NOT NULL,
        [UserId] [nvarchar](128) NOT NULL,
        [InsertUserId] [nvarchar](128) NOT NULL,
        [InsertTime] [datetime] NOT NULL,
        [UpdateUserId] [nvarchar](128),
        [UpdateTime] [datetime],
        [IsActive] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.BudgetUsers] PRIMARY KEY ([BudgetUserId])
    )
    CREATE INDEX [IX_BudgetId] ON [dbo].[BudgetUsers]([BudgetId])
    CREATE INDEX [IX_UserId] ON [dbo].[BudgetUsers]([UserId])
    CREATE INDEX [IX_InsertUserId] ON [dbo].[BudgetUsers]([InsertUserId])
    CREATE INDEX [IX_UpdateUserId] ON [dbo].[BudgetUsers]([UpdateUserId])
    CREATE TABLE [dbo].[Currencies] (
        [CurrencyId] [int] NOT NULL,
        [CurrencyCode] [nvarchar](15) NOT NULL,
        [LongName] [nvarchar](150) NOT NULL,
        [DisplayName] [nvarchar](15) NOT NULL,
        [IsActive] [bit] NOT NULL,
        CONSTRAINT [PK_dbo.Currencies] PRIMARY KEY ([CurrencyId])
    )
    CREATE TABLE [dbo].[AspNetRoles] (
        [Id] [nvarchar](128) NOT NULL,
        [Name] [nvarchar](256) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY ([Id])
    )
    CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]([Name])
    ALTER TABLE [dbo].[Accounts] ADD CONSTRAINT [FK_dbo.Accounts_dbo.AccountTypes_AccountTypeId] FOREIGN KEY ([AccountTypeId]) REFERENCES [dbo].[AccountTypes] ([AccountTypeId]) ON DELETE CASCADE
    ALTER TABLE [dbo].[Accounts] ADD CONSTRAINT [FK_dbo.Accounts_dbo.Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [dbo].[Budgets] ([BudgetId])
    ALTER TABLE [dbo].[Accounts] ADD CONSTRAINT [FK_dbo.Accounts_dbo.Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[Accounts] ADD CONSTRAINT [FK_dbo.Accounts_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[Budgets] ADD CONSTRAINT [FK_dbo.Budgets_dbo.Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies] ([CurrencyId]) ON DELETE CASCADE
    ALTER TABLE [dbo].[Budgets] ADD CONSTRAINT [FK_dbo.Budgets_dbo.Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[Budgets] ADD CONSTRAINT [FK_dbo.Budgets_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[BudgetPlans] ADD CONSTRAINT [FK_dbo.BudgetPlans_dbo.Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [dbo].[Budgets] ([BudgetId]) ON DELETE CASCADE
    ALTER TABLE [dbo].[BudgetPlans] ADD CONSTRAINT [FK_dbo.BudgetPlans_dbo.Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[BudgetPlans] ADD CONSTRAINT [FK_dbo.BudgetPlans_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[BudgetPlanCategories] ADD CONSTRAINT [FK_dbo.BudgetPlanCategories_dbo.BudgetPlans_BudgetPlanId] FOREIGN KEY ([BudgetPlanId]) REFERENCES [dbo].[BudgetPlans] ([BudgetPlanId]) ON DELETE CASCADE
    ALTER TABLE [dbo].[BudgetPlanCategories] ADD CONSTRAINT [FK_dbo.BudgetPlanCategories_dbo.Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId]) ON DELETE CASCADE
    ALTER TABLE [dbo].[BudgetPlanCategories] ADD CONSTRAINT [FK_dbo.BudgetPlanCategories_dbo.Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[BudgetPlanCategories] ADD CONSTRAINT [FK_dbo.BudgetPlanCategories_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [FK_dbo.Categories_dbo.Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [FK_dbo.Categories_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [FK_dbo.Categories_dbo.Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [dbo].[Budgets] ([BudgetId])
    ALTER TABLE [dbo].[Users] ADD CONSTRAINT [FK_dbo.Users_dbo.Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[Users] ADD CONSTRAINT [FK_dbo.Users_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[Transactions] ADD CONSTRAINT [FK_dbo.Transactions_dbo.Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[Transactions] ADD CONSTRAINT [FK_dbo.Transactions_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[Transactions] ADD CONSTRAINT [FK_dbo.Transactions_dbo.Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId])
    ALTER TABLE [dbo].[Transactions] ADD CONSTRAINT [FK_dbo.Transactions_dbo.Accounts_SourceAccountId] FOREIGN KEY ([SourceAccountId]) REFERENCES [dbo].[Accounts] ([AccountId]) ON DELETE CASCADE
    ALTER TABLE [dbo].[Transactions] ADD CONSTRAINT [FK_dbo.Transactions_dbo.Accounts_TargetAccountId] FOREIGN KEY ([TargetAccountId]) REFERENCES [dbo].[Accounts] ([AccountId])
    ALTER TABLE [dbo].[BudgetUsers] ADD CONSTRAINT [FK_dbo.BudgetUsers_dbo.Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [dbo].[Budgets] ([BudgetId]) ON DELETE CASCADE
    ALTER TABLE [dbo].[BudgetUsers] ADD CONSTRAINT [FK_dbo.BudgetUsers_dbo.Users_InsertUserId] FOREIGN KEY ([InsertUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[BudgetUsers] ADD CONSTRAINT [FK_dbo.BudgetUsers_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
    ALTER TABLE [dbo].[BudgetUsers] ADD CONSTRAINT [FK_dbo.BudgetUsers_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
    CREATE TABLE [dbo].[__MigrationHistory] (
        [MigrationId] [nvarchar](150) NOT NULL,
        [ContextKey] [nvarchar](300) NOT NULL,
        [Model] [varbinary](max) NOT NULL,
        [ProductVersion] [nvarchar](32) NOT NULL,
        CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
    )
    INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
    VALUES (N'201905202050246_InitialCreate', N'ExpenseTracker.Persistence.Context.ExpenseTrackerContext',  0x1F8B0800000000000400ED3D5D6FDC3892EF07DC7F68F4D3DD22EBB69DCD201BD8BB706C6737987C619C19DC3E0572376D0B514BBD923A63E3B0BFEC1EEE27DD5F38EA9B1F45B228515477D218601037C96255B1585522AB8AFFF73FFF7BF6D7C77534FB46D22C4CE2F3F9C9D1F17C46E265B20AE3FBF3F936BFFBE3CBF95FFFF2EFFF7676BD5A3FCE7E6BFA3D2FFAD19171763E7FC8F3CDABC5225B3E9075901DADC3659A64C95D7EB44CD68B60952C4E8F8FFFBC383959100A624E61CD6667BF6CE33C5C93F20FFAE765122FC926DF06D1FB6445A2ACFE9DB6DC9450671F8235C936C1929CCFAF1F3724CEC8E734587E25E9D1A702A52CA73893230A26278FF97C76118501C5EC864477F35910C7491EE414EF57BF66E4264F93F8FE66437F08A2CF4F1B42FBDD0551466A7A5E75DDB1A41D9F16A42DBA810DA8E536CB93B525C093E735AF16E2F05E1C9FB7BCA4DCBCA65CCF9F0AAA4B8E9ECF2F96CB842EC57C26CEF5EA324A8B7E187E1F5DDD56CB7654837B36637A3D6BA5860A57F1DFB3D9E536CAB729398FC9364F838876DFDE46E1F267F2F439F94AE2F3781B452CDE1473DAC6FD407FFA94261B92E64FBF903B9E9AB7ABF96CC10F5F88E3DBD1F2D08AECB771FEFC743EFB4051096E23D20A09C3A29B3C49C9DF484CD22027AB4F419E9394AEF1DB1529D92C21214C59FCBF998D4A25DD70F3D9FBE0F11D89EFF387F339FDE77CF6267C24ABE6971A835FE390EE4F3A284FB704C0503FEB4D1EA4399DEB75100574751A04AEC8325C07D17CF629A5FFAA95C1CBF9EC661914A0214EE8E7B9DCA629E5C3D8D3D4EB564037AF9D1ED4EBEDEA9E2024400FE52DDD2B694E154DDA410256F7E4F4E518AB5BCDFE39EC24EB8ACA66F5B725A85F372B3AD4192198C960BC0D146717CB3CFCD68E7B9D2411096233B91F826FE17DB991D522359FFD42A2B24FF6106E2A8372C4B47FA9FF9D5116A4C9FA9724E2C7B7ED5F3E0729152E8A64A2E974936CD3A505A695C48248564D207E4293849AD86E8B55B70374ECFBC2769398C7B4B6F30BAC63BB34246051A4C62CCE826589D9EBA76A82D6146A70D68E9389D0745752A51B338CCC6AB43D99C23813995C772499FC185B323B4DA5A58AED2613D1B52A7166BA40289E2D3AF70AE374550AC6A9E3550C9CD8F96A2C714F070C67C8CD4ED88724369A8E691CB0D10C56E6CE5A2976006CD27AED81C674B911FF0ADA5492DF398EB6428F7539F7FDA3A3FA18583E1D9CEB8373ADD155164EABA8A1544EAD9D2BFD897EACEA502BDABF34AA4B448E6954F8D46C8F7EBE7E21313A044BE7448520D3A84090ED618BE02595A9FB240D89766DD95ED2EA768DAAF5657AD8AE70A38340EC9AC69A780E39B14DE29DD4C1F117534D3BFCC12435AA5837E073C9E060D713C0FEB5D4A8C2CF9577DD6D35B7DE4501715A0FA3C0A0BF97D18CF6E569BCA76C7C1866EEFF4182F470A677703B86BB1DC69332B46587B51764FBED5D0F9409653A3616A7527610D640478D6F02F51EC596959075F64CE8A0E3FAD876AD9C4467DB840E3A5CDDDBB866C9DCDBBA06F2F436AFC16498ED63A1F8B281C5BC31595DACABA3D391EEC06CECBBE13B1DCDA383C1FC110C666558C633456A55A9335C965FA48A2F3E7902F8DB54DB51FE16D4F61ECD98B6D39A8C2AD011B30A3E8C6C3B99C9D8021D3134B832BEAE4DEED4867688799DC2A84E73847DF8923C18465CCC05F68853117731C05CA0BF241D193F51E9E24CA523E36730791843E7C2BCB1810C7A3CF99E00A66C0735AE5C2FC7C6D860823186D7B9B9AD90E8656A1B8B7254C098CABCF631AB931887BEAA725C1377B068E2056AB1480A6F443FF47A1D849186B4D3173F3920AD9C853AB97761BA26ABA1D2F429C8B2DF9374F5F7207B70E0F41962A3C9729B527D719307EBCDE8B37D7A4862F261BBBE25A9CFB99C2DCDE7DF9337D40E25E9755C8C1A0CEF5DB2FC9A6CF3EB7855EC855FF3A5EDD668013841E762B92459F6860A33595DB2E777FD3CFF42B118BE5CB0FBAFB7EF7A1905E11AF6524A3BDDB477169FF959B2F46C9B63DFAE040DFB7542138CD5007FEE5D721F2A3CB9125ED32E6054FD0C6353B7D9625240D02052370B7894BFC268544D8E3DC61232EC2D0A4D3052AEBCC4C6D52B209532A97319DFB7095317D9E603C93B47B182FB26A530A9D5F97A24817D36430FEE7CCC53AC8FF9FCE4F6EEF9CB173F05ABE73FFD893C7FE1DFDF1CFFF866AAC38F72F9AA98EB918D6D39D36F41B4753D55AFDD506A1FF7BBA104BBFBBBA14493FEFC2D5C15DA69611ED174A6E051FD1B79B6DD730266BEB70347A6EFC9FDE8805EDBA5B04DEE774B0175F7370B2CCA60D782A03E523F95F66FF0DD1189630EF15C5D233120A73AEA6250E8231BC2705F0E49F17939F8F468E4108F2B922DD37053C9CBD8EE8BA320102E777128302E43D0F2DCEB70E9B6EF976EB8480EBB0B1665E406780DE3E8608381AD38DF807B481FCF8A6EB65FF793A43E2B72E151E9D2E86BB929929D7184C109D28ECE6358B9808F65E01E5A01731BB63AE0424F11AE3AE5F55E8741FFF0545EA38FEFF1B8896599D6A01ECCF9FE9A73630C0D3A8D110EFB83121D9D865C6AEE27C00E3A3CC70EADD49CD0831D74B81A0C8116570C961AFC4C980D0FEB6CF3431D8575D6F0260BEB6472EEADC33AD1F9FA6E2A7134F35D52D6E934E18B3134FABB24BE37DCCC9EBC381E63E6AB30DB44C19371F29D0AF731687545109C4D7EB51452A64AC01E74D8EBFEA0773F0E79F7251E6DEA600975A90576BDBFD4DD3A71965B258106BAD888F4459625CBB0C4AA8B55918BFCF0745EC7AB19A6E24FC56FA17210E53C95CC70436591E243977C2ECAD9C7F88A442427B38BF293B1384EC996C14AE637A569658559FB512D612662F50769322AF5A4A842190645445A46F75418E7F21609E365B80922047F84B1E006D316C22A38D04E28B65C11EA5E14B281E087134CDA09853532F1ED6CC1C8A05E34C59A2EAAB5571678E996BDF9201065515C753550EFB2A4A00AB378AAAA525612A460C0D0F9BDC90D9B96AF5F6430475F949D2A3BD1B4D23AD08004A9C4D2A18A1C207B00F29EA50F5899BD923F28C3D52C30DA7457482EBB3B8E3EF2A92DF480D906BB24AF1A62F0920357CBE929BF9AD57481910779D6A77CA9040C99FFD509984A8A47F21891C9D9C3F65B2F6946710E233BEAF45C2B5946716A383E1E24990D5557C90518B7DE494175D0E84542A128790613206E79147104188259EC814207108F99551987E64DBC98B37DEDC24247FC7A31D39973D57D8001E4F1D191FC11D45F4A64047C498ACC4DD4CCDC7DE074325367866817574C13994E250949290A9554078F8FA7927886F812349EF8FD504955BE8F764D85E49FE9848B4F3552C85675003F9E6871DCF025591CE5FB2158CCDDB07651A12BE281B60EB84FF66BEB64047C098ACC4D94B4702123D37E48227C246D110EF347A34E7E74B53B8C9ACFED17DE240E9386B57BE234296261550B6E0A8CEDD69C4BAEC04B9421A4D68F50E991F020577A36EFA16821CC9B2924D6A9684D69F3F4787896AEFDB7805CC281D150C1E59D3047A7082B08A736F414DC61E610A2738A834E88257B71C00955E1320A805E6F0DF2B0A6D4581A247C3A597BABABF42562554B8FAC173BE235E6640E180A170FA2875A823D71C7F4357E2DA442AFE49C0BE1948A0F85CE3472F81D28433B2588567E3D638B7640D94DADE4BE07E566A7D4D0CAACA750ED86F29A5A69EDBBB22A11470540323DD5F2647B4AAFCEA7DB9B004800798C04380C8004566628067EE50F6D2C11311183E5706263395D888496C57B652CB1D78E86FC54874235BDB19CEE3A52CBE5FD339668891A4D9626554D130A8FADD84C2F30EC5B1DFA9505DF2676909AA48F561E394659499A670709E0C21E3848524EB3F2205D99E0CCAC745B7DC04FDCBCF24D6B934CBBBA2D52F004B3EEEACA067687FB0A1E0C47C19FFE427BE628AFDCDEE04DED8D4FE989EFBB178EF7C051DE770FE199DEEB9ED2E3DE5F6FBBA95D87D03E72574DA9011B110200FB55406A043C48909AAD7BA2823065254D4B8FA9176916B2716B59E00A61FA8CD3B1E01E46969892BC0EE419C12D0C5252E9E11D9271BEC2A89DF8C0A543CD320E9A4DBBC9764856412E4C2DAB20B730484995AD279555844F2877756CD2A7740BD53878B4EA7BEB180295B1548BAD2B9325E78B41B96223196E4D712EFF896C6A2E7910473527309317A33C09E27589665181938E20698D005FACB3AECF0954FCA394D545FFB2BA689C281A05F41B92F3AA329BCFAEDBF26CA2D293741C08A29C530DA6AABC6600D5165494A0349FE5280045AC841A4815868206C4BE25AD81D89D6E1B206BE1A1A154FA4806501914C360A94E03040928E66001B67959500BB64EC8B6005BE50DEBA1561ACC00947F5E4182C779832851512D087B0367928CEA1C562119ED99BA018A8945207B185D046EECAEE4DD8CE92AEF7045693CC833824B36B6D4887A45D2BC06808D9993014AC038554C7B22582394A283B8227651E32FD6B50378D12A460D1B44303E38C096B453F24059F70E401FAA7C27F1A1D6EF465E40B5EECC4C1DC40CA8BE9A9631C6826C0ABA7425D94086B17607C5395D1536DC92F4E0A4BEB217C0498B52601C9DB862600C9D48FEE18A7859AE500F46961F5C8D7197D9C636AB89617A412CA96D9E861B2C008076C811714339FB9E81827AD581BF4C0070DE6FCF05E06CDF04A42FF18DF3A5201CAAED23E32B54F7B1275828CAA358F606573794D7CE8F82F0AAD580365769A507D95CD11415D5359E6E8866DFC35050AE3A0993D1070EC2EC79001C7A8D20EB5012A0CE42A036BC2E47708035F0B3FB552FB2C94CC1D4ABE0283154AC6088E13FAD34CC31D4A818993FFA5D8329BAA02446BF877AF1C7F38EE271D4EC2975D500781F80750306EC2BB0540092D94318A4971E6366284C8C5E6E6C59E34764F439D9B84F1F9402C2A5713BFEF4F1A396F429C5381EA2240D9785EC9887BEE5102D7FB672879637F4D9846FF1428B95AD38A1C508CD1A9F5253CE623AC5623B992860FAAAD961F6A5D5098B239E62213E9F11D9742A3A305BC88A313EB710E26B0B9113A6A204B385AC58E37D0B619882678743467861019B7FA3BE0F401D4EAAD374FADD09E84F209D9E3BCA8FE301EEB136D984F76655E9262CFECCBD99CE3156A5898CA84C518AD44689A214284A267C2A4E94D2B4519828658962831F25090499AB2F5271F2A0094647DCF8994079E0852E5A5AC31C749035482226CCBA2FFB30D1D1E31D4D600274D16CD5C4F5224887237BDDB0150EE41D9FAD7A0D668A380509D3EB302B1EF9D162D05BA1322F4C71931C019AC8498600F0BAC404C7F1154CF380691B99D7B69D2D6E960F641DD43F9C2D689725D9E4DB20AA9ECF6E1ADE079B4D18DF67DDC8FA97D9CD2658162ECA1F6FE6B3C7751467E7F3873CDFBC5A2CB2127476B46E5FE25D26EB45B04A16A7C7C77F5E9C9C2CD6158CC592E3B31847D8CE942769704F84563A35C5F44D9866F9559007B741F1B8ECE56A2D7583E310791EB60C6FE614420DE5556C42959A01C5BFA1B847F091728596EAB8FB8612BC2E2241CB47BDE52D268FA4636F964114A4C233BA4C60FB65126DD7B131145F0DABF83F0FA6FA050FE1260FD29CD2F79AB6C54B0198D488875B39CA3908566CC34315DE6F05F8A77ADA550DB34B9A67C1A953E9D590F82C32169A3EBFCC04F17328AE32FB3B1E1A1F0FCFC2D347CA9B20CAF8B1BF5B50DBBEC0CED1DAFE2A433A5B08DB53540B0B492F08AA5AD433365AA88A2A1C471395A1CEBDB5113CDAE7AE1AAE99F6461AD407108304013E2646C8806AE0F80A70F8A2B365246493019797D088D04121FF180A99BD1B1A651B4221B5E8AD080FD66FC7E6695C794BAA1ECD55437C4F4979E041D53FE161FC8304290FA2FAE5E0671DB6B5876DADBDF170B4BFDBBCA921FB5C0DC4BCDFD97736E07DAF7B89433D43313226AB8B75757EC782169A6C77B34B2DA5A2BE1FCD071DF183E888F134C3007D60AF05DC4AFF7047FC60AD0F3BD17227AA8EE9076D42E81A04B101E1614A168B8269278ED60B74D82AE36C958AE3B0EA12DBF050AFD74118F1C0EA9F2C615099BE0BD3355901C098360BC72EC8B2DF9374F5F72013BEEFF8168BEB00B2DCA6C526CA83F546B80CE09B2CB07C4862F261BBBE25C21724D7D00B9E82A3700FFC0C9F7F4FDE044BAA52AEE3E03612A1CBAD78C8EF92E5D7649B5FC7ABAB62BFE44B1E34D0DC033680B3D86675E542B2EC0D1551B2BA943F2080668BDD4FF585ECAA74BFEE8C6903724C1DDB39B95286BDD143C018C7028216C1DA16942857255E380FB8FBD912D66F41B48580D5BFEFA47429F36A9D495755306598742960A83513ED4E7FFB16AE441B20345958817ACCCFE44950FE6CC35832BC0392A24A447626286591976172028318578D3465B65828AAD25B932DA421E571D01AB27587EC974F3B5AE930758344D60B4DF8752C3C201E54F58B85EB021C78DA9F745E916C99869BAA8C13870EDB30D539A754D89573D94D555F352B2A16E1E4D6D454A1530DF7F089FB839C06B12936A35CD9F43C19D20DD61F864242C1B74C71BCEAC6681EB6E50FB22D993C27D7D7254D71C11ED725CAA14A23EA344EA81955C42FC3F0AA169BB398F85E3ED9E87EB5703FC26C13054F3230AEE13B94D451BF6F067EDBD87DD70C3B5C91577E92C331297541ECD2CE5EFFD2FEDDA62ED469035C3E434975919D50529BD5290C621E41D5653E6BBEEDA96BFB4457767D547438BAF967741985A470EB9B0EEF8338BC2359FE39F94AE2F3F9E9F1C9E97C76118541562598D41912AFC4DAC7A8948993E745CA0459AD17E270FBC48B024A96AD2220EDA258263E4257C891F8993C892B6C51E2FF6C218E3F13BF9FBAA1550DD8B06071B927FE46A804503BB9FA14E43949E36E6BCC671FB651541C339FCFEF822893EBC482F25D4D107F0BD2E54390FEC73A78FC4F6B4852BA4205744596E13A880AE1A0FFCACA553E794909A1BB8D369F5ACF23E62F8C348D107BCD2C8154B1FB6DBC228FE7F3FF2E47BE9ABDFDAF2FDCE067B38F29DD14AF66C7B37F59A3D139A5761834E3064DCE7B9FBC90A0B06001F098BC0F1EDF91F83E7FA0CB74FAB22766959F580B00DD0F79F9B72528DE85ED41240B004B6459D71D859889460CA4CEBFA8E0DC86B9814DACB1C2E84639E5C14A3FC2190E681D096CD2C9D4E088BC86520B706C567DDA9A390CEA9FDD3542EC17889DC2EC461E54E641658EBF8DE5B4049BAD0C07F862B773337AC42D5DA738F4D7C8558243FFF107C7E9A005F6430BC0E1CAB6DA407D8165A3155828236A0721F161A44F38A5B243AB816AEC2055A0E028C62369471E74D141178DA98B8668A0217AC793B671F77971702A0E1B799737B27CB18EDBC47D362FB43E43E5C89A333BB24B50CB7ED8241690C48411F0431003A84E16D148EAE98B9F7A0165921BFA8B2B9F2A62B05118D48444110710B94411B7F09CB050CE08E90F0B48011926C762EA477FD4805C8FFEA7235D9687ADF66846963F3F9BBDCD7E8DC37F6E69C367CA0D4183083B6B8865336457F83073A3F8A64394B87B478EC9F670B0D1D9740F6B70BD24A37E8E56CDBC7FF5111B43960430429B0501BD950E06D0998553C0CCB13BC651E118F69472DF4BB4AA47771D4B962A7012FD7EB95944764BC13454F4C0A61AEA63D9954918B845D5E65C98174C183EA2EDA9323A067E8B8C7C86CBA579B8B049531FC74A5921765808C307A1222592D8A1220C57A182FF363E1C6DEDFB57FBE8F7657D0FB8749922D8FB317EE546D1C8939E2FEF96A7705009079560BAB602135790D756CA5415C4B5151C64D63354AD4A6ED17D5EBDE8718CD5A4BA68E11EDBBB636CDA8B5B9C47141475E2C85E5D8FF43BB02B881EFFC08E7974A33BAF6C6273BFC00F47C8AFA0B0474FECAB2CD5EFEFB7511E6EA27049E7A71C94B2933EC657242239995D2CAB6C92CB205B0672EA5841C3CA8009848588C11F24C0546448919C1006C5ED4096A74128E7017D4AC378196E8208A05DE80B4AA3A12AFCD9A29D406CB9221B12177B41A0D5C9AC2D7081D7269E700FB6E845AA7EF0CB284DCD43A0CC12363F893224AEE09482A0AC398F0FE81E6DF975137A5BF9F25959300ABED7DA8FA23F98E85E1987EAE7EF4278B495D977587EDA47AD9561D803D67274996A23B5607CBAE61165CC72CD55B5A87BC99AA62AB0FDFC1EE40E903655C964F69D536895556BFB9DCB9CD58AEBAAF88C2F6FFAD93D485BF97A2054219159D7EAFD436625AB1FBCC8947C89CF2202B48E22518AD23598CF3E2B29C21495B4B812F42640AAB76B6D85E8F8E848E75F1743AA620006387BB6EC2D5DA8B9B4F57DFCAD3954F8720795461DDFA1501A75EB5E4B0FA656E82E2A0DA05ECC0ECA4F15C4A1109FAAF1BB911E45919D5D141ED59BCB078BE3DEE2E82BB6F9FD2872E569E816DDEA5B6ACF16DEEAEB64075C0D26B4C9CBDAB3815C2C1CEEF7BD96007DB5E19D17024F8AFF2007BB6C05D4D5B8FB1F89EDA22C4C7192652B17931F61B552E149351CDC83DDD108C0258D0F2F61574EBA7D8945CFD3ED1D701D0001F1A4260E32B27F4AC4B3F2F07BDDEF5F10F64F49F8570E0719D82525509E2DEE45B8902485ECCFDF51B8105A6277225CC8E96DA85981F81702BF0A043FDBAE18119F97130719D8612372D8FE3E977EFA45FF8208D27312643EC5F1936FD3DF236673BAE3C73A29AB761B35E7D14DEE19B770ED8F1E7D478404BA3AC056BF31659360D743773898D19FEEF0E731FA5B7ABFA662BFBC44BF1EE20FBDE63BE015D649535E76B9FF14345F6B6E937AB6031BBD5974F6AAFAF513578BC594DA8A59C491FC84E9AEBC7B24987ABEF0363ED23CA17C7105765CC8D7CE86C9ECBE9C181FDDF62A279E3C8E83FDD911A7832D60511E46A93F4CB95A1750A83B14E6FE1D87DE1BDE9CDD8D10FCA2A327E1BAE6DF916DBE9DC5375F25F9AA9FA6E515D17C76DD560311F543F56AECF97C759BD075AF0A8AB4951B649D03C2AFEA7EA8E6A85A35F3141DCC73351F51D2344D033443733E84035E5D022B26A81AD59314ED361375C7879A09BB4EFA8999835013069A79F5B359CC511931097EF53304BB529626B040EAA93407D00714BD6CF3A13AECAE13716DE6AE33D8B473D77DF473D7F97C3673573A583B75D5453F739508669A987329A539B956683A2E6418B7331492C336AA77024E8ABAE35F7903B44DE006A85A311B80B7E2CAD532AD14BC4A8CC9005571577967C6749595B2A2420FE45BB21ABE7CE49DFB5DB287806F2A8F957D149E3004D162A521805E6D3122F0E08EC1B4F9696202D9823A4A129555771C10299B476664F5B34352A1DA2F5AB28DC5628611A3182D1A52014AD73C9835FAF224006B2CEA9970C48124A908999C2DE59D7F63BE6526B0CD6AC4598353A25AFDA02155E98594C3815637843227D92A625587DD0309EE12720DA3FA92D6B8410AB2A0AA070ED790F3E6A435AC5BDD105ADB73059D55EB5864B2AE884465D5E88648E6D44B45A9EA606C870515CA75D169DE91F6A29586EE41A622F717A01493253C9058E0F3A31CC8FDEE9464BDEC627262F7826438A95327CEEAEC4F470E8467C2F50B6DCC5BDAF15DAC4FD1C3F9D223EDE9291C457D421A8E1D2349C2B4EC404BC5F8D2E0E3C312BDEAE3AFF6D8E4966E96E9C880ED34CE9181C422F66797A4620479CC6F24CFE46204794C4FDB37B90842F797C42FFAC31C43ACBB83BD3AB63B22856B432E9736A49B475738C0AFD06D7F34AE29823DFDD712A388C65542A39287513CE32A9D71C803E245D5972763ADDFB8770998E8480DCDE8A0CABE244DF0398809E843B3441307B8872CD16F745384DB4E6F04204C0B20D114CC051EB42A0F59A103562F47B5CD4B406D2851DB76B6A82E9EEB1FE89F799206F7E47DB2225156FE7AB6F885B23C5C576F089D5D912CBCEF409C5198315972A14B6D9FB7F15DD244500918355D847793DE933CA0D2145CA4797847E59936174FA987F1FD7C563E4F7D3EBF5EDF92D5DBF8E336DF6C734A3259DF469CC52F22B174F39F2D249CCF3E96CF96662E48A06886C586F818BFDE86D1AAC5FB0DF04293024411E2553F5558AC655E3C5978FFD442FA90C4484035FBDAC8B4CF64BD8928B0EC637C1314EF77D9E346C5EF1DB90F964FDD5BD62A20E685E0D97E761506F769B0CE6A18DD78FA2795E1D5FAF12FFF0F1367A74179980100 , N'6.2.0-61023')
END

