using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WindowsMediaPlayer.XML;

namespace WindowsMediaPlayer.ViewModel
{
    public class VideoViewModel : ViewModelBase
    {
        #region VideoViewModelProperties

        /* PRIVATE */

        /* Command */

        private ICommand openFileVideoCommand;

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
            LoadVideosFromXML();
        }

        /* LOAD VIDEO FROM XML */

        private void LoadVideosFromXML()
        {
            XMLMedia XMLVideo = new XMLMedia();
            XMLVideo.LoadXML("Video.XML");

            List<String> videoList = XMLVideo.ExtractMedias();

            foreach (var video in videoList)
            {
                var DirectoriesVideos = from DirectoryVideo in VideoList where DirectoryVideo.Path == video select DirectoryVideo;

                if (DirectoriesVideos.Count() == 0)
                {
                    VideoList.Add(new Model.Video(video));
                }
            }
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
        
        public ICommand OpenFileVideoCommand
        {
            get
            {
                if (this.openFileVideoCommand == null)
                {
                    this.openFileVideoCommand = new RelayCommand(() => ExecuteOpenVideoFile(), () => CanExecuteOpenVideoFile());
                }
                return this.openFileVideoCommand;
            }
        }

        private bool CanExecuteOpenVideoFile()
        {
            return true;
        }

        private void ExecuteOpenVideoFile()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "MP4 Files (.mp4)|*.mp4|WMV Files (.wmv)|*.wmv";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Video NewVideo = new Model.Video(openFileDialog.FileName);
                VideoList.Add(NewVideo);
                XMLMedia XMLVideo = new XMLMedia();
                XMLVideo.LoadXML("Video.xml");
                XMLVideo.AddMedia(NewVideo);
                XMLVideo.WriteXML("Video.xml");
            }
        }
    }
}
