using MiniProject.Exceptions;
using MiniProject.Models;

namespace MiniProject
{
    internal class Program
    {
        public static List<Music> AllMusic { get; set; } = new List<Music>();
        static void Main(string[] args)
        {
        Music music1 = new Music(1, "Attention", "Charlie Puth", 180);
            Music music2 = new Music(2, "Enemy", "Imagine Dragons", 150);
            Music music3 = new Music(3, "Starboy", "The Weekend", 35);
            Music music4 = new Music(4, "Bad Guy", "Billie Eilish", 68);
            Music music5 = new Music(5, "Video Games", "Lana Del Rey", 225);
            AllMusic.Add(music1);
            AllMusic.Add(music2);
            AllMusic.Add(music3);
            AllMusic.Add(music4);
            AllMusic.Add(music5);
            UserMenu();
        }
        public static Music MusicAdding()
        {
            Console.WriteLine("Enter an id for your music:");
            int Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the name of your music:");
            string Name = Console.ReadLine();
            Console.WriteLine("Enter the artist name of your music:");
            string ArtistName = Console.ReadLine();
            Console.WriteLine("Enter the length of your music:");
            int Time = int.Parse(Console.ReadLine());
            Music music = new Music(Id, Name, ArtistName, Time);
            return music;
        }
        public static void UserMenu()
        {
            Music last;
            Stack<Music> RecentlyListened = new Stack<Music>();
            Music[] RecentlyListened2 = new Music[0];
            Playlist playlist = new Playlist();
            Music New;
            int option;
            bool isPlaying = false;
            int PlayId;
            int startTime;
            int endTime = 0;
            string add;
            bool empty;
            bool Found;
        Input:
            Console.WriteLine("Menu");
            Console.WriteLine("\n1.Create Music\n2.Look at musics in playlist\n3.Add music to playlist\n4.Play music\n5.Recently listened musics\n6.Quit");
            Console.WriteLine("Choose one option");
            option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    New = MusicAdding();
                    if (!AllMusic.Contains(New))
                    {
                        AllMusic.Add(New);
                        Console.WriteLine($"{AllMusic[AllMusic.Count - 1].Name} added to all musics.");
                    }
                    else
                    {
                        Console.WriteLine("Music already exists in all musics. ");
                    }
                    Console.WriteLine("\nIf you want to add more music to playlist, enter yes,or press anything to go back to menu");
                    add = Console.ReadLine().ToLower();
                    if (add == "yes")
                    {
                        goto case 1;
                    }
                    goto Input;
                case 2:
                    if (playlist.Playlistt.Count != 0)
                    {
                        playlist.ShowList();
                    }
                    else
                    {
                        Console.WriteLine("This playlist is empty");
                        Console.WriteLine("If you want to add music to playlist, enter yes,or press anything to go back to menu");
                        add = Console.ReadLine().ToLower();
                        if (add == "yes")
                        {
                            goto case 3;
                        }
                    }
                    goto Input;
                case 3:
                    Console.WriteLine("List of all musics");
                    for (int i = 0; i < AllMusic.Count; i++)
                    {
                        Console.WriteLine($"Id: {AllMusic[i].Id} Name: {AllMusic[i].ArtistName}- {AllMusic[i].Name} ");
                    }
                    Console.WriteLine("\nEnter the id of music to add to playlist:");
                    int selectedId = int.Parse(Console.ReadLine());
                    for (int i = 0; i < AllMusic.Count; i++)
                    {
                        if (AllMusic[i].Id == selectedId)
                        {
                            playlist.AddMusic(AllMusic[i]);
                            Console.WriteLine("Selected music added to playlist.\n if you want to continue adding, enter yes, or press anything to go back to menu");
                            add = Console.ReadLine().ToLower();
                            if (add == "yes")
                            {
                                goto case 3;
                            }
                            goto Input;
                        }
                    }
                    Console.WriteLine("There is no such music in all musics,going back to menu.\n");
                    goto Input;
                case 4:
                    empty = false;
                    Found = false;
                    if (playlist.Playlistt.Count != 0)
                    {
                        playlist.ShowList();
                        Console.WriteLine("\nSelect an Id to play:");
                    }
                    else
                    {
                        empty = true;
                        goto Empty;
                    }
                    PlayId = int.Parse(Console.ReadLine());
                    if (isPlaying == true && DateTime.Now.Second < endTime)
                    {
                        Console.WriteLine("Currently playing another music, enter H to change it, or Y to cancel it");
                        char change = Convert.ToChar(Console.ReadLine().ToLower());
                        if (change == 'h')
                        {
                            for (int i = 0; i < playlist.Playlistt.Count; i++)
                            {
                                if (playlist.Playlistt[i].Id == PlayId)
                                {
                                    Found = true;
                                    Console.WriteLine("Music changed.");
                                    playlist.Play(playlist.Playlistt[i]);
                                    RecentlyListened.Push(playlist.Playlistt[i]);
                                    startTime = DateTime.Now.Second;
                                    endTime = startTime + playlist.Playlistt[i].Time;
                                    break;
                                }
                            }
                            if (!Found)
                            {
                                Console.WriteLine("Music not found, enter manually.");
                                Music music = MusicAdding();
                                playlist.Play(music);
                                AllMusic.Add(music);
                                Console.WriteLine("Music not changed.");
                            }
                        }
                        else if (change == 'y')
                        {
                            Console.WriteLine("Change cancelled.");
                        }
                        else
                        {
                            WrongCommandException msg = new WrongCommandException("Invalid command, going back to menu as default.");
                            Console.WriteLine(msg.Message);
                            goto Input;
                        }
                        goto Continuation;
                    }
                    for (int i = 0; i < playlist.Playlistt.Count; i++)
                    {
                        if (playlist.Playlistt[i].Id == PlayId)
                        {
                            Found = true;
                            playlist.Play(playlist.Playlistt[i]);
                            RecentlyListened.Push(playlist.Playlistt[i]);
                            startTime = DateTime.Now.Second;
                            endTime = startTime + playlist.Playlistt[i].Time;
                            isPlaying = true;
                            break;
                        }
                    }
                    Empty:
                    if (!Found|| empty )
                    {
                        Console.WriteLine(" Playlist is empty or music wasn't found, enter manually.");
                        Music music = MusicAdding();
                        playlist.Play(music);
                        if (!AllMusic.Contains(music))
                        {
                            AllMusic.Add(music);
                        }
                    }
                    goto Continuation;
                case 5:
                    Array.Resize(ref RecentlyListened2, RecentlyListened.Count);
                    RecentlyListened.CopyTo(RecentlyListened2, 0);
                    Console.WriteLine("List of recently listened musics");
                    if (RecentlyListened.Count == 0)
                    {
                        Console.WriteLine("Haven't listened to music recently.");
                    }
                    while (RecentlyListened.Count > 0)
                    {
                        Console.WriteLine($"Id: {RecentlyListened.Peek().Id} Name: {RecentlyListened.Peek().ArtistName}- {RecentlyListened.Peek().Name}");
                        RecentlyListened.Pop();
                    }
                    for (int i = 0; i < RecentlyListened2.Length; i++)
                    {
                        RecentlyListened.Push(RecentlyListened2[i]);
                    }

                    goto Continuation;
                case 6:
                    Console.WriteLine("Program closed");
                    break;
                default:
                    break;
            }
            Continuation:
            Console.WriteLine();
            Console.WriteLine("If you want to go back to menu, enter menu or enter exit to quit the program");
            string Status = Console.ReadLine().ToLower();
            if (Status == "menu")
           {
                goto Input;
            }
            else if (Status == "exit")
            {
                Console.WriteLine("Exited program.");
            }
            else
            {
                WrongCommandException msg = new WrongCommandException("Invalid command, going back to menu as default.");
                Console.WriteLine(msg.Message);
                goto Input;

            }
        }
    }
}