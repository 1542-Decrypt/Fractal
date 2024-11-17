using UnityEngine.Events;
using UnityEngine;

public class on_start_node : MonoBehaviour
{
    public UnityEvent Output_OnTrigger;
    void Start()
    {
        Output_OnTrigger.Invoke();
    }
}
