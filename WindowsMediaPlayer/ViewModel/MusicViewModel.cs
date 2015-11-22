using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WindowsMediaPlayer.ViewModel
{
    public class MusicViewModel : ViewModelBase
    {
        #region MusicViewModelProperties

        /* PRIVATE */

        /* Command */

        private ICommand openFileMusicCommand;

        /* PUBLIC */

        public ObservableCollection<Model.Music> MusicList { get; set; }

        static private MusicViewModel instance = null;

        #endregion

        /* CTOR */

        private MusicViewModel()
        {
            MusicList = new ObservableCollection<Model.Music>();
            LoadMusicDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
            if (System.IO.Directory.Exists(@"C:\Users\Public\Music"))
                LoadMusicDirectory(@"C:\Users\Public\Music");

            /*Console.WriteLine("SIZE : " + VideoList.Count + "\n\n");
            foreach (Model.Video video in VideoList)
            {
                Console.WriteLine("Path : " + video.Path + "\n\n");
            }*/

            /*XMLMedia XMLVideo = new XMLMedia();
            XMLVideo.LoadXML("Video.XML");
            XMLVideo.AddMedia(this.VideoList[0]);
            XMLVideo.WriteXML("Video.XML");*/
            //Console.WriteLine(System.IO.File.ReadAllText("Video.XML"));
        }

        public static MusicViewModel getInstance()
        {
            if (MusicViewModel.instance == null)
                instance = new MusicViewModel();
            return instance;
        }

        /* LOADING FROM BASIC DIRECTORY */

        private void LoadMusicDirectory(String DirectoryName)
        {
            String[] filesEntries = System.IO.Directory.GetFiles(DirectoryName);
            String[] subdirectoryEntries = System.IO.Directory.GetDirectories(DirectoryName);

            foreach (String file in filesEntries)
            {
                LoadMusicFile(file);
            }
            foreach (String subdirectory in subdirectoryEntries)
            {
                LoadMusicDirectory(subdirectory);
            }
        }

        private void LoadMusicFile(String FileName)
        {
            //TODO CHECK IF MEDIA ALREADY LOADED + LOAD MEDIA

            /* ERASE */

            var Extension = System.IO.Path.GetExtension(FileName).ToUpper();

            if (Extension == ".MP3")
            {
                Model.Music NewMusic = new Model.Music(FileName);
                MusicList.Add(NewMusic);
            }
        }

        public ICommand OpenFileMusicCommand
        {
            get
            {
                if (this.openFileMusicCommand == null)
                {
                    this.openFileMusicCommand = new RelayCommand(() => ExecuteOpenMusicFile(), () => CanExecuteOpenMusicFile());
                }
                return this.openFileMusicCommand;
            }
        }

        private bool CanExecuteOpenMusicFile()
        {
            return true;
        }

        private void ExecuteOpenMusicFile()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "MP3 Files (.mp3)|*.mp3|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Music NewMusic = new Model.Music(openFileDialog.FileName);
                MusicList.Add(NewMusic);
            }
        }
    }
}
