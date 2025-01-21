using UnityEngine;

public class CubeCollisionSound : MonoBehaviour
{
    sound_node Sound;
    internal bool IsInteractable = true;
    public int HitSoundID;
    Pickup_system Pickup;
    public bool isCube;
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
        Sound.PlayAudio(HitSoundID);
        if (collision.gameObject.transform.CompareTag("StopCube"))
            if (Pickup.grabbedOBJ)
                Pickup.Ungrab();
    }
}
