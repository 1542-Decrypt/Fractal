using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trigger_block : MonoBehaviour
{
    enum TriggerType { Once, Multiple }
    [Tooltip("Once activates once, Multiple activates each time player walks in.")]
    [SerializeField] TriggerType TrigType;
    [Tooltip("Activates once player steps in a trigger")]
    public UnityEvent Output_OnTrigger;
    [Tooltip("Fired means that trigger was fired and wont activate once player steps in it. If true by default, then trigger will be disabled by default.")]
    public bool Fired = false;
    [Tooltip("This will make trigger not persistent between saves. Meaning it will not be marked as fired in the save when it was fired. Useful as softlock preventor. Cases of its use will be shown in the example scenes.")]
    public bool IgnoreSaveChanges = false;

    private void Start()
    {
        if (IgnoreSaveChanges && Fired)
        {
            Fired = false;
        }
        this.GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }
        if (TrigType == TriggerType.Once)
        {
            FireOnce(); 
        }
        else if (TrigType == TriggerType.Multiple)
        {
            FireMultiple();
        }
    }
    void FireOnce()
    {
        if (Fired != true)
        {
            Output_OnTrigger.Invoke();
            Fired = true;
        }
    }
    void FireMultiple()
    {
        Output_OnTrigger.Invoke();
    }
}
