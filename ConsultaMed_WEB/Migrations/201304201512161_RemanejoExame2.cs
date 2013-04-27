namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemanejoExame2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exame", "Descricao", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exame", "Descricao", c => c.String(nullable: false));
        }
    }
}
