/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
    class GraphicsDeviceManager
    {
        private Game game;

        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return game.GraphicsDevice;
            }
        }

        public GraphicsDeviceManager(Game game)
        {
            // TODO: Complete member initialization
            this.game = game;
        }
    }
}
          */

using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
    using Codefarts.Localization;

    using UnityEngine;

    public class GraphicsDeviceManager : IDisposable //, IGraphicsDeviceService, IGraphicsDeviceManager
    {
        private Game game;
      //  private GraphicsDevice device;
        private int backBufferHeight;
      //  private bool useResizedBackBuffer;
        private bool isDeviceDirty;
        private int backBufferWidth;
        private DepthFormat depthStencilFormat;
        private bool inDeviceTransition;
        private DisplayOrientation currentWindowOrientation;
        private bool isReallyFullScreen;




        public DepthFormat PreferredDepthStencilFormat
        {
            get
            {
                return this.depthStencilFormat;
            }
            set
            {
                this.depthStencilFormat = value;
                this.isDeviceDirty = true;
            }
        }

        public GraphicsDeviceManager(Game game)
        {
            this.game = game;
            //if (game.Services.GetService(typeof(IGraphicsDeviceManager)) != null)
            //{
            //    throw new ArgumentException(Resources.GraphicsDeviceManagerAlreadyPresent);
            //}
            //game.Services.AddService(typeof(IGraphicsDeviceManager), this);
            //game.Services.AddService(typeof(IGraphicsDeviceService), this);

             this.CreateDevice();
        }

        #region Implementation of IGraphicsDeviceService

        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;

        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return this.game.GraphicsDevice;//this.device;
            }
        }

        public int PreferredBackBufferHeight
        {
            get
            {
                return this.backBufferHeight;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", LocalizationManager.Instance.Get("XNA_BackBufferDimMustBePositive"));
                }
                this.backBufferHeight = value;
             //   this.useResizedBackBuffer = false;
                this.isDeviceDirty = true;
            }
        }

        public int PreferredBackBufferWidth
        {
            get
            {
                return this.backBufferWidth;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", LocalizationManager.Instance.Get("XNA_BackBufferDimMustBePositive"));
                }
                this.backBufferWidth = value;
             //   this.useResizedBackBuffer = false;
                this.isDeviceDirty = true;
            }
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IGraphicsDeviceManager

        public bool BeginDraw()
        {
            return true;
        }

        public void CreateDevice()
        {
           // var pp = new PresentationParameters();
          //  pp.BackBufferWidth = 800;
           // pp.BackBufferHeight = 480;
           // this.device = new GraphicsDevice(new GraphicsAdapter(), GraphicsProfile.Reach, pp);
           // this.device=new GraphicsDevice();
            this.OnDeviceCreated(this, EventArgs.Empty);
        }

        public void EndDraw()
        {
        }

        #endregion

        public void ApplyChanges()
        {
            //if ((this.device == null) || this.isDeviceDirty)
                if (this.isDeviceDirty)
            {
               // this.ChangeDevice(false);
                Screen.SetResolution(this.PreferredBackBufferWidth, this.PreferredBackBufferHeight, this.IsFullScreen);
                this.backBufferWidth = Screen.width;
                this.backBufferHeight = Screen.height;
                this.isDeviceDirty = false;   
            }
        }

        protected virtual void OnDeviceCreated(object sender, EventArgs args)
        {
            if (this.DeviceCreated != null)
            {
                this.DeviceCreated(sender, args);
            }
        }

        public bool IsFullScreen
        {
            get
            {
                return UnityEngine.Screen.fullScreen;
            }

            set
            {
                UnityEngine.Screen.fullScreen = value;
                this.isDeviceDirty = true;
            }
        }

        public void ToggleFullScreen()
        {
            this.IsFullScreen = !this.IsFullScreen;
            //  this.ChangeDevice(false);
        }

        //protected virtual bool CanResetDevice(GraphicsDeviceInformation newDeviceInfo)
        //{
        //    if (this.device.GraphicsProfile != newDeviceInfo.GraphicsProfile)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        protected virtual void OnDeviceReset(object sender, EventArgs args)
        {
            if (this.DeviceReset != null)
            {
                this.DeviceReset(sender, args);
            }
        }



        /*   private void ChangeDevice(bool forceCreate)
  {
     if (this.game == null)
      {
          throw new InvalidOperationException(Resources.GraphicsComponentNotAttachedToGame);
      }
            

      this.inDeviceTransition = true;
      string screenDeviceName = this.game.Window.ScreenDeviceName;
      int width = this.game.Window.ClientBounds.Width;
      int height = this.game.Window.ClientBounds.Height;
      bool flag = false;
      try
      {
          //this.game.Window.SetSupportedOrientations(Helpers.ChooseOrientation(this.supportedOrientations, this.PreferredBackBufferWidth, this.PreferredBackBufferHeight, true));
          GraphicsDeviceInformation graphicsDeviceInformation = this.FindBestDevice(forceCreate);
          //this.game.Window.BeginScreenDeviceChange(graphicsDeviceInformation.PresentationParameters.IsFullScreen);
          flag = true;
          bool flag2 = true;
          if (!forceCreate && (this.device != null))
          {
              //    this.OnPreparingDeviceSettings(this, new PreparingDeviceSettingsEventArgs(graphicsDeviceInformation));
              if (this.CanResetDevice(graphicsDeviceInformation))
              {
                  try
                  {
                      GraphicsDeviceInformation information2 = graphicsDeviceInformation.Clone();
                      //            this.MassagePresentParameters(graphicsDeviceInformation.PresentationParameters);
                      //            this.ValidateGraphicsDeviceInformation(graphicsDeviceInformation);
                      this.device.Reset(information2.PresentationParameters, information2.Adapter);
                      //            ConfigureTouchInput(information2.PresentationParameters);
                      flag2 = false;
                  }
                  catch
                  {
                  }
              }
          }
          if (flag2)
          {
              this.CreateDevice();// (graphicsDeviceInformation);
          }
          PresentationParameters presentationParameters = this.device.PresentationParameters;
          screenDeviceName = this.device.Adapter.DeviceName;
          this.isReallyFullScreen = presentationParameters.IsFullScreen;
          if (presentationParameters.BackBufferWidth != 0)
          {
              width = presentationParameters.BackBufferWidth;
          }
          if (presentationParameters.BackBufferHeight != 0)
          {
              height = presentationParameters.BackBufferHeight;
          }
          this.isDeviceDirty = false;
      }
      finally
      {
          if (flag)
          {
              //  this.game.Window.EndScreenDeviceChange(screenDeviceName, width, height);
              this.OnDeviceReset(this, EventArgs.Empty);
          }
          this.currentWindowOrientation = this.game.Window.CurrentOrientation;
          this.inDeviceTransition = false;
      }   
  }      

  protected virtual GraphicsDeviceInformation FindBestDevice(bool anySuitableDevice)
  {
      var dev = new GraphicsDeviceInformation();
      dev.PresentationParameters.BackBufferWidth = this.PreferredBackBufferWidth;
      dev.PresentationParameters.BackBufferHeight = this.PreferredBackBufferHeight;
      return dev;
      //  return this.FindBestPlatformDevice(anySuitableDevice);
  }  
         * */
    }
}
