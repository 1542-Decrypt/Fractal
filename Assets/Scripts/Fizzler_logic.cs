using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fizzler_logic : MonoBehaviour
{
    [SerializeField] Transform F, B;
    string OnAnim, OffAnim;
    AudioSource openSound, closeSound;
    private void Start()
    {
        F = transform.Find("front");
        B = transform.Find("back");
        OnAnim = name + "_on";
        OffAnim = name + "_off";
        openSound = transform.Find("soundOpen").GetComponent<AudioSource>();
        closeSound = transform.Find("soundClose").GetComponent<AudioSource>();
    }
    public void Enable()
    {
        //openSound.Play();
        StopAnimation(F, OffAnim);
        StopAnimation(B, OffAnim);
        PlayAnimation(F, OnAnim);
        PlayAnimation(B, OnAnim);
    }
    public void Disable()
    {
        //closeSound.Play();
        StopAnimation(F, OnAnim);
        StopAnimation(B, OnAnim);
        PlayAnimation(F, OffAnim);
        PlayAnimation(B, OffAnim);
    }
    void PlayAnimation(Transform Obj, string Name)
    {
        Obj.GetComponent<Animation>().Play(Name);
    }
    void StopAnimation(Transform Obj, string Name)
    {
        Obj.GetComponent<Animation>().Stop(Name);
    }
}
