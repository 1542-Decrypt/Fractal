using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMaster : MonoBehaviour
{
    [SerializeField]
    AudioClip openSound, closeSound;
    [SerializeField]
    AudioSource soundPlayer;
    public void Open_Door()
    {
        soundPlayer.Stop();
        base.gameObject.GetComponent<Animation>().Play("OpenDoor");
        soundPlayer.clip = openSound;
        soundPlayer.Play();
    }
    public void Close_Door()
    {
        soundPlayer.Stop();
        base.gameObject.GetComponent<Animation>().Play("CloseDoor");
        soundPlayer.clip = closeSound;
        soundPlayer.Play();
    }
}
