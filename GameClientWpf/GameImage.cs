using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    class GameImage : Image, IFlyweight
    {
        public GameImage(string key) : base()
        {
            //Height = 55;
            if (key.Contains("AK47"))
            {
                Source = new BitmapImage(new Uri("/images/ak47.png", UriKind.Relative));
                Height = 15;
            }
            else if (Name.Contains("M4A1"))
            {
                Source = new BitmapImage(new Uri("/images/m4a1.png", UriKind.Relative));
                Height = 15;
            }
            else if (Name.Contains("DesertEagle"))
            {
                Source = new BitmapImage(new Uri("/images/deserteagle.png", UriKind.Relative));
                Height = 15;
            }
            else if (Name.Contains("P250"))
            {
                Source = new BitmapImage(new Uri("/images/p250.png", UriKind.Relative));
                Height = 15;
            }
            else if (Name.Contains("Grenade"))
            {
                Source = new BitmapImage(new Uri("/images/grenade.png", UriKind.Relative));
                Height = 15;
            }
            Margin = new Thickness(0, 0, 0, 0);
        }

        public void Move(int posX, int posY)
        {
            RenderTransform = new TranslateTransform(posX - 400, posY - 300);
        }
    }
}
