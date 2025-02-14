using UnityEngine;

public class zoom_in : MonoBehaviour
{
    Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            ZoomIn();
        }
        if (Input .mouseScrollDelta.y < 0)
        {
            ZoomOut();
        }
    }
    void ZoomIn()
    {

    }
    void ZoomOut()
    {

    }
}
