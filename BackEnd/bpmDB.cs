using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace WPFMetronome.BackEnd
{
    public class bpmDB //not super elaborate but will work
    {
        public void addSong(string name, string artist, int BPM)
        {
            using (var db = new SongContext())
            {
                var song = new Song
                { 
                    Name = name,
                    Artist = artist,
                    bpm = BPM
                };
                db.Songs.Add(song);
                db.SaveChanges();
            }
        }
        public List<string[]> selectSong()
        {
            List<string[]> songs = new List<string[]>();
            string[] parts;
            using (var db = new SongContext())
            {
                var query = from s in db.Songs orderby s.songId select s;
                foreach (var item in query)
                {
                    parts = new string[] { $"{item.Name}", $"{item.Artist}", $"{item.bpm}" };
                    songs.Add(parts);
                }
                return songs;
            }
        }
        public class Song
        {
            public int songId { get; set; }
            public string Name { get; set; }
            public string Artist { get; set; }
            public int bpm { get; set; }
        }
        public class SongContext : DbContext
        {
            public SongContext() { }
            public SongContext(string conn)
            {
                Database.Connection.ConnectionString = conn;
            }
           
            public DbSet<Song> Songs { get; set; }
        }
        
    }
}
