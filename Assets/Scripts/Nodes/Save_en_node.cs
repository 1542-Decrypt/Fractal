using UnityEngine;

public class Save_en_node : MonoBehaviour
{
    public void ToggleSaving(bool state) {
    SavingSystem.isSavingEnabled = state;
    }
}
