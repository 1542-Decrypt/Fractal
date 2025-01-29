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
    internal GameObject buffer_cube;

    private Transform cube_obj;
    private bool dissolve = false;
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
        if (cube != null)
        {
            cube_obj = cube.transform.GetChild(2);
            dissolve = true;
            cube.GetComponent<Rigidbody>().useGravity = false;
            cube.GetComponent<Rigidbody>().AddForce(Vector3.up * 6f, ForceMode.Impulse);
            cube.GetComponent<CubeCollisionSound>().IsInteractable = false;
            foreach (BoxCollider col in cube.GetComponents<BoxCollider>())
            {
                col.enabled = false;
            }
            buffer_cube = Instantiate(prefab, spawndummy.position, spawndummy.rotation);
        }
        else
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
    private void Update()
    {
        if (dissolve)
        {
            cube_obj.gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Threshold", cube_obj.gameObject.GetComponent<MeshRenderer>().material.GetFloat("_Threshold") + Time.deltaTime);
            Debug.LogWarning(cube_obj.gameObject.GetComponent<MeshRenderer>().material.GetFloat("_Threshold"));
            if (cube_obj.gameObject.GetComponent<MeshRenderer>().material.GetFloat("_Threshold") >= 1)
            {
                Destroy(cube);
                cube = buffer_cube;
                dissolve = false;
            }
        }
    }
}
