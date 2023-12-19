declare @bid int = 1;
declare @uid varchar(50) = '66ba6cd8-e5fa-416f-b64a-5edbc0c96793';

delete [dbo].[BuisnessSubCategoryBarter] 
where [buisnessId] = @bid

delete [dbo].[BuisnessSubCategory] 
where [buisnessId] = @bid

delete [dbo].BuisnessStatus 
where [buisnessId] = @bid

delete [dbo].[BuisnessPicture]
where [buisnessId] = @bid

delete [dbo].[BuisnessArea] 
where [buisnessId] = @bid

delete [dbo].[Buisness] 
where [Id] = @bid

delete [dbo].[AspNetUserClaims] 
where [UserId] = @uid

delete [dbo].[AspNetUserLogins] 
where [UserId] = @uid

delete [dbo].[AspNetUserRoles] 
where [UserId] = @uid

delete [dbo].[AspNetUserTokens] 
where [UserId] = @uid

delete [dbo].[AspNetUsers] 
where [Id] = @uid


