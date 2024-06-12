using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using Aiv.Audio;

namespace FinalProject
{
    internal class AudioMngr
    {
        private static Dictionary<string, AudioClip> clips;

        static AudioMngr()
        {
            clips = new Dictionary<string, AudioClip>();
        }

        
        public static AudioClip AddClip(string name, string path)
        {
            AudioClip c = new AudioClip(path);

            if (c != null)
            {
                clips[name] = c;
            }

            return c;
        }

       
        public static AudioClip GetClip(string name)
        {
            AudioClip c = null;

            if (clips.ContainsKey(name))
            {
                c = clips[name];
            }

            return c;
        }

        
        public static void ClearAll()
        {
            clips.Clear();
        }
    }
}
