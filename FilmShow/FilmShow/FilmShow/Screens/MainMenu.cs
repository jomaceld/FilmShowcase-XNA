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

        public MainMenu(GameController gc)
        {
			gameController = gc;
        }

        public void Initialize()
        {
			spriteBatch = new SpriteBatch(gameController.GraphicsDevice);
			defaultFont = gameController.Content.Load<SpriteFont>("Font1");
        }

        public void UnloadContent()
        {
			spriteBatch.Dispose();
        }



        bool resetThumbLeftXControls = true;
        bool resetThumbLeftYControls = true;
        float deadAreaThumbLeft = 0.2f;
        double elapsedTimeThumbLeft = 0;
        private void updateGamepadSticks(GameTime gameTime)
        {
            if (resetThumbLeftXControls && resetThumbLeftYControls)
            {
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > deadAreaThumbLeft || 
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -deadAreaThumbLeft) 
                {
                    resetThumbLeftXControls = false;
                    resetThumbLeftYControls = false;
                    //changeNextCard();

                }else if(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -deadAreaThumbLeft || 
                         GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > deadAreaThumbLeft)
                {
                    resetThumbLeftXControls = false;
                    resetThumbLeftYControls = false;
                    //changePreviousCard();
                }
            }
            else {

                elapsedTimeThumbLeft +=  gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedTimeThumbLeft >= 200)
                {
                    resetThumbLeftXControls = true;
                    resetThumbLeftYControls = true;
                    elapsedTimeThumbLeft = 0;
                }

                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < deadAreaThumbLeft &&
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > -deadAreaThumbLeft)
                {
                    resetThumbLeftXControls = true;
                    
                } if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < deadAreaThumbLeft && 
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > -deadAreaThumbLeft)
                {
                    resetThumbLeftYControls = true;
                    
                }

                if (resetThumbLeftXControls && resetThumbLeftYControls)
                {
                    elapsedTimeThumbLeft = 0;
                }
            }
        }

        bool resetKeyboardXControls = true;
        bool resetKeyboardYControls = true;
        double elapsedTimeKeyboardLeft = 0;
        private void updateKeyboardInput(GameTime gameTime)
        {
            if (resetKeyboardXControls && resetKeyboardYControls)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right) ||
                    Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    resetKeyboardXControls = false;
                    resetKeyboardYControls = false;
                   // changeNextCard();

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left) ||
                    Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    resetKeyboardXControls = false;
                    resetKeyboardYControls = false;
                    //changePreviousCard();
                }
            } else {
                elapsedTimeKeyboardLeft += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedTimeKeyboardLeft >= 200)
                {
                    resetKeyboardXControls = true;
                    resetKeyboardYControls = true;
                    elapsedTimeKeyboardLeft = 0;
                }

                if (Keyboard.GetState().IsKeyUp(Keys.Right) &&
                    Keyboard.GetState().IsKeyUp(Keys.Down))
                {
                    resetKeyboardXControls = true;

                } if (Keyboard.GetState().IsKeyUp(Keys.Left) &&
                    Keyboard.GetState().IsKeyUp(Keys.Up))
                {
                    resetKeyboardYControls = true;
                }

                if (resetKeyboardXControls && resetKeyboardYControls)
                {
                    elapsedTimeKeyboardLeft = 0;
                }
            }
        }



		public void Update(GameTime gameTime)
        {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				gameController.Exit();

            updateGamepadSticks(gameTime);
            updateKeyboardInput(gameTime);
        }


        public void Draw(GameTime gameTime)
        {
            gameController.GraphicsDevice.Clear(Color.DarkBlue);

            spriteBatch.Begin();
			spriteBatch.DrawString(defaultFont, "Main Menu", 
									new Vector2(gameController._WIDTH / 2, 
									gameController._HEIGHT / 2), Color.Black);
            spriteBatch.End();
        }

        
    }
}
