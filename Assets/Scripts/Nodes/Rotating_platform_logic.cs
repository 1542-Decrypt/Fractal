using UnityEngine;
using UnityEngine.UIElements;

public class Rotating_platform_logic : MonoBehaviour
{
    public bool Continuous;
    public button_node parentButton;
    public float NeededAngle;
    public float DegreesPerSecond;
    [Tooltip("Debug->LocalAngle")]
    public float InitialAngle;
    private bool Reached;
    [SerializeField]private bool Started = false;
    private int Mod;
    void Update()
    {
        if (Continuous)
        {
            if (parentButton.isPressed)
            {
                if (!Reached)
                    transform.Rotate(new Vector3(0f, 0f, DegreesPerSecond) * Time.deltaTime);
                if (transform.rotation.z >= NeededAngle)
                    Reached = true;
                if (Reached)
                {
                    transform.Rotate(new Vector3(0f, 0f, DegreesPerSecond) * Time.deltaTime * -1);
                    if (transform.rotation.z <= InitialAngle)
                    {
                        Reached = false;
                    }
                }
            }
        }
        else
        {
            if (Started && !Pause.Paused)
            {
                print("Turning is"+Started);
                float Angle;
                if (Mod == -1)
                {
                    Angle = InitialAngle;
                }
                else
                {
                    Angle = NeededAngle;
                }
                transform.Rotate(new Vector3(0f, 0f, DegreesPerSecond) * Time.deltaTime * Mod);
                if (transform.rotation.z >= Angle && Mod == 1)
                {
                    Started = false;
                }
                if (transform.rotation.z <= Angle && Mod == -1)
                {
                    Started = false;
                }
            }
        }
    }
    public void Turn(int Modifier)
    {
        Started = true;
        Mod = Modifier;
    }
}
