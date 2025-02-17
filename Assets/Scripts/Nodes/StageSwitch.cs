using UnityEngine;
using UnityEngine.Events;

public class StageSwitch : MonoBehaviour
{
    [Tooltip("Activates different coroutine (list of actions) on Awake() (Earlier than start). Based on which value is set there. Used for solving progression bugs upon loading.")]
    public UnityEvent[] OnValue;
    [Tooltip("Activates different coroutine (list of actions) on Start() (A bit later). Based on which value is set there. Used for solving progression bugs upon loading.")]
    public UnityEvent[] OnValueLater;
    public void Switch(int value)
    {
        LevelStageHelper.ChamberStage = value;
    }
    private void Update()
    {
        print(LevelStageHelper.ChamberStage);
    }
    public void Check(int value, bool special)
    {
        if (special)
        {
            if (value != 0)
                OnValue[value - 1].Invoke();
        }
        else
        {
            if (value != 0)
                OnValueLater[value - 1].Invoke();
        }
    }
}
