using TMPro;
using UnityEngine;

public class tips : MonoBehaviour
{
    string Tip;
    public KeyCode Keycode;
     void Update()
    {
        base.gameObject.GetComponent<TextMeshProUGUI>().text = "["+Keycode.ToString()+"]"+" "+Tip;
    }
}
