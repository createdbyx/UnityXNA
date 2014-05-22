namespace Microsoft.Xna.Framework
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using UnityEditor;
#if UNITY3D
    using UnityEngine;
#endif

    public class Game : IDisposable
    {
        private GameComponentCollection _components;
        Content.ContentManager content;
        GraphicsDevice graphicsDevice;
        DrawQueue drawQueue;
        long totalTicks = 0;
        private List<IUpdateable> updateableComponents = new List<IUpdateable>();

        private List<IUpdateable> currentlyUpdatingComponents = new List<IUpdateable>();

        private List<IDrawable> drawableComponents = new List<IDrawable>();

        private List<IDrawable> currentlyDrawingComponents = new List<IDrawable>();

        private List<IGameComponent> notYetInitialized = new List<IGameComponent>();
        private bool inRun;

#if UNITY3D
        private GameObject gameObject;

#endif

        public void ResetElapsedTime()
        {
            // TODO: needs implementation
        }

        public DrawQueue DrawQueue
        {
            get
            {
                return this.drawQueue;
            }
            set
            {
                drawQueue = value;
            }
        }

        /// <summary>
        /// Returns a default value of true, but if running out of browser will return MainWindow.IsActive property.
        /// </summary>
        public virtual bool IsActive
        {
            get
            {
#if UNITY3D
                var component = this.gameObject.GetComponent<XnaGameBehavior>();
                return component.IsFocused;
#else
                throw new NotImplementedException();
#endif
            }

            internal set
            {
                //this.isActive = value;
            }
        }

        public ContentManager Content
        {
            get
            {
                return this.content;
            }

            set
            {
                this.content = value;
            }
        }

        public GraphicsDevice GraphicsDevice
        {
            get
            {
                if (graphicsDevice == null)
                {
                    graphicsDevice = new GraphicsDevice(DrawQueue);
                }

                return graphicsDevice;
            }
        }

        public Game()
        {
#if UNITY3D
            this.gameObject = new GameObject("Codefart.Super_Secret_XNA_Game_Object, no delete meh plez");
            var behavior = this.gameObject.AddComponent<XnaGameBehavior>();
            behavior.IsFocused = true;
#endif
            content = new ContentManager(null, "");

            _components = new GameComponentCollection();
            this._components.ComponentAdded += new EventHandler<GameComponentCollectionEventArgs>(this.GameComponentAdded);
            this._components.ComponentRemoved += new EventHandler<GameComponentCollectionEventArgs>(this.GameComponentRemoved);
        }

        protected virtual void Update(GameTime gameTime)
        {
            //  Logger.BeginLogEvent(LoggingEvent.Update, "");
            for (int i = 0; i < this.updateableComponents.Count; i++)
            {
                this.currentlyUpdatingComponents.Add(this.updateableComponents[i]);
            }
            for (int j = 0; j < this.currentlyUpdatingComponents.Count; j++)
            {
                IUpdateable item = this.currentlyUpdatingComponents[j];
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            this.currentlyUpdatingComponents.Clear();
            //  FrameworkDispatcher.Update();
            //  this.doneFirstUpdate = true;
            //  Logger.EndLogEvent(LoggingEvent.Update, "");
        }

        public GameComponentCollection Components
        {
            get
            {
                return this._components;
            }
        }

        protected virtual void Initialize()
        {
            //  this.HookDeviceEvents();
            while (this.notYetInitialized.Count != 0)
            {
                this.notYetInitialized[0].Initialize();
                this.notYetInitialized.RemoveAt(0);
            }

            // if (this.graphicsDeviceService != null && this.graphicsDeviceService.GraphicsDevice != null)
            {
                this.LoadContent();
            }
        }

        private void UpdateableUpdateOrderChanged(object sender, EventArgs e)
        {
            IUpdateable updateable = sender as IUpdateable;
            this.updateableComponents.Remove(updateable);
            int num = this.updateableComponents.BinarySearch(updateable, UpdateOrderComparer.Default);
            if (num < 0)
            {
                num = ~num;
                while (num < this.updateableComponents.Count && this.updateableComponents[num].UpdateOrder == updateable.UpdateOrder)
                {
                    num++;
                }
                this.updateableComponents.Insert(num, updateable);
            }
        }

        private void DrawableDrawOrderChanged(object sender, EventArgs e)
        {
            IDrawable drawable = sender as IDrawable;
            this.drawableComponents.Remove(drawable);
            int num = this.drawableComponents.BinarySearch(drawable, DrawOrderComparer.Default);
            if (num < 0)
            {
                num = ~num;
                while (num < this.drawableComponents.Count && this.drawableComponents[num].DrawOrder == drawable.DrawOrder)
                {
                    num++;
                }
                this.drawableComponents.Insert(num, drawable);
            }
        }

        private void GameComponentAdded(object sender, GameComponentCollectionEventArgs e)
        {
            if (!this.inRun)
            {
                this.notYetInitialized.Add(e.GameComponent);
            }
            else
            {
                e.GameComponent.Initialize();
            }

            IUpdateable gameComponent = e.GameComponent as IUpdateable;
            if (gameComponent != null)
            {
                int num = this.updateableComponents.BinarySearch(gameComponent, UpdateOrderComparer.Default);
                if (num < 0)
                {
                    num = ~num;
                    while (num < this.updateableComponents.Count && this.updateableComponents[num].UpdateOrder == gameComponent.UpdateOrder)
                    {
                        num++;
                    }
                    this.updateableComponents.Insert(num, gameComponent);
                    gameComponent.UpdateOrderChanged += new EventHandler<EventArgs>(this.UpdateableUpdateOrderChanged);
                }
            }
            IDrawable drawable = e.GameComponent as IDrawable;
            if (drawable != null)
            {
                int num1 = this.drawableComponents.BinarySearch(drawable, DrawOrderComparer.Default);
                if (num1 < 0)
                {
                    num1 = ~num1;
                    while (num1 < this.drawableComponents.Count && this.drawableComponents[num1].DrawOrder == drawable.DrawOrder)
                    {
                        num1++;
                    }
                    this.drawableComponents.Insert(num1, drawable);
                    drawable.DrawOrderChanged += new EventHandler<EventArgs>(this.DrawableDrawOrderChanged);
                }
            }
        }

        private void GameComponentRemoved(object sender, GameComponentCollectionEventArgs e)
        {
            if (!this.inRun)
            {
                this.notYetInitialized.Remove(e.GameComponent);
            }

            IUpdateable gameComponent = e.GameComponent as IUpdateable;
            if (gameComponent != null)
            {
                this.updateableComponents.Remove(gameComponent);
                gameComponent.UpdateOrderChanged -= new EventHandler<EventArgs>(this.UpdateableUpdateOrderChanged);
            }
            IDrawable drawable = e.GameComponent as IDrawable;
            if (drawable != null)
            {
                this.drawableComponents.Remove(drawable);
                drawable.DrawOrderChanged -= new EventHandler<EventArgs>(this.DrawableDrawOrderChanged);
            }
        }

        protected virtual void Draw(GameTime gameTime)
        {
            for (int i = 0; i < this.drawableComponents.Count; i++)
            {
                this.currentlyDrawingComponents.Add(this.drawableComponents[i]);
            }
            for (int j = 0; j < this.currentlyDrawingComponents.Count; j++)
            {
                IDrawable item = this.currentlyDrawingComponents[j];
                if (item.Visible)
                {
                    item.Draw(gameTime);
                }
            }

            this.currentlyDrawingComponents.Clear();
        }

        protected virtual void LoadContent()
        {
        }

        public void Exit()
        {
            this.inRun = false;

#if UNITY3D
            Application.Quit();
#endif

#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPaused = false;
            }
#endif
        }

        public GameWindow Window
        {
            get
            {
                // TODO
                return new UnityGameWindow();
            }
        }

        public GameServiceContainer Services
        {
            get
            {
                // TODO
                return null;
            }
        }

        internal void Run()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void Begin()
        {
            this.Initialize();
            this.inRun = true;
            LoadContent();
            // XNA's first update call has a zero elapsed time, so do one now.
            GameTime gameTime = new GameTime(new TimeSpan(0), new TimeSpan(0), new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0));
            Update(gameTime);
        }

        internal void Tick(float deltaTime)
        {
            long microseconds = (int)(deltaTime * 1000000);
            long ticks = microseconds * 10;
            totalTicks += ticks;
            GameTime gameTime = new GameTime(new TimeSpan(0), new TimeSpan(0), new TimeSpan(totalTicks), new TimeSpan(ticks));
            Update(gameTime);
            var device = this.graphicsDevice;
            if (device != null)
            {
                var camera = Camera.current ?? Camera.main;
                device.Viewport = new Viewport((int)camera.pixelRect.x, (int)camera.pixelRect.y, (int)camera.pixelWidth, (int)camera.pixelHeight);
            }
            Draw(gameTime);
        }
    }
}
