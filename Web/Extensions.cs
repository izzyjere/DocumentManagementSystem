using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using SimpleAuthentication;

namespace RTSADocs
{
    internal static class Extensions
    {
        public static bool TableExist<T>(this DbContext db) where T : class
        {
            try
            {
                db.Set<T>().Count();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal static IApplicationBuilder MigrateDb(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var domainDb = scope.ServiceProvider.GetService<Data.DataAccess.DatabaseContext>();
            var identityDb = scope.ServiceProvider.GetService<IdentityDatabaseContext>();
            domainDb.Database.Migrate();
            identityDb.Database.EnsureCreated();
            identityDb.Database.ExecuteSqlRaw(@"IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Identity') EXEC ('CREATE SCHEMA [Identity] AUTHORIZATION [dbo]');");
            identityDb.Database.OpenConnection();
            identityDb.Database.BeginTransaction();
            if (!identityDb.TableExist<User>())
            {

                identityDb.Database.ExecuteSqlRaw(@"
                                            CREATE TABLE [Identity].[Users](
	                                            [Id] [nvarchar](450) NOT NULL,
	                                            [IsActive] [bit] NOT NULL,
	                                            [RefreshToken] [nvarchar](max) NULL,
	                                            [RefreshTokenExpiryTime] [datetime2](7) NOT NULL,
	                                            [UserName] [nvarchar](max) NULL,
	                                            [NormalizedUserName] [nvarchar](max) NULL,
	                                            [Email] [nvarchar](max) NULL,
	                                            [NormalizedEmail] [nvarchar](max) NULL,
	                                            [EmailConfirmed] [bit] NOT NULL,
	                                            [PasswordHash] [nvarchar](max) NULL,
	                                            [SecurityStamp] [nvarchar](max) NULL,
	                                            [ConcurrencyStamp] [nvarchar](max) NULL,
	                                            [PhoneNumber] [nvarchar](max) NULL,
	                                            [PhoneNumberConfirmed] [bit] NOT NULL,
	                                            [TwoFactorEnabled] [bit] NOT NULL,
	                                            [LockoutEnd] [datetimeoffset](7) NULL,
	                                            [LockoutEnabled] [bit] NOT NULL,
	                                            [AccessFailedCount] [int] NOT NULL,
                                             CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
                                            (
	                                            [Id] ASC
                                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                                            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");
                identityDb.Database.ExecuteSqlRaw(@"CREATE TABLE [Identity].[UserProfiles](
	                                            [UserId] [nvarchar](450) NOT NULL,
	                                            [FirstName] [nvarchar](max) NOT NULL,
	                                            [LastName] [nvarchar](max) NOT NULL,
	                                            [MiddleName] [nvarchar](max) NULL,
	                                            [AvatarUrl] [nvarchar](max) NULL,
	                                            [Created] [datetime2](7) NOT NULL,
                                             CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
                                            (
	                                            [UserId] ASC
                                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                                            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

                                            ALTER TABLE [Identity].[UserProfiles]  WITH CHECK ADD  CONSTRAINT [FK_UserProfiles_Users_UserId] FOREIGN KEY([UserId])
                                            REFERENCES [Identity].[Users] ([Id])
                                            ON DELETE CASCADE

                                            ALTER TABLE [Identity].[UserProfiles] CHECK CONSTRAINT [FK_UserProfiles_Users_UserId]");
            }
            if (!identityDb.TableExist<Role>())
            {
                identityDb.Database.ExecuteSqlRaw(
                     @"CREATE TABLE [Identity].[Roles](
	                            [Id] [nvarchar](450) NOT NULL,
	                            [Description] [nvarchar](max) NOT NULL,
	                            [Name] [nvarchar](max) NULL,
	                            [NormalizedName] [nvarchar](max) NULL,
	                            [ConcurrencyStamp] [nvarchar](max) NULL,
                        CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
                        (
	                        [Id] ASC
                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]"
                    );
            }
            if (!identityDb.TableExist<IdentityUserRole<string>>())
            {
                identityDb.Database.ExecuteSqlRaw(
                    @"CREATE TABLE [Identity].[UserRoles](
	                             [UserId] [nvarchar](450) NOT NULL,
	                             [RoleId] [nvarchar](450) NOT NULL,
                         CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
                         (
	                         [UserId] ASC,
	                         [RoleId] ASC
                         )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                         ) ON [PRIMARY]"
                    );
            }
            if (!identityDb.TableExist<IdentityUserLogin<string>>())
            {
                identityDb.Database.ExecuteSqlRaw(
                    @"CREATE TABLE [Identity].[UserLogins](
	                         [LoginProvider] [nvarchar](450) NOT NULL,
	                         [UserId] [nvarchar](450) NOT NULL,
	                         [ProviderKey] [nvarchar](max) NOT NULL,
	                         [ProviderDisplayName] [nvarchar](max) NULL,
                          CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
                         (
	                         [UserId] ASC,
	                         [LoginProvider] ASC
                         )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                         ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]"
                    );
            }
            if (!identityDb.TableExist<IdentityUserClaim<string>>())
            {
                identityDb.Database.ExecuteSqlRaw(
                    @"CREATE TABLE [dbo].[UserClaims](
	                          [Id] [int] IDENTITY(1,1) NOT NULL,
	                          [UserId] [nvarchar](max) NULL,
	                          [ClaimType] [nvarchar](max) NULL,
	                          [ClaimValue] [nvarchar](max) NULL,
                              CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
                              (
	                              [Id] ASC
                              )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                              ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]"
                    );
            }

            if (!identityDb.TableExist<IdentityRoleClaim<string>>())
            {
                identityDb.Database.ExecuteSqlRaw(
                    @" CREATE TABLE [dbo].[RoleClaims](
	                           [Id] [int] IDENTITY(1,1) NOT NULL,
	                           [RoleId] [nvarchar](max) NULL,
	                           [ClaimType] [nvarchar](max) NULL,
	                           [ClaimValue] [nvarchar](max) NULL,
                                CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
                               (
	                               [Id] ASC
                               )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                               ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]"
                    );
            }
            if (!identityDb.TableExist<IdentityUserToken<string>>())
            {
                identityDb.Database.ExecuteSqlRaw(
                    @"CREATE TABLE [Identity].[UserTokens](
	                         [UserId] [nvarchar](450) NOT NULL,
	                         [LoginProvider] [nvarchar](450) NOT NULL,
	                         [Name] [nvarchar](max) NOT NULL,
	                         [Value] [nvarchar](max) NULL,
                              CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
                             (
	                             [UserId] ASC,
	                             [LoginProvider] ASC
                             )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                             ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]"
                    );
            }
            identityDb.Database.CommitTransaction();
            identityDb.Database.CloseConnection();
            return app;
        }
    }
}
