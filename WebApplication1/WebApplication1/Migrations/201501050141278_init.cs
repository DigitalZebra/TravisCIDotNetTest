namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "roles",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "userRoles",
                c => new
                    {
                        userId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        roleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.userId, t.roleId })
                .ForeignKey("roles", t => t.roleId, cascadeDelete: true)
                .ForeignKey("users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId, name: "IX_UserId")
                .Index(t => t.roleId, name: "IX_RoleId");
            
            CreateTable(
                "users",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        email = c.String(maxLength: 256, storeType: "nvarchar"),
                        emailConfirmed = c.Boolean(nullable: false),
                        passwordHash = c.String(unicode: false),
                        securityStamp = c.String(unicode: false),
                        phoneNumber = c.String(unicode: false),
                        phoneNumberConfirmed = c.Boolean(nullable: false),
                        twoFactorEnabled = c.Boolean(nullable: false),
                        lockoutEndDateUtc = c.DateTime(precision: 0),
                        lockoutEnabled = c.Boolean(nullable: false),
                        accessFailedCount = c.Int(nullable: false),
                        userName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.userName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "userClaims",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        claimType = c.String(unicode: false),
                        claimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId, name: "IX_UserId");
            
            CreateTable(
                "userLogins",
                c => new
                    {
                        loginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        providerKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        userId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.loginProvider, t.providerKey, t.userId })
                .ForeignKey("users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId, name: "IX_UserId");
            
        }
        
        public override void Down()
        {
            DropForeignKey("userRoles", "userId", "users");
            DropForeignKey("userLogins", "userId", "users");
            DropForeignKey("userClaims", "userId", "users");
            DropForeignKey("userRoles", "roleId", "roles");
            DropIndex("userLogins", "IX_UserId");
            DropIndex("userClaims", "IX_UserId");
            DropIndex("users", "UserNameIndex");
            DropIndex("userRoles", "IX_RoleId");
            DropIndex("userRoles", "IX_UserId");
            DropIndex("roles", "RoleNameIndex");
            DropTable("userLogins");
            DropTable("userClaims");
            DropTable("users");
            DropTable("userRoles");
            DropTable("roles");
        }
    }
}
