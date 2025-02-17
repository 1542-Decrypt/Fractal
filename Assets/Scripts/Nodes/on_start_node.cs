using UnityEngine.Events;
using UnityEngine;

public class on_start_node : MonoBehaviour
{
    [Tooltip("Activates once game starts")]
    public UnityEvent Output_OnTrigger;
    void Start()
    {
        Output_OnTrigger.Invoke();
    }
}
