using System.Collections;
using Scrips.Characters;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scrips.Menu
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Image startIcon;
        [SerializeField] private Button startButton;
        [SerializeField] private float startIconDelay = 5.0f;

        private bool _canStart;
        private bool _startRequested;

        private void Start()
        {
            startIcon.enabled = false;
            StartCoroutine(ShowStartIcon());
        }

        private void Update()
        {
            if (!_canStart)
            {
                return;
            }

            if (!_startRequested && Keyboard.current.anyKey.wasPressedThisFrame)
            {
                _startRequested = true;
                return;
            }

            if (_startRequested && !Keyboard.current.anyKey.isPressed)
            {
                Time.timeScale = 1.0f;
                SceneManager.LoadScene("bdeb_methodo_team1");
            }
        }

        private IEnumerator ShowStartIcon()
        {
            yield return new WaitForSeconds(startIconDelay);

            _canStart = true;
            startIcon.enabled = true;

            StartCoroutine(BlinkStartIcon());
        }

        private IEnumerator BlinkStartIcon()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                startIcon.enabled = !startIcon.enabled;
            }
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadNextScene()
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void StartGame()
        {
            SceneManager.LoadScene("IntroScene");
            
        }
    }
}