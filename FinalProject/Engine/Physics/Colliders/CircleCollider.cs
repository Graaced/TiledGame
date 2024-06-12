using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace FinalProject
{
    class CircleCollider : Collider
    {
        public float Radius;

        public CircleCollider(RigidBody owner, float radius) : base(owner)
        {
            Radius = radius;

            DebugMngr.AddItem(this);
        }

        public override bool Collides(Collider other, ref Collision collisionInfo)
        {
            return other.Collides(this, ref collisionInfo);
        }

        public override bool Collides(CircleCollider other, ref Collision collisionInfo)
        {
            Vector2 dist = other.Position - Position;
            return (dist.LengthSquared <= Math.Pow(other.Radius + Radius, 2));
        }

        public override bool Collides(BoxCollider collider, ref Collision collisionInfo)
        {
            return collider.Collides(this, ref collisionInfo);
        }

        public override bool Contains(Vector2 point)
        {
            Vector2 distaFromCenter = point - Position;
            return distaFromCenter.LengthSquared <= (Radius * Radius);
        }

        public override bool Collides(CompoundCollider other, ref Collision collisionInfo)
        {
            return other.Collides(this, ref collisionInfo);
        }
    }
}
