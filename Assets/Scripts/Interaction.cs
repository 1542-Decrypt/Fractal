using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField]internal Color recievedColor;
    public UnityEvent<Color> Output_OnEnterCollider;
    public UnityEvent Output_OnExitCollider;
    public void SendSignalTo()
    {
        Output_OnEnterCollider.Invoke(recievedColor);
    }
    public void RecallSignalFrom()
    {
        Output_OnExitCollider.Invoke();
    }
}
