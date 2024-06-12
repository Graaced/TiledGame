using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Aiv.Fast2D;

namespace FinalProject
{
    class TmxMap : IDrawable
    {
        private string tmxFilePath;
        public DrawLayer Layer { get; }

        
        TmxTileset tileset;
        
        TmxTileLayer tileLayer;
        
        TmxTileLayer[] tileLayers;



        public TmxMap(string filePath)
        {
            
            Layer = DrawLayer.Background;
            DrawMngr.AddItem(this);

            
            tmxFilePath = filePath;

            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(tmxFilePath);
            }
            catch(XmlException e)
            {
                Console.WriteLine("XML Exception: " + e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Generic Exception: " + e.Message);
            }

            
            XmlNode mapNode = xmlDoc.SelectSingleNode("map");
            int mapCols = GetIntAttribute(mapNode, "width");
            int mapRows = GetIntAttribute(mapNode, "height");
            int mapTileW = GetIntAttribute(mapNode, "tilewidth");
            int mapTileH = GetIntAttribute(mapNode, "tileheight");

            
            XmlNode tilesetNode = mapNode.SelectSingleNode("tileset");
            int tilesetTileW = GetIntAttribute(tilesetNode, "tilewidth");
            int tilesetTileH = GetIntAttribute(tilesetNode, "tileheight");
            int spacing = 0;
            int margin = 0; 
            int tileCount = GetIntAttribute(tilesetNode, "tilecount");
            int tilesetCols = GetIntAttribute(tilesetNode, "columns");
            int tilesetRows = tileCount / tilesetCols;
            
            tileset = new TmxTileset("map", tilesetCols, tilesetRows, tilesetTileW, tilesetTileH, spacing, margin); 


            XmlNodeList layersNodes = mapNode.SelectNodes("layer");

            tileLayers = new TmxTileLayer[layersNodes.Count];

            for (int i = 0; i < layersNodes.Count; i++)
            {
                string layerName = GetStringAttribute(layersNodes[i], "name");

                if (layerName == "layer_obj")
                {
                    TmxTileObjectLayer tileObjectLayer = new TmxTileObjectLayer(layersNodes[i], tilesetNode, tileset);
                }
                else
                {
                    tileLayers[i] = new TmxTileLayer(layersNodes[i], tileset, mapCols, mapRows, mapTileW, mapTileH);
                }
            }
        }

        public static int GetIntAttribute(XmlNode node, string attrName)
        {
            return int.Parse(GetStringAttribute(node, attrName));
        }

        public static bool GetBoolAttribute(XmlNode node, string attrName)
        {
            return bool.Parse(GetStringAttribute(node, attrName));
        }

        public static string GetStringAttribute(XmlNode node, string attrName)
        {
            return node.Attributes.GetNamedItem(attrName).Value;
        }

        public void Draw()
        {
 
            for (int i = 0; i < tileLayers.Length; i++)
            {
                
                if (i == tileLayers.Length - 1)
                {
                    continue;
                }

                tileLayers[i].Draw();
            }
        }
    }
}
