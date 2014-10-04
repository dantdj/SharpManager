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
using System.Windows.Shapes;

namespace SharpManager.Helpers.HelpersWindows
{
    /// <summary>
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        public string ImagePath;

        public ImageWindow()
        {
            InitializeComponent();
        }

        private void Show_Image(string filename)
        {
            Image_Area.Source = new BitmapImage(new Uri(filename));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Show_Image(ImagePath);
        }
    }
}
