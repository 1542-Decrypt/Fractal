using UnityEngine.Events;
using UnityEngine;
using System.Threading.Tasks;

public class button_node : MonoBehaviour
{
    internal bool Active = false;
    [Tooltip("Key to interact. Advised to set the same key which is set to other buttons. (E)")]
    public KeyCode Interact;
    [Tooltip("Set by default, do not change. If it is a custom model, set the object which will play the animation once button is 'pressed'")]
    public Animation AnimPlayer;
    [Tooltip("Set by default, do not change.")]
    public sound_node SoundPlayer;
    [Tooltip("If true, button will not press back if not pressed again.")]
    public bool Toggleable;
    [Tooltip("ID of a sound, which will play when button is pressed. Use soundscape manager as a reference.")]
    public int pressSoundID;
    [Range(-1f, 25f)]
    [Tooltip("Negative values will make button not press back")]
    [SerializeField] float pressTime = 0.5f;
    [Tooltip("Activates once button is pressed.")]
    public UnityEvent Output_OnEnable;
    [Tooltip("Activates once button is unpressed.")]
    public UnityEvent Output_OnDisable;

    private int AnimState;

    [SerializeField] internal bool isPressed = false;
    async private void Update()
    {
        if (Active)
        {
            RaycastHit raycastHit;
            if (Input.GetKeyDown(Interact) && Physics.Raycast(MiscStuff.Camray, out raycastHit, 2f) && raycastHit.transform.gameObject == gameObject)
            {
                Press();
                if (pressTime >= 0)
                    await Task.Delay(Mathf.RoundToInt(pressTime * 1000));
                else
                {
                    return;
                }
                if (Toggleable == false)
                    Unpress();
            }
        }
        if (AnimState == 0 && isPressed)
        {
            AnimPlayer.Play("button_press");
            AnimState = 1;
            Output_OnEnable.Invoke();
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
        AnimState = 1;
        SoundPlayer.PlayAudio(pressSoundID);
        Output_OnEnable.Invoke();
    }
    async void Unpress()
    {
        isPressed = false;
        AnimPlayer.Play("button_unpress");
        AnimState = 0;
        if (Toggleable == false)
            await Task.Delay(925);
        if (Output_OnDisable != null)
        {
            print("Invoked");
            Output_OnDisable.Invoke();
        }
    }
}
