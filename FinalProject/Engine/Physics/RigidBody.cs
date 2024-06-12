using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace FinalProject
{
    enum RigidBodyType { Player = 1, Enemy = 2,  Door = 4, Key = 8, Tile = 16 }

    class RigidBody
    {
        public Vector2 Velocity;

        public GameObject GameObject;   
        public bool IsGravityAffected;
        public bool IsCollisionAffected = false;        

        public Collider Collider;

        public RigidBodyType Type;

        protected uint collisionMask;

        public bool IsActive { get { return GameObject.IsActive; } }

        public bool IsCollides { get; private set; }
        public GameObject IsCollidesWith => isCollidesWith;

        private GameObject isCollidesWith;
        public Vector2 Position { get { return GameObject.Position; } }

        public RigidBody(GameObject owner)
        {
            GameObject = owner;
            PhysicsMngr.AddItem(this);
        }

        public void Update()
        {
            if (IsGravityAffected)
            {
                Velocity.Y += PhysicsMngr.G * Game.DeltaTime;
            }
           
            GameObject.Position += Velocity * Game.DeltaTime;
        }
        

        public bool Collides(RigidBody other, ref Collision collisionInfo)
        {
            IsCollides = Collider.Collides(other.Collider, ref collisionInfo);

            if (IsCollides)
                isCollidesWith = other.GameObject;
            else
                isCollidesWith = null;

            return IsCollides;
        }

        public void AddCollisionType(RigidBodyType type)
        {
            collisionMask |= (uint)type;
        }

        public void AddCollisionType(uint type)
        {
            collisionMask |= type;
        }

        public bool CollisionTypeMatches(RigidBodyType type)
        {
            return ((uint)type & collisionMask) != 0;
        }

        public void Destroy()
        {
            GameObject = null;
            Collider = null;           
            PhysicsMngr.RemoveItem(this);
        }

    }
}
