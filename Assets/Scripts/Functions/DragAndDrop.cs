using System;
using TMPro.EditorUtilities;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    // The prefab to instantiate
    public bool isDragging;
    public MenuPanel Menu;
    public int PrefabId;
    public Guid enviromentId;
    public void Initialize(int prefabId)
    {
        PrefabId = prefabId;
        Debug.Log(APIClient.Instance.WorldId);
        enviromentId = APIClient.Instance.WorldId;
    }

    void OnMouseDown()
    {
        isDragging = !isDragging;

        if (!isDragging && Menu != null)
        {
            Menu.HideMenu(true);
            GetObjectData();
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }
    public void GetObjectData()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        float scaleX = transform.localScale.x;
        float scaleY = transform.localScale.y;
        float rotZ = transform.rotation.eulerAngles.z;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            string sortingLayer = sr.sortingLayerName;
            Object2D object2D = new Object2D(enviromentId, PrefabId, posX, posY, scaleX, scaleY, rotZ, sortingLayer);
            APIClient.Instance.CreateObject(object2D);
        }
    }
}
