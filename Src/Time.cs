using System;

namespace Octopus
{
	public class Time
	{
		public static float deltaTime;
		public static float time;

		static DateTime startTime;
		static DateTime previousTime;
		static DateTime now;

		static Time()
		{
			now = previousTime = startTime = DateTime.Now;

			deltaTime = 0.0f;
		}

		public static void Update()
		{
			previousTime = now;
			now = DateTime.Now;

			time = (float)(now - startTime).TotalSeconds;
			deltaTime = (float)(now - previousTime).TotalSeconds;
		}
	}
}