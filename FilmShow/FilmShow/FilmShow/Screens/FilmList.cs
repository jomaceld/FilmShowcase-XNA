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
    public class FilmList : ScreenInterface
    {
        
        SpriteBatch spriteBatch;
        SideList filmList;
        FilmCard[] cards;
        private int numOfCards = 2;
        int cardIndex = 0;
        List<FilmData> items;
        int selectedFilm = 0;

		GameController gameController;

        public FilmList(GameController gc)
        {
			gameController = gc;
        }

        public void Initialize()
        {
            cards = new FilmCard[numOfCards];
            for (int i = 0; i < numOfCards; i++)
            {
				cards[i] = new FilmCard(gameController._WIDTH, gameController._HEIGHT, gameController, new Vector2(gameController._WIDTH * 0.2f, 0)); // Offset X 20% of screen
            }

			filmList = new SideList(new Vector2(0, 0), (int)(gameController._WIDTH * Variables.sideListScreenPercent / 100), gameController._HEIGHT, gameController);
            items = new List<FilmData>();
            for (int i = 0; i < 20; i++)
            {
                FilmData auxFilm = new FilmData();
                auxFilm.name = "Film " + i;
                items.Add(auxFilm);
            }
            filmList.setListItems(items);

			spriteBatch = new SpriteBatch(gameController.GraphicsDevice);
			showCard(selectedFilm);
        }

        public void UnloadContent()
        {
			spriteBatch.Dispose();  
        }

        public void showCard(int index)
        {
            cards[cardIndex].setName(items.ElementAt(index).name + " Card: " + cardIndex);
            cards[cardIndex].fadeIn();
            filmList.selectItem(index);
        }

        public void changeNextCard()
        {
            if (selectedFilm + 1 < items.Count)
            {
                // Update Index
                if (cardIndex + 1 < cards.Length)
                    cardIndex++;
                else
                    cardIndex = 0;

                selectedFilm++;
                showCard(selectedFilm);
            }
        }

        public void changePreviousCard()
        {
            if (selectedFilm > 0)
            {
                if (cardIndex > 0)
                    cardIndex--;
                else
                    cardIndex = cards.Length - 1;

                selectedFilm--;
                showCard(selectedFilm);
            }
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
                    changeNextCard();

                }else if(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -deadAreaThumbLeft || 
                         GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > deadAreaThumbLeft)
                {
                    resetThumbLeftXControls = false;
                    resetThumbLeftYControls = false;
                    changePreviousCard();
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
                    changeNextCard();

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left) ||
                    Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    resetKeyboardXControls = false;
                    resetKeyboardYControls = false;
                    changePreviousCard();
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
				gameController.changeToScreen(GameController.Screens.MainMenu);

            updateGamepadSticks(gameTime);
            updateKeyboardInput(gameTime);

            if (cards != null)
                cards.ElementAt(cardIndex).Update(gameTime);
        }


        public void Draw(GameTime gameTime)
        {
            gameController.GraphicsDevice.Clear(Color.DarkBlue);

            spriteBatch.Begin();
            if (cards != null)
                cards.ElementAt(cardIndex).Draw(spriteBatch,gameTime);
            if (filmList != null)
                filmList.Draw(spriteBatch, gameTime); 
            spriteBatch.End();
        }

        
    }
}
