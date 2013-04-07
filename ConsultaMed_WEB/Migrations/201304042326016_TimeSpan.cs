namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeSpan : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Horario", "TempoConsulta", c => c.Time(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Horario", "TempoConsulta", c => c.DateTime(nullable: false));
        }
    }
}
