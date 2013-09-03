using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FilmShow.Screens;

namespace FilmShow
{
    public class MainMenu : ScreenInterface
    {
        
        SpriteBatch spriteBatch;
		GameController gameController;
		SpriteFont defaultFont;

		List<GameController.Screens> menuOptions;
		int menuIndex = 0;


        public MainMenu(GameController gc)
        {
			gameController = gc;
        }

        public void Initialize()
        {
			spriteBatch = new SpriteBatch(gameController.GraphicsDevice);
			defaultFont = gameController.Content.Load<SpriteFont>("Font1");
			menuOptions = new List<GameController.Screens>();
			menuOptions.Add(GameController.Screens.FilmList);
			menuOptions.Add(GameController.Screens.MainMenu);
        }

        public void UnloadContent()
        {
			spriteBatch.Dispose();
        }


		public void Update(GameTime gameTime, InputHelper input)
        {
			if (input.IsNewPress(Buttons.Back) || input.IsNewPress(Keys.Escape))
				gameController.Exit();

			if(input.IsNewPress(Keys.Left))
			{
				menuIndex--;
				if (menuIndex < 0)
				{
					menuIndex = menuOptions.Count - 1;
				}
			}
			else if(input.IsNewPress(Keys.Right))
			{
				menuIndex ++;
				if (menuIndex >= menuOptions.Count)
				{
					menuIndex = 0;
				}
			}

			if (input.IsNewPress(Keys.Enter))
			{
				gameController.changeToScreen(menuOptions[menuIndex]);
			}

        }


        public void Draw(GameTime gameTime)
        {
            gameController.GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();
			Color c = Color.Black;

			for (int i = 0; i < menuOptions.Count; i++)
			{
				if (i == menuIndex)
					c = Color.White;
				else
					c = Color.Black;

				spriteBatch.DrawString(defaultFont, menuOptions[i].ToString(),
									new Vector2(gameController._WIDTH / 2 + (100*i),
									gameController._HEIGHT / 2), c);
			}
            
			
            spriteBatch.End();
        }

        
    }
}
