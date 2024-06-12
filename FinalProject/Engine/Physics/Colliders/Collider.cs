using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace FinalProject
{
    abstract class Collider
    {
        public Vector2 Offset;
        public Vector2 Position { get { return RigidBody.Position + Offset; } }
        public RigidBody RigidBody;

        public Collider(RigidBody owner)
        {
            RigidBody = owner;
            Offset = Vector2.Zero;
        }

        public abstract bool Collides(Collider collider, ref Collision collisionInfo);
        public abstract bool Collides(BoxCollider collider, ref Collision collisionInfo);
        public abstract bool Collides(CircleCollider collider, ref Collision collisionInfo);
        public abstract bool Collides(CompoundCollider compound, ref Collision collisionInfo);
        public abstract bool Contains(Vector2 point);
    }
}
