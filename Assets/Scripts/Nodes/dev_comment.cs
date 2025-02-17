using UnityEngine;
using UnityEngine.Events;

public class dev_comment : MonoBehaviour
{
    [Tooltip("Set by default, do not change.")]
    public sound_node SoundPlayer;
    [Tooltip("ID of a sound which will play once node is activated.")]
    public int SoundID;
    [Tooltip("Speed of node's rotation.")]
    public float RotSpeed;
    [Tooltip("Activates when node is activated")]
    public UnityEvent On_Start_Node;
    internal bool InField;
    private bool Activated;
    private void Update()
    {
        RaycastHit raycastHit;
        if (InField)
        {
            if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(MiscStuff.Camray, out raycastHit, 2f) && raycastHit.transform.gameObject == gameObject)
            {
                foreach (dev_comment comm in GameObject.FindObjectsByType<dev_comment>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    if (comm != this && comm.Activated)
                    {
                        comm.SoundPlayer.StopAudio();
                    }
                }
                On_Start_Node.Invoke();
                SoundPlayer.PlayAudio(SoundID);
                Activated = true;
            }
        }
        if (!Activated && !Pause.Paused)
        {
            transform.Rotate(new Vector3(0f, 0f, RotSpeed) * Time.deltaTime);
        }
    }
    public void Continue()
    {
        Activated = false;
    }
}
