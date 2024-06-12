using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    enum DrawLayer { Background, Middleground, Playground, Foreground, GUI, Last}

    static class DrawMngr
    {
        private static List<IDrawable>[] items;

        static DrawMngr()
        {
            items = new List<IDrawable>[(int)DrawLayer.Last];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new List<IDrawable>();
            }
        }

        public static void AddItem(IDrawable item)
        {
            items[(int)item.Layer].Add(item);
        }

        public static void RemoveItem(IDrawable item)
        {
            items[(int)item.Layer].Remove(item);
        }

        public static void ClearAll()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Clear();
            }
        }

        public static void Draw()
        {
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 0; j < items[i].Count; j++)
                {
                    items[i][j].Draw();
                }
            }
        }
    }
}
