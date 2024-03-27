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
GO

CREATE TABLE [CartItems_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Quantity] int NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [Sub_total] decimal(18,2) NOT NULL,
    [Product_Id] int NOT NULL,
    [ShoppingSession_Id] int NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_CartItems_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Discounts] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Desc] nvarchar(max) NULL,
    [Active] bit NOT NULL,
    [Discount_percent] decimal(18,2) NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    CONSTRAINT [PK_Discounts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Discounts_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Desc] nvarchar(max) NULL,
    [Active] bit NOT NULL,
    [Discount_percent] decimal(18,2) NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_Discounts_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Inventories] (
    [Id] int NOT NULL IDENTITY,
    [Quantity] int NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    CONSTRAINT [PK_Inventories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Inventories_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Quantity] int NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_Inventories_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OrderDetails_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Total] int NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    [PaymentDetailsId] int NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_OrderDetails_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [OrderItems_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Quantity] int NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [OrderDetailId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_OrderItems_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PasswordResetTokens_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [UserId] int NOT NULL,
    [Token] nvarchar(max) NOT NULL,
    [Expiration] datetime2 NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_PasswordResetTokens_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PaymentDetails] (
    [Id] int NOT NULL IDENTITY,
    [Amount] int NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Payment_Type] nvarchar(max) NOT NULL,
    [Card_Holder_Name] nvarchar(max) NULL,
    [Card_Number] bigint NULL,
    [CVV] int NULL,
    [Expiration_Month] int NULL,
    [Expiration_Year] int NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    CONSTRAINT [PK_PaymentDetails] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PaymentDetails_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Amount] int NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Payment_Type] nvarchar(max) NOT NULL,
    [Card_Holder_Name] nvarchar(max) NULL,
    [Card_Number] bigint NULL,
    [CVV] int NULL,
    [Expiration_Month] int NULL,
    [Expiration_Year] int NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_PaymentDetails_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Permissions] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Permissions_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_Permissions_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ProductCategories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Desc] nvarchar(max) NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    CONSTRAINT [PK_ProductCategories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ProductCategories_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Desc] nvarchar(max) NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductCategories_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Desc] nvarchar(max) NULL,
    [SKU] nvarchar(max) NULL,
    [Price] int NOT NULL,
    [In_stock] bit NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [DiscountId] int NOT NULL,
    [Inventory_Archive] int NOT NULL,
    [ProductCategoryId] int NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_Products_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Roles_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RolesPermissions_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [RoleId] int NOT NULL,
    [PermissionId] int NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_RolesPermissions_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ShoppingSessions_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_ShoppingSessions_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserAddresses_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Address] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_UserAddresses_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Username] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Telephone] nvarchar(max) NOT NULL,
    [Is_active] bit NOT NULL,
    [Last_login] datetime2 NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Username] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Telephone] nvarchar(max) NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [Is_active] bit NOT NULL,
    [Last_login] datetime2 NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_Users_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UsersRoles_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_UsersRoles_Archive] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Desc] nvarchar(max) NOT NULL,
    [SKU] nvarchar(max) NOT NULL,
    [Price] int NOT NULL,
    [In_stock] bit NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [DiscountId] int NULL,
    [InventoryId] int NULL,
    [ProductCategoryId] int NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Discounts_DiscountId] FOREIGN KEY ([DiscountId]) REFERENCES [Discounts] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_Products_Inventories_InventoryId] FOREIGN KEY ([InventoryId]) REFERENCES [Inventories] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_Products_ProductCategories_ProductCategoryId] FOREIGN KEY ([ProductCategoryId]) REFERENCES [ProductCategories] ([Id]) ON DELETE SET NULL
);
GO

CREATE TABLE [RolesPermissions] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] int NOT NULL,
    [PermissionId] int NOT NULL,
    CONSTRAINT [PK_RolesPermissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RolesPermissions_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RolesPermissions_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderDetails] (
    [Id] int NOT NULL IDENTITY,
    [Total] int NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [UserId] int NOT NULL,
    [PaymentDetailsId] int NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderDetails_PaymentDetails_PaymentDetailsId] FOREIGN KEY ([PaymentDetailsId]) REFERENCES [PaymentDetails] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderDetails_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [PasswordResetTokens] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [Token] nvarchar(max) NOT NULL,
    [Expiration] datetime2 NOT NULL,
    [Created_At] datetime2 NOT NULL,
    CONSTRAINT [PK_PasswordResetTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PasswordResetTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ShoppingSessions] (
    [Id] int NOT NULL IDENTITY,
    [Total] decimal(18,2) NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_ShoppingSessions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ShoppingSessions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserAddresses] (
    [Id] int NOT NULL IDENTITY,
    [Address] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_UserAddresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserAddresses_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserPayments] (
    [Id] int NOT NULL IDENTITY,
    [Payment_Type] nvarchar(max) NOT NULL,
    [Card_Holder_Name] nvarchar(max) NULL,
    [Card_Number] bigint NULL,
    [CVV] int NULL,
    [Expiration_Month] int NULL,
    [Expiration_Year] int NULL,
    [Created_At] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_UserPayments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserPayments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserPayments_Archive] (
    [Id] int NOT NULL IDENTITY,
    [Archive_Id] int NOT NULL,
    [Payment_Type] nvarchar(max) NOT NULL,
    [Card_Holder_Name] nvarchar(max) NULL,
    [Card_Number] bigint NULL,
    [CVV] int NULL,
    [Expiration_Month] int NULL,
    [Expiration_Year] int NULL,
    [Created_At] datetime2 NOT NULL,
    [UserId] int NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Peformed_At] datetime2 NOT NULL,
    [PeformedById] int NOT NULL,
    [Record_Type] nvarchar(max) NULL,
    [Reference_Id] nvarchar(max) NULL,
    CONSTRAINT [PK_UserPayments_Archive] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserPayments_Archive_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UsersRoles] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_UsersRoles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UsersRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UsersRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderItems] (
    [Id] int NOT NULL IDENTITY,
    [Quantity] int NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [OrderDetailsId] int NOT NULL,
    [ProductId] int NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItems_OrderDetails_OrderDetailsId] FOREIGN KEY ([OrderDetailsId]) REFERENCES [OrderDetails] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CartItems] (
    [Id] int NOT NULL IDENTITY,
    [Quantity] int NOT NULL,
    [Sub_total] decimal(18,2) NOT NULL,
    [Created_At] datetime2 NOT NULL,
    [Modified_At] datetime2 NOT NULL,
    [ProductId] int NOT NULL,
    [ShoppingSessionId] int NOT NULL,
    CONSTRAINT [PK_CartItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CartItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartItems_ShoppingSessions_ShoppingSessionId] FOREIGN KEY ([ShoppingSessionId]) REFERENCES [ShoppingSessions] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CartItems_ProductId] ON [CartItems] ([ProductId]);
GO

CREATE INDEX [IX_CartItems_ShoppingSessionId] ON [CartItems] ([ShoppingSessionId]);
GO

CREATE INDEX [IX_OrderDetails_PaymentDetailsId] ON [OrderDetails] ([PaymentDetailsId]);
GO

CREATE INDEX [IX_OrderDetails_UserId] ON [OrderDetails] ([UserId]);
GO

CREATE INDEX [IX_OrderItems_OrderDetailsId] ON [OrderItems] ([OrderDetailsId]);
GO

CREATE INDEX [IX_OrderItems_ProductId] ON [OrderItems] ([ProductId]);
GO

CREATE INDEX [IX_PasswordResetTokens_UserId] ON [PasswordResetTokens] ([UserId]);
GO

CREATE INDEX [IX_Products_DiscountId] ON [Products] ([DiscountId]);
GO

CREATE INDEX [IX_Products_InventoryId] ON [Products] ([InventoryId]);
GO

CREATE INDEX [IX_Products_ProductCategoryId] ON [Products] ([ProductCategoryId]);
GO

CREATE INDEX [IX_RolesPermissions_PermissionId] ON [RolesPermissions] ([PermissionId]);
GO

CREATE INDEX [IX_RolesPermissions_RoleId] ON [RolesPermissions] ([RoleId]);
GO

CREATE INDEX [IX_ShoppingSessions_UserId] ON [ShoppingSessions] ([UserId]);
GO

CREATE INDEX [IX_UserAddresses_UserId] ON [UserAddresses] ([UserId]);
GO

CREATE INDEX [IX_UserPayments_UserId] ON [UserPayments] ([UserId]);
GO

CREATE INDEX [IX_UserPayments_Archive_UserId] ON [UserPayments_Archive] ([UserId]);
GO

CREATE INDEX [IX_UsersRoles_RoleId] ON [UsersRoles] ([RoleId]);
GO

CREATE INDEX [IX_UsersRoles_UserId] ON [UsersRoles] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240326091907_Add cascade behavior', N'7.0.0');
GO

COMMIT;
GO

