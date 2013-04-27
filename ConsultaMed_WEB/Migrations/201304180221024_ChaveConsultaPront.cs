namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChaveConsultaPront : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prontuario", "AgendamentoId", c => c.Int());
            AddForeignKey("dbo.Prontuario", "AgendamentoId", "dbo.Agendamento", "AgendamentoId");
            CreateIndex("dbo.Prontuario", "AgendamentoId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Prontuario", new[] { "AgendamentoId" });
            DropForeignKey("dbo.Prontuario", "AgendamentoId", "dbo.Agendamento");
            DropColumn("dbo.Prontuario", "AgendamentoId");
        }
    }
}
