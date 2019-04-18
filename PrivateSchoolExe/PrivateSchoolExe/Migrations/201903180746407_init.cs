namespace PrivateSchoolExe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Submission = c.DateTime(nullable: false),
                        OralMark = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalMark = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.TrainerSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        TrainerId = c.Int(nullable: false),
                        AssignmentId = c.Int(nullable: false),
                        IsSubmitted = c.Boolean(nullable: false),
                        OralMark = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalMark = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.UserId)
                .Index(t => t.AssignmentId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Stream = c.String(),
                        Type = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        LevelId = c.Int(nullable: false),
                        Subject = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        TuitionsFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PasswordHash = c.Binary(),
                        PasswordSalt = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Levels", t => t.LevelId, cascadeDelete: true)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Access = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerSchedules", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.TrainerSchedules", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Assignments", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.TrainerSchedules", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.Users", new[] { "LevelId" });
            DropIndex("dbo.TrainerSchedules", new[] { "AssignmentId" });
            DropIndex("dbo.TrainerSchedules", new[] { "UserId" });
            DropIndex("dbo.TrainerSchedules", new[] { "CourseId" });
            DropIndex("dbo.Assignments", new[] { "Course_Id" });
            DropTable("dbo.Levels");
            DropTable("dbo.Users");
            DropTable("dbo.Courses");
            DropTable("dbo.TrainerSchedules");
            DropTable("dbo.Assignments");
        }
    }
}
