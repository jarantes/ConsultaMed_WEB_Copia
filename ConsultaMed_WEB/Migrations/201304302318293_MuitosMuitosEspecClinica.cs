namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MuitosMuitosEspecClinica : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClinicaEspecialidade",
                c => new
                    {
                        Clinica_ClinicaId = c.Int(nullable: false),
                        Especialidade_EspecialidadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clinica_ClinicaId, t.Especialidade_EspecialidadeId })
                .ForeignKey("dbo.Clinica", t => t.Clinica_ClinicaId)
                .ForeignKey("dbo.Especialidade", t => t.Especialidade_EspecialidadeId)
                .Index(t => t.Clinica_ClinicaId)
                .Index(t => t.Especialidade_EspecialidadeId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ClinicaEspecialidade", new[] { "Especialidade_EspecialidadeId" });
            DropIndex("dbo.ClinicaEspecialidade", new[] { "Clinica_ClinicaId" });
            DropForeignKey("dbo.ClinicaEspecialidade", "Especialidade_EspecialidadeId", "dbo.Especialidade");
            DropForeignKey("dbo.ClinicaEspecialidade", "Clinica_ClinicaId", "dbo.Clinica");
            DropTable("dbo.ClinicaEspecialidade");
        }
    }
}
