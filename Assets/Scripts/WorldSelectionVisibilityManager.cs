using UnityEngine;

public class WorldSelectionVisibilityManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject CreateWorldPanel;
    public GameObject EditWorldPanel;

    public void Start()
    {
        HideObject(CreateWorldPanel);
        HideObject(EditWorldPanel);
        ShowObject(MenuPanel);
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
