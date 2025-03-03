using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSelctorManager : MonoBehaviour
{
    public TMP_InputField WorldName;
    public string Name;
    public GameObject ErrorCreatingWorld;

    public void Start()
    {
        ErrorCreatingWorld.gameObject.SetActive(false);
    }
    public void ReadWorldName()
    {
        Name = WorldName.text;
    }
    public void CreateWorldFromName()
    {
        if (name != null)
        {
            ErrorCreatingWorld.gameObject.SetActive(false);
            APIClient.Instance.CreateWorld(Name);
            SceneManager.LoadScene("RoomMakerCorner");
        }
        else
        {
            ErrorCreatingWorld.gameObject.SetActive(true);
        }
    }
}
