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
		Vector2 size;
		Vector2 offset;
		public bool isAnimating = false;
		public bool delete = false;
		public enum AnimationType { MoveInFromBottom, MoveInFromTop, MoveOutToTop, MoveOutToBottom, None }
		AnimationType animationType = AnimationType.None;

        RectangleOverlay backgroundRectangle;
		GameController gameController;

		// Film Data
        String name = "";
		String duration = "";
		String description = "";

        public FilmCard(Vector2 pos, Vector2 size, GameController gc)
        {
			gameController = gc;
			position = pos;
			this.size = size;
            offset = Vector2.Zero;
			backgroundRectangle = new RectangleOverlay(new Rectangle((int)position.X, (int)position.Y, (int)size.X , (int)size.Y), Color.PaleGreen, gc);
        }

        public void animate(AnimationType t)
        {
			
			animationType = t;
			switch (animationType) 
			{ 
				case AnimationType.MoveInFromBottom:
					offset.Y = gameController._HEIGHT ;
					break;
				case AnimationType.MoveInFromTop:
					offset.Y = -size.Y  - 10;
					break;
			}
			isAnimating = true;
			
        }

		int animationSpeedFactor = 1000;
        public void Update(GameTime gameTime)
        {
			if(isAnimating)
			{
				if (animationType == AnimationType.MoveInFromBottom)
				{
					if (offset.Y > 0)
					{
						offset.Y -= (float)(1 / gameTime.ElapsedGameTime.TotalMilliseconds) * animationSpeedFactor;
						if (offset.Y < 0) {
							offset.Y = 0;
							isAnimating = false;
							animationType = AnimationType.None;
						}
					}
				
				} else if (animationType == AnimationType.MoveInFromTop) 
				{
					if (offset.Y < 0)
					{
						offset.Y += (float)(1 / gameTime.ElapsedGameTime.TotalMilliseconds) * animationSpeedFactor;
						if (offset.Y > 0)
						{
							offset.Y = 0;
							isAnimating = false;
							animationType = AnimationType.None;
						}
					}
				} else if (animationType == AnimationType.MoveOutToTop)
				{
					if (offset.Y > -(size.Y * 2))
					{
						offset.Y -= (float)(1 / gameTime.ElapsedGameTime.TotalMilliseconds) * animationSpeedFactor;
						if (offset.Y < -(size.Y * 2))
						{
							offset.Y = -size.Y * 2;
							isAnimating = false;
							animationType = AnimationType.None;
						}
					}
				} else if (animationType == AnimationType.MoveOutToBottom)
				{
					if (offset.Y < gameController._HEIGHT + size.Y)
					{
						offset.Y += (float)(1 / gameTime.ElapsedGameTime.TotalMilliseconds) * animationSpeedFactor;
						if (offset.Y > gameController._HEIGHT + size.Y)
						{
							offset.Y = gameController._HEIGHT + size.Y;
							isAnimating = false;
							animationType = AnimationType.None;
						}
					}
				}

				backgroundRectangle.changeRect(new Rectangle((int)(position.X + offset.X), (int)(position.Y + offset.Y), (int)size.X, (int)size.Y));
			}
        }

		public void resetAnimation()
		{
			offset = Vector2.Zero;
			isAnimating = false;
			animationType = AnimationType.None; 
		}

		public void setFilmData( FilmData data)
		{
			name = data.name;
			duration = data.lenght;
			description = data.description;
		}

        public void Draw(SpriteBatch sprite , GameTime gameTime)
        {
            SpriteFont Font1 = gameController.Content.Load<SpriteFont>("Font1");
            backgroundRectangle.Draw(gameTime);
			sprite.DrawString(Font1, name, new Vector2(position.X + offset.X + 10, position.Y + offset.Y + 10), Color.Red);
			sprite.DrawString(Font1, "Duration: " + duration, new Vector2(position.X + offset.X + 10, position.Y + offset.Y + 30), Color.Red);
			sprite.DrawString(Font1, "Descrition: " + description, new Vector2(position.X + offset.X + 10, position.Y + offset.Y + 60), Color.Red);
            if (isAnimating)
            {
				sprite.DrawString(Font1, "Move In : " + offset.Y, new Vector2(position.X + offset.X + 10, position.Y + offset.Y + 100), Color.Red);
            }
        }
    }
}
