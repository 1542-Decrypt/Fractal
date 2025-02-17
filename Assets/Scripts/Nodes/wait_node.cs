using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine;

public class wait_node : MonoBehaviour
{
    internal bool DoNotPlay;
    [Tooltip("Activates once time is passed. Warning: Time is not paused when you pause.")]
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
