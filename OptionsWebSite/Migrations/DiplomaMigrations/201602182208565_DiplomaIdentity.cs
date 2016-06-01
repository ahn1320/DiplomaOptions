namespace OptionsWebSite.Migrations.DiplomaMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiplomaIdentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        ChoiceId = c.Int(nullable: false, identity: true),
                        YearTermId = c.Int(nullable: false),
                        StudentId = c.String(maxLength: 9),
                        StudentFirstName = c.String(nullable: false, maxLength: 40),
                        StudentLastName = c.String(nullable: false, maxLength: 40),
                        FirstChoiceOptionId = c.Int(nullable: false),
                        SecondChoiceOptionId = c.Int(nullable: false),
                        ThirdChoiceOptionId = c.Int(nullable: false),
                        FourthChoiceOptionId = c.Int(nullable: false),
                        SelectionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ChoiceId)
                .ForeignKey("dbo.Options", t => t.FirstChoiceOptionId, cascadeDelete: false)
                .ForeignKey("dbo.Options", t => t.FourthChoiceOptionId, cascadeDelete: false)
                .ForeignKey("dbo.Options", t => t.SecondChoiceOptionId, cascadeDelete: false)
                .ForeignKey("dbo.Options", t => t.ThirdChoiceOptionId, cascadeDelete: false)
                .ForeignKey("dbo.YearTerms", t => t.YearTermId, cascadeDelete: false)
                .Index(t => t.YearTermId)
                .Index(t => t.FirstChoiceOptionId)
                .Index(t => t.SecondChoiceOptionId)
                .Index(t => t.ThirdChoiceOptionId)
                .Index(t => t.FourthChoiceOptionId);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        OptionId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OptionId);
            
            CreateTable(
                "dbo.YearTerms",
                c => new
                    {
                        YearTermId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Term = c.String(nullable: false, maxLength: 50),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.YearTermId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Choices", "YearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.Choices", "ThirdChoiceOptionId", "dbo.Options");
            DropForeignKey("dbo.Choices", "SecondChoiceOptionId", "dbo.Options");
            DropForeignKey("dbo.Choices", "FourthChoiceOptionId", "dbo.Options");
            DropForeignKey("dbo.Choices", "FirstChoiceOptionId", "dbo.Options");
            DropIndex("dbo.Choices", new[] { "FourthChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "ThirdChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "SecondChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "FirstChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "YearTermId" });
            DropTable("dbo.YearTerms");
            DropTable("dbo.Options");
            DropTable("dbo.Choices");
        }
    }
}
