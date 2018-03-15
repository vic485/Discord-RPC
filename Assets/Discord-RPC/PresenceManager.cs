using System;
using UnityEngine;
using UnityEngine.Events;

namespace DiscordPresence
{
    [Serializable]
    public class DiscordJoinEvent : UnityEvent<string> { }

    [Serializable]
    public class DiscordSpectateEvent : UnityEvent<string> { }

    [Serializable]
    public class DiscordJoinRequestEvent : UnityEvent<DiscordRpc.JoinRequest> { }

    public class PresenceManager : MonoBehaviour
    {
        public DiscordRpc.RichPresence presence = new DiscordRpc.RichPresence();
        public string applicationId;
        public string optionalSteamId;
        public int callbackCalls;
        public int clickCounter;
        public DiscordRpc.JoinRequest joinRequest;
        public UnityEvent onConnect;
        public UnityEvent onDisconnect;
        public UnityEvent hasResponded;
        public DiscordJoinEvent onJoin;
        public DiscordJoinEvent onSpectate;
        public DiscordJoinRequestEvent onJoinRequest;

        DiscordRpc.EventHandlers handlers;

        public static PresenceManager instance;

        public void OnClick()
        {
            Debug.Log("Discord: on click!");
            clickCounter++;

            presence.details = string.Format("Button clicked {0} times", clickCounter);

            DiscordRpc.UpdatePresence(presence);
        }

        public void RequestRespondYes()
        {
            Debug.Log("Discord: responding yes to Ask to Join request");
            DiscordRpc.Respond(joinRequest.userId, DiscordRpc.Reply.Yes);
            hasResponded.Invoke();
        }

        public void RequestRespondNo()
        {
            Debug.Log("Discord: responding no to Ask to Join request");
            DiscordRpc.Respond(joinRequest.userId, DiscordRpc.Reply.No);
            hasResponded.Invoke();
        }

        #region Discord Callbacks
        public void ReadyCallback()
        {
            ++callbackCalls;
            Debug.Log("Discord: ready");
            onConnect.Invoke();
        }

        public void DisconnectedCallback(int errorCode, string message)
        {
            ++callbackCalls;
            Debug.Log(string.Format("Discord: disconnect {0}: {1}", errorCode, message));
            onDisconnect.Invoke();
        }

        public void ErrorCallback(int errorCode, string message)
        {
            ++callbackCalls;
            Debug.Log(string.Format("Discord: error {0}: {1}", errorCode, message));
        }

        public void JoinCallback(string secret)
        {
            ++callbackCalls;
            Debug.Log(string.Format("Discord: join ({0})", secret));
            onJoin.Invoke(secret);
        }

        public void SpectateCallback(string secret)
        {
            ++callbackCalls;
            Debug.Log(string.Format("Discord: spectate ({0})", secret));
            onSpectate.Invoke(secret);
        }

        public void RequestCallback(ref DiscordRpc.JoinRequest request)
        {
            ++callbackCalls;
            Debug.Log(string.Format("Discord: join request {0}#{1}: {2}", request.username, request.discriminator, request.userId));
            joinRequest = request;
            onJoinRequest.Invoke(request);
        }
        #endregion

        #region Monobehaviour Callbacks
        // Singleton
        void Awake()
        {
            instance = this;
        }

        void Update()
        {
            DiscordRpc.RunCallbacks();
        }

        void OnEnable()
        {
            Debug.Log("Discord: init");
            callbackCalls = 0;

            handlers = new DiscordRpc.EventHandlers();
            handlers.readyCallback = ReadyCallback;
            handlers.disconnectedCallback += DisconnectedCallback;
            handlers.errorCallback += ErrorCallback;
            handlers.joinCallback += JoinCallback;
            handlers.spectateCallback += SpectateCallback;
            handlers.requestCallback += RequestCallback;
            DiscordRpc.Initialize(applicationId, ref handlers, true, optionalSteamId);
        }

        void OnDisable()
        {
            Debug.Log("Discord: shutdown");
            DiscordRpc.Shutdown();
        }

        void OnDestroy()
        {

        }
        #endregion

        #region Update Presence Method
        public static void UpdatePresence(string detail, string state = null, long start = -1, long end = -1, string largeKey = null,
            string largeText = null, string smallKey = null, string smallText = null, string partyId = null, int size = -1,
            int max = -1, string match = null, string join = null, string spectate = null/*, bool instance = false*/)
        {
            instance.Change(detail, state, start, end, largeKey, largeText, smallKey, smallText, partyId, size, max, match,
                join, spectate);
        }

        public void Change(string detail, string state, long start, long end, string largeKey,string largeText, 
            string smallKey, string smallText, string partyId, int size,int max, string match, string join, 
            string spectate/*, bool instance*/)
        {
            presence.details = detail ?? presence.details;
            presence.state = state ?? presence.state;
            presence.startTimestamp = (start == -1) ? presence.startTimestamp : start;
            presence.endTimestamp = (end == -1) ? presence.endTimestamp : end;
            presence.largeImageKey = largeKey ?? presence.largeImageKey;
            presence.largeImageText = largeText ?? presence.largeImageText;
            presence.smallImageKey = smallKey ?? presence.smallImageKey;
            presence.smallImageText = smallText ?? presence.smallImageText;
            presence.partyId = partyId ?? presence.partyId;
            presence.partySize = (size == -1) ? presence.partySize : size;
            presence.partyMax = (max == -1) ? presence.partyMax : max;
            presence.matchSecret = match ?? presence.matchSecret;
            presence.joinSecret = join ?? presence.joinSecret;
            presence.spectateSecret = spectate ?? presence.spectateSecret;
            //presence.instance =
            DiscordRpc.UpdatePresence(presence);
        }
        #endregion
    }
}
