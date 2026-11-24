using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Scrips.Menu
{
    public class OutroController : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;

        private void OnEnable()
        {
            videoPlayer.loopPointReached += VideoFinished;
        }

        private void OnDisable()
        {
            videoPlayer.loopPointReached -= VideoFinished;
        }

        private void VideoFinished(VideoPlayer source)
        {
            SceneManager.LoadScene("IntroScene");
        }
    }
}