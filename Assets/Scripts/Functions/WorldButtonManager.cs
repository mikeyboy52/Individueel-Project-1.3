using NUnit.Framework;
using UnityEngine;
using UnityEngine.LightTransport;
//using UnityEngine.UI;

//public class WorldButtonManager : MonoBehaviour
//{
//    public GameObject buttonPrefab;
//    public Transform buttonPanel;
//    public GameObject settingsPanel;
//    public GameObject MenuPanel;
//    public Enviroment[] worlds;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        //APIClient.Instance.GetAllWorldsForUser();
//        worlds = APIClient.Instance.Enviroments.Enviroments;

//        PopulateWorldButtons();
//    }

//    public void PopulateWorldButtons()
//    {
//        foreach (Enviroment enviroment in worlds)
//        {
//            GameObject button = Instantiate(buttonPrefab, buttonPanel);

//            Text buttonText = button.GetComponentInChildren<Text>();
//            buttonText.text = enviroment.Name;

//            Button buttonComponent = button.GetComponent<Button>();
//            buttonComponent.onClick.AddListener(() => OnWorldButtonClicked(enviroment));
//        }
//    }
//    void OnWorldButtonClicked(Enviroment world)
//    {
//        Debug.Log("Wereld geselecteerd: " + world.Name);
//        settingsPanel.SetActive(true);
//    }

//    public void HideSettings()
//    {
//        settingsPanel.SetActive(false);
//    }
//}
