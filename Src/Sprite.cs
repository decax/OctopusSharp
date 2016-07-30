using System;
using System.Drawing;

using SDL;

namespace Octopus
{
	public class Sprite
	{
		Renderer renderer;
		Texture texture;

		public Sprite(Renderer renderer)
		{
			this.renderer = renderer;
		}

		public void Load(string filename)
		{
			texture = SDL.Image.LoadTexture(renderer, filename);
		}

		public void Draw(Point position)
		{
			renderer.Copy(texture, position);
		}

	}
}

