using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScript : MonoBehaviour
{
    public void ReturnHome()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
