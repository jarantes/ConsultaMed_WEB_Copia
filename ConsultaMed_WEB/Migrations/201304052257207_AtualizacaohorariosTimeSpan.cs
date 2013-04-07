namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizacaohorariosTimeSpan : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Agendamento", "Horario", c => c.Time(nullable: false));
            AlterColumn("dbo.Horario", "HorarioIni", c => c.Time(nullable: false));
            AlterColumn("dbo.Horario", "HorarioFim", c => c.Time(nullable: false));
            AlterColumn("dbo.HorarioTemp", "HorarioIni", c => c.Time(nullable: false));
            AlterColumn("dbo.HorarioTemp", "HorarioFim", c => c.Time(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HorarioTemp", "HorarioFim", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HorarioTemp", "HorarioIni", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Horario", "HorarioFim", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Horario", "HorarioIni", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Agendamento", "Horario", c => c.DateTime(nullable: false));
        }
    }
}
