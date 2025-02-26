using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    // The prefab to instantiate
    public bool isDragging;
    public MenuPanel Menu;
    void OnMouseDown()
    {
        isDragging = !isDragging;

        if (!isDragging && Menu != null)
        {
            Menu.HideMenu(true);
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
}
