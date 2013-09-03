using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FilmShow
{
    public class RectangleOverlay : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D dummyTexture;
        Rectangle dummyRectangle;
        Color Colori;

        public RectangleOverlay(Rectangle rect, Color colori, Game game)
            : base(game)
        {
            // Choose a high number, so we will draw on top of other components.
            DrawOrder = 1000;
            dummyRectangle = rect;
            Colori = colori;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            dummyTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });
            
        }

        public void setColorAlpha(float alpha)
        {
            //Colori = new Color(Colori.R, Colori.G, Colori.B, (int)alpha);
            Color whiteColor = new Color(254, 254, 254, (int)alpha);
            dummyTexture.SetData(new Color[] { whiteColor });
        }

		public void changeRect(Rectangle r)
		{
			dummyRectangle = r;
		}


		/*public void changeRect(Rectangle r)
		{
			dummyRectangle = r;
		}*/

		
        public void setColor(Color c)
        {
            Colori = c;
        }

        public override void Draw(GameTime gameTime)
        { 
            spriteBatch.Begin();
            spriteBatch.Draw(dummyTexture, dummyRectangle, Colori);
            spriteBatch.End();
        }
    }
}
