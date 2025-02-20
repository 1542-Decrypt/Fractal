using UnityEngine;

public class tp_node : MonoBehaviour
{
    public Transform coord_end;

    public void Teleport(Transform obj)
    {
        obj.position = coord_end.position;
        obj.rotation = coord_end.rotation;
    }
}
