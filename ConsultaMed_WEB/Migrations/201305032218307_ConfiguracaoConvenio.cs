namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfiguracaoConvenio : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Convenio", "Telefone", c => c.String(nullable: false));
            AlterColumn("dbo.Convenio", "Site", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Convenio", "Site", c => c.String());
            AlterColumn("dbo.Convenio", "Telefone", c => c.String());
        }
    }
}
