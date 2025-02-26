using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void Login()
    {
        SceneManager.LoadScene("LoginScreen");
    }
    public void Register()
    {
        SceneManager.LoadScene("RegisterScreen");
    }
    public void StopGame()
    {
        Application.Quit();
    }
}
