using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayer.ViewModel
{
    class VideoViewModel : ViewModelBase
    {
        #region VideoViewModelProperties

        /* PRIVATE */

        /* PUBLIC */

        ObservableCollection<Model.Video> VideoList { get; set; }

        static private VideoViewModel instance = null;

        #endregion

        /* CTOR */

        private VideoViewModel()
        {
            LoadVideoDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
            if (System.IO.Directory.Exists(@"C:\Users\Public\Videos"));
                LoadVideoDirectory(@"C:\Users\Public\Videos");
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

            Console.WriteLine("File : " + FileName);
        }

        public static VideoViewModel getInstance()
        {
            if (instance == null)
                instance = new VideoViewModel();
            return instance;
        }
    }
}
