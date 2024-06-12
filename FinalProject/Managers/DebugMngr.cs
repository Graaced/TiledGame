using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace FinalProject
{
    static class DebugMngr
    {
        static List<Collider> items;

        static List<Sprite> sprites;

        static DebugMngr()
        {
            items = new List<Collider>();
            sprites = new List<Sprite>();
        }

        public static void AddItem(Collider c)
        {
            items.Add(c);

            if(c is CircleCollider)
            {
                sprites.Add(new Sprite(((CircleCollider)c).Radius * 2, ((CircleCollider)c).Radius * 2));
            }
            else if (c is BoxCollider)
            {
                sprites.Add(new Sprite(((BoxCollider)c).Width, ((BoxCollider)c).Height));
            }
            else
            {
                sprites.Add(new Sprite(c.RigidBody.GameObject.HalfWidth * 2, c.RigidBody.GameObject.HalfHeight * 2));
            }
        }

        public static void RemoveItem(Collider c)
        {
            int index = items.IndexOf(c);
            sprites.RemoveAt(index);
            items.Remove(c);
        }

        public static void ClearAll()
        {
            items.Clear();
            sprites.Clear();
        }

        public static void Draw()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].RigidBody.IsActive)
                {
                    OpenTK.Vector4 col = new OpenTK.Vector4();

                    if (items[i] is CircleCollider)
                    {
                        col = new OpenTK.Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                    }
                    else
                    {
                        col = new OpenTK.Vector4(0.0f, 1.0f, 0.0f, 1.0f);
                    }

                    sprites[i].position = items[i].Position - new OpenTK.Vector2(items[i].RigidBody.GameObject.HalfWidth, items[i].RigidBody.GameObject.HalfHeight) + items[i].Offset;
                    sprites[i].DrawWireframe(col);
                }
            }
        }
    }
}
