using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    //public List<GameObject> Objects;
    public List<GameObject> prefabs;
    public GameObject Menu;
    public ObjectList objects;

    public async void Start()
    {
        if (APIClient.Instance == null)
        {
            Debug.Log("Client not working");
        }
        else
        {
            Debug.Log("Client Working");
        }
        var Objects = await APIClient.Instance.GetAllObjectsForEnviroment();
        if (Objects != null)
        {
            objects = APIClient.Instance.objects;
            if (objects.Objects.Length > 0)
            {
                foreach (var object2D in APIClient.Instance.objects.Objects)
                {
                    Vector3 Placement = new Vector3(object2D.PositionX, object2D.PositionY, 1);
                    GameObject spawnedObject = Instantiate(prefabs[object2D.PrefabId], Placement, Quaternion.Euler(0, 0, object2D.RotationZ));
                    spawnedObject.transform.localScale = new Vector3(object2D.ScaleX, object2D.ScaleY, 1);
                    SpriteRenderer sr = spawnedObject.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.sortingLayerName = object2D.SortingLayer;
                    }
                }
            }
        }
    }
    public void CreateGameObjectFromClick(int prefabIndex)
    {
        if (prefabs[prefabIndex] == null)
        {
            Debug.LogError($"Prefab op index {prefabIndex} is NULL!");
            return;
        }
        var well = Instantiate(prefabs[prefabIndex], Vector3.zero, Quaternion.identity);
        var dadWell = well.GetComponent<DragAndDrop>();
        dadWell.Initialize(prefabIndex);
        dadWell.isDragging = true;
        dadWell.Menu = this;
        HideMenu(false);
    }

    public void BackToSelction()
    {

        SceneManager.LoadScene("WorldSelection");
    }
    public void HideMenu(bool show)
    {
        Menu.gameObject.SetActive(show);
    }
}
