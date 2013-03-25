namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class voltarColunaEstado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Endereco", "Estado", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Endereco", "Estado");
        }
    }
}
