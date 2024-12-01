using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMaster : MonoBehaviour
{
    [SerializeField]
    private int openSoundID, closeSoundID;
    [SerializeField]
    private sound_node soundPlayer;
    public void Open_Door()
    {
        soundPlayer.PlayAudio(openSoundID);
        base.GetComponent<Animation>().Play("OpenDoor");
    }
    public void Close_Door()
    {
        soundPlayer.PlayAudio(closeSoundID);
        base.GetComponent<Animation>().Play("CloseDoor");
    }
}
