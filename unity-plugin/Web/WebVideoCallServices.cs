using System;

namespace VideoCall
{
    public class WebVideoCallServices : IVideoCallService
    {
        public event Action OnCallStarted;
        public event Action OnCallEnded;
        public event Action<string> OnDisconnected;

        public void Initialize(VideoCallConfig config)
        {
            // Initial configuration goes here if needed.
            // Credentials, user name....
        }

        public void Connect()
        {
            WebComms.Connect();
        }

        public void Disconnect()
        {
            // Disconnection logic.
        }
        
        public void Process()
        {
            //In game processing
        }

        public void Shutdown()
        {
        }
    }
}