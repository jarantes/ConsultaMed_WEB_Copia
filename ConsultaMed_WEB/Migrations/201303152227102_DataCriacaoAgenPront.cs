namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataCriacaoAgenPront : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agendamento", "DataAgendamento", c => c.DateTime(nullable: false, defaultValue:DateTime.Now));
            AddColumn("dbo.Agendamento", "DataCriacao", c => c.DateTime(nullable: false, defaultValue: DateTime.Now));
            AddColumn("dbo.Prontuario", "DataCriacao", c => c.DateTime(nullable: false));
            DropColumn("dbo.Agendamento", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agendamento", "Data", c => c.DateTime(nullable: false));
            DropColumn("dbo.Prontuario", "DataCriacao");
            DropColumn("dbo.Agendamento", "DataCriacao");
            DropColumn("dbo.Agendamento", "DataAgendamento");
        }
    }
}
