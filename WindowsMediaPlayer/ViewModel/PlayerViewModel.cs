using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WindowsMediaPlayer.ViewModel
{
    class PlayerViewModel : ViewModelBase
    {
        /* PRIVATE */

        bool mediaIsPlaying = false;
        bool sliderIsDragged = false;
        static private PlayerViewModel _instance = null;

        /* PUBLIC */

        public MediaElement MediaElement { get; set; }
        public Image MediaImage { get; set; }

        /* COMMANDS */

        private ICommand mediaPlayCommand;
        private ICommand mediaPauseCommand;

        /* CTOR */

        private PlayerViewModel()
        {
            /* Setting the thread for media timer */

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTick;

            MediaElement = new MediaElement();
            MediaElement.LoadedBehavior = MediaState.Manual;
            MediaElement.Visibility = System.Windows.Visibility.Hidden;
            MediaImage = new Image();
            MediaImage.Visibility = System.Windows.Visibility.Visible;
            try
            {
                //MediaImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"C:\Users\Thibault\Downloads\PlayerBackground.png"));
                MediaImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "../../Ressources/PlayerBackground.png"));
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\n\n" + e.Message + "\n\n\n");
                Console.WriteLine("\n\n123\n\n");
            }
            Console.WriteLine(MediaImage.Source);
        }

        public static PlayerViewModel getInstance()
        {
            if (_instance == null)
                _instance = new PlayerViewModel();
            return _instance;
        }

        /* PLAY MEDIA FONCTIONS */

        public void PlayMedia(Model.Media media)
        {
            switch (media.type)
            {
                case Model.Media.MediaType.VIDEO:
                    break;
                case Model.Media.MediaType.IMAGE:
                    break;
                case Model.Media.MediaType.MUSIC:
                    break;
            }            
        }

        public void PlayVideo(Model.Media media)
        {
            this.MediaElement.Source = new Uri(media.Path);
            this.MediaElement.Play();
            this.MediaElement.Visibility = System.Windows.Visibility.Visible;
            this.MediaImage.Visibility = System.Windows.Visibility.Hidden;
        }

        public void PlayMusic(Model.Media media)
        {
            this.MediaElement.Source = new Uri(media.Path);
            this.MediaElement.Visibility = System.Windows.Visibility.Hidden;
            this.MediaImage.Visibility = System.Windows.Visibility.Visible;
            try
            {
                MediaImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "../../Ressources/PlayerBackground.png"));
            }
            catch { }
            this.MediaElement.Play();
        }

        public void PlayImage(Model.Media media)
        {
            try
            {
                this.MediaImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(media.Path));
            }
            catch { }

        }

        /* THREAD TICKING FONCTION */

        private void timerTick(object sender, EventArgs e)
        {
               
        }

        /* FONCTIONS GETTER */

        public ICommand MediaPlayCommand
        {
            get 
            {
                if (this.mediaPlayCommand == null)
                {
                    this.mediaPlayCommand = new RelayCommand(() => ExecutePlayMedia(), () => CanExecutePlayMedia());
                }
                return mediaPlayCommand;
            }
        }

        public ICommand MediaPauseCommand
        {
            get
            {
                if (this.mediaPauseCommand == null)
                {
                    this.mediaPauseCommand = new RelayCommand(() => ExecutePauseMedia(), () => CanExecutePauseMedia());
                }
                return this.mediaPauseCommand;
            }
        }

        /*  COMMANDS FONCTIONS */

        private bool CanExecutePlayMedia()
        {
            if (this.MediaElement.Source == null || mediaIsPlaying == true)
                return false;
            return true;
        }

        private void ExecutePlayMedia()
        {
            mediaIsPlaying = true;
            MediaElement.Play();
        }

        private bool CanExecutePauseMedia()
        {
            if (this.MediaElement.Source == null || mediaIsPlaying == false)
                return false;
            return true;
        }

        private void ExecutePauseMedia()
        {
            mediaIsPlaying = false;
            MediaElement.Pause();
        }
    }
}
