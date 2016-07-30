using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

namespace Octopus
{
	public class GameObject
	{
		List<Component> components = new List<Component>();

		public string Name { get; set; }

		public GameObject Parent { get; set; }

		public GameObject(string name)
		{
			Name = name;
		}

		public void AddComponent(Component component)
		{
			components.Add(component);
		}

		public T GetComponent<T>() where T : Component
		{
			return components.Find((component) => { return component is T; }) as T;
		}

		public void Update()
		{
			foreach (var component in components)
				component.Update();
		}

		public void Draw()
		{
			foreach (var component in components)
				component.Draw();
		}

		public void OnGUI()
		{
			foreach (var component in components)
				component.OnGUI();
		}
	}
}

