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
using UnityEngine.Windows;

public class WorldSelectionVisibilityManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject CreateWorldPanel;
    public GameObject EditWorldPanel;
    public GameObject settingsPanel;
    public GameObject DeletePanel;
    public GameObject ErrorCreatingWorld;
    public GameObject ErrorEdittingWorld;
    public GameObject buttonPrefab;
    public Transform buttonPanel;
    public TMP_InputField EditName;
    public List<GameObject> Buttons;
    public Enviroment[] worlds;
    public Enviroment ChosenWorld;
    public Guid ChosenWorldId;
    public string WorldName;
    

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
        PopulateWorldButtons();
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
    public void PopulateWorldButtons()
    {
        foreach (Enviroment enviroment in worlds)
        {
            GameObject button = Instantiate(buttonPrefab, buttonPanel);

            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            buttonText.text = enviroment.Name;

            Button buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => OnWorldButtonClicked(enviroment));
            Buttons.Add(button);
        }
    }
    void OnWorldButtonClicked(Enviroment world)
    {
        Debug.Log("Wereld geselecteerd: " + world.Name);
        settingsPanel.SetActive(true);
        MenuPanel.SetActive(false);
        ChosenWorld = world;
        Debug.Log(ChosenWorld.Name);
    }
    public void DeleteWorldButtons()
    {
        foreach (var button in Buttons)
        {
            Destroy(button);
        }
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
    public void JoinWorld()
    {
        ChosenWorldId = Guid.Parse(ChosenWorld.id);
        APIClient.Instance.WorldId = ChosenWorldId;
        SceneManager.LoadScene("RoomMakerCorner");
    }
    public void EditWorldButton()
    {
        EditWorldPanel.SetActive(true);
        settingsPanel.SetActive(false);
        ChosenWorldId = Guid.Parse(ChosenWorld.id);
        APIClient.Instance.WorldId = ChosenWorldId;
    }
    public void EditTextMethod()
    {
        WorldName = EditName.text;
    }
    public async void EditWorld()
    {
        if (WorldName != null)
        {
            ErrorEdittingWorld.SetActive(false);
            APIClient.Instance.EditWorld(WorldName);
            await APIClient.Instance.GetAllWorldsForUser();
            worlds = APIClient.Instance.Enviroments.Enviroments;
            DeleteWorldButtons();
            PopulateWorldButtons();
            HideObject(EditWorldPanel);
            ShowObject(MenuPanel);
        }
        else
        {
            ErrorEdittingWorld.SetActive(true);
        }
    }
    public void DeleteWorldButton()
    {
        settingsPanel.SetActive(false);
        DeletePanel.SetActive(true);
        ChosenWorldId = Guid.Parse(ChosenWorld.id);
        APIClient.Instance.WorldId = ChosenWorldId;
    }
    public async void DeleteWorld()
    {
        APIClient.Instance.DeleteWorld(ChosenWorldId);
        await APIClient.Instance.GetAllWorldsForUser();
        worlds = APIClient.Instance.Enviroments.Enviroments;
        DeleteWorldButtons();
        PopulateWorldButtons();
        HideObject(DeletePanel);
        ShowObject(MenuPanel);
    }
    public async void Logout()
    {
        await APIClient.Instance.Logout();
    }
}
