using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OpenTK;

namespace FinalProject
{
    internal class InsideScene : Scene
    {
        protected List<Player> players;       
        TmxMap mapTmx;
        public Map Map;
        public Key key;

        public List<Player> Players { get { return players; } }


        public InsideScene() : base()
        {
            
        }

        public override void Start()
        {
            LoadAssets();

            // Map Initialization
            mapTmx = new TmxMap("Assets/Map/InsideMap7.tmx"); 
            LoadTiledMap();

            players = new List<Player>();
            Player player = new Player();
            players.Add(player);
            player.Position = new OpenTK.Vector2(7.5f, 36.5f);

            key = new Key();

            base.Start();
        }

        private static void LoadAssets()
        {

            //PLAYER
            GfxMngr.AddTexture("player_idle_d", "Assets/SPRITES/Hero/HEROS_Idle_D.png");
            GfxMngr.AddTexture("player_walk_d", "Assets/SPRITES/Hero/HEROS_Walk_D.png");
            GfxMngr.AddTexture("player_walk_r", "Assets/SPRITES/Hero/HEROS_Walk_R.png");
            GfxMngr.AddTexture("player_walk_u", "Assets/SPRITES/Hero/HEROS_Walk_U.png");

            // MAP
            GfxMngr.AddTexture("map", "Assets/Map/PixelPackTOPDOWN8BIT.png");

            //ITEMS

            GfxMngr.AddTexture("key", "Assets/SPRITES/Key/01.png");

        }

        private void LoadTiledMap()
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(@".\Assets\Map\InsideMap7.tmx");
            }
            catch (XmlException e)
            {
                Console.WriteLine("XML Exception: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception: " + e.Message);
            }

            XmlNode mapNode = xmlDoc.SelectSingleNode("map");
            XmlNodeList layerList = mapNode.SelectNodes("layer");
            XmlNode layerNode;
            XmlNode dataNode;

            for (int i = 0; i < layerList.Count; i++)
            {
                string layerName = GetStringAttribute(layerList[i], "name");

                if (layerName == "layer_obj")
                {
                    layerNode = layerList[i];
                    dataNode = layerNode.SelectSingleNode("data");

                    string csvData = dataNode.InnerText;
                    csvData = csvData.Replace("\r\n", "").Replace("\n", "").Replace(" ", "");

                    string[] Ids = csvData.Split(',');
                    int[] ints = new int[Ids.Length];

                    for (int j = 0; j < Ids.Length; j++)
                    {
                        ints[j] = int.Parse(Ids[j]);
                    }

                    int[] cells = new int[Ids.Length];

                    for (int h = 0; h < ints.Length; h++)
                    {
                        if (ints[h] > 0)
                        {
                            cells[h] = int.MaxValue;
                        }
                        else
                        {
                            cells[h] = 0;
                        }
                    }

                    int width = int.Parse(layerNode.Attributes.GetNamedItem("width").Value);
                    int height = int.Parse(layerNode.Attributes.GetNamedItem("height").Value);

                    Map = new Map(width, height, cells);
                }
            }
        }


        public int GetIntAttribute(XmlNode node, string attrName)
        {
            return int.Parse(GetStringAttribute(node, attrName));
        }

        public bool GetBoolAttribute(XmlNode node, string attrName)
        {
            return bool.Parse(GetStringAttribute(node, attrName));
        }

        public string GetStringAttribute(XmlNode node, string attrName)
        {
            return node.Attributes.GetNamedItem(attrName).Value;
        }

        public override void Input()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].IsAlive)
                {
                    players[i].Input();
                }
            }
        }

        public override void Update()
        {
            PhysicsMngr.Update();
            UpdateMngr.Update();          
            PhysicsMngr.CheckCollisions();
        }

        public override Scene OnExit()
        {
            UpdateMngr.ClearAll();
            PhysicsMngr.ClearAll();
            DrawMngr.ClearAll();
            GfxMngr.ClearAll();
            DebugMngr.ClearAll();

            return base.OnExit();
        }

        public override void Draw()
        {
            DrawMngr.Draw();          
        }


    }
}
