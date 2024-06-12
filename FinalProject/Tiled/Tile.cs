using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Tile : GameObject
    {
        public Tile(string textureName = "", DrawLayer layer = DrawLayer.Playground) : base(textureName, layer)
        {
            
            
            RigidBody = new RigidBody(this);
            RigidBody.Type = RigidBodyType.Tile;
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            IsActive = true;


            DebugMngr.AddItem(RigidBody.Collider);
        }
    }
}
