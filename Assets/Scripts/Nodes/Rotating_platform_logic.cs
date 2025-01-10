using UnityEngine;

public class Rotating_platform_logic : MonoBehaviour
{
    public button_node parentButton;
    public float rotationSpeed;

    private bool Reached;
    void Update()
    {
        if (parentButton.isPressed)
        {   
            if (!Reached)
                transform.Rotate(new Vector3(0f, 0f, rotationSpeed) * Time.deltaTime);
                if (transform.rotation.z >= 0)
                    Reached = true;
            if (Reached)
            {
                transform.Rotate(new Vector3(0f, 0f, rotationSpeed) * Time.deltaTime * -1);
                if (transform.rotation.z <= -0.5)
                {
                    Reached = false;
                }
            }
        }
    }
}
