using UnityEngine;

public class dev_comment : MonoBehaviour
{
    public sound_node SoundPlayer;
    public int SoundID;
    public float RotSpeed;

    internal bool InField;
    private bool Activated;
    private void Update()
    {
        RaycastHit raycastHit;
        if (InField)
        {
            if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(MiscStuff.Camray, out raycastHit, 2f) && raycastHit.transform.gameObject == gameObject)
            {
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
