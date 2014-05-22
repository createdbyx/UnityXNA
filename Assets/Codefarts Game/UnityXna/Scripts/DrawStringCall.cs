namespace Codefarts.UnityXna
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class DrawStringCall
    {
        private SpriteFont font;
        private string value;
        private Vector2 position;
        private Vector4 color;
        private float rotation;
        private Vector2 origin;
        private Vector2 scale = Vector2.One;

        public Vector2 Origin
        {
            get
            {
                return this.origin;
            }
        }

        public Vector2 Scale
        {
            get
            {
                return this.scale;
            }
        }

        public float Rotation
        {
            get
            {
                return this.rotation;
            }
        }

        public SpriteFont Font
        {
            get
            {
                return this.font;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
        }

        public Vector4 Color
        {
            get
            {
                return this.color;
            }
        }

        public DrawStringCall(SpriteFont font, string value, Vector2 position, Vector4 color)
        {
            this.font = font;
            this.value = value;
            this.position = position;
            this.color = color;
        }

        public DrawStringCall(SpriteFont spriteFont, string value, Vector2 position, Vector4 color, float rotation, Vector2 origin, Vector2 scale)
        {
            this.font = spriteFont;
            this.value = value;
            this.position = position;
            this.color = color;
            this.rotation = rotation;
            this.origin = origin;
            this.scale = scale;
        }
    }
}