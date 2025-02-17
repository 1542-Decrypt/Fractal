using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class fade_node : MonoBehaviour
{
    [Tooltip("Sets if sound should fade with screen")]
    public bool FadeSound;
    [Tooltip("Activates once screen is faded out or in")]
    public UnityEvent On_finish_fade;
    [SerializeField] internal bool DoNotPlay;
    enum FadeType { In, Out }
    [Tooltip("Fade in or Fade out")]
    [SerializeField] FadeType Fade_Type;
    [Tooltip("Fade speed. Not advised to set higher than 1")]
    public float FadeModifier;
    float FadeTime = 1;
    RawImage Tint;
    float opacity = 1;
    float volume = 1;
    bool started = false;
    private void Awake()
    {
        Tint = GameObject.Find("fade_helper").GetComponent<RawImage>();
    }
    private void Update()
    {
        print(AudioListener.volume);
        Color tmp = Tint.color;
        if (!started)
        {
            opacity = tmp.a;
            if (FadeSound)
                volume = AudioListener.volume;
        }
        else
        {
            if (FadeSound)
                AudioListener.volume = volume;
            tmp.a = opacity;
            Tint.color = tmp;
        }
        if (started && FadeTime > 0)
        {
            if (Fade_Type == FadeType.In)
            {
                opacity += Time.deltaTime * FadeModifier;
                if (FadeSound && volume >= 0)
                    volume -= Time.deltaTime * FadeModifier;
            }
            else if (Fade_Type == FadeType.Out)
            {
                opacity -= Time.deltaTime * FadeModifier;
                if (FadeSound && volume <= 1)
                    volume += Time.deltaTime * FadeModifier;
            }
            FadeTime -= Time.deltaTime * FadeModifier;
        }
        if (started && FadeTime <= 0)
        {
            started = false;
            On_finish_fade.Invoke();
        }
    }
    public void OnTrigger()
    {
        if (DoNotPlay)
        {
            return;
        }
        started = true;
    }
    public void Disable()
    {
        DoNotPlay = true;
    }
}
