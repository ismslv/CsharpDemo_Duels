using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    public class Warrior
    {
        protected string Type;
        protected double hitPointsDiff = 0;
        protected double hitPointsTotal = 0;
        protected List<(Equipment, DamageModifier)> Equipped;
        protected int HandsTotal;
        protected int HandsFree;
        protected DamageModifier ModifierTotal;
        protected bool ToDrop = true;

        public Warrior(string type, int hitPointsQ, int hands = 2)
        {
            Type = type;
            hitPointsTotal = hitPointsQ;
            Equipped = new List<(Equipment, DamageModifier)>();
            HandsFree = HandsTotal = hands;
        }
        public void Engage(Warrior enemy, intI times = null) {
            if (times == null) times = new intI("∞");
            var count = new Counter(times);
            bool goon = true;
            while (goon)
            {
                this.Blow(enemy);
                if (enemy.HitPoints() > 0)
                    enemy.Blow(this);
                Console.WriteLine(this.HitPoints() + " x " + enemy.HitPoints());
                goon = count.Add();
                if (count.Max.IsInfinity)
                    goon = !(this.HitPoints() == 0 || enemy.HitPoints() == 0);
            }
        }
        public void Blow (Warrior enemy)
        {
            foreach (var w in WeaponsEquipped())
            {
                AttackWith(enemy, (Weapon)w.Item1, w.Item2);
            }
        }
        public void AttackWith(Warrior enemy, Weapon weapon, DamageModifier modifier)
        {
            intI damage = new intI("0");
            if (weapon.Chance.Current())
            {
                if (weapon.Damage.IsInfinity)
                {
                    damage.IsInfinity = true;
                } else
                {
                    damage.Val = weapon.Damage.Val;
                    if (modifier != null)
                    {
                        modifier.Apply(ref damage.Val);
                    }
                    if (ModifierTotal != null)
                    {
                        ModifierTotal.Apply(ref damage.Val);
                    }
                }
                //Check for armor reducing damage
                foreach (var a in ArmorEquipped())
                {
                    var armor = (Armor)a.Item1;
                    if (armor.Attack.Val > 0 || armor.Attack.IsInfinity)
                    {
                        if (armor.Attack.IsInfinity)
                        {
                            damage.IsInfinity = false;
                            damage.Val = 0;
                        } else
                        {
                            damage.Val -= armor.Attack.Val;
                            //No difference if infinite attack
                        }
                    }
                }
                enemy.ProtectFrom(weapon.Name, damage);
            }
            weapon.Chance.Next();
        }
        public void ProtectFrom(string weaponName, intI damage)
        {
            Console.WriteLine("Protect from " + weaponName + " , damageBefore = " + damage.ToString());
            var armors = ArmorEquipped();
            for (int i = armors.Length - 1; i >= 0; i--)
            {
                var armor = (Armor)armors[i].Item1;
                if (armor.Chance.Current())
                {
                    if (armor.Protection.IsInfinity)
                    {
                        damage.Val = 0;
                        damage.IsInfinity = false;
                        //What if it's an Unstoppable Weapon against an Unbeatable Armor?
                    } else
                    {
                        damage.Val -= armor.Protection.Val;
                    }
                    if (armor.Destroy != null)
                    {
                        if (armor.Destroy.ContainsKey(weaponName))
                        {
                            armor.Destroy[weaponName].Add();
                            if (armor.Destroy[weaponName].IsTopped())
                            {
                                Unequip(armors[i]);
                            }
                        }
                    }
                }
                armor.Chance.Next();
                if (damage.Val <= 0) break;
            }
            if (damage.IsInfinity)
            {
                hitPointsDiff = hitPointsTotal;
            } else
            {
                if (damage.Val < 0) damage.Val = 0;
                hitPointsDiff += damage.Val;
                OnDamage();
            }
            Console.WriteLine("Protect from " + weaponName + " , damageAfter = " + damage.ToString());
        }

        public virtual void OnDamage() { }
        public void Equip(string equipment, DamageModifier modifier = null)
        {
            if (Type == "Vicious") modifier = new DamageModifier(20, 1, new intI(2));

            var eq = EquipmentManager.GetItem(equipment);
            if (eq == null) throw new Exception("Inexistent equipment");
            if (ToDrop)
            {
                while (HandsFree - eq.Hands < 0)
                {
                    Unequip(0);
                }
            }
            if (HandsFree - eq.Hands >= 0)
            {
                Equipped.Add((eq, modifier));
                HandsFree -= eq.Hands;
            } else
            {
                Console.Write("Cannot equip " + eq.Name + ". Grow more hands!");
            }
        }
        public void Unequip(int num)
        {
            var eq = Equipped[num];
            Console.WriteLine("Unequipped " + eq.Item1.Name);
            Equipped.RemoveAt(num);
            HandsFree += eq.Item1.Hands;
            if (HandsFree > HandsTotal) HandsFree = HandsTotal;
        }
        public void Unequip((Equipment, DamageModifier) item)
        {
            HandsFree += item.Item1.Hands;
            if (HandsFree > HandsTotal) HandsFree = HandsTotal;
            Equipped.Remove(item);
        }
        public void Unequip(string equipment)
        {
            var eq = GetEquipped(equipment);
            if (eq != (null, null))
            {
                Equipped.Remove(eq);
                HandsFree += eq.Item1.Hands;
                if (HandsFree > HandsTotal) HandsFree = HandsTotal;
            }
        }
        public void SetModifierTotal(DamageModifier modifier)
        {
            ModifierTotal = modifier;
        }
        public void AddModifier(DamageModifier modifier, string name = "")
        {
            for (int i = 0; i < Equipped.Count; i++)
            {
                if ((name != "" && Equipped[i].Item1.Name == name) || name == "")
                {
                    Equipped[i] = (Equipped[i].Item1, modifier);
                }
            }
        }
        public (Equipment, DamageModifier) GetEquipped(string name)
        {
            return Equipped.FirstOrDefault(e => e.Item1.Name == name);
        }
        public (Equipment, DamageModifier)[] WeaponsEquipped()
        {
            return Equipped.Where(e => e.Item1.Type == "weapon").ToArray();
        }
        public (Equipment, DamageModifier)[] ArmorEquipped()
        {
            return Equipped.Where(e => e.Item1.Type == "armor").ToArray();
        }
        public double HitPoints()
        {
            var hit = hitPointsTotal - hitPointsDiff;
            if (hit < 0) hit = 0;
            return hit;
        }

        public double HitPointsPercent()
        {
            var hit = HitPoints();
            return hit / hitPointsTotal;
        }
    }
}
