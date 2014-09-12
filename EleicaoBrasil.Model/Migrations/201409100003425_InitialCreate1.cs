namespace EleicaoBrasil.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "idSocial", c => c.String());
            AddColumn("dbo.Users", "lastName", c => c.String());
            AddColumn("dbo.Users", "firstName", c => c.String());
            AddColumn("dbo.Users", "gender", c => c.String());
            AddColumn("dbo.Users", "birthday", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "birthday");
            DropColumn("dbo.Users", "gender");
            DropColumn("dbo.Users", "firstName");
            DropColumn("dbo.Users", "lastName");
            DropColumn("dbo.Users", "idSocial");
        }
    }
}
