namespace Microsoft.Xna.Framework.Graphics
{
    using Microsoft.Xna.Framework;

    using UnityEngine;

    using Vector4 = Microsoft.Xna.Framework.Vector4;

    public class GraphicsDevice
    {
        Viewport viewport = new Viewport();
        private DrawQueue drawQueue;

        public DrawQueue DrawQueue
        {
            get { return drawQueue; }
            set { drawQueue = value; }
        }

        public GraphicsDevice(DrawQueue drawQueue)
        {
            // TODO: Complete member initialization
            this.drawQueue = drawQueue;

            var camera = Camera.current ?? Camera.main;
            viewport = new Viewport((int)camera.pixelRect.x, (int)camera.pixelRect.y, (int)camera.pixelWidth, (int)camera.pixelHeight);
        }

        public Viewport Viewport
        {
            get { return viewport; }
            set { viewport = value; }
        }


        internal void Clear(Color color)
        {
            // TODO: need implementation
        }

        public void Clear(ClearOptions options, Vector4 color, float depth, int stencil)
        {
            this.Clear(options, new Color(color), depth, stencil);
        }

        public void Clear(ClearOptions options, Color color, float depth, int stencil)
        {
            // TODO: need implementation
        }
    }
}
