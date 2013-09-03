using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FilmShow.Screens;

namespace FilmShow
{
    public class GameController : Game
    {

        GraphicsDeviceManager graphicsDeviceMan;
        public int _WIDTH = 1360, _HEIGHT = 768;
		InputHelper inputHelper;

        public enum Screens 
        { 
            MainMenu,
            FilmList
        }

		ScreenInterface actualScreen;

        public GameController()
            : base()
        {
			
			graphicsDeviceMan = new GraphicsDeviceManager(this);
            graphicsDeviceMan.PreferredBackBufferHeight = _HEIGHT;
			graphicsDeviceMan.PreferredBackBufferWidth = _WIDTH;
			graphicsDeviceMan.IsFullScreen = true;
			inputHelper = new InputHelper();
			Content.RootDirectory = "Content";
        }

		public void changeToScreen(Screens s)
		{
			if (actualScreen != null)
				actualScreen.UnloadContent();

			switch (s)
			{ 
				case Screens.FilmList:
					actualScreen = new FilmList(this);
					break;
				default:
					actualScreen = new MainMenu(this);
					break;
			}

			if (actualScreen != null)
				actualScreen.Initialize();

		}

		protected override void Initialize()
		{
			changeToScreen(Screens.MainMenu);
			base.Initialize();
		}

		protected override void Update(GameTime gameTime)
		{
			inputHelper.Update();
			if (actualScreen != null)
			{
				actualScreen.Update(gameTime,inputHelper);
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			if (actualScreen != null)
			{
				actualScreen.Draw(gameTime);
			}

			base.Draw(gameTime);
		}

		protected override void UnloadContent()
		{ 
		
		}

    }
}
