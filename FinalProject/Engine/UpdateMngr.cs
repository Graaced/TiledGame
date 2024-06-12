using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    static class UpdateMngr
    {
        private static List<IUpdatable> items;

        static UpdateMngr()
        {
            items = new List<IUpdatable>();
        }

        public static void AddItem(IUpdatable item)
        {
            items.Add(item);
        }

        public static void RemoveItem(IUpdatable item)
        {
            items.Remove(item);
        }

        public static void ClearAll()
        {
            items.Clear();
        }

        public static void Update()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Update();
            }
        }
    }
}
