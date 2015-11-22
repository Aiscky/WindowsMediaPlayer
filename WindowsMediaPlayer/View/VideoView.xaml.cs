using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsMediaPlayer.ViewModel;

namespace WindowsMediaPlayer.View
{
    /// <summary>
    /// Interaction logic for VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl
    {
        public VideoView()
        {
            InitializeComponent();

            /* SETTING DATA CONTEXT */

            Console.WriteLine("Creation VideoView");
            this.DataContext = new View.VideoView.VideoDataContext { PlayerViewModel = PlayerViewModel.getInstance(), VideoViewModel = VideoViewModel.getInstance() };
        }

        protected void ItemVideoList_DoubleClicked(Object sender, MouseButtonEventArgs e)
        {
            ((VideoDataContext)this.DataContext).PlayerViewModel.PlayMedia((Model.Video)this.VideoListView.SelectedItem);
            ((VideoDataContext)this.DataContext).PlayerViewModel.MainWindowTabControl.SelectedIndex = 0;
            e.Handled = true;
        }

        public class VideoDataContext
        {
            public ViewModel.PlayerViewModel PlayerViewModel { get; set; }
            public ViewModel.VideoViewModel VideoViewModel { get; set; }
        }
    }
}
