namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudancaUserId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "UserId", c => c.Int(nullable: false, identity: true));
        }
    }
}
