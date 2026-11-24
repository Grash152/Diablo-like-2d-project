using Scrips.Characters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

namespace Scrips.Menu
{
    public class DeathMenuButtons : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;
         
        [SerializeField] private Sprite restartNormalButton;
        [SerializeField] private Sprite restartHoverButton;
        [SerializeField] private Sprite restartNormalText;
        [SerializeField] private Sprite restartHoverText;
        [SerializeField] private GameObject Boss;
        [SerializeField] private Sprite exitNormalButton;
        [SerializeField] private Sprite exitHoverButton;
        [SerializeField] private Sprite exitNormalText;
        [SerializeField] private Sprite exitHoverText;

        private Image _restart;
        private Image _restartChild;
        private Color _restartColor;
        private Color _restartColorChild;
        private Image _exit;
        private Image _exitChild;
        private Color _exitColor;
        private Color _exitColorChild;
        
        private float _timer;
        private float _alpha;

        private void Awake()
        {
            gameObject.SetActive(false);
            _alpha = 0.0f;
        }
        
        private void Start()
        {
            _timer = 5.25f;
            
            _restart = restartButton.GetComponent<Image>();    
            _restartChild = restartButton.transform.GetChild(0).GetComponent<Image>();// TODO: SET BACKGROUND OF OUTRO TO LES 4 GARS
            _exit = exitButton.GetComponent<Image>();
            _exitChild = exitButton.transform.GetChild(0).GetComponent<Image>();
            
            _restartColor = _restart.color;
            _restartColorChild = _restartChild.color;
            
            _restartColor.a = _alpha;
            _restartColorChild.a = _alpha;
            
            _restart.color = _restartColor;
            _restartChild.color = _restartColorChild;
            
            _exitColor = _exit.color;
            _exitColorChild = _exitChild.color;
            
            _exitColor.a = _alpha;
            _exitColorChild.a = _alpha;
            
            _exit.color = _exitColor;
            _exitChild.color = _exitColor;
        }

        private void Update()
        {
            ShowButtons();
            Alpha();
        }

        private void ShowButtons()
        {
            _timer -= Time.unscaledDeltaTime;

            if (_timer <= 0.0f)
            {
                _timer = 0.0f;
                _alpha = Mathf.MoveTowards(_alpha, 1.0f, 5.0f * Time.unscaledDeltaTime);
                Debug.Log(_alpha);
            }
        }

        private void Alpha()
        {
            _restartColor.a = _alpha;
            _restartColorChild.a = _alpha;
            _restart.color = _restartColor;
            _restartChild.color = _restartColorChild;
            
            _exitColor.a = _alpha;
            _exitColorChild.a = _alpha;
            _exit.color = _exitColor;
            _exitChild.color = _exitColorChild;
        }
        
        public void Restart()
        {
            SceneManager.LoadScene("bdeb_methodo_team1");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Leveling.currentLvl = 0;
            Leveling.currentXP = 0;
            Leveling.DiffFormLevel=10;
            Leveling.levelUp = 50;
            PlayerStats.Reset();
            Time.timeScale = 1.0f;
            //PlayerStats.ResetCurrentHealth();

        }

        public void Exit()
        {
            //Application.Quit();
           
            SceneManager.LoadScene("OutroScene");
            PlayerStats.Reset();
            
            Time.timeScale = 1.0f;
            
        }
        
        public void RestartHover()
        {
            _restart.sprite = restartHoverButton;
            _restartChild.sprite = restartHoverText;
        }

        public void RestartNormal()
        {
            _restart.sprite = restartNormalButton;
            _restartChild.sprite = restartNormalText;
        }

        public void ExitHover()
        {
            _exit.sprite = exitHoverButton;
            _exitChild.sprite = exitHoverText;
        }

        public void ExitNormal()
        {
            _exit.sprite = exitNormalButton;
            _exitChild.sprite = exitNormalText;
        }
    }
}
