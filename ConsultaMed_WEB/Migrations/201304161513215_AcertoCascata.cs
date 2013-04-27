namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcertoCascata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClinicaConvenio", "Clinica_ClinicaId", "dbo.Clinica");
            DropForeignKey("dbo.ClinicaConvenio", "Convenio_ConvenioId", "dbo.Convenio");
            DropIndex("dbo.ClinicaConvenio", new[] { "Clinica_ClinicaId" });
            DropIndex("dbo.ClinicaConvenio", new[] { "Convenio_ConvenioId" });
            CreateTable(
                "dbo.ConvenioClinica",
                c => new
                    {
                        Convenio_ConvenioId = c.Int(nullable: false),
                        Clinica_ClinicaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Convenio_ConvenioId, t.Clinica_ClinicaId })
                .ForeignKey("dbo.Convenio", t => t.Convenio_ConvenioId)
                .ForeignKey("dbo.Clinica", t => t.Clinica_ClinicaId)
                .Index(t => t.Convenio_ConvenioId)
                .Index(t => t.Clinica_ClinicaId);
            
            AddForeignKey("dbo.Agendamento", "MedicoUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Agendamento", "PacienteUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.Horario", "MedicoUserId", "dbo.Usuario", "UserId");
            AddForeignKey("dbo.HorarioTemp", "MedicoUserId", "dbo.Usuario", "UserId");
            CreateIndex("dbo.Agendamento", "MedicoUserId");
            CreateIndex("dbo.Agendamento", "PacienteUserId");
            CreateIndex("dbo.Horario", "MedicoUserId");
            CreateIndex("dbo.HorarioTemp", "MedicoUserId");
            DropTable("dbo.ClinicaConvenio");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClinicaConvenio",
                c => new
                    {
                        Clinica_ClinicaId = c.Int(nullable: false),
                        Convenio_ConvenioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clinica_ClinicaId, t.Convenio_ConvenioId });
            
            DropIndex("dbo.ConvenioClinica", new[] { "Clinica_ClinicaId" });
            DropIndex("dbo.ConvenioClinica", new[] { "Convenio_ConvenioId" });
            DropIndex("dbo.HorarioTemp", new[] { "MedicoUserId" });
            DropIndex("dbo.Horario", new[] { "MedicoUserId" });
            DropIndex("dbo.Agendamento", new[] { "PacienteUserId" });
            DropIndex("dbo.Agendamento", new[] { "MedicoUserId" });
            DropForeignKey("dbo.ConvenioClinica", "Clinica_ClinicaId", "dbo.Clinica");
            DropForeignKey("dbo.ConvenioClinica", "Convenio_ConvenioId", "dbo.Convenio");
            DropForeignKey("dbo.HorarioTemp", "MedicoUserId", "dbo.Usuario");
            DropForeignKey("dbo.Horario", "MedicoUserId", "dbo.Usuario");
            DropForeignKey("dbo.Agendamento", "PacienteUserId", "dbo.Usuario");
            DropForeignKey("dbo.Agendamento", "MedicoUserId", "dbo.Usuario");
            DropTable("dbo.ConvenioClinica");
            CreateIndex("dbo.ClinicaConvenio", "Convenio_ConvenioId");
            CreateIndex("dbo.ClinicaConvenio", "Clinica_ClinicaId");
            AddForeignKey("dbo.ClinicaConvenio", "Convenio_ConvenioId", "dbo.Convenio", "ConvenioId", cascadeDelete: true);
            AddForeignKey("dbo.ClinicaConvenio", "Clinica_ClinicaId", "dbo.Clinica", "ClinicaId", cascadeDelete: true);
        }
    }
}
