using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FinalProject
{
    class TmxObjectLayer
    {
        TmxObject[] objects;

        public TmxObjectLayer(XmlNode objectLayerNode, TmxTileset tileset)
        {
            XmlNodeList objectsNodes = objectLayerNode.SelectNodes("object");

            objects = new TmxObject[objectsNodes.Count];

            for (int i = 0; i < objectsNodes.Count; i++)
            {
                string objName = TmxMap.GetStringAttribute(objectsNodes[i], "name");
                int objId = TmxMap.GetIntAttribute(objectsNodes[i], "gid");
                int objX = TmxMap.GetIntAttribute(objectsNodes[i], "x");
                int objY = TmxMap.GetIntAttribute(objectsNodes[i], "y");
                int objW = TmxMap.GetIntAttribute(objectsNodes[i], "width");
                int objH = TmxMap.GetIntAttribute(objectsNodes[i], "height");

                int objXOff = tileset.GetAtIndex(objId).X;
                int objYOff = tileset.GetAtIndex(objId).Y;

                XmlNode propertyNode = objectsNodes[i].SelectSingleNode("properties/property");
                bool solid = TmxMap.GetBoolAttribute(propertyNode, "value");              

                objects[i] = new TmxObject(objId, objXOff, objYOff, objW, objH, solid);
                objects[i].Position = new OpenTK.Vector2(objX, objY);
            }
        }
    }
}
