using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AND_block : MonoBehaviour
{
    public int NeededCount;
    [SerializeField] int Count = 0;
    bool activated;
    public UnityEvent Output_OnEnable;
    public UnityEvent Output_OnDisable;
    public void Add()
    {
        if (Count != NeededCount)
        {
            Count += 1;
        }
        if (Count == NeededCount)
        {
            Output_OnEnable.Invoke();
            activated = true;
        }

    }
    public void Substract()
    {
        if (Count > 0)
        {
            Count -= 1;
        }
        if (activated)
        {
            Output_OnDisable.Invoke();
            activated = false;
        }
    }
}
