﻿using System;
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
                MediaImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "../../Ressources/PlayerBackground.png"));
            }
            catch
            { }
            Model.Music test = new Model.Music(@"C:\Users\Public\Music\Sample Music\Kalimba.mp3");
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
            }
        }

        public void PlayVideo(Model.Media media)
        {
            this.MediaElement.Source = new Uri(media.Path);
            this.MediaElement.Play();
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
            Console.WriteLine(((Model.Music)media).Artist);
            Console.WriteLine(((Model.Music)media).Album);
            Console.WriteLine(((Model.Music)media).Title);
            this.MediaElement.Play();
            this.mediaIsPlaying = true;
        }

        public void PlayImage(Model.Media media)
        {
            try
            {
                this.MediaImage.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(media.Path));
            }
            catch { }
            this.mediaIsPlaying = true;
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
