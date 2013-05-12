namespace ConsultaMed_WEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdcionadoLogEmail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CmLog",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        DescricaoAcao = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.Usuario", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EmailConfiguration",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Assunto = c.String(nullable: false),
                        Corpo = c.String(nullable: false),
                        Descriao = c.String(),
                    })
                .PrimaryKey(t => t.TypeId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CmLog", new[] { "UserId" });
            DropForeignKey("dbo.CmLog", "UserId", "dbo.Usuario");
            DropTable("dbo.EmailConfiguration");
            DropTable("dbo.CmLog");
        }
    }
}
