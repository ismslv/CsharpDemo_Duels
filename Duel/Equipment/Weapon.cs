using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    public class Weapon : Equipment
    {
        public intI Damage;

        public Weapon(string name, int hands, string damage, string chance)
        {
            Type = "weapon";
            Name = name;
            Damage = new intI(damage);
            Hands = hands;
            Chance = new Switch(chance);
        }

        public override Equipment Instantiate()
        {
            return new Weapon(Name, Hands, Damage.ToPattern(), Chance.ToPattern()) as Equipment;
        }
    }
}
