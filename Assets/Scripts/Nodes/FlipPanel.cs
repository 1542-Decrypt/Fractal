using UnityEngine;

public class FlipPanel : MonoBehaviour
{
    [Tooltip("Sound node, which will play the sound. Set by default, do not change.")]
    public sound_node soundnode;
    [Tooltip("1 - Mirror, 0 - Solid wall. If you have mirror by default, set as 1, and 0 if vice versa.")]
    public int state;
    public void Flip()
    {
        if (state == 0)
        {
            GetComponent<Animation>().Play("PanelFlip_one");
            state = 1;
        }
        else if (state == 1)
        {
            GetComponent<Animation>().Play("PanelFlip_two");
            state = 0;
        }
        soundnode.PlayAudio(0);
    }
}
