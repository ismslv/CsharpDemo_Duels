using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    public class Swordsman : Warrior
    {
        public Swordsman(string type = "") : base(type, 100)
        {
            base.Equip("sword");
        }

        public Swordsman Equip(string equipment)
        {
            base.Equip(equipment);
            return this;
        }
    }
}
