namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizadaDataConsulta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agendamento", "DataConsulta", c => c.DateTime(nullable: false));
            DropColumn("dbo.Agendamento", "DataAgendamento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agendamento", "DataAgendamento", c => c.DateTime(nullable: false));
            DropColumn("dbo.Agendamento", "DataConsulta");
        }
    }
}
