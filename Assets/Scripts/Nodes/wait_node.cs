using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine;

public class wait_node : MonoBehaviour
{
    public bool DoNotPlay;
    public UnityEvent Output_OnTrigger;
    public void Disable()
    {
        DoNotPlay = true;
    }
    async public void Wait(int amountOfWait)
    {
        if (DoNotPlay)
        {
            return;
        }
        await Task.Delay(amountOfWait);
        Output_OnTrigger.Invoke();
    }
}
