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
    public class PlayerViewModel : ViewModelBase
    {
        #region PlayerViewModelProperties

        /* PRIVATE */

        bool mediaIsPlaying = false;
        //bool sliderIsDragged = false;
        static private PlayerViewModel _instance = null;
        private double mediaPosition;
        public double MediaPosition
        {
            get
            {
                return mediaPosition;
            }
            set
            {
                mediaPosition = value;
                //MediaElement.Position = TimeSpan.FromSeconds(value);
                OnPropertyChanged("MediaPosition");
                progressSlider_ValueChanged();
            }
        }
        private double mediaDuration;
        public double MediaDuration
        {
            get
            {
                return mediaDuration;
            }
            set
            {
                mediaDuration = value;
                OnPropertyChanged("MediaDuration");
            }
        }
        private String mediaProgressStatus;
        public String MediaProgressStatus
        {
            get
            {
                return mediaProgressStatus;
            }
            set
            {
                mediaProgressStatus = value;
                OnPropertyChanged("MediaProgressStatus");
            }
        }


        /* PUBLIC */

        private TabControl mainWindowTabControl = null;
        public TabControl MainWindowTabControl
        {
            get
            {
                return mainWindowTabControl;
            }
            set
            {
                this.mainWindowTabControl = value;
                OnPropertyChanged("MainWindowTabControl");
            }
        }
        public MediaElement MediaElement { get; set; }
        public Image MediaImage { get; set; }
        public DispatcherTimer timer { get; set; }

        /* COMMANDS */

        private ICommand mediaPlayCommand;
        private ICommand mediaPauseCommand;
        private ICommand mediaStopCommand;

        #endregion

        /* CTOR */

        private PlayerViewModel()
        {
            /* Setting the thread for media timer */

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.05);
            timer.Tick += timerTick;

            MediaElement = new MediaElement();
            MediaElement.LoadedBehavior = MediaState.Manual;
            MediaElement.Visibility = System.Windows.Visibility.Hidden;
            MediaImage = new Image();
            MediaImage.Visibility = System.Windows.Visibility.Visible;
            try
            {
                MediaImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "../../Ressources/PlayerBackground.png"));
            }
            catch
            { }
            //Model.Image test = new Model.Image(@"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg");
            Model.Video test = new Model.Video(@"C:\Users\Thibault\Downloads\Sample.mp4");
            PlayMedia(test);
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
            /* SETTING INTERFACE VALUES */

            MediaProgressStatus = "00:00:00";
            MediaDuration = 0;
            MediaPosition = 0;

            /* CALLING MEDIA SPECIFIC FONCTION */

            if (media == null)
                return;

            switch (media.type)
            {
                case Model.Media.MediaType.VIDEO:
                    PlayVideo(media);
                    break;
                case Model.Media.MediaType.IMAGE:
                    PlayImage(media);
                    break;
                case Model.Media.MediaType.MUSIC:
                    PlayMusic(media);
                    break;
                default:
                    break;
            }
        }

        public void PlayVideo(Model.Media media)
        {
            this.MediaElement.Source = new Uri(media.Path);
            this.MediaElement.Play();
            this.timer.Start();
            this.mediaIsPlaying = true;
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
            this.timer.Start();
            this.mediaIsPlaying = true;
        }

        public void PlayImage(Model.Media media)
        {
            this.MediaElement.Visibility = System.Windows.Visibility.Hidden;
            this.MediaImage.Visibility = System.Windows.Visibility.Visible;
            try
            {
                this.MediaImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(media.Path));
            }
            catch
            { }
            this.mediaIsPlaying = true;
        }

        /* THREAD TICKING FONCTION */

        private void timerTick(object sender, EventArgs e)
        {
            if (MediaElement.Source != null)
            {
                if (MediaElement.NaturalDuration.HasTimeSpan)
                    MediaDuration = MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                MediaPosition = MediaElement.Position.TotalSeconds;
            }
        }

        /* FONCTIONS EVENT */

        public void progressSlider_ValueChanged()
        {
            MediaProgressStatus = TimeSpan.FromSeconds(MediaPosition).ToString(@"hh\:mm\:ss");
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

        public ICommand MediaStopCommand
        {
            get
            {
                if (this.mediaStopCommand == null)
                {
                    this.mediaStopCommand = new RelayCommand(() => ExecuteStopMedia(), () => CanExecuteStopMedia());
                }
                return this.mediaStopCommand;
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
            timer.Start();
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
            timer.Stop();
        }

        private bool CanExecuteStopMedia()
        {
            if (this.MediaElement.Source == null)
                return false;
            return true;
        }

        private void ExecuteStopMedia()
        {
            mediaIsPlaying = false;
            MediaElement.Stop();
            timer.Stop();
            MediaPosition = 0;
        }
    }
}
