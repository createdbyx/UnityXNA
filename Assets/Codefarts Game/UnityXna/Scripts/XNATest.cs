namespace Codefarts.UnityXna
{
    using System;

    using WindowsGSM1;

    using Microsoft.Xna.Framework;

    using Platformer;

    using UnityEngine;

    /// <summary>
    /// The xna test.
    /// </summary>
    public class XNATest : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The update interval.
        /// </summary>
        public float updateInterval = 0.5F;

        /// <summary>
        /// The accum.
        /// </summary>
        private float accum = 0; // FPS accumulated over the interval

        /// <summary>
        /// The draw queue.
        /// </summary>
        private DrawQueue drawQueue;

        /// <summary>
        /// The fps.
        /// </summary>
        private float fps;

        /// <summary>
        /// The frames.
        /// </summary>
        private int frames = 0; // Frames drawn over the interval

        /// <summary>
        /// The game.
        /// </summary>
        private Game game;

        /// <summary>
        /// The timeleft.
        /// </summary>
        private float timeleft; // Left time for current interval

        public bool showFps;

        public enum GameTest
        {
            Platformer,
            GameScreenManager
        }

        public GameTest GameToTest = GameTest.Platformer;

        #endregion

        #region Methods

        /// <summary>
        /// OnGUI is called for rendering and handling GUI events.
        /// </summary>
        private void OnGUI()
        {
            // Draw sprites from SpriteBatch.Draw()
            for (var i = 0; i < this.drawQueue.LastSpriteQueue.Length; i++)
            {
                var call = this.drawQueue.LastSpriteQueue[i];
                var x = call.Position.X;
                var y = call.Position.Y;
                x -= call.Origin.X;
                y -= call.Origin.Y;
                float width = call.Texture2D.UnityTexture.width;
                float height = call.Texture2D.UnityTexture.height;
                GUI.color = new Color(call.Color.X, call.Color.Y, call.Color.Z, call.Color.W);

                var sourceRect = new Rect(0, 0, 1, 1);

                if (call.Source != null)
                {
                    sourceRect.x = call.Source.Value.X / width;
                    sourceRect.y = call.Source.Value.Y / height;
                    sourceRect.width = call.Source.Value.Width / width;
                    sourceRect.height = call.Source.Value.Height / height;
                }

                if (call.SpriteEffects == Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally)
                {
                    sourceRect.x = 1 - sourceRect.x;
                    sourceRect.width *= -1;
                }
                else if (call.SpriteEffects == Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipVertically)
                {
                    sourceRect.y = 1 - sourceRect.y;
                    sourceRect.height *= -1;
                }

                GUI.DrawTextureWithTexCoords(
                    new Rect(x, y, width * Mathf.Abs(sourceRect.width), height * Mathf.Abs(sourceRect.height)),
                    call.Texture2D.UnityTexture,
                    sourceRect);
            }

            // Draw strings from SpriteBatch.DrawString()
            for (var i = 0; i < this.drawQueue.LastStringQueue.Length; i++)
            {
                var call = this.drawQueue.LastStringQueue[i];

                GUI.color = new Color(call.Color.X, call.Color.Y, call.Color.Z, call.Color.W);

                var style = GUI.skin.label;
                //  style.font = new Font(call.Font.FontName);
                style.fontSize = (int)call.Font.Size;
                var size = call.Font.MeasureString(call.Value); // style.CalcSize(new GUIContent(call.Value));

                var matrix = GUI.matrix;
                //  GUI.matrix = Matrix4x4.TRS(call.Origin, UnityEngine.Quaternion.Euler(0, 0, call.Rotation), new UnityEngine.Vector3(call.Scale.X, call.Scale.Y, 1));
                GUI.matrix = Matrix4x4.TRS(UnityEngine.Vector3.zero, UnityEngine.Quaternion.Euler(0, 0, call.Rotation), new UnityEngine.Vector3(call.Scale.X, call.Scale.Y, 1));

                size.X += (size.X * call.Scale.X) - size.X;
                size.Y = call.Font.LineSpacing * call.Scale.Y;// (size.Y * call.Scale.Y) - size.Y;
                var left = call.Position.X - call.Origin.X;
                var top = call.Position.Y - call.Origin.Y;
                GUI.Label(new Rect(left, top, size.X, size.Y), call.Value, style);

                GUI.matrix = matrix;
            }

            if (this.showFps)
            {
                GUI.Label(new Rect((Screen.width / 2) - 16, 10, 32, 20), this.fps.ToString());
            }
        }

        /// <summary>
        /// Start is called just before any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            Application.targetFrameRate = 60;

            // Add an audio source and tell the media player to use it for playing sounds
            Microsoft.Xna.Framework.Media.MediaPlayer.AudioSource = this.gameObject.AddComponent<AudioSource>();

            this.drawQueue = new DrawQueue();
            switch (this.GameToTest)
            {
                case GameTest.Platformer:
                    this.game = new PlatformerGame();
                    break;

                case GameTest.GameScreenManager:
                    this.game = new GameStateManagementGame();
                    break;
            }

            this.game.DrawQueue = this.drawQueue;
            this.game.Begin();
            this.timeleft = this.updateInterval;

            // Application.targetFrameRate = 30;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            var deltaTime = Time.deltaTime;
            if (deltaTime > 0.050f)
            {
                deltaTime = 0.050f;
            }

            // Debug.Log(deltaTime);
            this.game.Tick(deltaTime);
            this.drawQueue.Clear();

            this.timeleft -= Time.deltaTime;
            this.accum += Time.timeScale / Time.deltaTime;
            this.frames++;

            // Interval ended - update GUI text and start new interval
            if (this.timeleft <= 0.0)
            {
                // display two fractional digits (f2 format)
                this.fps = this.accum / this.frames;

                // DebugConsole.Log(format,level);
                this.timeleft = this.updateInterval;
                this.accum = 0.0F;
                this.frames = 0;
            }
        }

        #endregion
    }
}