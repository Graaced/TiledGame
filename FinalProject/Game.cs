using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;

namespace FinalProject
{
    static class Game
    {
        public static InsideScene InsideScene;
        public static PlayScene PlayScene;
        public static TitleScene TitleScene;
        public static GameOverScene GameOverScene;

        private static AudioDevice playerEar;
        private static AudioSource bgAudio;
        private static AudioClip bgAudioClip;


        public static Window Window;
        public static Scene CurrentScene { get; set; }
        public static float DeltaTime { get { return Window.DeltaTime; } }
        public static float UnitSize { get; private set; }
        public static float OptimalScreenHeight { get; private set; }
        public static float OptimalUnitSize { get; private set; }
        public static Vector2 ScreenCenter { get; private set; }
        public static float HalfDiagonalSquared { get { return ScreenCenter.LengthSquared; } }

        public static bool PickUpKey;

        public static void Init()
        {
            Window = new Window(640, 640, "FinalProject");  
            Window.SetVSync(true);
            Window.SetDefaultViewportOrthographicSize(40); 

            OptimalScreenHeight = 640;
            UnitSize = Window.Height / Window.OrthoHeight;
            OptimalUnitSize = OptimalScreenHeight / Window.OrthoHeight;

            ScreenCenter = new Vector2(Window.OrthoWidth * 0.5f, Window.OrthoHeight * 0.5f);
            Console.WriteLine(ScreenCenter);

            //BACKGROUND AUDIO
            //playerEar = new AudioDevice();
            //bgAudio = new AudioSource();
            //bgAudio.Volume = 1.0f;
            //bgAudioClip = AudioMngr.AddClip("background", "Assets/MUSIC/adventure.wav");

            // SCENES
            TitleScene = new TitleScene();
            PlayScene = new PlayScene();
            InsideScene = new InsideScene();
            GameOverScene = new GameOverScene();

        }

        public static float PixelsToUnits(float pixelsSize)
        {
            return pixelsSize / OptimalUnitSize;
        }       

        public static void Play()
        {
           
            SceneManager.Start();

            while (Window.IsOpened)
            {
                
                Window.SetTitle($"FPS: {1f / Window.DeltaTime}");

                
                if (Window.GetKey(KeyCode.Esc))
                {
                    break;
                }
                

                if (Window.MouseRight)
                {
                    Console.Clear();

                    Node node = PlayScene.Map.GetNode((int)Window.MousePosition.X, (int)Window.MousePosition.Y);

                    Console.WriteLine("Position: " + node.Position);
                    Console.WriteLine("\nIndex: " + node.Index);
                    Console.WriteLine("\nCost: " + node.Cost);

                    for (int i = 0; i < node.Neighbours.Count; i++)
                    {
                        Console.WriteLine($"Neghtbors {i}:");
                        Console.WriteLine("Index: " + node.Neighbours[i].Index);
                        Console.WriteLine("Cost: " + node.Neighbours[i].Cost);

                        Console.WriteLine();
                    }
                }

                // INPUT
                SceneManager.CurrentScene.Input();

                // UPDATE
                //bgAudio.Stream(bgAudioClip, DeltaTime);

                SceneManager.Update();
                SceneManager.CurrentScene.Update();


                // DRAW
                SceneManager.CurrentScene.Draw();

                Window.Update();
            }
        }
    }
}
