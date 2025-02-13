using UnityEngine;

public class SaveSlotHandler : MonoBehaviour
{
    public bool isSelected;
    public int Index;
    public string SlotName;
    private void Update()
    {
        if (isSelected)
        {
            Pause.SaveIndex = Index;
            Pause.SaveIndName = SlotName;
        }
        print("Chosen save is " + Pause.SaveIndex);
    }
    public void Toggle(bool onoff)
    {
        isSelected = onoff;
        Pause.SaveIndex = -1;
    }
}
