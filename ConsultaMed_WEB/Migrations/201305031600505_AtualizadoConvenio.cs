namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizadoConvenio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Convenio", "Telefone", c => c.String());
            AddColumn("dbo.Convenio", "Site", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Convenio", "Site");
            DropColumn("dbo.Convenio", "Telefone");
        }
    }
}
