using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayer.Model
{
    public class Playlist
    {
        #region PlaylistProperties
        public String Name { get; set; }

        /* FOR PLAYLIST MODIFICATION PURPOSE */
        public ObservableCollection<Model.Media> MediasList { get; set; }
        public int Index { get; set; }
        #endregion

        public Playlist()
        {
            Name = "";
            MediasList = new ObservableCollection<Media>();
            Index = 0;
        }
    }
}
