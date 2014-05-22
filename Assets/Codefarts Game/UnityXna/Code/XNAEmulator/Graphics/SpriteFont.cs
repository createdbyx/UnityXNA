using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Xna.Framework.Graphics
{
    using UnityEngine;

    using Vector2 = Microsoft.Xna.Framework.Vector2;

    public class SpriteFont : IDisposable
    {
        #region Fields
        private string fontName;
        private float size;
        private float spacing;
        private bool useKerning;
        private string style;
        private int lineSpacing;
        #endregion

        #region Properties
        public int LineSpacing
        {
            get
            {
                return this.lineSpacing;
            }
            set
            {
                this.lineSpacing = value;
            }
        }
        
        public string FontName
        {
            get
            {
                return this.fontName;
            }
        }

        public float Size
        {
            get
            {
                return this.size;
            }
        }
        
        public float Spacing
        {
            get
            {
                return this.spacing;
            }
        }

        public string Style
        {
            get
            {
                return this.style;
            }
        }

        public bool UseKerning
        {
            get
            {
                return this.useKerning;
            }
        } 
        #endregion

        public SpriteFont(string fontName, float size, float spacing, bool useKerning, string style)
        {
            this.fontName = fontName;
            this.size = size;
            this.spacing = spacing;
            this.useKerning = useKerning;
            this.style = style;
            this.lineSpacing = (int)this.MeasureString("jT").Y;
        }

        internal Vector2 MeasureString(string text)
        {
            var skin = ScriptableObject.CreateInstance<GUISkin>();
            skin.label.fontSize = (int)this.Size;
            var size = skin.label.CalcSize(new GUIContent(text));     
            return new Vector2(size.x, size.y);
        }

        public void Dispose()
        { }
    }
}
