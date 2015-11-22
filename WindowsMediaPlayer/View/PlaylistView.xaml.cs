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
    /// Interaction logic for PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        public PlaylistView()
        {
            InitializeComponent();

            this.DataContext = new PlaylistDataContext() { PlayerViewModel = PlayerViewModel.getInstance(), PlaylistViewModel = PlaylistViewModel.getInstance() };
        }

        public class PlaylistDataContext
        {
            public ViewModel.PlayerViewModel PlayerViewModel { get; set; }
            public ViewModel.PlaylistViewModel PlaylistViewModel { get; set; }
        }
    }
}
