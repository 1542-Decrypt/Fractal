using UnityEngine.Events;
using UnityEngine;
using System.Threading.Tasks;

public class button_node : MonoBehaviour
{
    internal bool Active = false;
    public KeyCode Interact;
    public Animation AnimPlayer;
    public sound_node SoundPlayer;
    public bool Toggleable;
    public int pressSoundID;
    [Range(-1f, 25f)]
    [Tooltip("Negative values will make button not press back")]
    [SerializeField] float pressTime = 0.5f;
    public UnityEvent Output_OnEnable;
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
