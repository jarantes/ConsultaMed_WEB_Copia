namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteRelacaoConvenio : DbMigration
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
                .ForeignKey("dbo.Convenio", t => t.Convenio_ConvenioId, cascadeDelete: true)
                .ForeignKey("dbo.Clinica", t => t.Clinica_ClinicaId, cascadeDelete: true)
                .Index(t => t.Convenio_ConvenioId)
                .Index(t => t.Clinica_ClinicaId);
            
            AlterColumn("dbo.Usuario", "ConvenioId", c => c.Int());
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
            DropForeignKey("dbo.ConvenioClinica", "Clinica_ClinicaId", "dbo.Clinica");
            DropForeignKey("dbo.ConvenioClinica", "Convenio_ConvenioId", "dbo.Convenio");
            AlterColumn("dbo.Usuario", "ConvenioId", c => c.Int(nullable: false));
            DropTable("dbo.ConvenioClinica");
            CreateIndex("dbo.ClinicaConvenio", "Convenio_ConvenioId");
            CreateIndex("dbo.ClinicaConvenio", "Clinica_ClinicaId");
            AddForeignKey("dbo.ClinicaConvenio", "Convenio_ConvenioId", "dbo.Convenio", "ConvenioId", cascadeDelete: true);
            AddForeignKey("dbo.ClinicaConvenio", "Clinica_ClinicaId", "dbo.Clinica", "ClinicaId", cascadeDelete: true);
        }
    }
}
