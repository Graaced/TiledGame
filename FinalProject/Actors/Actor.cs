using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    abstract class Actor : GameObject
    {
        protected int energy;
        protected float maxSpeed;
        
        protected Animation animation;       
        
        public Agent Agent { get; set; }
        public bool IsAlive { get { return energy > 0; } }
        public int MaxEnergy { get; protected set; }
        public virtual int Energy { get => energy; set { energy = MathHelper.Clamp(value, 0, MaxEnergy); } }
        


        public Actor(string texturePath, float w = 0, float h = 0) : base(texturePath, w:w ,h:h)
        {           
            MaxEnergy = 100;
        }

        

        public override void OnCollide(Collision collisionInfo)
        {           
            OnWallCollides(collisionInfo);
        }

        protected virtual void OnWallCollides(Collision collisionInfo)
        {
            if(collisionInfo.Delta.X < collisionInfo.Delta.Y)
            {
                
                if(X < collisionInfo.Collider.X)
                {
                    
                    collisionInfo.Delta.X = -collisionInfo.Delta.X;
                }

                X += collisionInfo.Delta.X;
                RigidBody.Velocity.X = 0.0f;
            }
            else
            {
               
                if (Y < collisionInfo.Collider.Y)
                {
                    
                    collisionInfo.Delta.Y = -collisionInfo.Delta.Y;
                    RigidBody.Velocity.Y = 0.0f;
                }
                else
                {
                    
                    RigidBody.Velocity.Y = -RigidBody.Velocity.Y * 0.8f;
                }

                Y += collisionInfo.Delta.Y;
            }
        }

        public virtual void AddDamage(int dmg)
        {
            Energy -= dmg;

            if (Energy <= 0)
            {
                OnDie();
            }
        }

        public virtual void AddEnergy(int amount)
        {
            Energy = Math.Min(Energy + amount, MaxEnergy);
        }

        public abstract void OnDie();

        public virtual void Reset()
        {
            Energy = MaxEnergy;
        }

        public override void Update()
        {
            if(IsActive)
            {
                if (IsActive && RigidBody.Velocity != Vector2.Zero)
                {
                    Forward = RigidBody.Velocity;
                }

                
            }
        }


    }
}

