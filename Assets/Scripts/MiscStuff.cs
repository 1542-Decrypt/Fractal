using UnityEngine;
using UnityEngine.UI;

public class MiscStuff : MonoBehaviour
{
    public bool DebugMode;
    public static Ray Camray;
    internal static Ray FlooRay;
    internal string floortag;
    public Image Crosshair;
    void Update()
    {

        //- RAYCAST FORWARD
        Camray = new Ray(transform.position, transform.forward * 10f);
        //- FLOOR SPEAKER
        FlooRay = new Ray(transform.position, transform.up * -0.5f);
        //- DEBUG DRAW
        if (DebugMode == true)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camray, out hit))
            {
                if (hit.collider.gameObject.tag == "Mirror")
                {
                    Color col = Crosshair.color;
                    col.a = 0.5f;
                    Crosshair.color = col;
                }
                else
                {
                    Color col = Crosshair.color;
                    col.a = 0.2f;
                    Crosshair.color = col;
                }
            }
        }
        RaycastHit hit2;
        if (Physics.Raycast(FlooRay, out hit2))
        {
            floortag = hit2.collider.gameObject.tag.ToString();
        }
    }
}
