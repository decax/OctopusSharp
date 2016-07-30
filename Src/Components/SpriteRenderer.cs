using System;
using System.Drawing;

namespace Octopus
{
	public class SpriteRenderer : Component
	{
		Sprite sprite;
		Transform2D transform2D;

		public SpriteRenderer(GameObject gameObject, Transform2D transform2D)
			: base(gameObject)
		{
			this.transform2D = transform2D;

			sprite = new Sprite(Game.renderer);
		}

		public void Load(string filename)
		{
			sprite.Load(filename);
		}

		public override void Draw()
		{
			sprite.Draw(new Point((int)transform2D.WorldPosition.X, (int)transform2D.WorldPosition.Y));
		}
	}
}

