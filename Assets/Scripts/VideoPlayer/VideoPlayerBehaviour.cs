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
        private string youtubeLink = "";

        public void SetYoutubeLink(string link)
        {
            youtubeLink = link;
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
                await youtubePlayer.PrepareVideoAsync(youtubeLink);
                print("loading complete!");
            }
            catch
            {
                print("error video not loading");
            }
        }
        public void PlayVideo()
        {
            Debug.Log("Video: Play");
            videoPlayer.Play();
        }
        public void PauseVideo()
        {
            Debug.Log("Video: Pause");
            videoPlayer.Pause();
        }
        public void ResetVideo()
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
