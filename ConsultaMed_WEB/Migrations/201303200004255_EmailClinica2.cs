namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailClinica2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clinica", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clinica", "Email", c => c.String());
        }
    }
}
