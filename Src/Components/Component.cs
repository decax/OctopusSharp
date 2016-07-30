using System;

namespace Octopus
{
	public class Component
	{
		public GameObject gameObject;

		public Component(GameObject gameObject)
		{
			this.gameObject = gameObject;
		}

		public virtual void Update() {}
		public virtual void Draw() {}
		public virtual void OnGUI() {}
	}
}

