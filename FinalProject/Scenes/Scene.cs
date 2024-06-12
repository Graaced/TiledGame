using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    abstract class Scene
    {
        public bool IsPlaying { get; protected set; }

        public Scene NextScene;


        public Scene()
        {
            SceneManager.AddScene(this);
        }

        public virtual void Start()
        {
            IsPlaying = true;
        }

        public virtual Scene OnExit()
        {
            IsPlaying = false;
            return NextScene;
            AudioMngr.ClearAll();
            DebugMngr.ClearAll();
            DrawMngr.ClearAll();
            FontMngr.ClearAll();
            GfxMngr.ClearAll();
            PhysicsMngr.ClearAll();
            UpdateMngr.ClearAll();

        }

        public abstract void Input();
        public virtual void Update()
        {

        }
        public abstract void Draw();

        protected void Quit()
        {
            if (Game.Window.GetKey(KeyCode.Esc))
            {
                Game.Window.Exit();
            }
        }
    }
}
