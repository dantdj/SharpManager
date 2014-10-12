using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SharpManager.Helpers.HelpersWindows
{
    /// <summary>
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow
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
