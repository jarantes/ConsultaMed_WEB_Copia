namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TratamentoExameProntuario : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Prontuario", "Exame");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prontuario", "Exame", c => c.String());
        }
    }
}
