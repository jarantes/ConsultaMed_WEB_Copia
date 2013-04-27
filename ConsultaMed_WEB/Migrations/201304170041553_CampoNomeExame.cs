namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoNomeExame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exame", "Nome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exame", "Nome");
        }
    }
}
