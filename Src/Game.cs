using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using SDL;

namespace Octopus
{
	public class Window
	{
		public Rectangle Rectangle { get; set; }
		Color color;

		protected SDL.Renderer renderer;

		protected Window(SDL.Renderer renderer, Rectangle rectangle, Color color)
		{
			this.renderer = renderer;
			this.Rectangle = rectangle;
			this.color = color;
		}

		public virtual void Draw()
		{
			renderer.DrawColor = color;
			renderer.FillRect(Rectangle);
		}
	}

	public class GameWindow : Window
	{
		List<GameObject> gameObjects;

		public GameWindow(SDL.Renderer renderer, Rectangle rectangle, List<GameObject> gameObjects)
			: base(renderer, rectangle, Color.CornflowerBlue)
		{
			this.gameObjects = gameObjects;
		}

		public override void Draw()
		{
			base.Draw();

			// Draw game objects
			foreach (var gameObject in gameObjects)
				gameObject.Draw();

			foreach (var gameObject in gameObjects)
				gameObject.OnGUI();
		}
	}

	public class HierarchyWindow : Window
	{
		List<GameObject> gameObjects;

		public HierarchyWindow(SDL.Renderer renderer, Rectangle rectangle, List<GameObject> gameObjects)
			: base(renderer, rectangle, Color.Gray)
		{
			this.gameObjects = gameObjects;
		}

		public override void Draw()
		{
			base.Draw();

			renderer.DrawColor = Color.White;

			var position = new Point(0, 0);
			var size = new Size(0, 100);

			foreach (var gameObject in gameObjects)
			{
				GUI.Label(new Rectangle(position, size), gameObject.Name);
				position.Y += size.Height;
			}
		}
	}


	public class Game : IDisposable
	{
		bool running = true;

		SDL.System system;
		SDL.Window window;
		public static Renderer renderer;

		static GUI gui;
		Input input;

		SDL.Image image = new SDL.Image(SDL.Image.Flags.PNG);

		GameObject rootGameObject = new GameObject("root");
		List<GameObject> gameObjects = new List<GameObject>();

#if OCTOPUS_EDITOR
		HierarchyWindow hierarchyWindow;
#endif
		GameWindow gameWindow;

		public Game()
		{
			system = new SDL.System();
			window = new SDL.Window("Pong", new Size(800, 600));
			renderer = new Renderer(window, Renderer.Type.Software);

			input = new Input(system);
			gui = new GUI(renderer);

			system.OnQuit += () => running = false;
			system.OnKeyboard += OnKeyboard;

			var rectangle = new Rectangle(0, 0, window.Size.Width, window.Size.Height);
#if OCTOPUS_EDITOR
			rectangle.Width = 100;
			hierarchyWindow = new HierarchyWindow(renderer, rectangle, gameObjects);

			rectangle.Left = 100;
			rectangle.Width = window.Size - rectangle.Width;
#endif
			gameWindow = new GameWindow(renderer, rectangle, gameObjects);
		}

		public void Dispose()
		{
			image.Dispose();

			renderer.Dispose();
			window.Dispose();
			system.Dispose();
		}

		public void AddGameObject(GameObject gameObject)
		{
			gameObjects.Add(gameObject);
			gameObject.Parent = rootGameObject;
		}

		public void Run()
		{
//			renderer.LogicalSize = new Size(200, 150);

			while (running)
			{
				Time.Update();

				system.PollEvent();

				// Update game objects
				foreach (var gameObject in gameObjects)
					gameObject.Update();

				// Draw all windows
				gameWindow.Draw();
#if OCTOPUS_EDITOR
				hierarchyWindow.Draw();
#endif

				renderer.Present();

				Input.Update();
			}
		}

		public void OnKeyboard(SDL.KeyboardEvent e)
		{
			switch (e.Scancode)
			{
			case ScanCode.Escape:
				running = false;
				break;
			}
		}
	}

}

