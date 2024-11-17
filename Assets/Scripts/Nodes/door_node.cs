using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_node : MonoBehaviour
{
    enum DoorType { Classic, Push }
    [SerializeField] DoorType Doors;
    public Animation AnimPlayer;
    public AudioClip Classic, ClassicClose, Push;
    public KeyCode Interact;
    public bool Locked;
    bool Active = false;
    bool Open = false;
    private void Start()
    {
        print("ABAS");
        if (Doors == DoorType.Push)
        {
            GetComponent<AudioSource>().clip = Push;
        }
        
    }
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
                if (Doors == DoorType.Classic) {
                    if (!Open)
                    {
                        GetComponent<AudioSource>().clip = Classic;
                        AnimPlayer.Play("ClassicDoorOpen");
                        GetComponent<AudioSource>().Play();
                        Open = true;
                    }
                    else
                    {
                        GetComponent<AudioSource>().clip = ClassicClose;
                        AnimPlayer.Play("ClassicDoorClose");
                        GetComponent<AudioSource>().Play();
                        Open = false;
                    }
                }
                else
                {
                    AnimPlayer.Play();
                    GetComponent<AudioSource>().Play();
                    Open = true;
                } 
            }
        }   
    }
}
