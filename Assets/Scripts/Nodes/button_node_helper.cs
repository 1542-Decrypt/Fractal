using System;
using UnityEngine;

public class button_node_helper : MonoBehaviour
{
    public button_node buttonNode;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonNode.Active = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        buttonNode.Active = false;
    }
}
