using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Key : GameObject
    {
        protected bool isVisible;
        protected bool isActive;
        protected int keyToAdd;

        public Key() : base("key")
        {
            sprite.position = new OpenTK.Vector2(23.5f,11.5f);
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Key;
            RigidBody.AddCollisionType(RigidBodyType.Player);

            isVisible = true;
            isActive = true;

            keyToAdd = 1;

        }

        public override void Update()
        {
            base.Update();

            if ((Position - Game.InsideScene.Players[0].Position).Length < 1f)
            {
                isActive = false;
                Game.InsideScene.Players[0].AddKey(keyToAdd);

                UpdateMngr.RemoveItem(this);
                DrawMngr.RemoveItem(this);
                Game.InsideScene.key = null;

                Game.PickUpKey = true;
            }

        }
       

        public override void Draw()
        {
            sprite.DrawTexture(texture);
        }
    }
}
