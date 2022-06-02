namespace FriendOrganizer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgrammingLanguages : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Friends", newName: "Friend");
            CreateTable(
                "dbo.FriendPhoneNumber",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false),
                        FriendId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friend", t => t.FriendId, cascadeDelete: true)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.ProgrammingLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meeting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MeetingFriend",
                c => new
                    {
                        Meeting_Id = c.Int(nullable: false),
                        Friend_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting_Id, t.Friend_Id })
                .ForeignKey("dbo.Meeting", t => t.Meeting_Id, cascadeDelete: true)
                .ForeignKey("dbo.Friend", t => t.Friend_Id, cascadeDelete: true)
                .Index(t => t.Meeting_Id)
                .Index(t => t.Friend_Id);
            
            AddColumn("dbo.Friend", "FavoriteLanguageId", c => c.Int());
            CreateIndex("dbo.Friend", "FavoriteLanguageId");
            AddForeignKey("dbo.Friend", "FavoriteLanguageId", "dbo.ProgrammingLanguage", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FriendPhoneNumber", "FriendId", "dbo.Friend");
            DropForeignKey("dbo.MeetingFriend", "Friend_Id", "dbo.Friend");
            DropForeignKey("dbo.MeetingFriend", "Meeting_Id", "dbo.Meeting");
            DropForeignKey("dbo.Friend", "FavoriteLanguageId", "dbo.ProgrammingLanguage");
            DropIndex("dbo.MeetingFriend", new[] { "Friend_Id" });
            DropIndex("dbo.MeetingFriend", new[] { "Meeting_Id" });
            DropIndex("dbo.Friend", new[] { "FavoriteLanguageId" });
            DropIndex("dbo.FriendPhoneNumber", new[] { "FriendId" });
            DropColumn("dbo.Friend", "FavoriteLanguageId");
            DropTable("dbo.MeetingFriend");
            DropTable("dbo.Meeting");
            DropTable("dbo.ProgrammingLanguage");
            DropTable("dbo.FriendPhoneNumber");
            RenameTable(name: "dbo.Friend", newName: "Friends");
        }
    }
}
