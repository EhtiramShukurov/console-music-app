using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject.Models
{
    internal class Music
    {
        private int _id;
        private string _name;
        private int _time;
        private string _artistName;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _name; }
            set {
                _name = value; }
        }
        public string ArtistName
        {
            get { return _artistName; }
            set {
                _artistName = value; }
        }
        public int Time
        {
            get { return _time; }
            set {
                if (value> 0)
                {
                    _time = value;
                }
            }
        }
        public Music(int id,string name,string artistName, int time)
        {
            Id = id;
            Name = name.Capitalize();
            ArtistName = artistName.Capitalize();
            Time = time;
        }

    }
}
