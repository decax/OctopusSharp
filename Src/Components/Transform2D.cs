using System;
using System.Drawing;

namespace Octopus
{
public class Transform2D : Component
	{
		Vector2 position = new Vector2();
		public Vector2 Position { 
			get {
				return position;
			}

			set {
				position = value;
				mustUpdateWorldPosition = true;
			}
		}


		public float Rotation { get; set; }
		public Vector2 Scale { get; set; } = new Vector2(1, 1);

		bool mustUpdateWorldPosition = true;
		Vector2 worldPosition = new Vector2();
		public Vector2 WorldPosition {
			get {
				if (mustUpdateWorldPosition) {
					UpdateWorldPosition();
				}
				return worldPosition;
			}

			set {
				worldPosition = value;
			}
		}

		Transform2D GetParentTransform2D()
		{
			var parent = gameObject.Parent;
			while (parent != null) {
				var transform2D = parent.GetComponent<Transform2D>();
				if (transform2D != null) {
					return transform2D;
				}

				parent = parent.Parent;
			}

			return null;
		}

		void UpdateWorldPosition()
		{
			WorldPosition = Position;

			var parent = GetParentTransform2D();
			if (parent != null) {
				WorldPosition += parent.WorldPosition;
			}

			mustUpdateWorldPosition = false;
		}

		public Transform2D(GameObject gameObject)
			: base(gameObject)
		{
		}
	}
}

