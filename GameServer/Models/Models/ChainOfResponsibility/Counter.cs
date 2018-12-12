namespace GameServer
{
    namespace Models
    {
        public class Counter : DamageCounter
        {
            public Counter(double distance, int damage) : base(distance, damage)
            {

            }

            public override void Calculate(double distanceValue)
            {
                if(distanceValue <= Distance && DamageSum == 0)
                {
                    DamageSum += Damage;
                }
                next.Calculate(distanceValue);
            }

            public override void SetNextChain(DamageCounter next)
            {
                this.next = next;
            }
        }
    }
}

