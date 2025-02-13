using UnityEngine;

public class perpetual_rotation_node : MonoBehaviour
{
    public bool Enabled;
    public float rotationSpeed = 10f;
    public Vector3 axis;

    void Update()
    {
        if (Pause.Paused || !Enabled) return;
        transform.Rotate(axis, rotationSpeed * Time.deltaTime);
    }
}
