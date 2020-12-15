namespace WPFMetronome.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            Sql("insert into dbo.Songs(Name, Artist, bpm) Values('Master of Puppets', 'Metallica', '212')"); //some test data
            Sql("insert into dbo.Songs(Name, Artist, bpm) Values('2112 Overture', 'Rush', '134')"); //Test that regex thing in the combobox changed 
            Sql("insert into dbo.Songs(Name, Artist, bpm) Values('Love', 'Gojira', '220')"); //what I'm learning now - to test timer accuracy
        }
        
        public override void Down()
        {
            Sql("delete from dbo.Songs"); //revert fully to empty table
        }
    }
}
