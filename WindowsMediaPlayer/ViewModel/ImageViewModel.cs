using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WindowsMediaPlayer.ViewModel
{
    public class ImageViewModel : ViewModelBase
    {
        #region ImageViewModelProperties

        /* PRIVATE */

        /* Command */

        private ICommand openFileImageCommand;

        /* PUBLIC */

        public ObservableCollection<Model.Image> ImageList { get; set; }

        static private ImageViewModel instance = null;

        #endregion

        /* CTOR */

        private ImageViewModel()
        {
            ImageList = new ObservableCollection<Model.Image>();
            LoadImageDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            if (System.IO.Directory.Exists(@"C:\Users\Public\Pictures"))
                LoadImageDirectory(@"C:\Users\Public\Pictures");

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

        public static ImageViewModel getInstance()
        {
            if (ImageViewModel.instance == null)
                instance = new ImageViewModel();
            return instance;
        }

        /* LOADING FROM BASIC DIRECTORY */

        private void LoadImageDirectory(String DirectoryName)
        {
            String[] filesEntries = System.IO.Directory.GetFiles(DirectoryName);
            String[] subdirectoryEntries = System.IO.Directory.GetDirectories(DirectoryName);

            foreach (String file in filesEntries)
            {
                LoadImageFile(file);
            }
            foreach (String subdirectory in subdirectoryEntries)
            {
                LoadImageDirectory(subdirectory);
            }
        }

        private void LoadImageFile(String FileName)
        {
            //TODO CHECK IF MEDIA ALREADY LOADED + LOAD MEDIA

            /* ERASE */

            var Extension = System.IO.Path.GetExtension(FileName).ToUpper();

            if (Extension == ".PNG" || Extension == ".JPG" || Extension == ".JPEG")
            {
                Model.Image NewImage = new Model.Image(FileName);
                ImageList.Add(NewImage);
            }
        }

        public ICommand OpenFileImageCommand
        {
            get
            {
                if (this.openFileImageCommand == null)
                {
                    this.openFileImageCommand = new RelayCommand(() => ExecuteOpenImageFile(), () => CanExecuteOpenImageFile());
                }
                return this.openFileImageCommand;
            }
        }

        private bool CanExecuteOpenImageFile()
        {
            return true;
        }

        private void ExecuteOpenImageFile()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "PNG Files (.png)|*.png|JPG Files (.jpg)|*.jpg|JPEG Files (.jpeg)|*.jpeg|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Image NewImage = new Model.Image(openFileDialog.FileName);
                ImageList.Add(NewImage);
            }
        }
    }
}
