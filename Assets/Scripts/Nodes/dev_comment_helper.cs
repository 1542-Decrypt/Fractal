using UnityEngine;

public class dev_comment_helper : MonoBehaviour
{
    private dev_comment mainScript;
    private void Start()
    {
        mainScript = transform.parent.GetComponent<dev_comment>();
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Player is inside");
        if (other.gameObject.CompareTag("Player"))
        {
            mainScript.InField = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        mainScript.InField = false;
    }
}
