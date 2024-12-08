using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDisabler : MonoBehaviour
{
    [SerializeField]LaserGunLogic Gun;
    private void Start()
    {
        Gun = FindAnyObjectByType<LaserGunLogic>();
    }
    public void Disable()
    {
        Gun.EnabledLaser = false;
    }
    public void Enable()
    {
        Gun.EnabledLaser = true;
    }
}
