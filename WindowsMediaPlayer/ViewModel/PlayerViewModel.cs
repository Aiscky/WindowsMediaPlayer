using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace WindowsMediaPlayer.ViewModel
{
    class PlayerViewModel : ViewModelBase
    {
        bool mediaPlayerIsPlaying = false;
        bool userIsDragingSlider = false;
            
        private ICommand playCommand;
        private ICommand pauseCommand;

        public PlayerViewModel()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTick;
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {

        }
    }
}
