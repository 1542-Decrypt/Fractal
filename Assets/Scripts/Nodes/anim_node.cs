using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_node : MonoBehaviour
{
    public bool DoNotPlay;
    public Animation AnimatedObject;
    public void Disable()
    {
        DoNotPlay = true;
    }
    public void PlayAnimation(string name)
    {
        if (DoNotPlay)
        {
            return;
        }
        AnimatedObject.Play(name);
    }
    public void StopAnimation(string name)
    {
        AnimatedObject.Stop(name);
    }
}
