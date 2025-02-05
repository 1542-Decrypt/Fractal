using UnityEngine;

public class WireLogic : MonoBehaviour
{
    public Material onMat, offMat;
    private Light glow;

    private void Start()
    {
        if (transform.childCount > 0)
        {
            glow = transform.GetComponentInChildren<Light>();
        }
    }
    public void Check(bool State)
    {
        if (State)
        {
            this.GetComponent<MeshRenderer>().material = onMat;
            if (glow != null)
                glow.enabled = true;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = offMat;
            if (glow != null)
                glow.enabled= false;
        }
    }
}
