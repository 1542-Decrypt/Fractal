using UnityEngine;
using UnityEngine.UI;

public class MiscStuff : MonoBehaviour
{
    public bool DebugMode;
    public static Ray Camray;
    internal static Ray FlooRay, CeilRay;
    [SerializeField]
    internal string floortag;
    public Image Crosshair;
    CubeCollisionSound CubeCache;
    public Transform GroundCheck, CeilingCheck;
    public static bool ForceCrouch = false;
    public static bool PlayerCrouching;
    private void Start()
    {
        Application.targetFrameRate = 120;
    }
    void Update()
    {

        //- RAYCAST FORWARD
        Camray = new Ray(transform.position, transform.forward * 10f);
        //- FLOOR SPEAKER
        FlooRay = new Ray(GroundCheck.position, GroundCheck.up * -0.2f);
        //-CEILING SPEAKER
        CeilRay = new Ray(CeilingCheck.position, CeilingCheck.up * 0.4f);
        //-DRAWING RAYS FOR LIFE
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.yellow);
        Debug.DrawRay(GroundCheck.position, GroundCheck.up * -0.2f, Color.green);
        Debug.DrawRay(CeilingCheck.position, CeilingCheck.up * 0.4f, Color.blue);
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
            print(floortag);
            if (floortag == "Physics")
            {
                print(hit2.transform.gameObject.name);
                hit2.transform.gameObject.GetComponent<CubeCollisionSound>().IsInteractable = false;
                CubeCache = hit2.transform.gameObject.GetComponent<CubeCollisionSound>();
            }
            else
            {
                if (CubeCache)
                {
                    CubeCache.IsInteractable = true;
                    CubeCache = null;
                }
            }
        }
        RaycastHit hit3;
        if (Physics.Raycast(CeilRay, out hit3, 0.4f))
        {
            if (PlayerCrouching != true)
            {
                return;
            }
            ForceCrouch = true;
        }
        else
        {
            ForceCrouch = false;
        }
    }
}
