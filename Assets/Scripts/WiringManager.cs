using System.Threading.Tasks;
using UnityEngine;

public class WiringManager : MonoBehaviour
{
    WireLogic[] Wires;
    private void Start()
    {
        Wires = transform.GetComponentsInChildren<WireLogic>();
    }
    async public void Toggle(bool value)
    {
        foreach (WireLogic t in Wires)
        {
            t.Check(value);
            await Task.Delay(50);
        }
    }
}
