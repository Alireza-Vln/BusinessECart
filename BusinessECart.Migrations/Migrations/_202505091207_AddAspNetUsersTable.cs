using FluentMigrator;

[Migration(202505091207)]
public class _202505091207_AddAspNetUsersTable : Migration
{
public override void Up()
{
    Create.Table("AspNetUsers")
        .WithColumn("Id").AsString(450).PrimaryKey()
        .WithColumn("UserName").AsString(256).Nullable()
        .WithColumn("NormalizedUserName").AsString(256).Nullable()
        .WithColumn("Email").AsString(256).Nullable()
        .WithColumn("NormalizedEmail").AsString(256).Nullable()
        .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
        .WithColumn("PasswordHash").AsString(int.MaxValue).Nullable()
        .WithColumn("SecurityStamp").AsString(int.MaxValue).Nullable()
        .WithColumn("ConcurrencyStamp").AsString(int.MaxValue).Nullable()
        .WithColumn("PhoneNumber").AsString(int.MaxValue).Nullable()
        .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
        .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
        .WithColumn("LockoutEnd").AsDateTimeOffset().Nullable()
        .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
        .WithColumn("AccessFailedCount").AsInt32().NotNullable();

    Create.Index("IX_AspNetUsers_NormalizedUserName")
        .OnTable("AspNetUsers").OnColumn("NormalizedUserName").Ascending()
        .WithOptions().Unique().WithOptions().NonClustered();

    Create.Index("IX_AspNetUsers_NormalizedEmail")
        .OnTable("AspNetUsers").OnColumn("NormalizedEmail").Ascending();



        Create.Table("AspNetRoles")
            .WithColumn("Id").AsString(450).PrimaryKey()
            .WithColumn("Name").AsString(256).Nullable()
            .WithColumn("NormalizedName").AsString(256).Nullable()
            .WithColumn("ConcurrencyStamp").AsString(int.MaxValue).Nullable();

        Create.Index("IX_AspNetRoles_NormalizedName")
            .OnTable("AspNetRoles").OnColumn("NormalizedName").Ascending()
            .WithOptions().Unique().WithOptions().NonClustered();

        Create.Table("AspNetUserRoles")
            .WithColumn("UserId").AsString(450).NotNullable()
            .WithColumn("RoleId").AsString(450).NotNullable();

        Create.PrimaryKey("PK_AspNetUserRoles")
            .OnTable("AspNetUserRoles")
            .Columns("UserId", "RoleId");

        Create.ForeignKey("FK_AspNetUserRoles_Users")
            .FromTable("AspNetUserRoles").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id");

        Create.ForeignKey("FK_AspNetUserRoles_Roles")
            .FromTable("AspNetUserRoles").ForeignColumn("RoleId")
            .ToTable("AspNetRoles").PrimaryColumn("Id");

        Create.Table("AspNetUserClaims")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsString(450).NotNullable()
            .WithColumn("ClaimType").AsString(int.MaxValue).Nullable()
            .WithColumn("ClaimValue").AsString(int.MaxValue).Nullable();

        Create.ForeignKey("FK_AspNetUserClaims_Users")
            .FromTable("AspNetUserClaims").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id");

        Create.Table("AspNetUserLogins")
            .WithColumn("LoginProvider").AsString(128).NotNullable()
            .WithColumn("ProviderKey").AsString(128).NotNullable()
            .WithColumn("ProviderDisplayName").AsString(int.MaxValue).Nullable()
            .WithColumn("UserId").AsString(450).NotNullable();

        Create.PrimaryKey("PK_AspNetUserLogins")
            .OnTable("AspNetUserLogins")
            .Columns("LoginProvider", "ProviderKey");

        Create.ForeignKey("FK_AspNetUserLogins_Users")
            .FromTable("AspNetUserLogins").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id");

        Create.Table("AspNetUserTokens")
            .WithColumn("UserId").AsString(450).NotNullable()
            .WithColumn("LoginProvider").AsString(128).NotNullable()
            .WithColumn("Name").AsString(128).NotNullable()
            .WithColumn("Value").AsString(int.MaxValue).Nullable();

        Create.PrimaryKey("PK_AspNetUserTokens")
            .OnTable("AspNetUserTokens")
            .Columns("UserId", "LoginProvider", "Name");

        Create.ForeignKey("FK_AspNetUserTokens_Users")
            .FromTable("AspNetUserTokens").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id");

        Create.Table("AspNetRoleClaims")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("RoleId").AsString(450).NotNullable()
            .WithColumn("ClaimType").AsString(int.MaxValue).Nullable()
            .WithColumn("ClaimValue").AsString(int.MaxValue).Nullable();

        Create.ForeignKey("FK_AspNetRoleClaims_Roles")
            .FromTable("AspNetRoleClaims").ForeignColumn("RoleId")
            .ToTable("AspNetRoles").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("AspNetRoleClaims");
        Delete.Table("AspNetUserTokens");
        Delete.Table("AspNetUserLogins");
        Delete.Table("AspNetUserClaims");
        Delete.Table("AspNetUserRoles");
        Delete.Table("AspNetRoles");
        Delete.Table("AspNetUsers");
    }
    
}