using UnityEngine;

public class ARObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;

    public void SpawnObject(Vector3 position)
    {
        if (objectPrefab != null)
        {
            Instantiate(objectPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Object Prefab is not assigned.");
        }
    }
}