using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class WorldSelectionVisibilityManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject CreateWorldPanel;
    public GameObject EditWorldPanel;
    public GameObject settingsPanel;
    public GameObject DeletePanel;
    public GameObject ErrorCreatingWorld;
    public GameObject buttonPrefab;
    public Transform buttonPanel;
    public List<GameObject> Buttons;
    public Enviroment[] worlds;
    public Enviroment ChosenWorld;
    public Guid ChosenWorldId;

    public async void Start()
    {
        HideObject(CreateWorldPanel);
        HideObject(EditWorldPanel);
        HideObject(settingsPanel);
        HideObject(ErrorCreatingWorld);
        HideObject(DeletePanel);
        ShowObject(MenuPanel);
        await APIClient.Instance.GetAllWorldsForUser();
        worlds = APIClient.Instance.Enviroments.Enviroments;
        Debug.Log(worlds[1].id);
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
            ErrorCreatingWorld.gameObject.SetActive(false);
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
            Debug.Log(enviroment.id);
            GameObject button = Instantiate(buttonPrefab, buttonPanel);

            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            buttonText.text = enviroment.Name;

            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => OnWorldButtonClicked(enviroment));
            Buttons.Add(button);
        }
    }
    public void DeleteWorldButtons()
    {
        foreach (var button in Buttons)
        {
            Destroy(button);
        }
    }
    void OnWorldButtonClicked(Enviroment world)
    {
        Debug.Log("Wereld geselecteerd: " + world.Name);
        settingsPanel.SetActive(true);
        MenuPanel.SetActive(false);
        ChosenWorld = world;
        Debug.Log(ChosenWorld.Name);
        Debug.Log(ChosenWorld.id);
    }

    public void EditWorldButton()
    {
        EditWorldPanel.SetActive(true);
        settingsPanel.SetActive(false);
        ChosenWorldId = ChosenWorld.id;
    }
    public void DeleteWorldButton()
    {
        settingsPanel.SetActive(false);
        DeletePanel.SetActive(true);
        ChosenWorldId = ChosenWorld.id;
    }
    public async void DeleteWorld()
    {
        Debug.Log(ChosenWorld.id);
        APIClient.Instance.DeleteWorld(ChosenWorld.id);
        await APIClient.Instance.GetAllWorldsForUser();
        worlds = APIClient.Instance.Enviroments.Enviroments;
        DeleteWorldButtons();
        PopulateWorldButtons();
        HideObject(DeletePanel);
        ShowObject(MenuPanel);
    }
    public async void EditWorld()
    {
        APIClient.Instance.EditWorld(ChosenWorld.Name);
        await APIClient.Instance.GetAllWorldsForUser();
        worlds = APIClient.Instance.Enviroments.Enviroments;
        DeleteWorldButtons();
        PopulateWorldButtons();
        HideObject(EditWorldPanel);
        ShowObject(MenuPanel);
    }
    public void JoinWorld()
    {
        APIClient.Instance.WorldId = ChosenWorldId;
        SceneManager.LoadScene("RoomMakerCorner");
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
