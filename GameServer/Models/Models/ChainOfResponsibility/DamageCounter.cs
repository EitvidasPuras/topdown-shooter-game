namespace GameServer
{
    namespace Models
    {
        public abstract class DamageCounter
        {
            public DamageCounter next { get; set; }

            public double Distance { get; set; }

            public int Damage { get; set; }

            protected static int DamageSum { get; set; }

            public DamageCounter(double distance, int damage)
            {
                Distance = distance;
                Damage = damage;            
            }

            public abstract void Calculate(double distanceValue);

            public abstract void SetNextChain(DamageCounter next);

            public double GetDamageSum()
            {
                return DamageSum;
            }

            public void EmptyDamageSum()
            {
                DamageSum = 0;
            }

        }
    }
}

