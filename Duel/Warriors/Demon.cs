using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    class Demon : Warrior
    {
        public Demon(string type = "") : base(type, 150, 6)
        {
            base.Equip("sword");
        }
    }
}
