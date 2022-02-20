using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Video;
using UnityEngine.UI;

namespace YoutubePlayer
{
    public class VideoPlayerBehaviour : NetworkBehaviour
    {
        public VideoPlayer videoPlayer;
        YoutubePlayer youtubePlayer;
        public GameObject videoButtons;

        NetworkVariable<NetworkString> youtubeLink = new NetworkVariable<NetworkString>("");

        [ClientRpc]
        public void SetYoutubeLinkClientRpc(string link)
        {
            youtubeLink.Value = link;
        }

        private void Awake()
        {
            youtubePlayer = videoPlayer.GetComponent<YoutubePlayer>();
            videoPlayer.prepareCompleted += VideoPlayerPreparedCompleted;
        }
        void VideoPlayerPreparedCompleted(VideoPlayer source)
        {
            if (source.isPrepared)
            {
                if(IsClient && IsServer)
                {

                    VideoPlayerClient clientScript = GetComponent<VideoPlayerClient>();
                    if(clientScript != null)
                    {
                        Destroy(clientScript);
                    }
                }
                else
                {
                    Destroy(videoButtons);
                    VideoPlayerHost hostScript = GetComponent<VideoPlayerHost>();
                    if (hostScript)
                    {
                        Destroy(hostScript);
                    }
                }
            }
        }

        public async void Prepare()
        {
            Debug.Log("Video: Prepare");
            print("loading video..");
            try
            {
                await youtubePlayer.PrepareVideoAsync(youtubeLink.Value);
                print("loading complete!");
            }
            catch
            {
                print("error video not loading");
            }
        }


        public void Play()
        {
            Debug.Log("Video: Play");
            videoPlayer.Play();
        }


        [ClientRpc]
        public async void PrepareClientRpc()
        {
            Prepare();
        }

        [ClientRpc]
        public void PlayVideoClientRpc()
        {
            Play();
        }

        [ClientRpc]
        public void PauseVideoClientRpc()
        {
            Debug.Log("Video: Pause");
            videoPlayer.Pause();
        }

        [ClientRpc]
        public void ResetVideoClientRpc()
        {
            Debug.Log("Video: Reset");
            videoPlayer.Stop();
            videoPlayer.Play();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            videoPlayer.prepareCompleted -= VideoPlayerPreparedCompleted;
        }
    }
}
