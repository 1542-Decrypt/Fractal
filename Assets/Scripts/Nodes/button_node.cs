using UnityEngine.Events;
using UnityEngine;
using System.Threading.Tasks;

public class button_node : MonoBehaviour
{
    bool Active = false;
    bool Open = false;
    public KeyCode Interact;
    public Animation AnimPlayer;
    public sound_node SoundPlayer;
    public bool Toggleable;
    public int pressSoundID;
    [Range(-1f, 25f)]
    [Tooltip("Negative values will make button not press back")]
    [SerializeField] float pressTime = 0.5f;
    public UnityEvent Output_OnTrigger;

    [SerializeField] internal bool isPressed = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Active = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Active = false;
    }
    async private void Update()
    {
        if (Active)
        {
            RaycastHit raycastHit;
            if (Input.GetKeyDown(Interact) && Physics.Raycast(MiscStuff.Camray, out raycastHit, 2f) && raycastHit.transform.gameObject == gameObject)
            {
                Press();
                if (pressTime >= 0 )
                    await Task.Delay(Mathf.RoundToInt(pressTime * 1000));
                else
                {
                    return;
                }
                if (Toggleable == false)
                    Unpress();
            }
        }
    }
    void Press()
    {
        if (isPressed == true)
        {
            if (Toggleable)
            {
                Unpress();
                return;
            }
            else
            {
                SoundPlayer.PlayAudio(19);
                return;
            }
        }
        isPressed = true;
        AnimPlayer.Play("button_press");
        SoundPlayer.PlayAudio(pressSoundID);
        Output_OnTrigger.Invoke();
    }
    async void Unpress()
    {
        AnimPlayer.Play("button_unpress");
        if (Toggleable == false)
            await Task.Delay(925);
        isPressed = false;
    }
}
