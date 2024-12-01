using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class LaserReciever : MonoBehaviour
{
    AudioSource idleSound, disound,ensound;
    bool Enabled = false;
    public bool Is_Reciever = false;
    public Color neededColor;
    public UnityEvent Output_OnEnable;
    public UnityEvent Output_OnDisable;
    [SerializeField]int Queue;
    void Awake()
    {
        idleSound = base.transform.Find("idlesound").GetComponent<AudioSource>();
        disound = base.transform.Find("deactivate").GetComponent<AudioSource>();
        ensound = base.transform.Find("activate").GetComponent<AudioSource>();
    }
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
                    ensound.Play();
                    if (Is_Reciever)
                    {
                        base.GetComponent<Animation>().Play("Recieve");
                        base.GetComponent<Animation>().PlayQueued("Actidle");
                        idleSound.PlayDelayed(2);
                    }
                    else
                    {
                        base.GetComponent<Animation>().Play("active_r_idle");
                        idleSound.Play();
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
            idleSound.Stop();
            disound.Play();
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
