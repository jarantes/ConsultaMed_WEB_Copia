namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClinicaAlterada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clinica", "HorarioInicial", c => c.String(nullable: false));
            AddColumn("dbo.Clinica", "HorarioFinal", c => c.String(nullable: false));
            DropColumn("dbo.Clinica", "HorarioAtendimento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clinica", "HorarioAtendimento", c => c.String(nullable: false));
            DropColumn("dbo.Clinica", "HorarioFinal");
            DropColumn("dbo.Clinica", "HorarioInicial");
        }
    }
}
