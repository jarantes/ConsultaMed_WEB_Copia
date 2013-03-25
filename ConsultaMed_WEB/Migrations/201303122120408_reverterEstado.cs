namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reverterEstado : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Endereco", "EstadoId", "dbo.Estado");
            DropIndex("dbo.Endereco", new[] { "EstadoId" });
            DropColumn("dbo.Endereco", "EstadoId");
            DropTable("dbo.Estado");
        }
        
        public override void Down()
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
            CreateIndex("dbo.Endereco", "EstadoId");
            AddForeignKey("dbo.Endereco", "EstadoId", "dbo.Estado", "EstadoId", cascadeDelete: true);
        }
    }
}
