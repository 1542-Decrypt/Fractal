using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_node : MonoBehaviour
{
    internal bool DoNotPlay;
    [Tooltip("Object which will be animated.")]
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
