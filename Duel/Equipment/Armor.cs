using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    public class Armor : Equipment
    {
        public intI Protection;
        public intI Attack;
        public Dictionary<string, Counter> Destroy;
        private readonly string destroyPattern;
        public Armor(string name, int hands, string protection, string attack, string chance, string destroyPattern)
        {
            Type = "armor";
            Name = name;
            Protection = new intI(protection);
            Attack = new intI(attack);
            Hands = hands;
            Chance = new Switch(chance);
            this.destroyPattern = destroyPattern;
            ParseDestroyPattern(destroyPattern);
        }

        void ParseDestroyPattern(string pattern)
        {
            Destroy = new Dictionary<string, Counter>();
            if (pattern != "")
            {
                var p = pattern.Split("/"[0]);
                foreach (var ip in p)
                {
                    var ip_ = ip.Split("-"[0]);
                    int.TryParse(ip_[1], out int max);
                    Destroy[ip_[0]] = new Counter(max);
                }
            }
        }

        public override Equipment Instantiate()
        {
            return new Armor(Name, Hands, Protection.ToPattern(), Attack.ToPattern(), Chance.ToPattern(), destroyPattern) as Equipment;
        }
    }
}