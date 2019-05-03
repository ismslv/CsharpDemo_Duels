using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    public class Viking : Warrior
    {
        public Viking(string type = "") : base(type, 120)
        {
            base.Equip("axe");
        }

        public Viking Equip(string equipment)
        {
            base.Equip(equipment);
            return this;
        }
    }
}
