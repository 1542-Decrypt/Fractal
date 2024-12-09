using UnityEngine;

public class PointRay : MonoBehaviour
{
    public static Ray HoldRay;
    private void Update()
    {
        HoldRay = new Ray(transform.position, transform.forward * -0.5f);
        Debug.DrawRay(transform.position, transform.forward * -1f);
    }
}
