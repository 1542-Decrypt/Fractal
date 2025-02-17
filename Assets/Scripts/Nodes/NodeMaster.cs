using UnityEngine;
using UnityEditor;

public class NodeMaster : MonoBehaviour
{
    [MenuItem("GameObject/Add Player")]
    static void AddPrefab() {
        GameObject prefab = Resources.Load("player") as GameObject;
        GameObject.Instantiate (prefab, Vector3.zero, Quaternion.identity);
    }
}
