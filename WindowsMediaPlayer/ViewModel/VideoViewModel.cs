using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using WindowsMediaPlayer.XML;

namespace WindowsMediaPlayer.ViewModel
{
    public class VideoViewModel : ViewModelBase
    {
        #region VideoViewModelProperties

        /* PRIVATE */

        /* PUBLIC */

        public ObservableCollection<Model.Video> VideoList { get; set; }

        static private VideoViewModel instance = null;

        #endregion

        /* CTOR */

        private VideoViewModel()
        {
            VideoList = new ObservableCollection<Model.Video>();
            LoadVideoDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            if (System.IO.Directory.Exists(@"C:\Users\Public\Videos"))
                LoadVideoDirectory(@"C:\Users\Public\Videos");

            /*Console.WriteLine("SIZE : " + VideoList.Count + "\n\n");
            foreach (Model.Video video in VideoList)
            {
                Console.WriteLine("Path : " + video.Path + "\n\n");
            }*/

            /*XMLMedia XMLVideo = new XMLMedia();
            XMLVideo.LoadXML("Video.XML");
            XMLVideo.AddMedia(this.VideoList[0]);
            XMLVideo.WriteXML("Video.XML");*/
            Console.WriteLine(System.IO.File.ReadAllText("Video.XML"));
        }

        public static VideoViewModel getInstance()
        {
            if (VideoViewModel.instance == null)
                instance = new VideoViewModel();
            return instance;
        }

        /* LOADING FROM BASIC DIRECTORY */

        private void LoadVideoDirectory(String DirectoryName)
        {
            String[] filesEntries = System.IO.Directory.GetFiles(DirectoryName);
            String[] subdirectoryEntries = System.IO.Directory.GetDirectories(DirectoryName);

            foreach (String file in filesEntries)
            {
                LoadVideoFile(file);
            }
            foreach (String subdirectory in subdirectoryEntries)
            {
                LoadVideoDirectory(subdirectory);
            }
        }

        private void LoadVideoFile(String FileName)
        {
            //TODO CHECK IF MEDIA ALREADY LOADED + LOAD MEDIA

            /* ERASE */

            var Extension = System.IO.Path.GetExtension(FileName).ToUpper();

            if (Extension == ".MP4" || Extension == ".WMV")
            {
                Model.Video NewVideo = new Model.Video(FileName);
                VideoList.Add(NewVideo);
            }
        }
    }
}
