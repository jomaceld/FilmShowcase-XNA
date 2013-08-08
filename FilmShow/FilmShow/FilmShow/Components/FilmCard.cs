using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FilmShow
{
    class FilmCard
    {
        Vector2 position;
        RectangleOverlay backgroundRectangle;
        float colorAlpha;
        bool animateFadeIn = false;
        Vector2 offset;
        Game game;
        String name = "";

        public FilmCard(int width, int height, Game g, Vector2 o)
        {
            colorAlpha = 0;
            game = g;
            position = new Vector2(0, 0);
            offset = o;
            backgroundRectangle = new RectangleOverlay(new Rectangle((int)o.X + 5, (int)o.Y + 5, width - (int)o.X - 10, height - 10), Color.PaleGreen, game);
        }

        public void fadeIn()
        {
            colorAlpha = 0;
            animateFadeIn = true;
        }

        public FilmCard(float posX, float posY)
        {
            position = new Vector2(posX, posY);
        }

        public void Update(GameTime gameTime)
        {
            if (animateFadeIn)
            {
                if (colorAlpha < 255)
                    colorAlpha += (float) (1 / gameTime.ElapsedGameTime.TotalMilliseconds) *100;
                else {
                    colorAlpha = 255;
                    animateFadeIn = false;
                }

                backgroundRectangle.setColorAlpha(colorAlpha);
            }

        }

        public void setName(String n)
        { 
            name = n;
        }

        public void Draw(SpriteBatch sprite , GameTime gameTime)
        {
            SpriteFont Font1 = game.Content.Load<SpriteFont>("Font1");
            backgroundRectangle.Draw(gameTime); 
            sprite.DrawString(Font1, name, new Vector2(offset.X + 10, 10), Color.Red);
            
            if (animateFadeIn)
            {
                sprite.DrawString(Font1, "Fade In : " + colorAlpha, new Vector2(offset.X + 10, offset.Y + 100), Color.Red);
            }
        }
    }
}
