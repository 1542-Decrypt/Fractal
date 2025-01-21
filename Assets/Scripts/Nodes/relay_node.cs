using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine;

public class relay_node : MonoBehaviour
{
    public bool DoNotPlay;
    public UnityEvent Output_OnTrigger;
    public void OnTrigger()
    {
        if (DoNotPlay)
        {
            return;
        }
        Output_OnTrigger.Invoke();
    }
    public void Disable()
    {
        DoNotPlay = true;
    }
}
