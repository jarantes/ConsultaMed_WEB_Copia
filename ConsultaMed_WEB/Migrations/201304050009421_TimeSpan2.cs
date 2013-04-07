namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeSpan2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HorarioTemp", "TempoConsulta", c => c.Time(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HorarioTemp", "TempoConsulta", c => c.DateTime(nullable: false));
        }
    }
}
