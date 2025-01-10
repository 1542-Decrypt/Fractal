using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    public MiscStuff speaker;
    sound_node SoundMaster;
    public string[] groundTypes;
    public int[] WalkSoundIds;
    public int JumpIndex;

    internal int currentSurface;
    internal bool Disabled = false;
    public void PlayFootStep()
    {
        for (int i = 0; i < groundTypes.Length; i++)
        {
            if (groundTypes[i] == speaker.floortag)
            {
                currentSurface = i;
                break;
            }
        }
        if (Disabled != true)
        {
            SoundMaster.PlayAudio(WalkSoundIds[currentSurface]);
        }
    }
    public void PlayJump()
    {
        //SoundMaster.PlayAudio(WalkSoundIds[JumpIndex]);
    }
}
