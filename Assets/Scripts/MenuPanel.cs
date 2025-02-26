using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    //public List<GameObject> Objects;
    public List<GameObject> prefabs;
    public GameObject Menu;
    public void CreateGameObjectFromClick(int prefabIndex)
    {
        if (prefabs[prefabIndex] == null)
        {
            Debug.LogError($"Prefab op index {prefabIndex} is NULL!");
            return;
        }
        var well = Instantiate(prefabs[prefabIndex], Vector3.zero, Quaternion.identity);
        //Objects.Add(well);
        var dadWell = well.GetComponent<DragAndDrop>();
        dadWell.isDragging = true;
        dadWell.Menu = this;
        HideMenu(false);
    }
    public void HideMenu(bool show)
    {
        Menu.gameObject.SetActive(show);
    }
}
