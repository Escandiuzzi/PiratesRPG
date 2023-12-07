namespace Models
{
    public class Special
    {
        public Special(string name, int damage, int heal, int range, int energy)
        {
            Name = name;
            Damage = damage;
            Heal = heal;
            Range = range;
            Energy = energy;
        }

        public string Name { get; private set; }
        public int Damage { get; private set; }
        public int Heal { get; private set; }
        public int Range { get; private set; }
        public int Energy { get; private set; }
    }
}