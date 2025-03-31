using UnityEngine;

public class ARObjectManipulator : MonoBehaviour
{
    private Vector3 initialScale;
    private float rotationSpeed = 100f;

    void Start()
    {
        initialScale = transform.localScale;
    }

    public void ScaleObject(float scaleFactor)
    {
        transform.localScale = initialScale * scaleFactor;
    }

    public void RotateObject(Vector2 rotationDelta)
    {
        float rotationX = rotationDelta.x * rotationSpeed * Time.deltaTime;
        float rotationY = rotationDelta.y * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, -rotationX, Space.World);
        transform.Rotate(Vector3.right, rotationY, Space.World);
    }

    public void MoveObject(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}