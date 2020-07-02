namespace Brainstorm.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class S7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "Code");
        }
    }
}
