using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Tournament
{
    public class ReadData
    {
        public static void ReadFile(string file)
        {
            //EquipmentManager.Add(new Armor("", 0));
            foreach (var line in File.ReadLines(file))
            {
                ProcessLine(line);
            }
        }

        public static void ProcessLine(string line)
        {
            line = Regex.Replace(line, @"\s+", " ");
            var lineAll = line.Split(" "[0]);
            if (lineAll[0] == "🛡️")
            {
                //It is armor
                ParseArmor(lineAll);
            }
            if (lineAll[0] == "🗡️")
            {
                //It is one-handed weapon
                ParseWeapon(lineAll);

            }
            else
            {
                //It is something else
            }
        }

        static void ParseWeapon(string[] line)
        {
            var weapon = new Weapon(line[1], int.Parse(line[2]), line[3], line[4]);
            EquipmentManager.Add(weapon);
        }

        static void ParseArmor(string[] line)
        {
            var armor = new Armor(line[1], int.Parse(line[2]), line[3], line[4], line[5], line.Length >= 7 ? line[6] : "");
            EquipmentManager.Add(armor);
        }
    }
}
