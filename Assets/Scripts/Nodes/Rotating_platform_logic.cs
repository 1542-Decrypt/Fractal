using UnityEngine;
using UnityEngine.UIElements;

public class Rotating_platform_logic : MonoBehaviour
{
    [Tooltip("If set, panel will rotate infinitely between initial and needed angles if activated.")]
    public bool Continuous;
    public button_node parentButton;
    [Tooltip("Debug -> LocalAngle.z")]
    public float NeededAngle;
    [Tooltip("Speed of rotation")]
    public float DegreesPerSecond;
    [Tooltip("Debug -> LocalAngle.z")]
    public float InitialAngle;
    private bool Reached;
    [Tooltip("True if platform is active. If set, activates it. Not advised to use manually.")]
    [SerializeField] private bool Started = false;
    [Tooltip("Sets the rotation direction. Can only be 1 or -1")]
    [SerializeField] private int Mod;
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
                print("Turning is" + Started);
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
                if (transform.rotation.z >= Angle && Mod >= 1)
                {
                    Started = false;
                }
                if (transform.rotation.z <= Angle && Mod <= -1)
                {
                    Started = false;
                }
            }
        }
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (Blocked)
    //        Started = true;
    //}
    public void Turn(int Modifier)
    {
        Started = true;
        Mod = Modifier;
    }
}
