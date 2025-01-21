using System.Threading.Tasks;
using UnityEngine;

public class drop_node : MonoBehaviour
{
    public GameObject prefab;
    public bool dropAtStart;

    private sound_node soundMaster;
    private Transform spawndummy;
    private bool open = false;
    internal GameObject cube;
    private void Start()
    {
        spawndummy = transform.Find("spawndummy").transform;
        soundMaster = transform.Find("soundmaster").gameObject.GetComponent<sound_node>();
        if (dropAtStart)
        {
            DropCube();
        }
    }
    async public void DropCube()
    {
        if (open)
        {
            return;
        }
        //it will have a fancy animation, but it will be later, or I would outright disable spawning if cube is present;
        Destroy(cube);
        cube = Instantiate(prefab, spawndummy.position, spawndummy.rotation);
        soundMaster.PlayAudio(0);
        await Task.Delay(600);
        this.gameObject.GetComponent<Animation>().Play("DispenserOpen");
        open = true;
        await Task.Delay(1000);
        soundMaster.PlayAudio(0);
        this.gameObject.GetComponent<Animation>().Play("DispenserClose");
        await Task.Delay(1000);
        open = false;
    }
}
