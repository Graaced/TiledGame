using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace FinalProject
{
    class Agent
    {
        public int X { get { return Convert.ToInt32(owner.Position.X); } }
        public int Y { get { return Convert.ToInt32(owner.Position.Y); } }
        public Node Target { get { return target; } set { target = value; } }

        Node current;
        Node target;
        List<Node> path;

        Actor owner;
        Sprite pathSpr;

        public Agent(Actor owner)
        {
            this.owner = owner;
            target = null;

            pathSpr = new Sprite(0.25f, 0.25f);
            pathSpr.pivot = new Vector2(0.5f * pathSpr.Width);
        }

        public virtual void SetPath(List<Node> newPath)
        {
            path = newPath;
           
            if(target == null && path.Count > 0)
            {
                target = path[0];
                path.RemoveAt(0);
            }
            
            else if(path.Count > 0)
            {              
                int dist = Math.Abs(path[0].X - target.X) + Math.Abs(path[0].Y - target.Y);
               
                if(dist > 1)
                {
                    path.Insert(0, current);
                }
            }
        }

        public void ResetPath()
        {
            if(path != null)
            {
                path.Clear();
            }

            target = null;
        }

        public Node GetLastNode()
        {
            if(path.Count > 0)
            {
                return path.Last();
            }

            return null;
        }

        public virtual void Update(float speed)
        {
            if(target != null)
            {
                Vector2 destination = new Vector2(target.X + 0.5f, target.Y + 0.5f);
                Vector2 direction = (destination - owner.Position);
                float distance = direction.Length;

                if(distance < 0.05f)
                {
                    current = target;
                    owner.Position = destination;

                    if(path.Count == 0)
                    {
                        target = null;                        
                    }
                    else
                    {
                        target = path[0];
                        path.RemoveAt(0);
                    }
                }
                else
                {
                    
                    owner.Position += direction.Normalized() * speed * Game.DeltaTime;                   

                    if (direction != Vector2.Zero)
                    {
                        ((Player)owner).animationTimer += Game.Window.DeltaTime;
                        if (((Player)owner).animationTimer >= ((Player)owner).animationDuration)
                        {
                            ((Player)owner).animationTimer = 0;
                            ((Player)owner).ChangeFrame();
                        }

                        if (direction.X > 0)
                        {
                            ((Player)owner).ChangeTexture(AnimationDir.Right);
                        }
                        if (direction.X < 0)
                        {
                            ((Player)owner).ChangeTexture(AnimationDir.Left);
                        }
                        if (direction.Y > 0)
                        {
                            ((Player)owner).ChangeTexture(AnimationDir.Down);
                        }
                        if (direction.Y < 0)
                        {
                            ((Player)owner).ChangeTexture(AnimationDir.Up);
                        }
                    }
                }
            }
        }
      
    }
}
