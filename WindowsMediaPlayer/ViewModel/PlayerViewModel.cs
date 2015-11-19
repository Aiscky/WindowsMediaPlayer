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

            /* */

            MediaElement = new MediaElement();
            MediaElement.Source = new Uri(@"C:\Users\Thibault\Downloads\Sample.mp4");
            MediaElement.LoadedBehavior = MediaState.Manual;
        }

        public static PlayerViewModel getInstance()
        {
            if (_instance == null)
                _instance = new PlayerViewModel();
            return _instance;
        }

        /* PLAY MEDIA FONCTIONS */

        public void PlayMedia()
        {

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
