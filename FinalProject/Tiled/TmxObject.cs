using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace FinalProject
{
    class TmxObject : GameObject
    {
        int id;

        int xOff, yOff;


        public TmxObject(int id, int offsetX, int offsetY, int w, int h, bool solid) : base("map", DrawLayer.Middleground, w, h) 
        {
            sprite.pivot = new Vector2(0.0f, 0.0f);
            this.id = id;
            xOff = offsetX;
            yOff = offsetY;

            if(solid)
            {
                RigidBody = new RigidBody(this);
                RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
                RigidBody.Collider.Offset = new Vector2(HalfWidth, HalfHeight);
                RigidBody.Type = RigidBodyType.Tile;
            }

            DebugMngr.AddItem(RigidBody.Collider);
            IsActive = true;
        }

        public override void Draw()
        {
            if(IsActive)
            {
                sprite.DrawTexture(texture, xOff, yOff, 16, 16);
            }
        }
    }
}
