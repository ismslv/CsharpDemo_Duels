using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    public class EquipmentManager
    {
        public static List<Equipment> Items;
        public static void Add(Equipment item)
        {
            if (Items == null) Items = new List<Equipment>();
            if (Has(item.Name)) return; //Already in library
            Items.Add(item);
        }

        public static bool Has(string name)
        {
            if (Items == null) throw new Exception("Empty equipment collection or asked before initialized");
            return Items.Where(i => i.Name == name).FirstOrDefault() != null;
        }
        public static Equipment GetItem(string name)
        {
            if (Items == null) throw new Exception("Empty equipment collection or asked before initialized");
            return Items.Where(i => i.Name == name).FirstOrDefault().Instantiate() as Equipment;
        }

        public static T GetItem<T>(string name) where T : Equipment
        {
            return GetItem(name) as T;
        }
    }
}
