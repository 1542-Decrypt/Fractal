using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trigger_block : MonoBehaviour
{
    enum TriggerType { Once, Multiple }
    [SerializeField] TriggerType TrigType;
    public UnityEvent Output_OnTrigger;
    bool Fired = false;

    private void Start()
    {
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
