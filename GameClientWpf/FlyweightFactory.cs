using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class FlyweightFactory
    {
        private Dictionary<string, IFlyweight> flyweights;

        public FlyweightFactory()
        {
            flyweights = new Dictionary<string, IFlyweight>();
        }

        public IFlyweight GetFlyweight(string key)
        {
            IFlyweight flyweight;
            if (!flyweights.TryGetValue(key, out flyweight))
            {
                flyweight = new GameImage(key);
                flyweights.Add(key, flyweight);
            }
            return flyweight;
        }
    }
}
