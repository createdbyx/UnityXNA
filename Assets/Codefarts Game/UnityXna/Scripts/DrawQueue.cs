namespace Microsoft.Xna.Framework
{
    using System.Collections.Generic;

    using Codefarts.UnityXna;

    public class DrawQueue
    {
        List<DrawSpriteCall> spriteQueue = new List<DrawSpriteCall>();
        DrawSpriteCall[] lastSpriteQueue = new DrawSpriteCall[0];

        List<DrawStringCall> stringQueue = new List<DrawStringCall>();
        DrawStringCall[] lastStringQueue = new DrawStringCall[0];

        internal DrawSpriteCall[] LastSpriteQueue
        {
            get { return this.lastSpriteQueue; }
        }

        internal DrawStringCall[] LastStringQueue
        {
            get { return this.lastStringQueue; }
        }

        public DrawQueue()
        {
        }

        public void Clear()
        {
            this.lastSpriteQueue = new DrawSpriteCall[this.spriteQueue.Count];
            this.spriteQueue.CopyTo(this.lastSpriteQueue);
            this.spriteQueue.Clear();

            this.lastStringQueue = new DrawStringCall[this.stringQueue.Count];
            this.stringQueue.CopyTo(this.lastStringQueue);
            this.stringQueue.Clear();
        }
        internal void EnqueueSprite(DrawSpriteCall drawSpriteCall)
        {
            this.spriteQueue.Add(drawSpriteCall);
        }

        internal void EnqueueString(DrawStringCall drawStringQueue)
        {
            this.stringQueue.Add(drawStringQueue);
        }
    }

}