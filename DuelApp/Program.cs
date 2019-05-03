using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament;

namespace DuelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Give me equipment data file");
            //var equipmentfile = Console.ReadLine();
            var equipmentfile = "Equipment.cfg";
            ReadData.ReadFile(equipmentfile);

            Warrior w1 = new Swordsman();
            Warrior w2 = new Viking();
            w1.AddModifier(new DamageModifier(10, 3), "sword");
            w1.SetModifierTotal(new DamageModifier(0, 2));
            w2.Equip("buckler");
            Console.WriteLine(w1.HitPoints().ToString() + " x " + w2.HitPoints().ToString());
            w1.Engage(w2);
            Console.WriteLine(w1.HitPoints().ToString() + " x " + w2.HitPoints().ToString());

            Console.ReadLine();
        }
    }
}
