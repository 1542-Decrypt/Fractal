using System.Collections.Generic;
using UnityEngine;

public class CubeCollisionSound : MonoBehaviour
{
    sound_node Sound;
    internal bool IsInteractable = true;
    public int HitSoundID;
    Pickup_system Pickup;
    public bool isCube;
    List<string> collisions = new List<string>();
    private void Start()
    {
        Pickup = GameObject.FindAnyObjectByType<Pickup_system>().GetComponent<Pickup_system>();
    }
    //private void Update()
    //{
    //    print(IsInteractable);
    //}
    private void Awake()
    {
        Sound = base.transform.GetChild(0).GetComponent<sound_node>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisions.Add(collision.gameObject.transform.tag);
        Sound.PlayAudio(HitSoundID);
        if (collisions.Contains("StopCube") && collisions.Contains("Player"))
            if (Pickup.grabbedOBJ)
                Pickup.Ungrab();
    }
    private void OnCollisionExit(Collision collision)
    {
        collisions.Remove(collision.gameObject.transform.tag);
    }
}
