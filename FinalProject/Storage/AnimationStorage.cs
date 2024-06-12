using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{   
    static class AnimationStorage
    {
        public static void LoadPlayerAnimations() 
        {
            // PLAYER MOVEMENT
            GfxMngr.AddAnimation("idleD", 15, 1, 28, 16, 1, 1);            
            GfxMngr.AddAnimation("WalkD", 15, 4, 28, 16, 1, 1);
            GfxMngr.AddAnimation("WalkR", 15, 4, 28, 16, 1, 1);
            GfxMngr.AddAnimation("WalkU", 15, 4, 28, 16, 1, 1);

        }
    }
}
