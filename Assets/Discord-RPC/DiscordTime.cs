using System;

namespace DiscordPresence
{
	public static class DiscordTime
	{
        /// <summary>
        /// Get the current time
        /// </summary>
        /// <returns>The current time epoch</returns>
		public static long TimeNow()
        {
            TimeSpan time;
            DateTime today = DateTime.UtcNow;
            time = DateTime.UtcNow - new DateTime(today.Year, today.Month, today.Day, 0, 0, 0, DateTimeKind.Utc);
            return (long)time.TotalSeconds;
        }

        /// <summary>
        /// Add time to an epoch value
        /// </summary>
        /// <param name="time">Epoch value</param>
        /// <param name="seconds">Seconds to add</param>
        /// <param name="minutes">Minutes to add</param>
        /// <param name="hours">Hours to add</param>
        /// <returns></returns>
        public static long AddTime(ref long time, int seconds, int minutes = 0, int hours = 0)
        {
            time += seconds;
            time += minutes * 60;
            time += hours * 3600;
            return 0;
        }
	}
}
