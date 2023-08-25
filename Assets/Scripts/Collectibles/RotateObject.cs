using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float _rotateSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _rotateSpeed, 0, Space.World);
    }
}
