namespace EleicaoBrasil.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        state = c.String(),
                        idTitleJob = c.String(),
                        total = c.Int(nullable: false),
                        qtd = c.Int(nullable: false),
                        average = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        idUser = c.Int(nullable: false),
                        idCandidate = c.String(nullable: false, maxLength: 128),
                        rating = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        comment = c.String(),
                    })
                .PrimaryKey(t => new { t.idUser, t.idCandidate })
                .ForeignKey("dbo.Candidates", t => t.idCandidate, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.idUser, cascadeDelete: true)
                .Index(t => t.idUser)
                .Index(t => t.idCandidate);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        photo = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "idUser", "dbo.Users");
            DropForeignKey("dbo.Comments", "idCandidate", "dbo.Candidates");
            DropIndex("dbo.Comments", new[] { "idCandidate" });
            DropIndex("dbo.Comments", new[] { "idUser" });
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Candidates");
        }
    }
}
