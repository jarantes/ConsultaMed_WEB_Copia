namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletoMesmo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExameEspecialidade", "Exame_ExameId", "dbo.Exame");
            DropForeignKey("dbo.ExameEspecialidade", "Especialidade_EspecialidadeId", "dbo.Especialidade");
            DropIndex("dbo.ExameEspecialidade", new[] { "Exame_ExameId" });
            DropIndex("dbo.ExameEspecialidade", new[] { "Especialidade_EspecialidadeId" });
            DropTable("dbo.Exame");
            DropTable("dbo.ExameEspecialidade");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExameEspecialidade",
                c => new
                    {
                        Exame_ExameId = c.Int(nullable: false),
                        Especialidade_EspecialidadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Exame_ExameId, t.Especialidade_EspecialidadeId });
            
            CreateTable(
                "dbo.Exame",
                c => new
                    {
                        ExameId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.ExameId);
            
            CreateIndex("dbo.ExameEspecialidade", "Especialidade_EspecialidadeId");
            CreateIndex("dbo.ExameEspecialidade", "Exame_ExameId");
            AddForeignKey("dbo.ExameEspecialidade", "Especialidade_EspecialidadeId", "dbo.Especialidade", "EspecialidadeId");
            AddForeignKey("dbo.ExameEspecialidade", "Exame_ExameId", "dbo.Exame", "ExameId");
        }
    }
}
