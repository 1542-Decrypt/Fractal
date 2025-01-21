using UnityEngine;
using UnityEngine.Events;

public class StageSwitch : MonoBehaviour
{
    public UnityEvent[] OnValue;
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
