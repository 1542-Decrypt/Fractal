using UnityEngine;

public class zoomCamera : MonoBehaviour
{
    Camera cam;
    float currentFOV;
    public float normalFOV;
    public float zoomedFOV;
    public float zoomSpeed;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        currentFOV = cam.fieldOfView;
        if (Input.mouseScrollDelta.y > 0)
        {
            ZoomIn();
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            ZoomOut();
        }
    }
    void ZoomIn()
    {
        if (currentFOV != zoomedFOV)
        {
            cam.fieldOfView = Mathf.MoveTowards(currentFOV, zoomedFOV, zoomSpeed * Time.deltaTime);
        }
    }
    void ZoomOut()
    {
        if (currentFOV != normalFOV)
        {
            cam.fieldOfView = Mathf.MoveTowards(currentFOV, normalFOV, zoomSpeed * Time.deltaTime);
        }
    }
}
