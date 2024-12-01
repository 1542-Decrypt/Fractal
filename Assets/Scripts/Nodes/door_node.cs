using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_node : MonoBehaviour
{
    enum DoorType { Classic, Push }
    [SerializeField] DoorType Doors;
    public Animation AnimPlayer;
    public sound_node SoundPlayer;
    public int ClassicID, ClassicCloseID, PushID;
    public KeyCode Interact;
    public bool Locked;
    bool Active = false;
    bool Open = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Active = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Active = false;
    }
    private void Update()
    {
        if (Active)
        {
            RaycastHit raycastHit;
            if (Input.GetKeyDown(Interact) && !Locked && Physics.Raycast(MiscStuff.Camray, out raycastHit, 2f) && raycastHit.transform.gameObject == gameObject &&
                AnimPlayer.isPlaying == false)
            {
                switch (Doors)
                {
                    case DoorType.Classic:
                        if (!Open)
                        {
                            AnimPlayer.Play("ClassicDoorOpen");
                            SoundPlayer.PlayAudio(ClassicID);
                            Open = true;
                        }
                        else
                        {
                            AnimPlayer.Play("ClassicDoorClose");
                            SoundPlayer.PlayAudio(ClassicCloseID);
                            Open = false;
                        }
                        break;
                    case DoorType.Push:
                        AnimPlayer.Play();
                        GetComponent<AudioSource>().Play();
                        Open = true;
                        break;
                }
            }
        }   
    }
}
