using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FilmShow
{
    class ListItem
    {

        RectangleOverlay backgroundRectangle;
        Vector2 offset;
        Game game;
        string name;
        public bool isSelected = false;
        int width;

        public ListItem(Vector2 o, int w, int height, Game g, string n)
        {
            name = n;
            offset = o;
            game = g;
            width = w;
            backgroundRectangle = new RectangleOverlay(new Rectangle((int)offset.X + 5, (int)offset.Y +1 , width - 10, height -1),Color.Wheat,g); 
        }

        public void setOffset(Rectangle r)
        {
            offset.X = r.X;
            offset.Y = r.Y;
            backgroundRectangle.changeRect(r);
        }

        public void setSelected(bool s)
        {
            isSelected = s;
            if (isSelected) {
                backgroundRectangle.setColor(Color.Black);
            }else
                backgroundRectangle.setColor(Color.Wheat);
        }
        

        public void Update(GameTime gameTime)
        {
        } 

        public void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            SpriteFont Font1 = game.Content.Load<SpriteFont>("Font1");
            backgroundRectangle.Draw(gameTime);
            sprite.DrawString(Font1,name, new Vector2(offset.X + 10, offset.Y +2), Color.Red);
        }
    }
}
