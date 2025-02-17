//TeamSawblade 2025 all rights reserved
using UnityEditor;
using UnityEngine;

public class NodeMaster : MonoBehaviour
{
    [MenuItem("GameObject/Nodes/AND Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/AND Node", false, 0)]
    static void CreateAND()
    {
        GameObject prefab = Resources.Load("AND") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Animation Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Animation Node", false, 0)]
    static void CreateANIM()
    {
        GameObject prefab = Resources.Load("Anim") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Debug Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Debug Node", false, 0)]
    static void CreateDBG()
    {
        GameObject prefab = Resources.Load("DebugBlock") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Disable LaserGun Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Disable LaserGun Node", false, 0)]
    static void CreateDLG()
    {
        GameObject prefab = Resources.Load("DebugBlock") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Train Point Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Train Point Node", false, 0)]
    static void CreateEP()
    {
        GameObject prefab = Resources.Load("TrainPoint") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Fade Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Fade Node", false, 0)]
    static void CreateFADE()
    {
        GameObject prefab = Resources.Load("Fade") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Train Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Train Node", false, 0)]
    static void CreateTRAIN()
    {
        GameObject prefab = Resources.Load("FuncTrain") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Next Stage Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Next Stage Node", false, 0)]
    static void CreateNEXT()
    {
        GameObject prefab = Resources.Load("Next") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/On Start Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/On Start Node", false, 0)]
    static void CreateSTRT()
    {
        GameObject prefab = Resources.Load("On_Start_Node") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Relay Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Relay Node", false, 0)]
    static void CreateRL()
    {
        GameObject prefab = Resources.Load("Relay") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Save Bug Preventor Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Save Bug Preventor Node", false, 0)]
    static void CreateSBP()
    {
        GameObject prefab = Resources.Load("SavingBugPreventor3000") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Sound Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Sound Node", false, 0)]
    static void CreateSND()
    {
        GameObject prefab = Resources.Load("Sound") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Soundscape Manager Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Soundscape Manager Node", false, 0)]
    static void CreateSMN()
    {
        GameObject prefab = Resources.Load("SoundscapeManager") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Steam Manager Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Steam Manager Node", false, 0)]
    static void CreateSteam()
    {
        GameObject prefab = Resources.Load("SteamManager") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Switch Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Switch Node", false, 0)]
    static void CreateSwitch()
    {
        GameObject prefab = Resources.Load("Switch") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
    [MenuItem("GameObject/Nodes/Wait Node", false, 0)]
    [MenuItem("Assets/Create/Nodes/Wait Node", false, 0)]
    static void CreateWait()
    {
        GameObject prefab = Resources.Load("Wait") as GameObject;
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        obj.name = prefab.name;
    }
}
