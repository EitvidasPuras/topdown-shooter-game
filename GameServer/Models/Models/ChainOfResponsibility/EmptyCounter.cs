namespace GameServer
{
    namespace Models
    {
        public class EmptyCounter : DamageCounter
        {
            public EmptyCounter(double distance = 0, int damage = 0) : base(distance, damage)
            {

            }
            public override void Calculate(double distanceValue)
            {
            }

            public override void SetNextChain(DamageCounter next)
            {
                this.next = next;
            }
        }
    }
}

