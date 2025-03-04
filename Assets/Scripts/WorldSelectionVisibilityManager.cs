using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UI;

public class WorldSelectionVisibilityManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject CreateWorldPanel;
    public GameObject EditWorldPanel;
    public GameObject settingsPanel;
    public GameObject ErrorCreatingWorld;
    public GameObject buttonPrefab;
    public Transform buttonPanel;
    public Enviroment[] worlds;

    public async void Start()
    {
        HideObject(CreateWorldPanel);
        HideObject(EditWorldPanel);
        HideObject(settingsPanel);
        ShowObject(MenuPanel);
        await APIClient.Instance.GetAllWorldsForUser();
        worlds = APIClient.Instance.Enviroments.Enviroments;
        PopulateWorldButtons();
    }
    public void CreateWorldPopup()
    {
        int Worldcount = APIClient.Instance.Worlds;
        Debug.Log(Worldcount);
        if (Worldcount < 5)
        {
            CreateWorldPanel.gameObject.SetActive(true);
            MenuPanel.gameObject.SetActive(false);
        }
        else
        {
            ErrorCreatingWorld.gameObject.SetActive(true);
        }
    }

    public void PopulateWorldButtons()
    {
        foreach (Enviroment enviroment in worlds)
        {
            Debug.Log(enviroment.Name);
            GameObject button = Instantiate(buttonPrefab, buttonPanel);

            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = enviroment.Name;

            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => OnWorldButtonClicked(enviroment));
        }
    }
    void OnWorldButtonClicked(Enviroment world)
    {
        Debug.Log("Wereld geselecteerd: " + world.Name);
        settingsPanel.SetActive(true);
    }

    public void ChangeVisibilityOfObject(bool show, GameObject panel)
    {
        panel.gameObject.SetActive(show);
    }

    public void ShowObject(GameObject panel)
    {
        ChangeVisibilityOfObject(true, panel);
    }
    
    public void HideObject(GameObject panel)
    {
        ChangeVisibilityOfObject(false, panel);
    }

}
