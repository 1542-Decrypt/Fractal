using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDisabler : MonoBehaviour
{
    internal bool DoNotPlay;
    [Tooltip("Laser gun, which will be disabled upon its activation.")]
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
