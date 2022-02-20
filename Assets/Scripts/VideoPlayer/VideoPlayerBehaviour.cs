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
        VideoPlayer videoPlayer;
        public YoutubePlayer youtubePlayer;
        public GameObject videoButtons;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            videoPlayer.prepareCompleted += VideoPlayerPreparedCompleted;
        }
        void VideoPlayerPreparedCompleted(VideoPlayer source)
        {
            if (source.isPrepared)
            {
                if(IsClient && IsServer)
                {
                    Debug.Log("Loading buttons");
                    VideoControls videoControls = videoButtons.GetComponent<VideoControls>();

                    videoControls.Prepare.onClick.AddListener(() =>
                    {
                        var link = videoControls.YoutubeLink.text;
                        if (!string.IsNullOrEmpty(link))
                        {
                            Prepare(link);
                        }
                    });

                    videoControls.Play.onClick.AddListener(() =>
                    {
                        PlayVideo();
                    });

                    videoControls.Reset.onClick.AddListener(() =>
                    {
                        ResetVideo();
                    });

                    videoControls.Pause.onClick.AddListener(() =>
                    {
                        PauseVideo();
                    });

                    VideoPlayerClient clientScript = GetComponent<VideoPlayerClient>();
                    if(clientScript != null)
                    {
                        Destroy(clientScript);
                    }
                    Debug.Log("Finished loading");
                }
                else
                {
                    Destroy(videoButtons);
                    VideoPlayerHost hostScript = GetComponent<VideoPlayerHost>();
                }
            }
        }
        public async void Prepare(string link = null)
        {
            print("loading video..");
            try
            {
                await youtubePlayer.PrepareVideoAsync(link);
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
