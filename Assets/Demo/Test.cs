using UnityEngine;
using DiscordPresence;

namespace Gazzotto
{
	public class Test : MonoBehaviour
	{
        public void Click()
        {
            long timeToAdd = DiscordTime.TimeNow();
            DiscordTime.AddTime(ref timeToAdd, 30);
            PresenceManager.UpdatePresence(null, end: timeToAdd);
        }
    }
}
