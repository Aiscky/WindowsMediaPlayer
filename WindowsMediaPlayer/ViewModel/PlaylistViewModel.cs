using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayer.ViewModel
{
    public class PlaylistViewModel : ViewModelBase
    {
        #region PlaylistViewModelProperties

        private static PlaylistViewModel instance = null;
        public ObservableCollection<Model.Playlist> PlaylistsList;
        public Model.Playlist CurrentPlaylist;
        private String playlistName;
        public String PlaylistName
        {
            get
            {
                return playlistName;
            }
            set
            {
                playlistName = value;
                OnPropertyChanged("PlaylistName");
            }
        }

        #endregion

        private PlaylistViewModel()
        {
            LoadPlaylistsFromXML();
        }

        /* LOAD PLAYLISTS FROM XML */

        private void LoadPlaylistsFromXML()
        {
            XML.XMLPlaylist XMLPlaylist = new XML.XMLPlaylist();
            PlaylistsList = XMLPlaylist.ExtractPlaylists();
        }

        public static PlaylistViewModel getInstance()
        {
            if (instance == null)
            {
                instance = new PlaylistViewModel();
            }
            return instance;
        }
    }
}
