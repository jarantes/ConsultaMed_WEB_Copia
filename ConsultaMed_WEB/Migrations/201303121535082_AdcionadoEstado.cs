namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdcionadoEstado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoId = c.Int(nullable: false, identity: true),
                        Sigla = c.String(nullable: false),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.EstadoId);
            
            AddColumn("dbo.Endereco", "EstadoId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Endereco", "EstadoId", "dbo.Estado", "EstadoId", cascadeDelete: true);
            CreateIndex("dbo.Endereco", "EstadoId");
            DropColumn("dbo.Endereco", "Estado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Endereco", "Estado", c => c.String(nullable: false));
            DropIndex("dbo.Endereco", new[] { "EstadoId" });
            DropForeignKey("dbo.Endereco", "EstadoId", "dbo.Estado");
            DropColumn("dbo.Endereco", "EstadoId");
            DropTable("dbo.Estado");
        }
    }
}
