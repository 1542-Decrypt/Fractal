using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_node : MonoBehaviour
{
    public Animation AnimatedObject;
    public void PlayAnimation(string name)
    {
        AnimatedObject.Play(name);
    }
    public void StopAnimation(string name)
    {
        AnimatedObject.Stop(name);
    }
}
