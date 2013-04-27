namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacaoEspecExame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExameEspecialidade",
                c => new
                    {
                        Exame_ExameId = c.Int(nullable: false),
                        Especialidade_EspecialidadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Exame_ExameId, t.Especialidade_EspecialidadeId })
                .ForeignKey("dbo.Exame", t => t.Exame_ExameId)
                .ForeignKey("dbo.Especialidade", t => t.Especialidade_EspecialidadeId)
                .Index(t => t.Exame_ExameId)
                .Index(t => t.Especialidade_EspecialidadeId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ExameEspecialidade", new[] { "Especialidade_EspecialidadeId" });
            DropIndex("dbo.ExameEspecialidade", new[] { "Exame_ExameId" });
            DropForeignKey("dbo.ExameEspecialidade", "Especialidade_EspecialidadeId", "dbo.Especialidade");
            DropForeignKey("dbo.ExameEspecialidade", "Exame_ExameId", "dbo.Exame");
            DropTable("dbo.ExameEspecialidade");
        }
    }
}
