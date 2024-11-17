using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_node : MonoBehaviour
{
    public bool Loop;
    public AudioSource AudiatedObject;
    public void PlayAudio(AudioClip clip)
    {
        if (Loop)
        {
            AudiatedObject.loop = true;
        }
        else
        {
            AudiatedObject.loop = false;
        }
        AudiatedObject.clip = clip;
        AudiatedObject.Play();
    }
    public void StopAudio()
    {
        AudiatedObject.Stop();
    }
}
