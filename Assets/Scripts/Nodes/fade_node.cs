using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class fade_node : MonoBehaviour
{
    public bool FadeSound;
    public UnityEvent On_finish_fade;
    public bool DoNotPlay;
    enum FadeType { In, Out }
    [SerializeField] FadeType FadeTp;
    public float FadeModifier;
    float FadeTime = 1;
    RawImage Tint;
    float opacity = 1;
    float volume = 1;
    bool started = false;
    float Cache_volume = 0;
    private void Awake()
    {
        Tint = GameObject.Find("fade_helper").GetComponent<RawImage>();
    }
    private void Update()
    {
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
            if (FadeTp == FadeType.In)
            {
                opacity += Time.deltaTime * FadeModifier;
                if (FadeSound && volume >= 0)
                    volume -= Time.deltaTime * FadeModifier;
            }
            else if (FadeTp == FadeType.Out)
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
        AudioListener.pause = false;
        started = true;
    }
    public void Disable()
    {
        DoNotPlay = true;
    }
}
