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
    /// Interaction logic for MusicView.xaml
    /// </summary>
    public partial class MusicView : UserControl
    {
        public MusicView()
        {
            InitializeComponent();

            this.DataContext = new View.MusicView.MusicDataContext { PlayerViewModel = PlayerViewModel.getInstance(), MusicViewModel = MusicViewModel.getInstance() };
        }

        private void ItemMusicList_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            ((MusicDataContext)this.DataContext).PlayerViewModel.PlayMedia((Model.Music)this.MusicListView.SelectedItem);
            ((MusicDataContext)this.DataContext).PlayerViewModel.MainWindowTabControl.SelectedIndex = 0;
        }

        public class MusicDataContext
        {
            public ViewModel.PlayerViewModel PlayerViewModel { get; set; }
            public ViewModel.MusicViewModel MusicViewModel { get; set; }
        }
    }
}
