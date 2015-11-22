using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsMediaPlayer.XML;

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
            LoadImagesFromXML();
        }

        /* LOAD IMAGES FROM XML */

        private void LoadImagesFromXML()
        {
            XMLMedia XMLImage = new XMLMedia();
            XMLImage.LoadXML("Image.XML");

            List<String> imagesList = XMLImage.ExtractMedias();

            foreach (var image in imagesList)
            {
                var DirectoriesImages = from DirectoryImage in ImageList where DirectoryImage.Path == image select DirectoryImage;

                if (DirectoriesImages.Count() == 0)
                {
                    ImageList.Add(new Model.Image(image));
                }
            }
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
                XMLMedia XMLImage = new XMLMedia();
                XMLImage.LoadXML("Image.xml");
                XMLImage.AddMedia(NewImage);
                XMLImage.WriteXML("Image.xml");
            }
        }
    }
}
