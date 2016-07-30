using System;
using System.Collections.Generic;
using System.Drawing;

using SDL;

namespace Octopus
{
	public class GUI
	{
		static Renderer renderer;

		static TTF ttf;
		static GUIStyle guiStyle;

		static Dictionary<GUIStyle.FontStyle, SDL.Font.Style> octopus2sdlStyle = new Dictionary<GUIStyle.FontStyle, SDL.Font.Style>
		{
			{ GUIStyle.FontStyle.Normal,        SDL.Font.Style.Normal }, 
			{ GUIStyle.FontStyle.Bold,          SDL.Font.Style.Bold   }, 
			{ GUIStyle.FontStyle.Italic,        SDL.Font.Style.Italic }, 
			{ GUIStyle.FontStyle.BoldAndItalic, SDL.Font.Style.Bold | SDL.Font.Style.Italic }, 
		};

		public GUI(SDL.Renderer renderer)
		{
			GUI.renderer = renderer;
			Init();
		}

		static public void Init()
		{
			ttf = new TTF();

			guiStyle = new GUIStyle
			{
				fontSize = 20, 
				fontStyle = GUIStyle.FontStyle.Normal
			};
		}

		static public void Dispose()
		{
			ttf.Dispose();
		}

		public static void Label(Rectangle position, string text)
		{
			Label(position, text, guiStyle);
		}

		public static void Label(Rectangle position, string text, GUIStyle guiStyle)
		{
			using (var font = new SDL.Font("Arial.ttf", guiStyle.fontSize))
			{
				font.FontStyle = octopus2sdlStyle[guiStyle.fontStyle];

				using (var texture = new Texture(renderer, font.RenderTextSolid(text, Color.Black)))
				{
					renderer.Copy(texture, new Point(position.X, position.Y));
				}
			}
		}
	}

	public class GUIStyle
	{
		public enum FontStyle
		{
			Normal, 
			Bold, 
			Italic, 
			BoldAndItalic
		}

		public int fontSize;
		public FontStyle fontStyle;
	}
}

