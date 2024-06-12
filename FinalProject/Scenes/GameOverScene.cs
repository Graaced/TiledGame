using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class GameOverScene : Scene
    {
        protected Sprite sprite;
        protected Texture texture;
        

        public GameOverScene() : base()
        {
             LoadAssets();

            texture = GfxMngr.GetTexture("theend");
            sprite = new Sprite(Game.Window.OrthoWidth, Game.Window.OrthoHeight);
        }       

        public override void Input()
        {
            if (Game.Window.GetKey(KeyCode.N))
            {
                Game.Window.Exit();
            }
            if (Game.Window.GetKey(KeyCode.Y))
            {
                IsPlaying = false;
            }
        }

        public override Scene OnExit()
        {
            texture = null;
            sprite = null;
            return base.OnExit();
        }

        public override void Update() { }

        public override void Draw()
        {
            sprite.DrawTexture(texture);
            
        }

        private void LoadAssets()
        {
            
            GfxMngr.AddTexture("theend", "Assets/SPRITES/Bg/gameOverBg.png");
        }
    }
}
