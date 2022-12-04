using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Models
{
    internal class Playlist
    {
        public List<Music> Playlistt;
        public Playlist()
        {
            Playlistt = new List<Music>();
        }
        public void AddMusic(Music music)
        {
            Playlistt.Add(music);
        }
        public void Play(Music music)
        {
            if (Playlistt.Contains(music))
            {
                Console.WriteLine($"Listening to {music.ArtistName}- {music.Name}");
            }
            else
            {
                Console.WriteLine("There is no such music in playlist, added now.");
                AddMusic(music);
            }
        }
        public void ShowList()
        {
            Console.WriteLine("List of musics in playlist:\n");
            for (int i = 0; i < Playlistt.Count; i++)
            {
                Console.WriteLine($"Id: {Playlistt[i].Id} Name: {Playlistt[i].ArtistName}- {Playlistt[i].Name} ");
            }
        }
    }
}
