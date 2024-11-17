using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteraction : MonoBehaviour
{
    public void Act()
    {
        base.gameObject.SetActive(false);
    }
    public void StopActing()
    {
        base.gameObject.SetActive(true);
    }
}
