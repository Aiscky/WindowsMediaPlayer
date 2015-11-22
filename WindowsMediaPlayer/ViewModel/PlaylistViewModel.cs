using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WindowsMediaPlayer.ViewModel
{
    public class PlaylistViewModel : ViewModelBase
    {
        #region PlaylistViewModelProperties

        private static PlaylistViewModel instance = null;
        public ObservableCollection<Model.Playlist> PlaylistsList { get; set; }
        private Model.Playlist currentPlaylist = null;
        public Model.Playlist CurrentPlaylist
        {
            get
            {
                return currentPlaylist;
            }
            set
            {
                currentPlaylist = value;
                OnPropertyChanged("CurrentPlaylist");
            }
        }
        private ICommand createPlaylistCommand = null;
        private String playlistName = "";
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
            XMLPlaylist.LoadXML("Playlist.xml");
            XMLPlaylist.RemoveAllPlaylists();
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

        /* COMMAND GETTER */

        public ICommand CreatePlaylistCommand
        {
            get
            {
                if (createPlaylistCommand == null)
                {
                    createPlaylistCommand = new RelayCommand(() => ExecuteCreatePlaylist(), () => CanCreatePlaylist());
                }
                return createPlaylistCommand;
            }
        }
        
        /* COMMAND FONCTIONS */

        private bool CanCreatePlaylist()
        {
            if (playlistName != "")
                return true;
            return false;
        } 

        private void ExecuteCreatePlaylist()
        {
            Model.Playlist NewPlaylist = new Model.Playlist() { Name = playlistName };

            this.PlaylistsList.Add(NewPlaylist);
            this.CurrentPlaylist = NewPlaylist;
            XML.XMLPlaylist XMLPlaylist = new XML.XMLPlaylist();
            XMLPlaylist.LoadXML("Playlist.xml");
            XMLPlaylist.AddPlaylist(playlistName);
            XMLPlaylist.WriteXML("Playlist.xml");
        }
    }
}
