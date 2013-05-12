namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizacaoEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailConfiguration", "TipoEmail", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailConfiguration", "TipoEmail");
        }
    }
}
