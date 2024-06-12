using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Font
    {
        protected int numCol;
        protected int firstVal;

        public string TextureName { get; protected set; }
        public Texture Texture { get; protected set; }
        public int CharacterWidth { get; protected set; }
        public int CharacterHeight { get; protected set; }


        public Font(string textureName, string texturePath, int numColumns, int firstCharacterASCIIvalue, int charWidth, int charHeight)
        {
            TextureName = textureName;
            Texture = GfxMngr.AddTexture(TextureName, texturePath);
            firstVal = firstCharacterASCIIvalue;
            CharacterWidth = charWidth;
            CharacterHeight = charHeight;
            numCol = numColumns;
        }

        public virtual Vector2 GetOffset(char c)
        {
            int cVal = c;//implicit conversion, from char to int
            int delta = cVal - firstVal;
            int x = delta % numCol;
            int y = delta / numCol;

            return new Vector2(x * CharacterWidth, y * CharacterHeight);
        }
    }
}
