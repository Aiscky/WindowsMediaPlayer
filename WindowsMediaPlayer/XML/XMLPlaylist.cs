using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayer.XML
{
    class XMLPlaylist
    {
        System.Xml.Linq.XElement XElement = null;

        /* LOADING XML FILE */

        public void LoadXML(string Filename)
        {
            try
            {
                XElement = System.Xml.Linq.XElement.Load(Filename);
            }
            catch
            {
                XElement = new System.Xml.Linq.XElement("Playlists");
            }
        }

        public System.Collections.ObjectModel.ObservableCollection<Model.Playlist> ExtractPlaylists()
        {
            IEnumerable<System.Xml.Linq.XElement> playlists = XElement.Elements();
            System.Collections.ObjectModel.ObservableCollection<Model.Playlist> playlistsList = new System.Collections.ObjectModel.ObservableCollection<Model.Playlist>();
            if (XElement == null)
                return null;
            try
            {
                /* CREATING AN OBSERVABLECOLLECTION FROM ALL PLAYLIST */

                foreach (var playlist in playlists)
                {
                    IEnumerable<System.Xml.Linq.XElement> medias = XElement.Elements("Media");
                    System.Collections.ObjectModel.ObservableCollection<Model.Media> mediasList = new System.Collections.ObjectModel.ObservableCollection<Model.Media>();

                    /* CREATING AN MEDIA COLLECTION CORRESPONDING TO ALL MEDIAS OF PLAYLIST */

                    foreach (var media in medias)
                    {
                        /* TEST IF XML ELEMENT IS VALID FOR MEDIA */

                        if (media != null && media.Element("Path") != null && media.Element("Type") != null)
                        {
                            Model.Media newMedia = null;

                            switch ((Model.Media.MediaType)Enum.Parse(typeof(Model.Media.MediaType), media.Element("Type").Value))
                            {
                                case Model.Media.MediaType.IMAGE:
                                    newMedia = new Model.Image(media.Element("Path").Value);
                                    break;
                                case Model.Media.MediaType.VIDEO:
                                    newMedia = new Model.Video(media.Element("Path").Value);
                                    break;
                                case Model.Media.MediaType.MUSIC:
                                    newMedia = new Model.Music(media.Element("Path").Value);
                                    break;
                            }

                            if (newMedia != null) 
                                mediasList.Add(newMedia);
                        }
                    }

                    /* ADDING NEW PLAYLIST ELEMENT TO SELECTION */

                    playlistsList.Add(new Model.Playlist() { Name = playlist.Element("Name").Value, MediasList = mediasList });
                }
            }
            catch
            { }

            return playlistsList;
        }

        /* ADD PLAYLIST XELEMENT */

        public void AddPlaylist(String playlistName)
        {
            var playlists = from playlist in XElement.Elements() where (String)playlist.Element("Name").Value == playlistName select playlist;

            if (playlists.Count() != 0)
                return;

            XElement.Add(new System.Xml.Linq.XElement("Playlist",  new System.Xml.Linq.XElement("Name", playlistName)));
        }

        /* REMOVE PLAYLIST XELEMENT FROM NAME */

        public void RemovePlaylist(Model.Playlist rPlaylist)
        {
            var playlists = from playlist in XElement.Elements() where (String)playlist.Element("Name").Value == playlist.Name select playlist;

            foreach (var playlist in playlists)
            {
                playlist.Remove();
            }
        }

        /* ADD MEDIAOBJECT TO PLAYLIST */

        public void AddPlaylistItem(String playlistName, Model.Media media)
        {
            var playlists = from playlist in XElement.Elements() where (String)playlist.Element("Name").Value == playlistName select playlist;

            if (playlists.Count() != 0)
                return;

            foreach (var playlist in playlists)
            {
                playlist.Add(new System.Xml.Linq.XElement("Media", 
                    new System.Xml.Linq.XElement("Path", media.Path),
                    new System.Xml.Linq.XElement("Type", media.type)));
            }
        }

        /*  REMOVE MEDIAOBJECT FROM PLAYLIST */

        public void RemovePlaylistItem(String playlistName, Model.Media rMedia)
        {
            var playlists = from playlist in XElement.Elements() where (String)playlist.Element("Name").Value == playlistName select playlist;

            if (playlists.Count() != 0)
                return;

            foreach (var playlist in playlists)
            {
                var medias = from media in playlist.Elements() where (String)media.Element("Path").Value == rMedia.Path select media;

                foreach (var media in medias)
                {
                    media.Remove();
                }
            }
        }

        /* REMOVE ALL PLAYLISTS */

        public void RemoveAllPlaylists()
        {
            XElement.RemoveAll();
        }

        /* SAVE XML FILE TO FILENAME */

        public void WriteXML(String XMLFile)
        {
            if (XElement != null)
            {
                XElement.Save(XMLFile);
            }
        }
    }
}
