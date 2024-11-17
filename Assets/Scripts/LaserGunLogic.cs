using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunLogic : MonoBehaviour
{
    public bool Mono;
    internal bool EnabledLaser = true;
    bool Fired = false;
    bool LaserExists = false;
    bool IsSet = false;
    Transform bufferPosition;
    [SerializeField] float sizeMod;
    [SerializeField] Vector3 position, newScale;
    [SerializeField] string GiveAnimName, ShootAnimName;
    [SerializeField] Transform dummyCaster, dummyCaster2;
    ShootLaser castScript, castScript2;
    [SerializeField] Transform hand, handObject, handObject2;
    [SerializeField] ParticleSystem shootPart;
    [SerializeField] Animation Cam;
    [SerializeField] Image Crosshair, Crosshair2, CrossPrime, CrossAlt;
    private void Start()
    {
        castScript = dummyCaster.gameObject.GetComponent<ShootLaser>();
        castScript2 = dummyCaster2.gameObject.GetComponent<ShootLaser>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(Fired == false)
        {
            GiveGun();
        }
    }
    void GiveGun()
    {
        if (Fired == false)
        {
            base.transform.SetParent(hand);
            base.transform.position = position;
            base.transform.localScale = newScale;
            handObject.gameObject.SetActive(true);
            handObject2.gameObject.SetActive(true);
            base.gameObject.GetComponent<Animation>().Play(GiveAnimName);
            if (LaserExists != true)
            {
                Crosshair2.gameObject.GetComponent<Animation>().Play("show_lit");
                if (Mono) {
                    Crosshair.gameObject.GetComponent<Animation>().Play("show_unlit");
                }
            }
            else
            {
                Crosshair2.gameObject.GetComponent<Animation>().Play("show_lit");
                if (Mono) {
                    Crosshair.gameObject.GetComponent<Animation>().Play("show_lit");
                }
            }
            Fired = true;
        }
    }
    private void Update()
    {
        if (EnabledLaser)
        {
            if (Fired && base.gameObject.GetComponent<Animation>().IsPlaying(GiveAnimName) != true)
            {
                if (base.gameObject.GetComponent<Animation>().IsPlaying(ShootAnimName) != true)
                {
                    if ((Input.GetKeyDown(KeyCode.Mouse0)))
                    {
                        Cam.Play("throwback");
                        shootPart.Play();
                        base.gameObject.GetComponent<Animation>().Play(ShootAnimName);
                        base.gameObject.GetComponent<AudioSource>().Play();
                        if (Mono)
                        {
                            Color col = Crosshair.color;
                            col.a = 0.5f;
                            Crosshair.color = col;
                            //LaserExists = true;
                        }
                        else
                        {
                            Color col = CrossPrime.color;
                            col.a = 0.5f;
                            CrossPrime.color = col;
                            //LaserExists = true;
                        }
                    }
                    if ((!Mono && Input.GetKeyDown(KeyCode.Mouse1)))
                    {
                        Cam.Play("throwback");
                        shootPart.Play();
                        base.gameObject.GetComponent<Animation>().Play(ShootAnimName);
                        base.gameObject.GetComponent<AudioSource>().Play();
                        Color col = CrossAlt.color;
                        col.a = 0.5f;
                        CrossAlt.color = col;
                    }
                }
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    IsSet = false;
                    castScript.TriggersStuff = false;
                    castScript.CastLaser(dummyCaster);
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    bufferPosition = dummyCaster;
                    IsSet = true;
                    castScript.TriggersStuff = true;
                    castScript.bufPosition = bufferPosition.position;
                    castScript.bufDirection = bufferPosition.forward;
                }
                if (!Mono && Input.GetKey(KeyCode.Mouse1))
                {
                    castScript2.TriggersStuff = false;
                    castScript2.CastLaser(dummyCaster);
                }
                if (!Mono && Input.GetKeyUp(KeyCode.Mouse1))
                {
                    castScript2.TriggersStuff = true;
                    castScript2.CastLaser(dummyCaster);
                }
                if (IsSet)
                {
                    castScript.UpdateLaserCollision();
                }
            }
        }
    }
}
