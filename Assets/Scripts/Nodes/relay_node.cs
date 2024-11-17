using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine;

public class relay_node : MonoBehaviour
{
    public UnityEvent Output_OnTrigger;
    public void OnTrigger()
    {
        Output_OnTrigger.Invoke();
    }
}
