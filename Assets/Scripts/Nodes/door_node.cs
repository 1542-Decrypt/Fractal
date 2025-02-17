using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_node : MonoBehaviour
{
    enum DoorType { Classic, Push }
    [Tooltip("Classic - normal door with handle, Push - P2 old aperture-ahh push door. Push door cannot be closed, unlike Classic one.")]
    [SerializeField] DoorType Doors;
    [Tooltip("Object, which will play the animation.")]
    public Animation AnimPlayer;
    [Tooltip("Object, which will play the sound.")]
    public sound_node SoundPlayer;
    [Tooltip("Sound IDS for different doors.")]
    public int ClassicID, ClassicCloseID, PushID;
    [Tooltip("Set as (E), (E) is interaction key for now, until I will unfiy them in one controls manager-ahh script.")]
    public KeyCode Interact;
    [Tooltip("Locked means door cannot be opened. You cant unlock them properly, except using nodes.")]
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
