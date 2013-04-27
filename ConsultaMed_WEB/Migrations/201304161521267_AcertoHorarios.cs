namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcertoHorarios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agendamento", "HorarioId", c => c.Int());
            AddColumn("dbo.Agendamento", "HorarioTempId", c => c.Int());
            AddColumn("dbo.Horario", "Horario_HorarioId", c => c.Int());
            AddColumn("dbo.HorarioTemp", "Horario_HorarioId", c => c.Int());
            AddForeignKey("dbo.Agendamento", "HorarioId", "dbo.Horario", "HorarioId");
            AddForeignKey("dbo.Agendamento", "HorarioTempId", "dbo.HorarioTemp", "HorarioTempId");
            AddForeignKey("dbo.Horario", "Horario_HorarioId", "dbo.Horario", "HorarioId");
            AddForeignKey("dbo.HorarioTemp", "Horario_HorarioId", "dbo.Horario", "HorarioId");
            CreateIndex("dbo.Agendamento", "HorarioId");
            CreateIndex("dbo.Agendamento", "HorarioTempId");
            CreateIndex("dbo.Horario", "Horario_HorarioId");
            CreateIndex("dbo.HorarioTemp", "Horario_HorarioId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.HorarioTemp", new[] { "Horario_HorarioId" });
            DropIndex("dbo.Horario", new[] { "Horario_HorarioId" });
            DropIndex("dbo.Agendamento", new[] { "HorarioTempId" });
            DropIndex("dbo.Agendamento", new[] { "HorarioId" });
            DropForeignKey("dbo.HorarioTemp", "Horario_HorarioId", "dbo.Horario");
            DropForeignKey("dbo.Horario", "Horario_HorarioId", "dbo.Horario");
            DropForeignKey("dbo.Agendamento", "HorarioTempId", "dbo.HorarioTemp");
            DropForeignKey("dbo.Agendamento", "HorarioId", "dbo.Horario");
            DropColumn("dbo.HorarioTemp", "Horario_HorarioId");
            DropColumn("dbo.Horario", "Horario_HorarioId");
            DropColumn("dbo.Agendamento", "HorarioTempId");
            DropColumn("dbo.Agendamento", "HorarioId");
        }
    }
}
