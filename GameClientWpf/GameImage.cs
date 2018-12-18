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
    class GameImage : Image, IFlyweight, ICloneable
    {
        public GameImage(string key) : base()
        {
            if (key.Equals("AK47"))
            {
                Source = new BitmapImage(new Uri("/images/ak47.png", UriKind.Relative));
                Height = 15;
                
            }
            else if (key.Equals("M4A1"))
            {
                Source = new BitmapImage(new Uri("/images/m4a1.png", UriKind.Relative));
                Height = 15;
            }
            else if (key.Equals("DesertEagle"))
            {
                Source = new BitmapImage(new Uri("/images/deserteagle.png", UriKind.Relative));
                Height = 15;
            }
            else if (key.Equals("P250"))
            {
                Source = new BitmapImage(new Uri("/images/p250.png", UriKind.Relative));
                Height = 15;
            }
            else if (key.Equals("GrenadeAdapter"))
            {
                Source = new BitmapImage(new Uri("/images/grenade.png", UriKind.Relative));
                Height = 15;
            }
            else if (key.Equals("Player"))
            {
                Source = new BitmapImage(new Uri("/images/basicPlayer.png", UriKind.Relative));
                Height = 50;
            }
            Margin = new Thickness(0, 0, 0, 0);
        }

        public GameImage() : base()
        {
        }

        public object Clone()
        {
            GameImage imageclone = new GameImage();
            imageclone.Source = Source;
            imageclone.Height = Height;
            imageclone.Margin = Margin;
            return imageclone;
        }

        public void Move(int posX, int posY)
        {
            RenderTransform = new TranslateTransform(posX - 400, posY - 300);
        }
    }
}
