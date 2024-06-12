using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace FinalProject
{
    struct CameraLimits
    {
        public float MaxX;
        public float MaxY;
        public float MinX;
        public float MinY;

        public CameraLimits(float maxX, float minX, float maxY, float minY)
        {
            MaxX = maxX;
            MaxY = maxY;
            MinX = minX;
            MinY = minY;
        }
    }

    static class CameraMngr
    {
        private static Dictionary<string, Tuple<Camera, float>> cameras;

        public static Camera MainCamera;

        public static GameObject Target;
        public static float CameraSpeed = 5;
        public static CameraLimits CameraLimits;

        public static float HalfDiagonalSquared { get { return MainCamera.pivot.LengthSquared; } }

        public static void Init(GameObject target, CameraLimits limits)
        {
            MainCamera = new Camera(Game.Window.OrthoWidth * 0.5f, Game.Window.OrthoHeight * 0.5f);
            MainCamera.pivot = new Vector2(Game.Window.OrthoWidth * 0.5f, Game.Window.OrthoHeight * 0.5f);
            Target = target;
            CameraLimits = limits;

            cameras = new Dictionary<string, Tuple<Camera, float>>();
        }

        public static void Update()
        {
            Vector2 oldCameraPos = MainCamera.position;
            MainCamera.position = Vector2.Lerp(MainCamera.position, Target.Position, Game.DeltaTime * CameraSpeed);
            FixPosition();

            Vector2 cameraDelta = MainCamera.position - oldCameraPos;

            if(cameraDelta != Vector2.Zero)
            {
                //camera moved

                foreach (var item in cameras)
                {
                    item.Value.Item1.position += cameraDelta * item.Value.Item2;//camera position += delta * cameraSpeed
                }
            }
        }

        public static void AddCamera(string cameraName, Camera camera = null, float cameraSpeed = 0)
        {
            if (camera == null)
            {
                camera = new Camera(MainCamera.position.X, MainCamera.position.Y);
                camera.pivot = MainCamera.pivot;
            }

            cameras[cameraName] = new Tuple<Camera, float>(camera, cameraSpeed);
        }

        public static Camera GetCamera(string cameraName)
        {
            if (cameras.ContainsKey(cameraName))
            {
                return cameras[cameraName].Item1;//return camera associated with the given cameraName
            }
            return null;
        }

        private static void FixPosition()
        {
            MainCamera.position.X = MathHelper.Clamp(MainCamera.position.X, CameraLimits.MinX, CameraLimits.MaxX);
            MainCamera.position.Y = MathHelper.Clamp(MainCamera.position.Y, CameraLimits.MinY, CameraLimits.MaxY);
        }
    }
}
