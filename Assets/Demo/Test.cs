using UnityEngine;
using DiscordPresence;

namespace Gazzotto
{
	public class Test : MonoBehaviour
	{
        public void Click()
        {
            PresenceManager.UpdatePresence(null, start: DiscordTime.TimeNow());
        }
    }
}
