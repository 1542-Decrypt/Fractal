using UnityEngine.Events;
using System.Threading.Tasks;
using UnityEngine;

public class wait_node : MonoBehaviour
{
    public UnityEvent Output_OnTrigger;
    async public void Wait(int amountOfWait)
    {
        await Task.Delay(amountOfWait);
        Output_OnTrigger.Invoke();
    }
}
