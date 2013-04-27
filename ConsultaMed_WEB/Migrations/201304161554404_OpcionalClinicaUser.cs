namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpcionalClinicaUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuario", "ClinicaId", "dbo.Clinica");
            DropIndex("dbo.Usuario", new[] { "ClinicaId" });
            AlterColumn("dbo.Usuario", "ClinicaId", c => c.Int());
            AddForeignKey("dbo.Usuario", "ClinicaId", "dbo.Clinica", "ClinicaId");
            CreateIndex("dbo.Usuario", "ClinicaId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Usuario", new[] { "ClinicaId" });
            DropForeignKey("dbo.Usuario", "ClinicaId", "dbo.Clinica");
            AlterColumn("dbo.Usuario", "ClinicaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Usuario", "ClinicaId");
            AddForeignKey("dbo.Usuario", "ClinicaId", "dbo.Clinica", "ClinicaId", cascadeDelete: true);
        }
    }
}
