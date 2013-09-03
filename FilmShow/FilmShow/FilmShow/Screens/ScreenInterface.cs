using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FilmShow.Screens
{
	public interface ScreenInterface
	{
		void Initialize();
		void UnloadContent();
		void Draw(GameTime gameTime);
		void Update(GameTime gameTime,InputHelper input);


	}
}
