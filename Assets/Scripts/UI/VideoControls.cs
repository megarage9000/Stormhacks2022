using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class VideoControls : MonoBehaviour
{

    public YoutubePlayer.YoutubePlayer VideoPlayer;

    // UI
    public Button Reset;
    public Button Play;
    public Button Pause;
    public Button Prepare;

    public InputField YoutubeLink;

    public void Setup()
    {
        Play.onClick.AddListener(() =>
        {

        });

        Pause.onClick.AddListener(() =>
        {

        });

        Reset.onClick.AddListener(() =>
        {

        });

        Prepare.onClick.AddListener(() =>
        {
            string youtubeLink = YoutubeLink.text;
            if (!string.IsNullOrEmpty(youtubeLink))
            {

            }
        });
    }
}
