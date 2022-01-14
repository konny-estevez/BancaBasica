USE [BancaBasica]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'23977903-5851-44a6-a637-4f3c591c0776', N'ROLE_ADMIN', N'ROLE_ADMIN', N'af930cb4-2813-44e7-9126-42b68a8b6ae7')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e38a5b6f-2ef7-4ead-b3ad-1821eb7863c5', N'ROLE_USER', N'ROLE_USER', N'ab1d7b8c-37a7-4569-bb90-890c256aa04e')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator], [Nombres]) VALUES (N'10b7e05b-8589-4e61-93f3-2dd86f0fd639', N'Cliente', N'CLIENTE', N'anna.patricia.najera@gmail.com', N'ANNA.PATRICIA.NAJERA@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEBezEgzf8hj1EwOnPoNncmpvNZCNQH3GlFuaDlzpe6IIMW4Lr5RjiMmDzvPaxIwR7Q==', N'DPRNGVPIRNFZF4Y27GL5RNBULBETAAVV', N'38a7d8f8-2a73-4fa2-a48c-dd920a45556f', NULL, 0, 0, NULL, 1, 0, N'UsuarioAplicacion', N'Anna Patricia Najera')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator], [Nombres]) VALUES (N'679bb697-9f8b-4826-a476-8190af0ce65e', N'Admin', N'ADMIN', N'admin.bancabasica@gmail.com', N'ADMIN.BANCABASICA@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEFKcLUKibBL7tNrdE/bNiWUszyYMN4+Xa4ewAFm1nhiA7jZJZ6fgflrzMgaOkQunXQ==', N'RUGQ4ZSEOR5BTFANBTSYEWYARDPUGJGX', N'055bf538-1032-4f28-88d0-d89304af2c57', NULL, 0, 0, NULL, 1, 0, N'UsuarioAplicacion', N'Administrador')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator], [Nombres]) VALUES (N'82bf0011-4833-4bad-9eab-5247745be359', N'Usuario', N'USUARIO', N'konny.estevez@gmail.com', N'KONNY.ESTEVEZ@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEAQw7j6vTcFpHHXMyHHRC3jlxe1bDQ1CZHBuWdKHKy+fV6TCjWhqecNZUbFMNOtexg==', N'X7NYVT3SOROAUF6NCAT5TCQQJCYY3NUI', N'1d20961e-e457-4acf-833f-f95f3bd79a11', NULL, 0, 0, NULL, 1, 0, N'UsuarioAplicacion', N'Konny Estevez')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'679bb697-9f8b-4826-a476-8190af0ce65e', N'23977903-5851-44a6-a637-4f3c591c0776')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'10b7e05b-8589-4e61-93f3-2dd86f0fd639', N'e38a5b6f-2ef7-4ead-b3ad-1821eb7863c5')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'82bf0011-4833-4bad-9eab-5247745be359', N'e38a5b6f-2ef7-4ead-b3ad-1821eb7863c5')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Direccion], [Telefono], [Email], [CreadoEn], [ActualizadoEn], [NombreUsuario]) VALUES (N'817851c8-0adf-445a-3120-08d9d77a862f', N'Konny Estevez', N'Miami Oe7D y AV. Giovanny Calles', N'0991398258', N'konny.estevez@gmail.com', CAST(N'2022-01-14T11:25:48.6828244' AS DateTime2), CAST(N'2022-01-14T11:47:39.4552291' AS DateTime2), N'Usuario')
GO
INSERT [dbo].[Clientes] ([Id], [Nombres], [Direccion], [Telefono], [Email], [CreadoEn], [ActualizadoEn], [NombreUsuario]) VALUES (N'b8bdfc25-68e8-4d9a-129b-08d9d77ddd9d', N'Anna Patricia Najera', N'Miami Oe7D y AV. Giovanny Calles', N'0996330480', N'anna.patricia.najera@gmail.com', CAST(N'2022-01-14T11:49:43.8564233' AS DateTime2), CAST(N'2022-01-14T11:50:02.8829366' AS DateTime2), N'Cliente')
GO
INSERT [dbo].[Cuentas] ([Id], [Tipo], [Numero], [Saldo], [CreadoEn], [ActualizadoEn], [IdCliente]) VALUES (N'a789ed14-bdd8-4ae8-dff9-08d9d77e06a6', N'A', N'1111111111111111', CAST(80.00 AS Decimal(18, 2)), CAST(N'2022-01-14T11:50:52.7074177' AS DateTime2), CAST(N'2022-01-14T13:39:48.5925462' AS DateTime2), N'b8bdfc25-68e8-4d9a-129b-08d9d77ddd9d')
GO
INSERT [dbo].[Cuentas] ([Id], [Tipo], [Numero], [Saldo], [CreadoEn], [ActualizadoEn], [IdCliente]) VALUES (N'00da5649-7f36-4ea5-dffa-08d9d77e06a6', N'A', N'1111111111111112', CAST(0.00 AS Decimal(18, 2)), CAST(N'2022-01-14T11:51:13.3523926' AS DateTime2), CAST(N'2022-01-14T12:10:33.0231584' AS DateTime2), N'817851c8-0adf-445a-3120-08d9d77a862f')
GO
INSERT [dbo].[Cuentas] ([Id], [Tipo], [Numero], [Saldo], [CreadoEn], [ActualizadoEn], [IdCliente]) VALUES (N'4667bf13-3c98-4eac-66ce-08d9d7818f39', N'A', N'1111111111111113', CAST(0.00 AS Decimal(18, 2)), CAST(N'2022-01-14T12:16:10.3108466' AS DateTime2), CAST(N'2022-01-14T12:16:10.3110002' AS DateTime2), N'b8bdfc25-68e8-4d9a-129b-08d9d77ddd9d')
GO
INSERT [dbo].[Cuentas] ([Id], [Tipo], [Numero], [Saldo], [CreadoEn], [ActualizadoEn], [IdCliente]) VALUES (N'224b3fa7-d668-4707-66cf-08d9d7818f39', N'C', N'1111111111111114', CAST(0.00 AS Decimal(18, 2)), CAST(N'2022-01-14T12:16:34.3974463' AS DateTime2), CAST(N'2022-01-14T12:16:34.3974546' AS DateTime2), N'817851c8-0adf-445a-3120-08d9d77a862f')
GO
INSERT [dbo].[Movimientos] ([Id], [EsCredito], [Fecha], [Descripcion], [Valor], [ActualizadoEn], [IdCuenta]) VALUES (N'bd37f84c-6f6b-4b36-49d7-08d9d78457a6', 0, CAST(N'2022-01-14T00:00:00.0000000' AS DateTime2), N'correccion', CAST(20.00 AS Decimal(18, 2)), CAST(N'2022-01-14T13:39:48.5926058' AS DateTime2), N'a789ed14-bdd8-4ae8-dff9-08d9d77e06a6')
GO
INSERT [dbo].[Movimientos] ([Id], [EsCredito], [Fecha], [Descripcion], [Valor], [ActualizadoEn], [IdCuenta]) VALUES (N'25c91d5a-e384-4ed2-49d8-08d9d78457a6', 1, CAST(N'2022-01-14T00:00:00.0000000' AS DateTime2), N'deposito 2', CAST(100.00 AS Decimal(18, 2)), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'a789ed14-bdd8-4ae8-dff9-08d9d77e06a6')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'5.0.13')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220114160811_CargaInicial', N'5.0.13')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220114160951_CreacionEntidades', N'5.0.13')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220114161050_ExtensionMicrosoftIdentity', N'5.0.13')
GO
