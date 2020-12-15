namespace WPFMetronome.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up() //Initial table creation: no data
        {
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        songId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Artist = c.String(),
                        bpm = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.songId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Songs");
        }
    }
}
