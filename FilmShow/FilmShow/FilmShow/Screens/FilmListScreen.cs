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
        List<FilmCard> cards;
        int cardIndex = 0;
        List<FilmData> items;
        int selectedFilm = 0;
		//FilmCard actualCard;
		GameController gameController;

        public FilmList(GameController gc)
        {
			gameController = gc;
        }

		public void createCard(FilmData data)
		{
			FilmCard fc = new FilmCard(new Vector2(gameController._WIDTH * 0.2f + 10, 0 + 10), new Vector2(gameController._WIDTH - (gameController._WIDTH * 0.2f) - 20, gameController._HEIGHT - 20), gameController);
			fc.setFilmData(data);
			cards.Add(fc);
			cardIndex = cards.Count -1;
			
		}

        public void Initialize()
        {
			cards = new List<FilmCard>();

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
			createCard(items[index]);
            filmList.selectItem(index);
        }

        public void changeNextCard()
        {
            if (selectedFilm + 1 < items.Count)
            {
				cards[cardIndex].animate(FilmCard.AnimationType.MoveOutToTop);
                selectedFilm++;
                showCard(selectedFilm);
				cards[cardIndex].animate(FilmCard.AnimationType.MoveInFromBottom);
            }
        }

        public void changePreviousCard()
        {
            if (selectedFilm > 0)
            {
				//cards[cardIndex].resetAnimation();
				cards[cardIndex].animate(FilmCard.AnimationType.MoveOutToBottom);
                selectedFilm--;
                showCard(selectedFilm);
				cards[cardIndex].animate(FilmCard.AnimationType.MoveInFromTop);
            }
        }

        float deadAreaThumbLeft = 0.2f;
		private void updateGamepadSticks(GameTime gameTime, InputHelper input)
        {
          
        }


        double elapsedTimeKeyboardLeft = 0;
        private void updateKeyboardInput(GameTime gameTime,InputHelper input)
        {

			if (input.IsNewPress(Keys.Right))
			{
				changeNextCard();

				elapsedTimeKeyboardLeft = 0;
			}
			else if (input.IsCurPress(Keys.Right))
			{ 
				elapsedTimeKeyboardLeft += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedTimeKeyboardLeft >= 100)
                {
                    elapsedTimeKeyboardLeft = 0;
					changeNextCard();
                }
			}
			if (input.IsNewPress(Keys.Left))
			{
				changePreviousCard();
				elapsedTimeKeyboardLeft = 0;
			}
			else if (input.IsCurPress(Keys.Left))
			{
				elapsedTimeKeyboardLeft += gameTime.ElapsedGameTime.TotalMilliseconds;
				if (elapsedTimeKeyboardLeft >= 100)
				{
					elapsedTimeKeyboardLeft = 0;
					changePreviousCard();
				}

			}
        }

		public void Update(GameTime gameTime, InputHelper input)
        {
			if (input.IsNewPress(Buttons.Back) || input.IsNewPress(Keys.Escape))
				gameController.changeToScreen(GameController.Screens.MainMenu);

            updateGamepadSticks(gameTime, input);
            updateKeyboardInput(gameTime, input);

			foreach (FilmCard c in cards)
			{
				if (c.delete)
					cards.Remove(c);	
				else
					c.Update(gameTime);
			}
        }


        public void Draw(GameTime gameTime)
        {
            gameController.GraphicsDevice.Clear(Color.DarkBlue);

            spriteBatch.Begin();

			foreach (FilmCard c in cards)
			{
				c.Draw(spriteBatch, gameTime);
			}

            if (filmList != null)
                filmList.Draw(spriteBatch, gameTime); 
            spriteBatch.End();
        }

        
    }
}
