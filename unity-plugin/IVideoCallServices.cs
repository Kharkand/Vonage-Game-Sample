using System;

namespace VideoCall
{
    public interface IVideoCallService
    {
        event Action OnCallStarted;
        event Action OnCallEnded;
        event Action<string> OnDisconnected;

        void Initialize(VideoCallConfig config);
        void Connect();
        void Disconnect();
        void Process();
        void Shutdown();
    }
}