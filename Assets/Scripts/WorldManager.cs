using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    void Start()
    {
        // code om objecten op te halen
    }
    
    public void Back()
    {
        SceneManager.LoadScene("WorldSelection");
    }
}
