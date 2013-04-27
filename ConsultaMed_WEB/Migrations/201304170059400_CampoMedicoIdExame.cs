namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoMedicoIdExame : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConvenioClinica", "Convenio_ConvenioId", "dbo.Convenio");
            DropForeignKey("dbo.ConvenioClinica", "Clinica_ClinicaId", "dbo.Clinica");
            DropIndex("dbo.ConvenioClinica", new[] { "Convenio_ConvenioId" });
            DropIndex("dbo.ConvenioClinica", new[] { "Clinica_ClinicaId" });
            CreateTable(
                "dbo.ClinicaConvenio",
                c => new
                    {
                        Clinica_ClinicaId = c.Int(nullable: false),
                        Convenio_ConvenioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clinica_ClinicaId, t.Convenio_ConvenioId })
                .ForeignKey("dbo.Clinica", t => t.Clinica_ClinicaId)
                .ForeignKey("dbo.Convenio", t => t.Convenio_ConvenioId)
                .Index(t => t.Clinica_ClinicaId)
                .Index(t => t.Convenio_ConvenioId);
            
            AddColumn("dbo.Exame", "MedicoUserId", c => c.Int());
            AddForeignKey("dbo.Exame", "MedicoUserId", "dbo.Usuario", "UserId");
            CreateIndex("dbo.Exame", "MedicoUserId");
            DropTable("dbo.ConvenioClinica");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ConvenioClinica",
                c => new
                    {
                        Convenio_ConvenioId = c.Int(nullable: false),
                        Clinica_ClinicaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Convenio_ConvenioId, t.Clinica_ClinicaId });
            
            DropIndex("dbo.ClinicaConvenio", new[] { "Convenio_ConvenioId" });
            DropIndex("dbo.ClinicaConvenio", new[] { "Clinica_ClinicaId" });
            DropIndex("dbo.Exame", new[] { "MedicoUserId" });
            DropForeignKey("dbo.ClinicaConvenio", "Convenio_ConvenioId", "dbo.Convenio");
            DropForeignKey("dbo.ClinicaConvenio", "Clinica_ClinicaId", "dbo.Clinica");
            DropForeignKey("dbo.Exame", "MedicoUserId", "dbo.Usuario");
            DropColumn("dbo.Exame", "MedicoUserId");
            DropTable("dbo.ClinicaConvenio");
            CreateIndex("dbo.ConvenioClinica", "Clinica_ClinicaId");
            CreateIndex("dbo.ConvenioClinica", "Convenio_ConvenioId");
            AddForeignKey("dbo.ConvenioClinica", "Clinica_ClinicaId", "dbo.Clinica", "ClinicaId");
            AddForeignKey("dbo.ConvenioClinica", "Convenio_ConvenioId", "dbo.Convenio", "ConvenioId");
        }
    }
}
