using System;
using System.Collections.Generic;
using System.Linq;

using SDL;

namespace Octopus
{
	public enum KeyCode
	{
		None, 
		Backspace, 
		Delete, 
		Tab, 
		Clear, 
		Return, 
		Pause, 
		Escape, 
		Space, 
		Keypad0, Keypad1, Keypad2, Keypad3, Keypad4, Keypad5, Keypad6, Keypad7, Keypad8, Keypad9, 
		KeypadPeriod, KeypadDivide, KeypadMultiply, KeypadMinus, KeypadPlus, KeypadEnter, KeypadEquals, 
		UpArrow, DownArrow, RightArrow, LeftArrow, 
		Insert, Home, End, PageUp, PageDown, 
		F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, 
		Alpha0, Alpha1, Alpha2, Alpha3, Alpha4, Alpha5, Alpha6, Alpha7, Alpha8, Alpha9, 
		Exclaim, 
		DoubleQuote, 
		Hash, 
		Dollar, 
		Ampersand, 
		Quote, 
		LeftParen, 
		RightParen, 
		Asterisk, 
		Plus, 
		Comma, 
		Minus, 
		Period, 
		Slash, 
		Colon, 
		Semicolon, 
		Less, 
		Equals, 
		Greater, 
		Question, 
		At, 
		LeftBracket, 
		Backslash, 
		RightBracket, 
		Caret, 
		Underscore, 
		BackQuote, 

		A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
	}

	public class Input
	{
		static bool[] keys;
		static bool[] oldKeys;

		public Input(SDL.System system)
		{
			var maxScancodeValue = (int)Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>().Max();
			keys = new bool[maxScancodeValue];
			oldKeys = new bool[maxScancodeValue];

			SDLToOctopusKey = new Dictionary<SDL.KeyCode, KeyCode> {
				{ SDL.KeyCode.Escape, KeyCode.Escape }, 
				{ SDL.KeyCode.Left, KeyCode.LeftArrow }, 
				{ SDL.KeyCode.Right, KeyCode.RightArrow }, 
				{ SDL.KeyCode.Down, KeyCode.DownArrow }, 
				{ SDL.KeyCode.Up, KeyCode.UpArrow }, 
				{ SDL.KeyCode.a, KeyCode.A }, 
			};

			system.OnKeyboard += OnKeyboard;
		}

		public static void Update()
		{
			for (int i = 0; i < keys.Length; i++)
			{
				oldKeys[i] = keys[i];
			}
		}

		public static bool GetKey(KeyCode key)
		{
			return keys[(int)key];
		}

		public static bool GetKeyDown(KeyCode key)
		{
			return keys[(int)key] && !oldKeys[(int)key];
		}

		public static bool GetKeyUp(KeyCode key)
		{
			return !keys[(int)key] && oldKeys[(int)key];
		}

		static void OnKeyboard(SDL.KeyboardEvent e)
		{
			keys[(int)SDLToOctopusKey[e.KeyCode]] = e.Type == SDL.Event.EventType.KeyDown;
		}

		static Dictionary<SDL.KeyCode, KeyCode> SDLToOctopusKey;
	}
}