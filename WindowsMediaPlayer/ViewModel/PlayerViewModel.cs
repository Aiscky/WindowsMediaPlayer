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
        /*bool mediaPlayerIsPlaying = false;
        bool userIsDragingSlider = false;
           
        private ICommand playCommand;
        private ICommand pauseCommand;*/

        public MediaElement MediaElement;
        public String TextBlock { get; set; }

        public PlayerViewModel()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTick;
            MediaElement = new MediaElement();
            TextBlock = "Bonjour";
            MediaElement.Source = new Uri(@"C:\Users\Thibault\Downloads\Sample.mp4", UriKind.Absolute);
            MediaElement.LoadedBehavior = MediaState.Play;
        }

        private void timerTick(object sender, EventArgs e)
        {
        }
    }
}
