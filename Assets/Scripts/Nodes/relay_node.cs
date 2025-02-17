using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine;

public class relay_node : MonoBehaviour
{
    internal bool DoNotPlay;
    [Tooltip("Activates upon its activation by a different node.")]
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
