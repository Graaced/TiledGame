using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class BoxCollider : Collider
    {
        protected float halfWidth;
        protected float halfHeight;

        public float Width { get { return halfWidth * 2.0f; } }
        public float Height { get { return halfHeight * 2.0f; } }

        public BoxCollider(RigidBody owner, float w, float h) : base(owner)
        {
            halfWidth = w * 0.5f;
            halfHeight = h * 0.5f;

            DebugMngr.AddItem(this);
        }

        public override bool Collides(Collider collider, ref Collision collisionInfo)
        {
            return collider.Collides(this, ref collisionInfo);
        }

        public override bool Collides(CircleCollider circle, ref Collision collisionInfo)
        {
            float deltaX = circle.Position.X - Math.Max(Position.X - halfWidth,  Math.Min(circle.Position.X, Position.X + halfWidth));
            float deltaY = circle.Position.Y - Math.Max(Position.Y - halfHeight, Math.Min(circle.Position.Y, Position.Y + halfHeight));

            return (deltaX * deltaX + deltaY * deltaY) < (circle.Radius * circle.Radius);
        }

        public override bool Contains(Vector2 point)
        {
            return
                point.X >= Position.X - halfWidth &&
                point.X <= Position.X + halfWidth &&
                point.Y >= Position.Y - halfHeight &&
                point.Y <= Position.Y + halfHeight;
        }

        public override bool Collides(BoxCollider other, ref Collision collisionInfo)
        {
            Vector2 distance = other.Position - Position;

            // Horizontal distance - the sum of the halfWidths of the 2 objects
            float deltaX = Math.Abs(distance.X) - (other.halfWidth + this.halfWidth);
            // If the distance is greater (and then deltaX > 0) it means no collision
            if(deltaX > 0)
            {
                return false;
            }

            // Vertical distance - the sum of the halfHeights of the 2 objects
            float deltaY = Math.Abs(distance.Y) - (other.halfHeight + this.halfHeight);
            // If the distance is greater (and then deltaY > 0) it means no collision
            if (deltaY > 0)
            {
                return false;
            }

            // If we're here it means we've collided, so fill up collisionInfo
            collisionInfo.Type = CollisionType.RectsIntersection;   // we know it's rectTorect
            collisionInfo.Delta = new Vector2(-deltaX, -deltaY);    // to have positive values

            return true;
        }

        public override bool Collides(CompoundCollider other, ref Collision collisionInfo)
        {
            return other.Collides(this, ref collisionInfo);
        }
    }
}
