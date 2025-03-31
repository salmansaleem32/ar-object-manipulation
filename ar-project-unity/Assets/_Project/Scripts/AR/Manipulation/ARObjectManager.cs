using UnityEngine;

public class ARObjectManager : MonoBehaviour
{
    private GameObject selectedObject;

    public void SelectObject(GameObject obj)
    {
        selectedObject = obj;
    }

    public void DeselectObject()
    {
        selectedObject = null;
    }

    public void DeleteSelectedObject()
    {
        if (selectedObject != null)
        {
            Destroy(selectedObject);
            selectedObject = null;
        }
    }
}
