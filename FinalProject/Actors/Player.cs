using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace FinalProject
{
    enum AnimationDir { Down, Right, Up, Left }

    class Player : Actor
    {                          
        public bool CanOpen { get; set; }

        protected bool clickedL;
        private int key;


        //ANIMATION
        private Texture[] movement;
        private Vector2 AnimationOffSet;
        private float textureOffset = 16;

        public int maxFrame = 4;
        public int currentFrame;
        public float animationTimer = 0;
        public float animationDuration = 0.2f;


        public Player() : base("player_idle_d", 16, 16)
        {                      
            maxSpeed = 5;           
            IsActive = true;          
            Position = new Vector2(6.5f, 6.5f);
            
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.AddCollisionType(RigidBodyType.Enemy | RigidBodyType.Tile | RigidBodyType.Player | RigidBodyType.Door);           

            // ANIMATION
            AnimationStorage.LoadPlayerAnimations();
            animation = GfxMngr.GetAnimation("idleD");           
            animation.Start();

            Agent = new Agent(this);

            movement = new Texture[4];
            movement[0] = GfxMngr.GetTexture("player_walk_d"); 
            movement[1] = GfxMngr.GetTexture("player_walk_r"); 
            movement[2] = GfxMngr.GetTexture("player_walk_u"); 
            movement[3] = GfxMngr.GetTexture("player_walk_r");

            CanOpen = false;
            clickedL = false;

            Reset();
        }

        public void Input()
        {
            if (Game.Window.MouseLeft)
            {
                if (SceneManager.CurrentScene == Game.PlayScene)
                {
                    if (!clickedL)
                    {
                        clickedL = true;
                        Vector2 mousePos = Game.Window.MousePosition;
                        List<Node> path = ((PlayScene)SceneManager.CurrentScene).Map.GetPath((int)Position.X, (int)Position.Y, (int)mousePos.X, (int)mousePos.Y);
                        Agent.SetPath(path);
                    }
                    else
                    {
                        clickedL = false;
                    }
                }
                else if (SceneManager.CurrentScene == Game.InsideScene)
                {
                    if (!clickedL)
                    {
                        clickedL = true;
                        Vector2 mousePos = Game.Window.MousePosition;
                        List<Node> path = ((InsideScene)SceneManager.CurrentScene).Map.GetPath((int)Position.X, (int)Position.Y, (int)mousePos.X, (int)mousePos.Y);
                        Agent.SetPath(path);
                    }
                    else if (clickedL)
                    {
                        clickedL = false;
                    }
                }
            }
            
            if (Game.Window.GetKey(KeyCode.E) && key == 1) 
            {
                key -= 1;
                CanOpen = true;
                
            }
            else
            {
                CanOpen = false;
            }

        }
       
        public void ChangeFrame()
        {
            currentFrame++;
            if (currentFrame >= maxFrame)
            {
                currentFrame = 0;
            }

            AnimationOffSet.X = currentFrame * textureOffset;
        }

        public void ChangeTexture(AnimationDir direction)
        {
            texture = movement[(int)direction];
            if (direction == AnimationDir.Left)
            {
                sprite.FlipX = true;
            }
            else if (direction == AnimationDir.Right)
            {
                sprite.FlipX = false;
            }
        }

        public override void Update()
        {
            Agent.Update(10);

            Vector2 doorOpenPosition = new Vector2(31.5f, 15.5f);
            Vector2 doorClosePosition = new Vector2(9.5f, 33.5f);
            Vector2 InsideDoorPosition = new Vector2(11.5f, 0.5f);

            if (SceneManager.CurrentScene == Game.PlayScene && Position == doorOpenPosition)
            {
                ((PlayScene)SceneManager.CurrentScene).NextScene = Game.InsideScene;
                SceneManager.CurrentScene.OnExit();
            }
            else if (SceneManager.CurrentScene == Game.InsideScene && Position == InsideDoorPosition)
            {
                SceneManager.currentSceneIndex = 0;
                ((InsideScene)SceneManager.CurrentScene).NextScene = Game.PlayScene;
                SceneManager.CurrentScene.OnExit();
                Position = new Vector2(31.5f, 15.5f);

            }
            else if (Game.PickUpKey && SceneManager.CurrentScene == Game.PlayScene && Position == doorClosePosition)
            {
                SceneManager.currentSceneIndex = 2;
                ((PlayScene)SceneManager.CurrentScene).NextScene = Game.GameOverScene;
                SceneManager.CurrentScene.OnExit();
            }
            
        }

        public void AddKey(int keys)
        {
            key += MathHelper.Clamp(keys, 0, 1);
        }

        public override void Draw()
        {
            if (IsActive)
            {               
                sprite.DrawTexture(texture, (int)AnimationOffSet.X, (int)AnimationOffSet.Y, 16, 16);
                
            }
        }

        public override void OnDie()
        {
           
        }

    }
}
