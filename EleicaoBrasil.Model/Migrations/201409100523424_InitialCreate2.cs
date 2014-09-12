namespace EleicaoBrasil.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Comments", "longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "longitude");
            DropColumn("dbo.Comments", "latitude");
        }
    }
}
