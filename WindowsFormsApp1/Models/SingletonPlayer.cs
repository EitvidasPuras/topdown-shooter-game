using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameClient.Models
{
    class SingletonPlayer
    {
        private static SingletonPlayer instance = null;
        private static Player player;

        private SingletonPlayer() { }

        public static SingletonPlayer getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonPlayer();
            }

            return instance;
        }

        public long getId()
        {
            return player.Id;
        }

        public string getName()
        {
            return player.Name;
        }

        public long getScore()
        {
            return player.Score;
        }

        public long getPosX()
        {
            return player.PosX;
        }

        public long getPosY()
        {
            return player.PosY;
        }

        public int getHealth()
        {
            return player.Health;
        }

        public void setId(long id)
        {
            player.Id = id;
        }

        public void setName(string name)
        {
            player.Name = name;
        }

        public void setScore(long score)
        {
            player.Score = score;
        }

        public void setPosX(long posx)
        {
            player.PosX = posx;
        }

        public void setPosY(long posy)
        {
            player.PosY = posy;
        }

        public void setHealth(int health)
        {
            player.Health = health;
        }
    }
}
