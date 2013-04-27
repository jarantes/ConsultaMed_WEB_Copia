namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TratamentoExameProntuario2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prontuario", "Exame", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prontuario", "Exame");
        }
    }
}
