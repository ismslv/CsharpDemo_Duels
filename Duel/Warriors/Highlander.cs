using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    public class Highlander : Warrior
    {
        private bool m_berserk;
        protected bool Berserk
        {
            get { return m_berserk; }
            set
            {
                if (m_berserk != value)
                {
                    if (value)
                    {
                        ModifierTotal = new DamageModifier(0, 2);
                    } else
                    {
                        ModifierTotal = null;
                    }
                }
                m_berserk = value;
            }
        }
        public Highlander(string type = "") : base(type, 150)
        {
            base.Equip("greatsword");
        }

        public Highlander Equip(string equipment)
        {
            base.Equip(equipment);
            return this;
        }

        public override void OnDamage()
        {
            base.OnDamage();
            if (Type == "Veteran")
            {
                Berserk = HitPointsPercent() <= 0.3;
            }
        }
    }
}
