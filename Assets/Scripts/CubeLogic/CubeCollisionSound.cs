using UnityEngine;

public class CubeCollisionSound : MonoBehaviour
{
    sound_node Sound;
    internal bool IsInteractable = true;
    private void Update()
    {
        print(IsInteractable);
    }
    private void Awake()
    {
        Sound = base.transform.GetChild(0).GetComponent<sound_node>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Sound.PlayAudio(27);
    }
}
