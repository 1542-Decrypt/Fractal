using UnityEngine;

public class Fizzler_logic : MonoBehaviour
{
    [SerializeField] Transform F, B;
    string OnAnim, OffAnim;
    public sound_node SndMng;
    public int enableID, disableID;
    private void Awake()
    {
        F = transform.Find("front");
        B = transform.Find("back");
        OnAnim = name + "_on";
        OffAnim = name + "_off";
    }
    public void Enable()
    {
        SndMng.PlayAudio(enableID);
        StopAnimation(F, OffAnim);
        StopAnimation(B, OffAnim);
        PlayAnimation(F, OnAnim);
        PlayAnimation(B, OnAnim);
    }
    public void Disable()
    {
        SndMng.PlayAudio(disableID);
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
