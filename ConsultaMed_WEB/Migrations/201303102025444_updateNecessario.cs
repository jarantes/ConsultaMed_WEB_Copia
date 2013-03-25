namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNecessario : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "UserName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "UserName", c => c.String());
        }
    }
}
