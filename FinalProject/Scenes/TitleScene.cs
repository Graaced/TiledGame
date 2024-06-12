using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;
using Aiv.Audio;

namespace FinalProject
{
    class TitleScene : Scene
    {
       

        protected Texture texture;       
        protected Sprite sprite;
        
        private AudioSource audioSource;
        private AudioClip audioClip;

  
        public TitleScene() : base()  
        {
            
        }

        public override void Start()
        {
            LoadAssets();

            texture = GfxMngr.GetTexture("title");
            sprite = new Sprite(Game.Window.OrthoWidth, Game.Window.OrthoHeight);

            //audioSource = new AudioSource();
            audioClip = AudioMngr.GetClip("confirm");

            base.Start();
        }

        public override void Input()
        {
            if (Game.Window.GetKey(KeyCode.Return))
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

        private void LoadAssets()
        {
            GfxMngr.AddTexture("title", "Assets/SPRITES/Bg/title2.png");
            GfxMngr.AddTexture("enter", "Assets/SPRITES/Bg/pressEnter.png");

            AudioMngr.AddClip("confirm", "Assets/MUSIC/MenuValid01.wav");
        }

        public override void Draw()
        {
            sprite.DrawTexture(texture);          
        }
    }

}
