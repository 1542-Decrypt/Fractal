using UnityEngine;

public class floor_button_trigger : MonoBehaviour
{
    private floor_button floor_Button;
    private void Start()
    {
        floor_Button = transform.parent.GetComponent<floor_button>();
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            floor_Button.Check(0, collision.gameObject);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            floor_Button.Check(1, collision.gameObject);
        }
    }
}
