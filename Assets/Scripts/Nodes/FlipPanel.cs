using UnityEngine;

public class FlipPanel : MonoBehaviour
{
    public sound_node soundnode;
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
