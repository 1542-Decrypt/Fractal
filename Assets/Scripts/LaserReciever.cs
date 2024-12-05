using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class LaserReciever : MonoBehaviour
{
    public sound_node soundManager;
    public int enableID, idleID, disableID;
    bool Enabled = false;
    public bool Is_Reciever = false;
    public Color neededColor;
    public UnityEvent Output_OnEnable;
    public UnityEvent Output_OnDisable;
    [SerializeField]int Queue;
    private void Update()
    {
        if (Queue < 0)
        {
            Queue = 0;
        }
    }
    public void Enable(Color col)
    {
        if (Queue <= 0)
        {
            if (neededColor == col)
            {
                Queue += 1;
                if (Enabled == false)
                {
                    soundManager.PlayAudio(enableID);
                    if (Is_Reciever)
                    {
                        base.GetComponent<Animation>().Play("Recieve");
                        base.GetComponent<Animation>().PlayQueued("Actidle");
                        soundManager.PlayAudioDelay(idleID, 2000);
                    }
                    else
                    {
                        base.GetComponent<Animation>().Play("active_r_idle");
                        soundManager.PlayAudio(idleID);
                    }
                    Output_OnEnable.Invoke();
                    Enabled = true;
                }
            }
        }
    }
    async public void Disable()
    {
        Queue -= 1; 
        await Task.Delay(50);
        if (Queue <= 0 && Enabled)
        {
            //rare stop audio usage here
            soundManager.StopAudio();
            soundManager.PlayAudio(disableID);
            if (Is_Reciever)
            {
                base.GetComponent<Animation>().Stop("Actidle");
                base.GetComponent<Animation>().Play("Unrecieve");
            }
            else
            {
                base.GetComponent<Animation>().Stop("active_r_idle");
            }
            Output_OnDisable.Invoke();
            Enabled = false;
        }
    }
}
