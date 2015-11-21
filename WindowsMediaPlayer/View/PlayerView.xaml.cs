using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsMediaPlayer.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        public PlayerView()
        {
            InitializeComponent();
        }

        /* PREVENT AUTO UPDATE FROM SLIDER AND UPDATE MEDIA POSITION BASED ON DRAGCOMPLETED */

        public void progressSlider_DragStarted(object sender, DragStartedEventArgs e)
        {
            ((ViewModel.PlayerViewModel)this.DataContext).timer.Stop();
        }

        public void progressSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            ((ViewModel.PlayerViewModel)this.DataContext).MediaElement.Position = TimeSpan.FromSeconds(this.progressSlider.Value);
            ((ViewModel.PlayerViewModel)this.DataContext).timer.Start();
        }
    }
}
