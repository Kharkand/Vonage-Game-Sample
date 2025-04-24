using System;
using UnityEngine;

namespace VideoCall
{
    public class VideoCallManager : MonoBehaviour
    {
        public static VideoCallManager Instance { get; private set; }

        /// <summary>
        /// Current status of the call.
        /// </summary>
        public VideoCallStatus VideoCallStatus { get; private set; } = VideoCallStatus.Unknown;

        /// <summary>
        /// Triggered after a successful initialisation.
        /// </summary>
        public event Action OnInitialized;
        
        /// <summary>
        /// Triggered after a failed initialisation.
        /// </summary>
        public event Action<string> OnInitializationFailed;
                
        /// <summary>
        /// Triggered when the call successfully starts.
        /// </summary>
        public event Action OnCallStarted;
        
        /// <summary>
        /// Triggered when the call successfully ends.
        /// </summary>
        public event Action OnCallEnded;

        /// <summary>
        /// Triggered when the call unexpectedly ends. 
        /// </summary>
        /// <returns>Error message.</returns>
        public event Action<string> OnCallDisconnected;
        
        private bool _initialized = false;
        private const string LOG_TAG = "[Video Call]";
        
        private IVideoCallService _videoCallService;
 

        /// <summary>
        /// Initialize Video Call services. 
        /// </summary>
        /// <param name="config">Desired configuration. If null, native config will be used.</param>
        public void Initialize(VideoCallConfig config = null)
        {
            if (_videoCallService != null)
            {
                Debug.LogWarning("VideoCallManager is already initialized.");
                return;
            }

            #if UNITY_WEBGL && !UNITY_EDITOR
            _videoCallService = new WebVideoCallServices();
            #else
            var noSupportError = $"{LOG_TAG} Current platform is not supported";
            OnInitializationFailed?.Invoke(noSupportError);
            Debug.LogError(noSupportError);
            return;
            #endif
         
            _videoCallService.Initialize( config);

            _videoCallService.OnCallStarted += HandleCallStarted;
            _videoCallService.OnCallEnded += HandleCallEnded;
            _videoCallService.OnDisconnected += HandleDisconnected;

            _initialized = true;
            OnInitialized?.Invoke();
            Debug.Log($"{LOG_TAG} Initialized.");
        }

        public void Connect()
        {
            if(!_initialized) return;
            Debug.Log($"{LOG_TAG} Connecting...");
            _videoCallService.Connect();
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                //Automatically Initialize for testing purposes.
                Instance.Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            _videoCallService?.Process();
        }
        
        private void HandleCallStarted()
        {
            VideoCallStatus = VideoCallStatus.Connected;
            OnCallStarted?.Invoke();
        }
        
        private void HandleCallEnded()
        {
            VideoCallStatus = VideoCallStatus.Disconnected;
            OnCallEnded?.Invoke();
        }
        
        private void HandleDisconnected(string error)
        {
            OnCallDisconnected?.Invoke(error);
            VideoCallStatus = VideoCallStatus.Disconnected;
        }
        
        private void OnDestroy()
        {
            _videoCallService?.Shutdown();
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeOnLoad()
        {
            // For testing purposes, it will automatically attach to a GameObject. 
            if (Instance != null) return;
            
            var sdkGameObject = new GameObject("VideoCallManager");
            Instance = sdkGameObject.AddComponent<VideoCallManager>();
        }
    }
}