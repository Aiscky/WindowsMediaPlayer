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
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : UserControl
    {
        public ImageView()
        {
            InitializeComponent();

            this.DataContext = new View.ImageView.ImageDataContext { PlayerViewModel = PlayerViewModel.getInstance(), ImageViewModel = ImageViewModel.getInstance() };
        }

        private void ItemImageList_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            ((ImageDataContext)this.DataContext).PlayerViewModel.PlayMedia((Model.Image)this.ImageListView.SelectedItem);
            ((ImageDataContext)this.DataContext).PlayerViewModel.MainWindowTabControl.SelectedIndex = 0;
        }

        public class ImageDataContext
        {
            public ViewModel.PlayerViewModel PlayerViewModel { get; set; }
            public ViewModel.ImageViewModel ImageViewModel { get; set; }
        }
    }
}
