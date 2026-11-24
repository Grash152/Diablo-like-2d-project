using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("bdeb_methodo_team1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
