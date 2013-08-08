using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FilmShow
{
    class SideList
    {
        RectangleOverlay backgroundRectangle;
        Vector2 offset;
        Game game;
        List<ListItem> items;
        int width;
        int height;
        int selectedItem = 0;
        int numOfItems = 0;
        int min = 0, max = 0;

        public SideList(Vector2 o, int w, int h, Game g)
        {
            width = w;
            height = h;
            offset = o;
            game = g;
            numOfItems = (int)((height -10)/ Variables.listItemHeight );
            backgroundRectangle = new RectangleOverlay(new Rectangle((int)offset.X + 5, (int)offset.Y + 5, width -10, numOfItems * Variables.listItemHeight), Color.Black, g);
            items = new List<ListItem>();
        }

        public void setListItems(List<FilmData> i)
        {
            int count = 0;
            items.Clear();
            foreach(FilmData f in i)
            {
                items.Add(new ListItem(new Vector2(offset.X + 1, (int)offset.Y + 5 + (Variables.listItemHeight * count)), width - 2, 30, game, f.name));
                count++;
            }

            selectedItem = 0;
        }

        public void selectItem(int s)
        {
            items[selectedItem].setSelected(false);
            selectedItem = s;
            items[selectedItem].setSelected(true);

            max = items.Count;
            min = Math.Max(0, selectedItem - (numOfItems / 2));
            if (items.Count > numOfItems)
            {
                if (max > numOfItems + (selectedItem - (numOfItems / 2)))
                {
                    if ((selectedItem - (numOfItems / 2)) < 0)
                        max = numOfItems;
                    else
                        max = numOfItems + (selectedItem - (numOfItems / 2));

                    if (max > items.Count)
                        max = items.Count;
                }
            }
        }


        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch sprite, GameTime gameTime)
        {
            backgroundRectangle.Draw(gameTime);

            int count = 0;
            for (int i = min; i < max; i++)
            {
                items[i].setOffset(new Rectangle((int)offset.X + 1, (int)offset.Y + 5 + (Variables.listItemHeight * count),width-2,Variables.listItemHeight));
                items[i].Draw(sprite, gameTime);
                count++;
            }
        }

    }
}
