using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSelctorManager : MonoBehaviour
{
    public TMP_InputField WorldName;
    public string Name;
    public GameObject ErrorCreatingWorld;
    public GameObject ExistingWorld;
    public Enviroment enviroment;

    public void Start()
    {
        ErrorCreatingWorld.gameObject.SetActive(false);
        ExistingWorld.gameObject.SetActive(false);
    }
    public void ReadWorldName()
    {
        Name = WorldName.text;
    }
    public async void CreateWorldFromName()
    {
        if (Name != null)
        {
            ErrorCreatingWorld.gameObject.SetActive(false);
            bool nameAlreadyExists = false;
            foreach (var Enviroment in APIClient.Instance.Enviroments.Enviroments)
            {
                Debug.Log(Name);
                Debug.Log(Enviroment.Name);
                if (Name != Enviroment.Name)
                {
                    continue;
                }
                else
                {
                    nameAlreadyExists = true;
                    break;
                }
            }
            if (nameAlreadyExists == false)
            {
                APIClient.Instance.CreateWorld(Name);
                await APIClient.Instance.GetWorldFromNameFromUser();
                enviroment = APIClient.Instance.ChosenWorld;
                Debug.Log(enviroment.id);
                APIClient.Instance.WorldId = enviroment.id;
                SceneManager.LoadScene("RoomMakerCorner");
            }
            else
            {
                ExistingWorld.gameObject.SetActive(true);
            }
        }
        else
        {
            ErrorCreatingWorld.gameObject.SetActive(true);
        }
    }
}
