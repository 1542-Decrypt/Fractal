using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDisabler : MonoBehaviour
{
    public bool DoNotPlay;
    [SerializeField]LaserGunLogic Gun;
    private void Start()
    {
        Gun = FindAnyObjectByType<LaserGunLogic>();
    }
    public void DisableScript()
    {
        DoNotPlay = true;
    }
    public void Disable()
    {
        if (DoNotPlay)
        {
            return;
        }
        Gun.EnabledLaser = false;
    }
    public void Enable()
    {
        if (DoNotPlay)
        {
            return;
        }
        Gun.EnabledLaser = true;
    }
}
