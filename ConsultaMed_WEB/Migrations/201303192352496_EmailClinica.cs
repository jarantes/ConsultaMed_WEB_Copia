namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailClinica : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clinica", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clinica", "Email");
        }
    }
}