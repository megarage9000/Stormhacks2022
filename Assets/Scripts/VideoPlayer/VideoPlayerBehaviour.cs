using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Video;

namespace YoutubePlayer
{
    public class VideoPlayerBehaviour : NetworkBehaviour
    {
        VideoPlayer videoPlayer;
        public YoutubePlayer youtubePlayer;
        public GameObject videoControls;
        void VideoPlayerPreparedCompleted(VideoPlayer source)
        {
            if (source.isPrepared)
            {
                if(IsClient && IsServer)
                {
                    Canvas canvas = FindObjectOfType<Canvas>();
                    canvas.gameObject.AddComponent(videoControls);
                }
            }
        }
        public async void Prepare()
        {
            print("loading video..");
            try
            {
                await youtubePlayer.PrepareVideoAsync();
                print("loading complete!");
            }
            catch
            {
                print("error video not loading");
            }
        }
        public void PlayVideo()
        {
            videoPlayer.Play();
        }
        public void PauseVideo()
        {
            videoPlayer.Pause();
        }
        public void ResetVideo()
        {
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
