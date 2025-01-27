using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderHelper : MonoBehaviour
{
    public TMP_InputField Text;
    public Slider slider;
    private void Start()
    {
        Text.text = slider.value.ToString();
    }
    public void UpdateText (float Value)
    {
        Text.text = Value.ToString();
    }
    public void UpdateSlider(string Value)
    {
        slider.value = float.Parse(Value);
    }
}
