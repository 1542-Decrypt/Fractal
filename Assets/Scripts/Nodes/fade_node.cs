using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class fade_node : MonoBehaviour
{
    public UnityEvent On_finish_fade;
    public bool DoNotPlay;
    enum FadeType { In, Out }
    [SerializeField] FadeType FadeTp;
    public float FadeModifier;
    float FadeTime = 1;
    RawImage Tint;
    float opacity = 1;
    bool started = false;
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
        }
        else
        {
            tmp.a = opacity;
            Tint.color = tmp;
        }
        if (started && FadeTime > 0)
        {
            if (FadeTp == FadeType.In)
            {
                opacity += Time.deltaTime * FadeModifier;
            }
            else if (FadeTp == FadeType.Out)
            {
                opacity -= Time.deltaTime * FadeModifier;
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
